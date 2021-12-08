using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDI.DrugApi.Apis;
using DDI.DrugApi.HttpClients;
using DDI.DrugApi.TransferModels.NlmDrugApi;
using FakeItEasy;
using Xunit;

namespace DDI.DrugApi.Tests.Apis
{
	public class TheNlmDrugApi
	{
		private readonly NlmHttpClient _fakeNlmHttpClient;

		private readonly NlmDrugApi _nlmDrugApi;

		public TheNlmDrugApi()
		{
			_fakeNlmHttpClient = A.Fake<NlmHttpClient>();

			_nlmDrugApi = new NlmDrugApi(
				_fakeNlmHttpClient
			);
		}

		[Fact]
		public async Task WhenValidatingDrugName_GivenNoDrugName_ReturnsFalse()
		{
			string drugName = null;

			Assert.False(await _nlmDrugApi.FetchIsDrugValidByDrugNameAsync(drugName));
		}

		[Fact]
		public async Task WhenValidatingDrugName_GivenEmptyDrugName_ReturnsFalse()
		{
			string drugName = "";

			Assert.False(await _nlmDrugApi.FetchIsDrugValidByDrugNameAsync(drugName));
		}

		[Fact]
		public async Task WhenValidatingDrugName_GivenBadDrugName_ReturnsFalse()
		{
			string drugName = "Bad Drug Name";

			Mock_NlmHttpClient_FetchDrugIdByName_BadDrugName(drugName);

			Assert.False(await _nlmDrugApi.FetchIsDrugValidByDrugNameAsync(drugName));
		}

		[Fact]
		public async Task WhenValidatingDrugName_GivenGoodDrugName_ReturnsTrue()
		{
			int drugId = 12345;
			string drugName = "Good Drug Name";

			Mock_NlmHttpClient_FetchDrugIdByName_GoodDrugName(drugName, drugId);

			Assert.True(await _nlmDrugApi.FetchIsDrugValidByDrugNameAsync(drugName));
		}

		[Fact]
		public async Task WhenFetchingInteractions_GivenNoDrugName_ReturnsEmptyList()
		{
			string drugName = null;

			Assert.Empty(await _nlmDrugApi.FetchDrugInteractionsByDrugNameAsync(drugName));
		}

		[Fact]
		public async Task WhenFetchingInteractions_GivenEmptyDrugName_ReturnsEmptyList()
		{
			string drugName = "";

			Assert.Empty(await _nlmDrugApi.FetchDrugInteractionsByDrugNameAsync(drugName));
		}

		[Fact]
		public async Task WhenFetchingInteractions_GivenBadDrugName_ReturnsEmptyList()
		{
			string drugName = "Bad Drug Name";

			Mock_NlmHttpClient_FetchDrugIdByName_BadDrugName(drugName);
			Mock_NlmHttpClient_FetchDrugInteractionsByDrugId_NullDrugId();

			Assert.Empty(await _nlmDrugApi.FetchDrugInteractionsByDrugNameAsync(drugName));
		}

		[Fact]
		public async Task WhenFetchingInteractions_GivenGoodDrugName_GivenNoInteractions_ReturnsEmptyList()
		{
			int drugId = 12345;
			string drugName = "Good Drug Name";

			Mock_NlmHttpClient_FetchDrugIdByName_GoodDrugName(drugName, drugId);
			Mock_NlmHttpClient_FetchDrugInteractionsByDrugId_GoodDrugId_NoInteractions(drugId);

			Assert.Empty(await _nlmDrugApi.FetchDrugInteractionsByDrugNameAsync(drugName));
		}

		[Fact]
		public async Task WhenFetchingInteractions_GivenGoodDrugName_GivenInteractions_ReturnsInteractions()
		{
			int drugId = 12345;
			string drugName = "Good Drug Name";

			int otherDrugId = 67890;
			string otherDrugName = "Other Drug Name";
			string otherDescription = "This is a description.";

			Mock_NlmHttpClient_FetchDrugIdByName_GoodDrugName(drugName, drugId);
			Mock_NlmHttpClient_FetchDrugInteractionsByDrugId_GoodDrugId_WithInteractions(drugId, new List<Tuple<int, string, string, string>>
			{
				new Tuple<int, string, string, string>(otherDrugId, otherDrugName, otherDescription, "")
			});

			var result = (await _nlmDrugApi.FetchDrugInteractionsByDrugNameAsync(drugName))[0];

			Assert.Equal(otherDrugId, result.Drug.Id);
			Assert.Equal(otherDrugName, result.Drug.Name);
			Assert.Equal(otherDescription, result.Description);
		}

		[Fact]
		public async Task WhenFetchingInteractions_GivenGoodDrugName_GivenDuplicateInteractions_ReturnsDistinctInteractions()
		{
			int drugId = 12345;
			string drugName = "Good Drug Name";

			string otherDrugName_1 = "Drug 1";
			string otherDrugName_2 = "Drug 2";

			string otherId_1 = "Other ID #1";
			string otherId_2 = "Other ID #2";

			Mock_NlmHttpClient_FetchDrugIdByName_GoodDrugName(drugName, drugId);
			Mock_NlmHttpClient_FetchDrugInteractionsByDrugId_GoodDrugId_WithInteractions(drugId, new List<Tuple<int, string, string, string>>
			{
				new Tuple<int, string, string, string>(1, otherDrugName_1, "Description 1", otherId_1),
				new Tuple<int, string, string, string>(1, "Drug 2", "Description 2", otherId_1),
				new Tuple<int, string, string, string>(1, otherDrugName_2, "Description 3", otherId_2),
			});

			var results = await _nlmDrugApi.FetchDrugInteractionsByDrugNameAsync(drugName);

			Assert.Equal(2, results.Count);
			Assert.Equal(otherDrugName_1, results[0].Drug.Name);
			Assert.Equal(otherDrugName_2, results[1].Drug.Name);
		}

		private void Mock_NlmHttpClient_FetchDrugIdByName_BadDrugName(string drugName) =>
			A.CallTo(() => _fakeNlmHttpClient.FetchDrugIdByName(drugName))
				.Returns(new DrugIdResult
				{
					Name = drugName,
					DrugId = null,
				});

		private void Mock_NlmHttpClient_FetchDrugIdByName_GoodDrugName(string drugName, int drugId) =>
			A.CallTo(() => _fakeNlmHttpClient.FetchDrugIdByName(drugName))
				.Returns(new DrugIdResult
				{
					DrugId = new List<int> { drugId },
					Name = drugName,
				});

		private void Mock_NlmHttpClient_FetchDrugInteractionsByDrugId_NullDrugId() =>
			A.CallTo(() => _fakeNlmHttpClient.FetchDrugInteractionsByDrugId(null))
				.Returns<List<InteractionResult>>(null);

		private void Mock_NlmHttpClient_FetchDrugInteractionsByDrugId_GoodDrugId_NoInteractions(int drugId) =>
			A.CallTo(() => _fakeNlmHttpClient.FetchDrugInteractionsByDrugId(drugId))
				.Returns(new List<InteractionResult>());

		private void Mock_NlmHttpClient_FetchDrugInteractionsByDrugId_GoodDrugId_WithInteractions(int drugId, List<Tuple<int, string, string, string>> otherDrugs) =>
			A.CallTo(() => _fakeNlmHttpClient.FetchDrugInteractionsByDrugId(drugId))
				.Returns(otherDrugs
					.Select(interaction => new InteractionResult
					{
						Description = interaction.Item3,
						Drugs = new List<InteractionDrug>
							{
								new InteractionDrug(),
								new InteractionDrug
								{
									MinimumDetails = new InteractionMinimumDetails
									{
										DrugId = interaction.Item1,
										Name = interaction.Item2,
									},
									SourceDetails = new InteractionSourceDetails
									{
										Id = interaction.Item4,
									}
								}
							}
					})
					.ToList());
	}
}

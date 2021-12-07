using System.Collections.Generic;
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
			Mock_NlmHttpClient_FetchDrugInteractionsByDrugId_GoodDrugId_WithInteraction(drugId, otherDrugName, otherDrugId, otherDescription);

			var result = (await _nlmDrugApi.FetchDrugInteractionsByDrugNameAsync(drugName))[0];

			Assert.Equal(result.Drug.Id, otherDrugId);
			Assert.Equal(result.Drug.Name, otherDrugName);
			Assert.Equal(result.Description, otherDescription);
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

		private void Mock_NlmHttpClient_FetchDrugInteractionsByDrugId_GoodDrugId_WithInteraction(int drugId, string otherDrugName, int otherDrugId, string otherDescription) =>
			A.CallTo(() => _fakeNlmHttpClient.FetchDrugInteractionsByDrugId(drugId))
				.Returns(new List<InteractionResult>
				{
					new InteractionResult
					{
						Description = otherDescription,
						Drugs = new List<InteractionDrug>
						{
							new InteractionDrug(),
							new InteractionDrug
							{
								MinimumDetails = new InteractionMinimumDetails
								{
									DrugId = otherDrugId,
									Name = otherDrugName,
								},
							}
						},
					},
				});
	}
}

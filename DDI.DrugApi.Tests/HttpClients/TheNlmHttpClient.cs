using System.Collections.Generic;
using System.Threading.Tasks;
using DDI.DrugApi.HttpClients;
using DDI.DrugApi.TransferModels.NlmDrugApi;
using FakeItEasy;
using Xunit;

namespace DDI.DrugApi.Tests.HttpClients
{
	public class TheNlmDrugApi
	{
		private readonly DefaultHttpClient _fakeHttpClient;

		private readonly NlmHttpClient _nlmHttpClient;

		public TheNlmDrugApi()
		{
			_fakeHttpClient = A.Fake<DefaultHttpClient>();

			_nlmHttpClient = new NlmHttpClient(
				_fakeHttpClient
			);
		}

		[Fact]
		public async Task WhenFetchingDrugId_GivenNoDrugName_ReturnsNull()
		{
			string drugName = null;

			Assert.Null(await _nlmHttpClient.FetchDrugIdByName(drugName));
		}

		[Fact]
		public async Task WhenFetchingDrugId_GivenDrugName_ReturnsResult()
		{
			int drugId = 12345;
			string drugName = "Drug Name";

			Mock_HttpClient_GetFromJsonAsync_DrugIdResultWrapper(drugName, drugId);

			var result = await _nlmHttpClient.FetchDrugIdByName(drugName);

			Assert.Equal(drugName, result.Name);
			Assert.Equal(drugId, result.DrugId[0]);
		}

		[Fact]
		public async Task WhenFetchingDrugInteractions_GivenNoDrugName_ReturnsNull()
		{
			int? drugId = null;

			Assert.Null(await _nlmHttpClient.FetchDrugInteractionsByDrugId(drugId));
		}

		[Fact]
		public async Task WhenFetchingDrugInteractions_GivenDrugName_ReturnsInteractions()
		{
			int drugId = 12345;

			int otherDrugId = 67890;
			string otherDrugName = "Other Drug Name";
			string otherDescription = "This is a description.";

			Mock_HttpClient_GetFromJsonAsync_InteractionWrapper(otherDrugName, otherDrugId, otherDescription);

			var results = await _nlmHttpClient.FetchDrugInteractionsByDrugId(drugId);

			Assert.Equal(otherDescription, results[0].Description);
			Assert.Equal(otherDrugId, results[0].Drugs[1].MinimumDetails.DrugId);
			Assert.Equal(otherDrugName, results[0].Drugs[1].MinimumDetails.Name);
		}

		private void Mock_HttpClient_GetFromJsonAsync_DrugIdResultWrapper(string drugName, int drugId) =>
			A.CallTo(() => _fakeHttpClient.GetFromJsonAsync<DrugIdResultWrapper>(A<string>._))
				.Returns(new DrugIdResultWrapper
				{
					Result = new DrugIdResult
					{
						DrugId = new List<int> { drugId },
						Name = drugName,
					}
				});

		private void Mock_HttpClient_GetFromJsonAsync_InteractionWrapper(string otherDrugName, int otherDrugId, string otherDescription) =>
			A.CallTo(() => _fakeHttpClient.GetFromJsonAsync<InteractionWrapper>(A<string>._))
				.Returns(new InteractionWrapper
				{
					TypeGroups = new List<InteractionTypeGroup>
					{
						new InteractionTypeGroup
						{
							Types = new List<InteractionType>
							{
								new InteractionType
								{
									Interactions = new List<InteractionResult>
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
												},
											},
										},
									},
								},
							},
						},
					},
				});
	}
}

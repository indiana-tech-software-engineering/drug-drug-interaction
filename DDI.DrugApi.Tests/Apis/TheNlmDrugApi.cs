using DDI.DrugApi.Apis;
using DDI.DrugApi.HttpClients;
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
		public void Works()
		{
			Assert.True(true);
		}
	}
}

using System.Net.Http;
using DDI.DrugApi.Apis;
using DDI.DrugApi.HttpClients;
using FakeItEasy;
using Xunit;

namespace DDI.DrugApi.Tests.HttpClients
{
	public class TheNlmDrugApi
	{
		private readonly HttpClient _fakeHttpClient;

		private readonly NlmHttpClient _nlmHttpClient;

		public TheNlmDrugApi()
		{
			_fakeHttpClient = A.Fake<HttpClient>();

			_nlmHttpClient = new NlmHttpClient(
				_fakeHttpClient
			);
		}

		[Fact]
		public void Works()
		{
			Assert.True(true);
		}
	}
}

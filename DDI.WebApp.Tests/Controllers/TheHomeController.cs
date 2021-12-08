using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDI.DrugApi.Apis;
using DDI.Models;
using DDI.WebApp.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DDI.WebApp.Tests.Controllers
{
	public class TheHomeController
	{
		private readonly HomeController _homeController;
		private readonly IDrugApi _fakeDrugApi;

		public TheHomeController()
		{
			_fakeDrugApi = A.Fake<IDrugApi>();

			_homeController = new HomeController(
				_fakeDrugApi
			);
		}

		[Fact]
		public async Task WhenAtIndex_GivenInvalidDrugName_ReturnsInvalidAndEmpty()
		{
			string drugName = "Invalid Drug Name";

			Mock_IsDrugValid(drugName, false);
			Mock_FetchInteractions(drugName, 0);

			var (isValid, interactions) = GetIndexModel(await _homeController.Index(drugName));

			Assert.False(isValid);
			Assert.Empty(interactions);
		}

		[Fact]
		public async Task WhenAtIndex_GivenValidDrugName_GivenNoInteractions_ReturnsValidAndEmpty()
		{
			string drugName = "Valid Drug Name";

			Mock_IsDrugValid(drugName, true);
			Mock_FetchInteractions(drugName, 0);

			var (isValid, interactions) = GetIndexModel(await _homeController.Index(drugName));

			Assert.True(isValid);
			Assert.Empty(interactions);
		}

		[Fact]
		public async Task WhenAtIndex_GivenValidDrugName_GivenInteractions_ReturnsValidAndInteractions()
		{
			int count = 3;
			string drugName = "Valid Drug Name";

			Mock_IsDrugValid(drugName, true);
			Mock_FetchInteractions(drugName, count);

			var (isValid, interactions) = GetIndexModel(await _homeController.Index(drugName));

			Assert.True(isValid);
			Assert.Equal(count, interactions.Count);
		}

		private void Mock_IsDrugValid(string drugName, bool isValid) =>
			A.CallTo(() => _fakeDrugApi.FetchIsDrugValidByDrugNameAsync(drugName))
				.Returns(isValid);

		private void Mock_FetchInteractions(string drugName, int count) =>
			A.CallTo(() => _fakeDrugApi.FetchDrugInteractionsByDrugNameAsync(drugName))
				.Returns(Enumerable.Repeat(A.Fake<Interaction>(), count).ToList());

		private (bool, List<Interaction>) GetIndexModel(IActionResult result) =>
			Assert.IsAssignableFrom<(bool, List<Interaction>)>(
				Assert.IsType<ViewResult>(result).ViewData.Model);
	}
}

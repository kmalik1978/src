using System;
using ESFA.UI.Specflow.Framework.Project.Framework.Helpers;
using ESFA.UI.Specflow.Framework.Project.Tests.Pages;
using ESFA.UI.Specflow.Framework.Project.Tests.TestSupport;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace ESFA.UI.Specflow.Framework.Project.Tests.StepDefinitions
{
	[Binding]
	public class SearchResultsSteps : BaseTest
	{

		[Given(@"I am on the Find a Course Search page")]
		public void NavigateToSearchResults()
		{
			//webDriver.Url = Configurator.GetConfiguratorInstance().GetBaseUrl();
		}



		[Then(@"I should be on Find a Course Search Results page")]
		public void ConfirmSearchResultsPage()
		{
			ScenarioContext.Current.Pending();
		}

	}
}
using System;
using ESFA.UI.Specflow.Framework.Project.Framework.Helpers;
using ESFA.UI.Specflow.Framework.Project.Tests.Pages;
using ESFA.UI.Specflow.Framework.Project.Tests.TestSupport;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace ESFA.UI.Specflow.Framework.Project.Tests.StepDefinitions
{
	[Binding]
	public class FindACourseSteps : BaseTest
	{

		[Given(@"I navigate to Find a Course home page")]
		public void NavigateToFindACourseHomePage()
		{
			webDriver.Url = Configurator.GetConfiguratorInstance().GetBaseUrl();
		}


		[When(@"I enter course (.*)")]
		public void EnterCourse(string courseTxt)
		{
			FindACoursePage findACoursePage = new FindACoursePage(webDriver);
			findACoursePage.EnterCourseName(courseTxt);
		}


		[When(@"I select qualification (.*)")]
		public void SelectQualification(string qualification)
		{
			FindACoursePage findACoursePage = new FindACoursePage(webDriver);
			findACoursePage.SelectQualification(qualification);
		}


		[When(@"I enter location (.*)")]
		public void EnterLocation(string location)
		{
			FindACoursePage findACoursePage = new FindACoursePage(webDriver);
			findACoursePage.EnterLocation(location);
		}


		[When(@"I select distance (.*)")]
		public void SelectDistance(string distance)
		{
			FindACoursePage findACoursePage = new FindACoursePage(webDriver);
			findACoursePage.SelectDistance(distance);
		}

		[When(@"I click Search")]
		public void ClickSearch()
		{
			//FindACoursePage findACoursePage = new FindACoursePage(webDriver);
			//findACoursePage.ClickSearch();
			webDriver.FindElement(By.Name("Search")).Submit();
		}


	}
}
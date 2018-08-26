using System;
using ESFA.UI.Specflow.Framework.Project.Framework.Helpers;
using ESFA.UI.Specflow.Framework.Project.Tests.Pages;
using ESFA.UI.Specflow.Framework.Project.Tests.TestSupport;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace ESFA.UI.Specflow.Framework.Project.Tests.StepDefinitions
{
    [Binding]
    public class FindACourseSteps : BaseTest
    {
        private IWebDriver _driver;
        private BrowserStackDriver _bsDriver;


        [Scope(Tag = "regression")]
        [Given(@"I navigate to Find a Course home page")]
        public void NavigateToFindACourseHomePage()
        {
            webDriver.Url = Configurator.GetConfiguratorInstance().GetBaseUrl();
		}

        [Scope(Tag = "BrowserStack")]
        [Given(@"I am on Find a Course for (.*) and (.*)")]
        public void BSNavigateToFindACourseHomePage(string profile, string environment)
        {
            _bsDriver = (BrowserStackDriver)ScenarioContext.Current["bsDriver"];
            _driver = _bsDriver.Init(profile, environment);
            _driver.Url = Configurator.GetConfiguratorInstance().GetBaseUrl();
        }


        [When(@"I enter course (.*)")]
		public void EnterCourse(string courseTxt)
		{
			FindACoursePage findACoursePage = new FindACoursePage(webDriver);
			findACoursePage.EnterCourseName(courseTxt);
		}

        [Scope(Tag = "BrowserStack")]
        [When(@"Using BrowserStack I enter course (.*)")]
        public void BSEnterCourse(string courseTxt)
        {
            _driver.FindElement(By.Name("SubjectKeyword")).SendKeys(courseTxt);
        }


        [When(@"I select qualification (.*)")]
		public void SelectQualification(string qualification)
		{
			FindACoursePage findACoursePage = new FindACoursePage(webDriver);
			findACoursePage.SelectQualification(qualification);
		}

        [Scope(Tag = "BrowserStack")]
        [When(@"Using BrowserStack I select qualification (.*)")]
        public void BSSelectQualification(string qualification)
        {
            var selectElement = new SelectElement(_driver.FindElement(By.Name("QualificationLevel")));
            selectElement.SelectByText(qualification);
        }

        [When(@"I enter location (.*)")]
		public void EnterLocation(string location)
		{
			FindACoursePage findACoursePage = new FindACoursePage(webDriver);
			findACoursePage.EnterLocation(location);
		}

        [Scope(Tag = "BrowserStack")]
        [When(@"Using BrowserStack I enter location (.*)")]
        public void BSEnterLocation(string location)
        {
            _driver.FindElement(By.Name("Location")).SendKeys(location);
        }


        [When(@"I select distance (.*)")]
		public void SelectDistance(string distance)
		{
			FindACoursePage findACoursePage = new FindACoursePage(webDriver);
			findACoursePage.SelectDistance(distance);
		}

        [Scope(Tag = "BrowserStack")]
        [When(@"Using BrowserStack I select distance (.*)")]
        public void BSSelectDistance(string distance)
        {
            var selectElement = new SelectElement(_driver.FindElement(By.Name("LocationRadius")));
            selectElement.SelectByText(distance);
        }


        [When(@"I click Search")]
		public void ClickSearch()
		{
			webDriver.FindElement(By.Name("Search")).Submit();
		}


        [Scope(Tag = "BrowserStack")]
        [When(@"Using BrowserStack I click Search")]
        public void BSClickSearch()
        {
            _driver.FindElement(By.Name("Search")).Submit();
        }


        [When(@"I click (.*) link")]
		public void ClickContactAnAdviserLink(string contactAdvisor)
		{
			webDriver.FindElement(By.LinkText("Contact an adviser")).Click();
		}

        [Scope(Tag = "BrowserStack")]
        [When(@"Using BrowserStack I click (.*) link")]
        public void BSClickContactAnAdviserLink(string contactAdvisor)
        {
            _driver.FindElement(By.LinkText("Contact an adviser")).Click();
        }

    }
}
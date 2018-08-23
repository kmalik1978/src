using System;
using System.Threading;
using ESFA.UI.Specflow.Framework.Project.Framework.Helpers;
using ESFA.UI.Specflow.Framework.Project.Tests.Pages;
using ESFA.UI.Specflow.Framework.Project.Tests.TestSupport;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace ESFA.UI.Specflow.Framework.Project.Tests.StepDefinitions
{ 
// Google Steps
[Binding]
public class SingleSteps
{
	private IWebDriver _driver;
	readonly BrowserStackDriver _bsDriver;

	public SingleSteps()
	{
		_bsDriver = (BrowserStackDriver)ScenarioContext.Current["bsDriver"];
	}

	[Given(@"I am on the google page for (.*) and (.*)")]
	public void GivenIAmOnTheGooglePage(string profile, string environment)
	{
		_driver = _bsDriver.Init(profile, environment);
		_driver.Navigate().GoToUrl("https://www.google.com/ncr");
	}

	[When(@"I search forr (.*)")]
	public void WhenISearchFor(string keyword)
	{
		var q = _driver.FindElement(By.Name("q"));
		q.SendKeys(keyword);
		q.Submit();
	}

	[Then(@"I should see title (.*)")]
	public void ThenIShouldSeeTitle(string title)
	{
		Thread.Sleep(5000);
		Assert.That(_driver.Title, Is.EqualTo(title));
	}
}
}
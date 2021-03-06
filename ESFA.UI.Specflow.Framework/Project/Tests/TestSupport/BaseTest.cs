﻿using System;
using System.Diagnostics;
using System.IO;
using ESFA.UI.Specflow.Framework.Project.Framework.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;

namespace ESFA.UI.Specflow.Framework.Project.Tests.TestSupport
{
    [Binding]
    public class BaseTest
    {
        protected static IWebDriver webDriver;
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private static string screenshotPath;
        private static string reportNm = null;
        private static string reportPath = null;
        protected static int counter;
        protected static int counter2;

        [BeforeTestRun(Order =1)]
        public static void InitializeReport(object sender, EventArgs e)
        {
            String reportsDirectory = AppDomain.CurrentDomain.BaseDirectory
                       + "../../"
                       + "\\Project\\Reports\\"
                       + DateTime.Now.ToString("dd-MM-yyyy")
                       + "\\";
            if (!Directory.Exists(reportsDirectory))
            {
                Directory.CreateDirectory(reportsDirectory);
            }
            reportNm = (DateTime.Now.ToString("yyyyMMddHHmmss") + ".html");
            reportPath = Path.Combine(reportsDirectory, reportNm);
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [AfterTestRun(Order = 1)]
        public static void TearDown()
        {
            try
            {
                TakeScreenshotOnFailure();
            }
            finally
            {
                webDriver.Quit();
            }
        }

        [BeforeTestRun(Order = 2)]
        public static void SetUp()
        {
            String browser = Configurator.GetConfiguratorInstance().GetBrowser();
            switch(browser)
            {
                case "firefox" :
                    webDriver = new FirefoxDriver();
                    webDriver.Manage().Window.Maximize();
                    break;

                case "chrome" :
                    webDriver = new ChromeDriver();
                    webDriver.Manage().Window.Maximize();
                    break;

                case "ie":
                    webDriver = new InternetExplorerDriver();
                    webDriver.Manage().Window.Maximize();
                    break;

                //km case "phantomjs":
                //km    webDriver = new PhantomJSDriver();
                //km    break;

                case "zapProxyChrome":
                    InitialiseZapProxyChrome();
                    break;

                default:
                    throw new Exception("Driver name does not match OR this framework does not support the webDriver specified");
            }
            
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            webDriver.Manage().Cookies.DeleteAllCookies();
            String currentWindow = webDriver.CurrentWindowHandle;
            webDriver.SwitchTo().Window(currentWindow);
            
            PageInteractionHelper.SetDriver(webDriver);

        }


        [AfterTestRun(Order = 2)]
        public static void TearDown2()
        {
            webDriver.Quit();
            extent.Flush();
        }

        public static void TakeScreenshotOnFailure()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                try
                {
                    DateTime dateTime = DateTime.Now;
                    String featureTitle = FeatureContext.Current.FeatureInfo.Title;
                    String scenarioTitle = ScenarioContext.Current.ScenarioInfo.Title;
                    String failureImageName = dateTime.ToString("HH-mm-ss")
                        + "_"
                        + scenarioTitle
                        + ".png";
                    String screenshotsDirectory = AppDomain.CurrentDomain.BaseDirectory
                        + "../../"
                        + "\\Project\\Screenshots\\"
                        + dateTime.ToString("dd-MM-yyyy")
                        + "\\";
                    if (!Directory.Exists(screenshotsDirectory))
                    {
                        Directory.CreateDirectory(screenshotsDirectory);
                    }
                
                    ITakesScreenshot screenshotHandler = webDriver as ITakesScreenshot;
                    Screenshot screenshot = screenshotHandler.GetScreenshot();
                    screenshotPath = Path.Combine(screenshotsDirectory, failureImageName);
                    screenshot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
                    Console.WriteLine(scenarioTitle
                        + " -- Scenario failed and the screenshot is available at -- "
                        + screenshotPath);
                } catch (Exception exception)
                {
                    Console.WriteLine("Exception occurred while taking screenshot - " + exception);
                }
            }            
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
            counter++;
            counter2++;
        }


        [BeforeScenario]
        public void BeforeScenario()
        {
            if (counter / counter2 == 5)
            { 
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
                counter2++;
            }
            counter++;
        }


        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }


        [BeforeStep]
        public void BeforeStep()
        {
            //TODO: implement logic that has to run before executing each step
        }

        [AfterStep]
        public static void InsertReportingSteps(object sender, EventArgs e)
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "But")
                    scenario.CreateNode<But>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail((ScenarioContext.Current.TestError.Message) 
                                                                                            + (ScenarioContext.Current.TestError.StackTrace));
                    TakeScreenshotOnFailure();
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioStepContext.Current.StepInfo.Text).AddScreenCaptureFromPath(screenshotPath);
                }
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType == "But")
                    scenario.CreateNode<But>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
            }

            if (ScenarioContext.Current.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
            }
        }
  

    private static void InitialiseZapProxyChrome()
        {
            const string PROXY = "localhost:8095";
            var chromeOptions = new ChromeOptions();
            var proxy = new Proxy();
            proxy.HttpProxy = PROXY;
            proxy.SslProxy = PROXY;
            proxy.FtpProxy = PROXY;
            chromeOptions.Proxy = proxy;

            webDriver = new ChromeDriver(chromeOptions);
        }
    }
}
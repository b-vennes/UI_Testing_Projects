using System;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace F1Tester
{
    [TestClass]
    public class Tests
    {
        IWebDriver driver;

        private void TestSetup()
        {
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("https://formula1.com");

            // accept privacy request
            new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(driver => driver.FindElement(By.XPath(".//*[contains(@class,'truste-close')]")));
            driver.FindElement(By.XPath(".//*[contains(@class,'truste-close')]")).Click();
        }

        private void TestTeardown()
        {
            driver.Dispose();
        }

        [TestMethod]
        public void RacesTest()
        {
            TestSetup();

            RacesPage _racesPage = new RacesPage(driver);

            foreach(RacesPage.Races race in Enum.GetValues(typeof(RacesPage.Races)))
            {
                _racesPage.Open();
                _racesPage.ClickRace(race);

                var raceTitle = driver.FindElement(By.XPath($".//*[contains(@class,'race-location') and contains(text(), '{race}')]"));
                raceTitle.Enabled.Should().BeTrue();
            }

            TestTeardown();
        }
    }
}

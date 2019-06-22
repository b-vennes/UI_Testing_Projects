using System;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;

namespace F1Tester
{
    [TestClass]
    public class Tests
    {
        FirefoxDriver firefox;

        private void TestSetup()
        {
            firefox = new FirefoxDriver();
        }

        private void TestTeardown()
        {
            firefox.Dispose();
        }

        [TestMethod]
        public void FirstTest()
        {
            TestSetup();

            RacesPage _racesPage = new RacesPage(firefox);

            foreach(RacesPage.Races race in Enum.GetValues(typeof(RacesPage.Races)))
            {
                _racesPage.Open();
                _racesPage.ClickRace(race);

                var raceTitle = firefox.FindElementByXPath($".//*[contains(@class,'race-location') and contains(text(), '{race}')]");
                raceTitle.Enabled.Should().BeTrue();
            }

            TestTeardown();
        }
    }
}

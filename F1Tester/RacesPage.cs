using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace F1Tester
{
    public class RacesPage
    {
        FirefoxDriver firefox;
        string baseUrl = "https://www.formula1.com/en/racing/2019.html";

        public enum Races
        {
            Australia,
            Bahrain,
            China,
            Azerbaijan,
            Spain,
            Monaco
        }

        public RacesPage(FirefoxDriver firefoxDriver)
        {
            firefox = firefoxDriver;
        }

        public void Open()
        {
            firefox.Navigate().GoToUrl(baseUrl);
        }

        public void ClickRace(Races race)
        {
            var image = firefox.FindElementByXPath($".//*[contains(@class, 'teaser-image') and contains(@style, '{race}')]");
            new Actions(firefox)
                .Click(image)
                .Perform();
            new WebDriverWait(firefox, TimeSpan.FromSeconds(20))
                .Until(driver => driver.FindElement(By.XPath($".//*[contains(@class,'race-location') and contains(text(), '{race}')]")));
        }
    }
}
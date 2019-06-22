using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace F1Tester
{
    public class RacesPage
    {
        IWebDriver _driver;
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

        public RacesPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Open()
        {
            _driver.Navigate().GoToUrl(baseUrl);
            new WebDriverWait(_driver, TimeSpan.FromSeconds(20))
                .Until(driver => driver.FindElement(By.XPath(".//*[contains(@class,'countdown-wrapper')]")));
        }

        public void ClickRace(Races race)
        {
            IWebElement image = _driver.FindElement(By.XPath($".//*[contains(@class, 'teaser-image') and contains(@style, '{race}')]"));

            new Actions(_driver)
                .MoveToElement(image)
                .Click()
                .Build()
                .Perform();

            new WebDriverWait(_driver, TimeSpan.FromSeconds(20))
                .Until(driver => driver.FindElement(By.XPath($".//*[contains(@class,'race-location') and contains(text(), '{race}')]")));
        }
    }
}
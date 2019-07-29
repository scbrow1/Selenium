
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

[TestClass]
public class Search
{
    private IWebDriver driver;

    [TestMethod]
    public void SearchHubWithValidSearch()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.FindElement(By.Id("search-small")).Click();
        IWebElement textField = driver.FindElement(By.Id("search-small"));
        textField.SendKeys("Quotations");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("sr-1")));
        driver.FindElement(By.Id("sr-1")).Click();
        Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Quotations")), "Search result click failed on hub page");
    }

    [TestMethod]
    public void SearchHubNoResults()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.FindElement(By.Id("search-small")).Click();
        IWebElement textField = driver.FindElement(By.Id("search-small"));
        textField.SendKeys("xxxxxx");
        System.Threading.Thread.Sleep(500);
        Assert.IsTrue(driver.FindElement(By.ClassName("no-results")).Displayed, "Bad message for no results on hub page");
    }

    [TestMethod]
    public void SearchMainWithValidSearch()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.FindElement(By.Id("close-slide-panel1")).Click();
        IWebElement textField = driver.FindElement(By.Id("search-home"));
        textField.Click();
        System.Threading.Thread.Sleep(100);
        textField.SendKeys("Quotations");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("sr-1")));
        driver.FindElement(By.Id("sr-1")).Click();
        Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Quotations")), "Search result click failed");
    }

    [TestMethod]
    public void SearchMainNoResults()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.FindElement(By.Id("close-slide-panel1")).Click();
        IWebElement textField = driver.FindElement(By.Id("search-home"));
        textField.Click();
        System.Threading.Thread.Sleep(100);
        textField.SendKeys("xxxxx");
        System.Threading.Thread.Sleep(500);
        Assert.IsTrue(driver.FindElement(By.ClassName("no-results")).Displayed, "Bad message for no results on main page");
    }

    [TestMethod]
    public void SearchMainWithForeignCharacters()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.FindElement(By.Id("close-slide-panel1")).Click();
        IWebElement textField = driver.FindElement(By.Id("search-home"));
        textField.Click();
        System.Threading.Thread.Sleep(100);
        textField.SendKeys("It's not just you");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("sr-1")));
        driver.FindElement(By.Id("sr-1")).Click();
        Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("It's not just you")), "Foreign character search failed");
    }

    [TestMethod]
    public void SearchMainWithAllCaps()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.FindElement(By.Id("close-slide-panel1")).Click();
        IWebElement textField = driver.FindElement(By.Id("search-home"));
        textField.Click();
        System.Threading.Thread.Sleep(100);
        textField.SendKeys("SPORTS");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("sr-1")));
        driver.FindElement(By.Id("sr-1")).Click();
        Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Sports")), "Foreign character search failed");
    }

    [TestMethod]
    public void SearchHiddenHub()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.FindElement(By.Id("close-slide-panel1")).Click();
        IWebElement textField = driver.FindElement(By.Id("search-home"));
        textField.Click();
        System.Threading.Thread.Sleep(100);
        textField.SendKeys("Amateur Sports");
        System.Threading.Thread.Sleep(500);
        Assert.IsTrue(driver.FindElement(By.ClassName("no-results")).Displayed, "Hidden hub is displayed");
    }

    [TestCleanup]
    public void After()
    {
        driver.Quit();
    }
}

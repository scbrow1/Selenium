
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

[TestClass]
public class Sharing
{
    private IWebDriver driver;

    [TestMethod]
    public void ShareOnNubPage()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/1670");
        driver.FindElement(By.ClassName("star")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog4")));
        string msg = driver.FindElement(By.Id("dialog-content4")).Text;
        Assert.IsTrue(msg.Contains("Share this post on"), "Share dialog did not present on nub page");
    }

    [TestMethod]
    public void ShareNubOnHubPage()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.FindElement(By.ClassName("star")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog4")));
        string msg = driver.FindElement(By.Id("dialog-content4")).Text;
        Assert.IsTrue(msg.Contains("Share this post on"), "Share dialog did not present on hub page");
    }

    [TestMethod]
    public void ShareHubOnHubPage()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.FindElement(By.Id("share-hub-button")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog4")));
        string msg = driver.FindElement(By.Id("dialog-content4")).Text;
        Assert.IsTrue(msg.Contains("Share this post on"), "Share dialog did not present on hub page");
    }

    [TestMethod]
    public void ValidateShareMetaTags()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/1670");

        string tagVal = "";
        
        tagVal = driver.FindElement(By.XPath("//meta[@name='twitter:site']")).GetAttribute("content");
        Assert.IsTrue(tagVal.Contains("@TweetBubHubs"), "Invalid twitter site meta tag");

        tagVal = driver.FindElement(By.XPath("//meta[@name='twitter:card']")).GetAttribute("content");
        Assert.IsTrue(tagVal.Contains("photo"), "Invalid twitter card meta tag");

        tagVal = driver.FindElement(By.XPath("//meta[@name='twitter:description']")).GetAttribute("content");
        Assert.IsTrue(tagVal.Contains("View on BubHubs"), "Invalid twitter description meta tag");

        tagVal = driver.FindElement(By.XPath("//meta[@name='twitter:image']")).GetAttribute("content");
        Assert.IsTrue(tagVal.Contains("/images/nubs/1670.jpg"), "Invalid twitter image meta tag");

        tagVal = driver.FindElement(By.XPath("//meta[@property='og:type']")).GetAttribute("content");
        Assert.IsTrue(tagVal.Contains("article"), "Invalid og type meta tag");

        tagVal = driver.FindElement(By.XPath("//meta[@property='og:image']")).GetAttribute("content");
        Assert.IsTrue(tagVal.Contains("/images/nubs/1670.jpg"), "Invalid og image meta tag");

        tagVal = driver.FindElement(By.XPath("//meta[@property='og:title']")).GetAttribute("content");
        Assert.IsTrue(tagVal.Contains("Supporting the team"), "Invalid og title meta tag");

        tagVal = driver.FindElement(By.XPath("//meta[@property='og:site_name']")).GetAttribute("content");
        Assert.IsTrue(tagVal.Contains("BubHubsGroup"), "Invalid og site_name meta tag");

        tagVal = driver.FindElement(By.XPath("//meta[@property='article:author']")).GetAttribute("content");
        Assert.IsTrue(tagVal.Contains("https://www.facebook.com/BubHubsGroup"), "Invalid facebook author meta tag");
    }

    [TestCleanup]
    public void After()
    {
        driver.Quit();
    }
}

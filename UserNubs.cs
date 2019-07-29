
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.Collections.Generic;

[TestClass]
public class Comments
{
    private IWebDriver driver;

    [TestMethod]
    public void NubsDisplayingInSettings()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab4")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("#settings-main-content > .nub-box")));
        int cnt = driver.FindElements(By.ClassName("nub-box")).Count;
        Assert.IsTrue(cnt == 64, "Wrong number of nubs displaying");
    }

    [TestMethod]
    public void NubViewScreenObjectsPresent()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab4")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='settings-main-content']/div[1]/div[1]/div[2]")));
        Assert.IsTrue(driver.FindElement(By.ClassName("nub-headline")).Displayed, "Headline missing on nub settings page");
        Assert.IsTrue(driver.FindElement(By.ClassName("nub-image-box")).Displayed, "Image missing on nub settings page");
        Assert.IsTrue(driver.FindElement(By.ClassName("delete-button")).Displayed);
    }

    [TestMethod]
    public void NubViewButtonWorks()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab4")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='settings-main-content']/div[1]/div[1]/div[2]")));
        driver.FindElement(By.XPath("//*[@id='settings-main-content']/div[1]/div[1]/div[2]")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("slide-panel3")));
    }

    [TestCleanup]
    public void After()
    {
        driver.Quit();
    }
}
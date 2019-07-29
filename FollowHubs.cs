
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

[TestClass]
public class FollowHubs
{
    private IWebDriver driver;

    [TestMethod]
    public void FollowHubWithoutLoginMainPage()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hub-add-remove-big")));
        driver.FindElement(By.ClassName("hub-add-remove-big")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("login")));
        string msg = driver.FindElement(By.ClassName("dialog-header-top")).Text;
        Assert.IsTrue(msg.Contains("Account Required"), "Adding hub possible without logging in");
    }

    [TestMethod]
    public void UnfollowHub()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Popular_today");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hub-add-remove-big")));

        //Click 'Settings' button
        driver.FindElement(By.ClassName("hub-add-remove-big")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='dialog-content1']/div[2]/div")));

        //Uncheck 'Add to everything feed' radio button
        driver.FindElement(By.ClassName("indent-left")).Click();
        driver.FindElement(By.XPath("//*[@id='dialog-content1']/div[2]/div")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hub-add-remove-big")));
        string buttonText = driver.FindElement(By.ClassName("image-button-text")).Text;
        Assert.IsTrue(buttonText.Contains("Add This Hub"), "Unfollow hub failed");
    }

    [TestMethod]
    public void FollowHub()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Popular_today");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hub-add-remove-big")));

        //Click 'Add this hub' button
        driver.FindElement(By.ClassName("hub-add-remove-big")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='dialog-content1']/div[2]/div")));

        //Click 'Add to everything feed' radio button
        driver.FindElement(By.ClassName("indent-left")).Click();
        driver.FindElement(By.XPath("//*[@id='dialog-content1']/div[2]/div")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hub-add-remove-big")));
        string buttonText = driver.FindElement(By.ClassName("image-button-text")).Text;
        Assert.IsTrue(buttonText.Contains("Settings"), "Follow hub failed");
    }

    [TestMethod]
    public void FollowFavoritesFeedHub()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Popular_today");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hub-add-remove-big")));

        //Click settings button
        driver.FindElement(By.ClassName("hub-add-remove-big")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("radio-nub2")));

        //Click 'Add to favorites feed' radio button
        driver.FindElement(By.Id("radio-nub2")).Click();
        driver.FindElement(By.XPath("//*[@id='dialog-content1']/div[2]/div")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hub-add-remove-big")));
        string buttonText = driver.FindElement(By.ClassName("image-button-text")).Text;
        System.Threading.Thread.Sleep(100);
        Assert.IsTrue(buttonText.Contains("Settings"), "Follow favorites feed failed");
    }

    [TestMethod]
    public void FollowNewsFeedHub()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Popular_today");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hub-add-remove-big")));

        //Click settings button
        driver.FindElement(By.ClassName("hub-add-remove-big")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("radio-nub2")));

        //Click 'Add to news feed' radio button
        driver.FindElement(By.Id("radio-nub3")).Click();
        driver.FindElement(By.XPath("//*[@id='dialog-content1']/div[2]/div")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hub-add-remove-big")));
        string buttonText = driver.FindElement(By.ClassName("image-button-text")).Text;
        System.Threading.Thread.Sleep(100);
        Assert.IsTrue(buttonText.Contains("Settings"), "Follow news feed failed");
    }

    [TestMethod]
    public void FollowHubWithoutLoginRightPanel()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hub-add-remove-small")));
        driver.FindElement(By.ClassName("hub-add-remove-small")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog1")));
        string msg = driver.FindElement(By.XPath("//*[@id='dialog-content1']/div[1]/div/div[1]")).Text;
        System.Threading.Thread.Sleep(100);
        Assert.IsTrue(msg.Contains("Account Required"), "Adding hub possible without logging in");
    }

    [TestCleanup]
    public void After()
    {
        driver.Quit();
    }
}

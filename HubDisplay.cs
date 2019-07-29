
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

[TestClass]
public class HubDisplay
{
    private IWebDriver driver;

    [TestMethod]
    public void DisplayHiddenHubWithAdminUser()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "s", "a");
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Editor_Posts");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Editor Posts")), "Hidden hub not displayed for admin user");
    }

    [TestMethod]
    public void DisplayHiddenHubWithNoUser()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Editor_Posts");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Page Not Found")), "Hidden hub displayed without logged in user");
    }

    [TestMethod]
    public void DisplayHiddenHubWithNonAdminUser()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "s.hammond@yahoo.com", "777777");
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Editor_Posts");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Page Not Found")), "Hidden hub displayed without logged in user");
    }

    [TestMethod]
    public void PostsDisplayedOnHubPage()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        ReadOnlyCollection<IWebElement> listItems = driver.FindElements(By.CssSelector(".feed-nubs .nub-box"));
        Assert.IsTrue(listItems.Count == 40, "Wrong number of posts displayed on home page");
    }

    [TestMethod]
    public void BubsDisplayOnHubPage()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='slide-panel1']/div[5]/div[1]/div/div/div[1]/div[2]")));
        driver.FindElement(By.XPath("//*[@id='slide-panel1']/div[5]/div[1]/div/div/div[1]/div[2]")).Click();
        ReadOnlyCollection<IWebElement> listItems = driver.FindElements(By.CssSelector(".hubs-and-bubs-panel .hub-bub"));
        Assert.IsTrue(listItems.Count == 9, "Wrong number of bubs displayed on hub bubs page");
    }

    [TestMethod]
    public void SubHubsDisplayOnHubsPage()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='slide-panel1']/div[5]/div[1]/div/div/div[1]/div[3]")));
        driver.FindElement(By.XPath("//*[@id='slide-panel1']/div[5]/div[1]/div/div/div[1]/div[3]")).Click();
        ReadOnlyCollection<IWebElement> listItems = driver.FindElements(By.CssSelector(".hubs-and-bubs-panel .hub-stub-browse"));
        Assert.IsTrue(listItems.Count == 8, "Wrong number of bubs displayed on hub bubs page");
    }

    [TestMethod]
    public void SubHubsDisplayOnHubsPageViaAjax()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='slide-panel1']/div[5]/div[3]/div/div/div[1]/div[1]/div[1]")));

        //sub hub dropdown icon clicked
        driver.FindElement(By.XPath("//*[@id='slide-panel1']/div[5]/div[3]/div/div/div[1]/div[1]/div[1]")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='slide-panel1']/div[5]/div[3]/div/div/div[2]/div[2]/div[1]")));

        //'Question Everything' Hub selected
        driver.FindElement(By.XPath("//*[@id='slide-panel1']/div[5]/div[3]/div/div/div[2]/div[2]/div[1]")).Click();
        Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Question Everything")), "Sub Hub not displayed/clickable in dropdown");
    }

    [TestMethod]
    public void PostTypeFilterOptionSelected()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='feed-filters']/div[1]")));

        //Post type filter icon clicked
        driver.FindElement(By.XPath("//*[@id='feed-filters']/div[1]")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='slide-panel1']/div[5]/div[3]/div/div/div[2]/div[2]/div[3]")));

        //'Video clips' selected
        driver.FindElement(By.XPath("//*[@id='slide-panel1']/div[5]/div[3]/div/div/div[2]/div[2]/div[3]")).Click();
        ReadOnlyCollection<IWebElement> listItems = driver.FindElements(By.CssSelector(".feed-nubs .nub-box"));
        Assert.IsTrue(listItems.Count == 1, "Wrong number of posts displayed on home page after choosing 'video clips' filter option");
    }

    [TestMethod]
    public void ImpactFilterOptionSelected()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='feed-filters']/div[3]")));

        //Impact filter icon clicked
        driver.FindElement(By.XPath("//*[@id='feed-filters']/div[3]")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='slide-panel1']/div[5]/div[3]/div/div/div[2]/div[2]/div[3]")));

        //'Cute' selected
        driver.FindElement(By.XPath("//*[@id='slide-panel1']/div[5]/div[3]/div/div/div[2]/div[2]/div[3]")).Click();
        ReadOnlyCollection<IWebElement> listItems = driver.FindElements(By.CssSelector(".feed-nubs .nub-box"));
        Assert.IsTrue(listItems.Count == 1, "Wrong number of posts displayed on home page after choosing 'cute' filter option");
    }

    [TestMethod]
    public void EverythingLinkClicked()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='feed-filters']/div[3]")));
        Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Everything")), "Everything link click failed");
    }

    [TestCleanup]
    public void After()
    {
        driver.Quit();
    }
}
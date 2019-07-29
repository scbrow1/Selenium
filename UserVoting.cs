
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

[TestClass]
public class UserVoting
{
    private IWebDriver driver;

    [TestMethod]
    public void VoteWithoutLogin()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/1670");

        //execute the vote and ensure dialog received
        driver.FindElement(By.ClassName("up")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog1")));
        System.Threading.Thread.Sleep(100);
        string msg = driver.FindElement(By.XPath("//*[@id='dialog-content1']/div[1]/div/div[1]")).Text;
        Assert.IsTrue(msg.Contains("Account Required"), "Voting permitted by non-user");
    }

    [TestMethod]
    public void VoteAfterLogout()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        Lib.Logout(driver);
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/1670");

        //execute the vote and ensure dialog received
        driver.FindElement(By.ClassName("up")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog1")));
        string msg = driver.FindElement(By.XPath("//*[@id='dialog-content1']/div[1]/div/div[1]")).Text;
        Assert.IsTrue(msg.Contains("Account Required"), "Voting permitted after logout");
    }

    [TestMethod]
    public void VoteUp()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/1670");

        //execute the vote and assert
        driver.FindElement(By.ClassName("up")).Click();
        System.Threading.Thread.Sleep(100);
        string classes = driver.FindElement(By.ClassName("up")).GetAttribute("Class");
        Assert.IsTrue(classes.IndexOf("selected") > 0, "Up vote failed");
    }

    [TestMethod]
    public void VoteDown()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/1608");

        //execute the vote and assert
        driver.FindElement(By.ClassName("down")).Click();
        System.Threading.Thread.Sleep(100);
        string classes = driver.FindElement(By.ClassName("down")).GetAttribute("Class");
        Assert.IsTrue(classes.IndexOf("selected") > 0, "Down vote failed");
    }

    [TestMethod]
    public void DoubleVote()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/1670");

        //execute the double vote
        driver.FindElement(By.ClassName("up")).Click();
        System.Threading.Thread.Sleep(100);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("up")));
        driver.FindElement(By.ClassName("up")).Click();

        //validate dialog
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog5")));
        string msg = driver.FindElement(By.Id("dialog-content5")).Text;
        Assert.IsTrue(msg.Equals("You already voted on this"), "re-voting (up vote) on nub allowed after logout");
    }

    [TestMethod]
    public void UpVotePersistsAfterLogout()
    {
        //first login
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/1670");
        driver.FindElement(By.ClassName("up")).Click();
        driver.Quit();

        //second login
        IWebDriver driver2;
        driver2 = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver2, "j.benedict@yahoo.com", "777777");
        driver2.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/1670");
        driver2.FindElement(By.ClassName("up")).Click();

        //validate dialog
        WebDriverWait wait = new WebDriverWait(driver2, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog5")));
        string msg = driver2.FindElement(By.Id("dialog-content5")).Text;
        Assert.IsTrue(msg.Equals("You already voted on this"), "re-voting (up vote) on nub allowed after logout");
    }

    [TestMethod]
    public void DownVotePersistsAfterLogout()
    {
        //first login
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/1670");
        driver.FindElement(By.ClassName("down")).Click();
        driver.Quit();

        //second login
        IWebDriver driver2;
        driver2 = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver2, "j.benedict@yahoo.com", "777777");
        driver2.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/1670");
        driver2.FindElement(By.ClassName("down")).Click();

        //validate dialog
        WebDriverWait wait = new WebDriverWait(driver2, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog5")));
        string msg = driver2.FindElement(By.Id("dialog-content5")).Text;
        Assert.IsTrue(msg.Equals("You already voted on this"), "re-voting (down vote) on nub allowed after logout");
    }

    [TestCleanup]
    public void After()
    {
        driver.Quit();
    }
}

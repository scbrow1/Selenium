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
public class CommentsTest
{
    private IWebDriver driver;

    [TestMethod]
    public void PostCommentWithoutLogin()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/1670");
        driver.FindElement(By.Id("comment0-text")).Click();
        System.Threading.Thread.Sleep(100);
        driver.FindElement(By.Id("comment0-text")).SendKeys("test post");

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog1")));
        string msg = driver.FindElement(By.XPath("//*[@id='dialog-content1']/div[1]/div/div[1]")).Text;
        Assert.IsTrue(msg.Contains("Account Required"), "Account required dialog not displayed");
    }

    [TestMethod]
    public void PostBlankComment()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        PostComment("1670", "", false, false);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("poster-comment")));
        string msg = driver.FindElement(By.Id("comment0-error")).Text;
        Assert.IsTrue(msg.Contains("You left it blank"), "Error creating blank comment");
    }

    [TestMethod]
    public void PostNewComment()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        PostComment("1670", "test comment");

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("poster-comment")));
        string msg = driver.FindElement(By.Id("poster-comment")).Text;
        Assert.IsTrue(driver.FindElement(By.Id("poster-name")).Text.Contains("Joe Benedict"), "Post not added");
        Assert.IsTrue(driver.FindElement(By.Id("poster-comment")).Text.Contains("test post"), "Post not added");
    }

    [TestMethod]
    public void PostValidCommentWithQuotes()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        PostComment("1670", "test \"comment\" with quotes");

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("poster-comment")));
        string msg = driver.FindElement(By.Id("poster-comment")).Text;
        Assert.IsTrue(driver.FindElement(By.Id("poster-name")).Text.Contains("Joe Benedict"), "Post not added");
        Assert.IsTrue(driver.FindElement(By.Id("poster-comment")).Text.Contains("with quotes"), "Post not added");
    }

    [TestMethod]
    public void PostValidCommentWithAmpersand()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        PostComment("1670", "test comment with &Amp");

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("poster-comment")));
        Assert.IsTrue(driver.FindElement(By.Id("poster-name")).Text.Contains("Joe Benedict"), "Post not added");
        Assert.IsTrue(driver.FindElement(By.Id("poster-comment")).Text.Contains("test comment with &Amp"), "Post not added");
    }

    [TestMethod]
    public void PostAnonymousPost()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        PostComment("1670", "test comment", true, true);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("poster-comment")));
        string msg = driver.FindElement(By.Id("poster-comment")).Text;
        Assert.IsTrue(driver.FindElement(By.Id("poster-name")).Text.Contains("Anonymous"), "Post not added");
    }

    [TestMethod]
    public void ReplyToComment()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        PostComment("1670", "test comment", true, false);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("poster-comment")));
        driver.FindElement(By.ClassName("reply-link")).Click();
        driver.FindElement(By.Id("reply-text-box")).SendKeys("test reply");
        driver.FindElement(By.ClassName("reply-button")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("poster-comment")));
        Assert.IsTrue(driver.FindElement(By.Id("reply-comment")).Text.Contains("test reply"), "Post not added");
        new Comment().Delete();
    }

    [TestMethod]
    public void ReplyWithBlankComment()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "j.benedict@yahoo.com", "777777");
        PostComment("1670", "test comment", true, false);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("poster-comment")));
        driver.FindElement(By.ClassName("reply-link")).Click();
        driver.FindElement(By.Id("reply-text-box")).SendKeys("test reply");
        driver.FindElement(By.ClassName("reply-button")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("poster-comment")));
        string msg = driver.FindElement(By.Id("comment0-error")).Text;
        Assert.IsTrue(msg.Contains("You left it blank"), "Error creating blank comment");
        new Comment().Delete();
    }

    public void PostComment(string nub, string comment, bool anonymous = false, bool delete = true)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Sports/nubs/"+ nub);
        driver.FindElement(By.Id("comment0-text")).Click();
        System.Threading.Thread.Sleep(100);
        driver.FindElement(By.Id("comment0-text")).SendKeys(comment);
        if (anonymous) driver.FindElement(By.Id("llllllll")).Click();  
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("comment0-save-comment")));
        driver.FindElement(By.Id("comment0-save-comment")).Click();
        System.Threading.Thread.Sleep(500);
        if (delete) new Comment().Delete();
    }

    [TestCleanup]
    public void After()
    {
        driver.Quit();
    }
}


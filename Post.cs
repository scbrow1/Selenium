
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
public class Post
{
    private IWebDriver driver;

    [TestMethod]
    public void PostButtonClickedByNonLoggedInUser()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/Sports");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("add-new-nub-button")));
        driver.FindElement(By.Id("add-new-nub-button")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("login")));
        string msg = driver.FindElement(By.XPath("//*[@id='dialog-content1']/div[1]/div/div[1]")).Text;
        Assert.IsTrue(msg.Contains("Account Required"), "Adding hub possible without logging in");
    }

    [TestMethod]
    public void ProperHubDisplayedOnPostPage()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        DisplayPostPage(driver, "Sports");
        string hub = driver.FindElement(By.ClassName("hub-name")).Text;
        Assert.IsTrue(hub.Contains("Sports"), "Improper or missing hub displayed on post page");
    }

    [TestMethod]
    public void AddValidCommentPost()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        DisplayPostPage(driver, "Sports");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //Add data to form
        new SelectElement(driver.FindElement(By.Id("nub-type"))).SelectByText("Comment / Question");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("comment-type")));
        new SelectElement(driver.FindElement(By.Id("comment-type"))).SelectByText("Comment");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("headline-box")));
        driver.FindElement(By.Id("headline-box")).SendKeys("test headline");
        driver.FindElement(By.XPath("//*[@id='nub-comment']/div[2]/textarea")).SendKeys("test comment");
        driver.FindElement(By.Id("save-nub")).Click();
        System.Threading.Thread.Sleep(300);

        string headline = driver.FindElement(By.XPath("//*[@id='nub-1']/div[3]/div[1]/div[1]/a")).Text;
        Assert.IsTrue(headline.Contains("test headline"), "New post not added");
    }

    [TestMethod]
    public void AddAnonymousPost()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        DisplayPostPage(driver, "Sports");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //Add data to form
        new SelectElement(driver.FindElement(By.Id("nub-type"))).SelectByText("Comment / Question");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("comment-type")));
        new SelectElement(driver.FindElement(By.Id("comment-type"))).SelectByText("Comment");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("headline-box")));
        driver.FindElement(By.Id("headline-box")).SendKeys("test headline");
        driver.FindElement(By.XPath("//*[@id='create-nub']/div[15]/div[2]")).Click();
        driver.FindElement(By.Id("save-nub")).Click();
        System.Threading.Thread.Sleep(300);

        string headline = driver.FindElement(By.XPath("//*[@id='nub-1']/div[3]/div[1]/div[1]/a")).Text;
        Assert.IsTrue(headline.Contains("test headline"), "New post not added");
    }

    [TestMethod]
    public void AddValidLinkPost()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        DisplayPostPage(driver, "Sports");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //Add data to form
        new SelectElement(driver.FindElement(By.Id("nub-type"))).SelectByText("Link to Article");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("hyperlink-type")));
        new SelectElement(driver.FindElement(By.Id("hyperlink-type"))).SelectByText("News Article");

        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("headline-box")));
        driver.FindElement(By.Id("headline-box")).SendKeys("test headline");
        driver.FindElement(By.XPath("//*[@id='nub-url']/div[2]/textarea")).SendKeys("http://www.cnn.com");
        new SelectElement(driver.FindElement(By.XPath("//*[@id='nub-impact']/div[2]/select"))).SelectByText("Interesting");

        driver.FindElement(By.XPath("//*[@id='nub-comment']/div[2]/textarea")).SendKeys("test comment");
        driver.FindElement(By.Id("save-nub")).Click();
        System.Threading.Thread.Sleep(300);

        string headline = driver.FindElement(By.XPath("//*[@id='nub-1']/div[3]/div[1]/div[1]/a")).Text;
        Assert.IsTrue(headline.Contains("test headline"), "New post not added");
    }

    [TestMethod]
    public void AddLinkPostWithMissingLink()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        DisplayPostPage(driver, "Sports");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //Add data to form
        new SelectElement(driver.FindElement(By.Id("nub-type"))).SelectByText("Link to Article");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("hyperlink-type")));
        new SelectElement(driver.FindElement(By.Id("hyperlink-type"))).SelectByText("News Article");

        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("headline-box")));
        driver.FindElement(By.Id("headline-box")).SendKeys("test headline");
        new SelectElement(driver.FindElement(By.XPath("//*[@id='nub-impact']/div[2]/select"))).SelectByText("Interesting");

        driver.FindElement(By.XPath("//*[@id='nub-comment']/div[2]/textarea")).SendKeys("test comment");
        driver.FindElement(By.Id("save-nub")).Click();
        System.Threading.Thread.Sleep(300);

        string msg = driver.FindElement(By.Id("nub-message")).Text;
        Assert.IsTrue(msg.Contains("URL required"), "Error not presented when enter blank comment text when creating post");
    }

    [TestMethod]
    public void AddLinkPostWithMissingHealine()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        DisplayPostPage(driver, "Sports");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //Add data to form
        new SelectElement(driver.FindElement(By.Id("nub-type"))).SelectByText("Link to Article");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("hyperlink-type")));
        new SelectElement(driver.FindElement(By.Id("hyperlink-type"))).SelectByText("News Article");

        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("headline-box")));
        driver.FindElement(By.XPath("//*[@id='nub-url']/div[2]/textarea")).SendKeys("http://www.cnn.com");
        new SelectElement(driver.FindElement(By.XPath("//*[@id='nub-impact']/div[2]/select"))).SelectByText("Interesting");

        driver.FindElement(By.XPath("//*[@id='nub-comment']/div[2]/textarea")).SendKeys("test comment");
        driver.FindElement(By.Id("save-nub")).Click();
        System.Threading.Thread.Sleep(300);

        string headline = driver.FindElement(By.XPath("//*[@id='nub-1']/div[3]/div[1]/div[1]/a")).Text;
        Assert.IsTrue(headline.Contains("Healine Required"), "New post not added");
    }

    [TestMethod]
    public void AddLinkPostWithInvalidURL()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        DisplayPostPage(driver, "Sports");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //Add data to form
        new SelectElement(driver.FindElement(By.Id("nub-type"))).SelectByText("Link to Article");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("hyperlink-type")));
        new SelectElement(driver.FindElement(By.Id("hyperlink-type"))).SelectByText("News Article");

        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("headline-box")));
        driver.FindElement(By.Id("headline-box")).SendKeys("test headline");
        driver.FindElement(By.XPath("//*[@id='nub-url']/div[2]/textarea")).SendKeys("zz");
        new SelectElement(driver.FindElement(By.XPath("//*[@id='nub-impact']/div[2]/select"))).SelectByText("Interesting");

        driver.FindElement(By.XPath("//*[@id='nub-comment']/div[2]/textarea")).SendKeys("test comment");
        driver.FindElement(By.Id("save-nub")).Click();
        System.Threading.Thread.Sleep(300);

        string headline = driver.FindElement(By.XPath("//*[@id='nub-1']/div[3]/div[1]/div[1]/a")).Text;
        Assert.IsTrue(headline.Contains("Invalid URL"), "New post not added");
    }

    [TestMethod]
    public void AddValidQuestionPost()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        DisplayPostPage(driver, "Sports");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //Add data to form
        new SelectElement(driver.FindElement(By.Id("nub-type"))).SelectByText("Comment / Question");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("comment-type")));
        new SelectElement(driver.FindElement(By.Id("comment-type"))).SelectByText("Question");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("headline-box")));
        driver.FindElement(By.Id("headline-box")).SendKeys("test question?");
        driver.FindElement(By.Id("save-nub")).Click();
        System.Threading.Thread.Sleep(300);

        string headline = driver.FindElement(By.XPath("//*[@id='nub-1']/div[3]/div[1]/div[1]/a")).Text;
        Assert.IsTrue(headline.Contains("test question?"), "New post not added");
    }

    [TestMethod]
    public void AddBlankCommentPost()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        DisplayPostPage(driver, "Sports");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //Add data to form
        new SelectElement(driver.FindElement(By.Id("nub-type"))).SelectByText("Comment / Question");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("comment-type")));
        new SelectElement(driver.FindElement(By.Id("comment-type"))).SelectByText("Comment");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("nub-create-save-button")));
        System.Threading.Thread.Sleep(300);
        driver.FindElement(By.ClassName("nub-create-save-button")).Click();
        System.Threading.Thread.Sleep(300);

        string headline = driver.FindElement(By.XPath("//*[@id='nub-1']/div[3]/div[1]/div[1]/a")).Text;
        Assert.IsTrue(headline.Contains("Headline required"), "New post not added");

        string msg = driver.FindElement(By.Id("nub-message")).Text;
        Assert.IsTrue(msg.Contains("Headline required"), "Error not presented when enter blank comment text when creating post");
    }

    [TestMethod]
    public void AddPicturePostWithoutUpload()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        DisplayPostPage(driver, "Sports");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //Add data to form
        new SelectElement(driver.FindElement(By.Id("nub-type"))).SelectByText("Picture");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("hyperlink-type")));
        new SelectElement(driver.FindElement(By.Id("picture-type"))).SelectByText("Photo");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("save-nub")));
        System.Threading.Thread.Sleep(300);
        driver.FindElement(By.Id("save-nub")).Click();
        System.Threading.Thread.Sleep(300);

        string msg = driver.FindElement(By.Id("nub-message")).Text;
        Assert.IsTrue(msg.Contains("Please upload a file"), "Error not presented when enter blank comment text when creating post");
    }

    [TestMethod]
    public void AddVideoPostWithoutUpload()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        DisplayPostPage(driver, "Sports");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //Add data to form
        new SelectElement(driver.FindElement(By.Id("nub-type"))).SelectByText("Video / Animated GIF");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("hyperlink-type")));
        new SelectElement(driver.FindElement(By.Id("video-type"))).SelectByText("Video File");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("save-nub")));
        System.Threading.Thread.Sleep(300);
        driver.FindElement(By.Id("save-nub")).Click();
        System.Threading.Thread.Sleep(300);

        string msg = driver.FindElement(By.Id("nub-message")).Text;
        Assert.IsTrue(msg.Contains("Please upload a file"), "Error not presented when enter blank comment text when creating post");
    }

    public void DisplayPostPage(IWebDriver driver, string hub)
    {
        Lib.Login(driver, "s.hammond@yahoo.com", "777777");
        driver.Navigate().GoToUrl(Constants.SiteUrl + "/" + hub);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("add-new-nub-button")));

        //Click 'Post Button'
        driver.FindElement(By.Id("add-new-nub-button")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog1")));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("save-nub")));
    }

    [TestCleanup]
    public void After()
    {
        driver.Quit();
    }
}
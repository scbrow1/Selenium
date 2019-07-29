
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

[TestClass]
public class Account
{
    private IWebDriver driver;

    [TestMethod]
    public void UpdateEmailAddress()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab5")).Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("email-change")));
        driver.FindElement(By.Id("email-change")).Click();

        //blank email        
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email-save")));
        driver.FindElement(By.Id("email-save")).Click();
        System.Threading.Thread.Sleep(500);
        string msg = driver.FindElement(By.Id("email-message")).Text;
        Assert.IsTrue(msg.Contains("Email required"), "Bad error when email blank");
        driver.FindElement(By.Id("email-cancel")).Click();

        //invalid email
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email-change")));
        driver.FindElement(By.Id("email-change")).Click();
        driver.FindElement(By.Id("email1")).SendKeys("xxxx");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email-save")));
        driver.FindElement(By.Id("email-save")).Click();
        System.Threading.Thread.Sleep(500);
        String msg2 = driver.FindElement(By.Id("email-message")).Text;
        Assert.IsTrue(msg2.Contains("Invalid email"), "Invalid error received");

        //valid email
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email-change")));
        driver.FindElement(By.Id("email-change")).Click();
        driver.FindElement(By.Id("email1")).SendKeys("test@email.com");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email-save")));
        driver.FindElement(By.Id("email-save")).Click();
        System.Threading.Thread.Sleep(500);
        string name = driver.FindElement(By.XPath("//*[@id='language-top']/div[2]")).Text;
        Assert.IsTrue(name.Contains("test@email.com"), "email address not updated");

        //change the email back
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email-change")));
        driver.FindElement(By.Id("email-change")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("email1")));
        driver.FindElement(By.Id("email1")).SendKeys("s.hammond@yahoo.com");
        driver.FindElement(By.Id("email-save")).Click();
    }

    [TestMethod]
    public void UpdatePassword()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab5")).Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("password-change")));
        driver.FindElement(By.Id("password-change")).Click();

        //wrong current password     
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("password-save")));
        driver.FindElement(By.Id("password1")).SendKeys("xxxx");
        driver.FindElement(By.Id("password2")).SendKeys("TestPassW0rd");
        driver.FindElement(By.Id("password3")).SendKeys("TestPassW0rd");
        driver.FindElement(By.Id("password-save")).Click();
        System.Threading.Thread.Sleep(500);
        string msg = driver.FindElement(By.Id("password-message")).Text;
        Assert.IsTrue(msg.Contains("Wrong current password"), "Bad error when current password is wrong");

        //password too short  
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("password-save")));
        driver.FindElement(By.Id("password1")).SendKeys("777777");
        driver.FindElement(By.Id("password2")).SendKeys("a");
        driver.FindElement(By.Id("password3")).SendKeys("a");
        driver.FindElement(By.Id("password-save")).Click();
        System.Threading.Thread.Sleep(500);
        string msg2 = driver.FindElement(By.Id("password-message")).Text;
        Assert.IsTrue(msg2.Contains("too short"), "Bad error when password too short");

        //passwords don't match
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("password-save")));
        driver.FindElement(By.Id("password1")).SendKeys("777777");
        driver.FindElement(By.Id("password2")).SendKeys("testuuu777!");
        driver.FindElement(By.Id("password3")).SendKeys("testuuu888!");
        driver.FindElement(By.Id("password-save")).Click();
        System.Threading.Thread.Sleep(500);
        string msg3 = driver.FindElement(By.Id("password-message")).Text;
        Assert.IsTrue(msg3.Contains("don't match"), "Bad error when passwords don't match");

        //Valid password
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("password-save")));
        driver.FindElement(By.Id("password1")).SendKeys("777777");
        driver.FindElement(By.Id("password2")).SendKeys("NewTestPassw0rd");
        driver.FindElement(By.Id("password3")).SendKeys("NewTestPassw0rd");
        driver.FindElement(By.Id("password-save")).Click();

        //change password back
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("password-save")));
        driver.FindElement(By.Id("password1")).SendKeys("NewTestPassw0rd");
        driver.FindElement(By.Id("password2")).SendKeys("777777");
        driver.FindElement(By.Id("password3")).SendKeys("777777");
        driver.FindElement(By.Id("password-save")).Click();
    }

    [TestMethod]
    public void UpdateEmailOnComment()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab5")).Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("email-on-comment-change")));
        driver.FindElement(By.Id("email-on-comment-change")).Click();
    
        //Update radio button
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email-on-comment-save")));
        driver.FindElement(By.XPath("//*[@id='email-on-comment-box']/div[1]/input[2]")).Click();
        driver.FindElement(By.Id("email-on-comment-save")).Click();
        System.Threading.Thread.Sleep(500);
        string msg = driver.FindElement(By.XPath("//*[@id='email-on-comment-top']/div[2]")).Text;
        Assert.IsTrue(msg.Contains("No"), "Email on comment not updated properly");

        //Change back
        driver.FindElement(By.Id("email-on-comment-change")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email-on-comment-save")));
        driver.FindElement(By.XPath("//*[@id='email-on-comment-box']/div[1]/input[1]")).Click();
        driver.FindElement(By.Id("email-on-comment-save")).Click();
    }

    [TestMethod]
    public void UpdateEmailOnFollow()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab5")).Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("email-on-follow-change")));
        driver.FindElement(By.Id("email-on-follow-change")).Click();

        //Update radio button
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email-on-follow-save")));
        driver.FindElement(By.XPath("//*[@id='email-on-follow-box']/div[1]/input[2]")).Click();
        driver.FindElement(By.Id("email-on-follow-save")).Click();
        System.Threading.Thread.Sleep(500);
        string msg = driver.FindElement(By.XPath("//*[@id='email-on-follow-top']/div[2]")).Text;
        Assert.IsTrue(msg.Contains("No"), "Email on follow not updated properly");

        //Change back
        driver.FindElement(By.Id("email-on-follow-change")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email-on-follow-save")));
        driver.FindElement(By.XPath("//*[@id='email-on-follow-box']/div[1]/input[1]")).Click();
        driver.FindElement(By.Id("email-on-follow-save")).Click();
    }

    [TestMethod]
    public void UpdateEmailOnFavorites()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab5")).Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("email-favorites-change")));
        driver.FindElement(By.Id("email-favorites-change")).Click();

        //Update radio button
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email-favorites-save")));
        driver.FindElement(By.XPath("//*[@id='email-favorites-box']/div[1]/input[2]")).Click();
        driver.FindElement(By.Id("email-favorites-save")).Click();
        System.Threading.Thread.Sleep(500);
        string msg = driver.FindElement(By.XPath("//*[@id='email-favorites-top']/div[2]")).Text;
        Assert.IsTrue(msg.Contains("No"), "Email on follow not updated properly");

        //Change back
        driver.FindElement(By.Id("email-favorites-change")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("email-favorites-save")));
        driver.FindElement(By.XPath("//*[@id='email-favorites-box']/div[1]/input[1]")).Click();
        driver.FindElement(By.Id("email-favorites-save")).Click();
    }

    [TestMethod]
    public void UpdateViewExplicitPosts()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab5")).Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("explicit-change")));
        driver.FindElement(By.Id("explicit-change")).Click();

        //Update radio button
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("explicit-save")));
        driver.FindElement(By.XPath("//*[@id='explicit-box']/div[1]/input[2]")).Click();
        driver.FindElement(By.Id("explicit-save")).Click();
        System.Threading.Thread.Sleep(500);
        string msg = driver.FindElement(By.XPath("//*[@id='explicit-top']/div[2]")).Text;
        Assert.IsTrue(msg.Contains("No"), "Email on follow not updated properly");

        //Change back
        driver.FindElement(By.Id("explicit-change")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("explicit-save")));
        driver.FindElement(By.XPath("//*[@id='explicit-box']/div[1]/input[1]")).Click();
        driver.FindElement(By.Id("explicit-save")).Click();
    }

    [TestMethod]
    public void UpdateRememberMe()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab5")).Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("remember-me-change")));
        driver.FindElement(By.Id("remember-me-change")).Click();

        //Update radio button
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("remember-me-save")));
        driver.FindElement(By.XPath("//*[@id='remember-me-box']/div[1]/input[2]")).Click();
        driver.FindElement(By.Id("remember-me-save")).Click();
        System.Threading.Thread.Sleep(500);
        string msg = driver.FindElement(By.XPath("//*[@id='remember-me-top']/div[2]")).Text;
        Assert.IsTrue(msg.Contains("No"), "Email on follow not updated properly");

        //Change back
        driver.FindElement(By.Id("remember-me-change")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("remember-me-save")));
        driver.FindElement(By.XPath("//*[@id='remember-me-box']/div[1]/input[1]")).Click();
        driver.FindElement(By.Id("remember-me-save")).Click();
    }

    [TestCleanup]
    public void After()
    {
        driver.Quit();
    }
}

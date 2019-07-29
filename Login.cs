
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

[TestClass]
public class Login
{
    private IWebDriver driver;

    [TestMethod]
    public void LoginBadCredentials()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "nouser", "nopass");
        System.Threading.Thread.Sleep(500);
        Assert.IsTrue(driver.FindElement(By.Id("dialog1")).Displayed, "Login occurred despite bad credentials");
        string msg = driver.FindElement(By.Id("login-message")).Text;
        Assert.IsTrue(msg.Contains("have this email address on file"), "Bad error message");
    }

    [TestMethod]
    public void LoginValidUser()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "s", "a");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Everything Feed"));
    }

    [TestMethod]
    public void LogoutUser()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "s", "a");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Everything Feed"));
        Lib.Logout(driver);
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("A hub for everything"));
    }

    [TestMethod]
    public void ForgotPasswordValidEmail()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.ForgotPassword(driver, "testuser@gmail.com");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog5")));
    }

    [TestMethod]
    public void ForgotPasswordInvalidEmail()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.ForgotPassword(driver, "bademail@email.com");
        System.Threading.Thread.Sleep(500);
        string msg = driver.FindElement(By.Id("forgot-password-message")).Text;
        Assert.IsTrue(msg.Contains("have this email address on file"), "No error received for bad email");
    }

    [TestMethod]
    public void ForgotPasswordNoEmail()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.ForgotPassword(driver, "");
        System.Threading.Thread.Sleep(500);
        string msg = driver.FindElement(By.Id("forgot-password-message")).Text;
        Assert.IsTrue(msg.Contains("have this email address on file"), "No error received for bad email");
    }

    [TestMethod]
    public void LoginViaAccountRequiredDialog()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hub-add-remove-big")));
    
        //Click "Settings" button
        driver.FindElement(By.ClassName("hub-add-remove-big")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog1")));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("login")));
        driver.FindElement(By.Id("login")).Click();
        System.Threading.Thread.Sleep(1000);
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog1")));

        //Login as test user
        driver.FindElement(By.Id("login-email")).SendKeys("j.benedict@yahoo.com");
        driver.FindElement(By.Id("login-password")).SendKeys("777777");
        driver.FindElement(By.Id("login-button")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Everything Feed"));
    }

    [TestMethod]
    public void SettingsOptionEnabledAfterLogin()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "s.hammond@yahoo.com", "777777");
        driver.FindElement(By.Id("close-slide-panel1")).Click();
        driver.FindElement(By.Id("down-button")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("tabs-pane")));
    }

    [TestMethod]
    public void SettingsOptionPresentInMenuAfterLogin()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, "s.hammond@yahoo.com", "777777");
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dropdown")));
        Assert.IsNotNull(driver.FindElements(By.Id("dropdown-settings")), "Settings option not in dropdown after login");
    }

    [TestCleanup]
    public void After()
    {
        driver.Quit();
    }
}
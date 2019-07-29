
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

[TestClass]
public class Signup
{
    private IWebDriver driver;

    [TestMethod]
    public void SignUpUserValid()
    {
        User user = Lib.GetUserData(1, "SignupUsers");
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Signup(driver, user);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("signup-complete-dialog"))).Displayed, "Signup complete dialog not presented");
    }

    [TestMethod]
    public void NewUserCanLogin()
    {
        User user = Lib.GetUserData(1, "SignupUsers");
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Login(driver, user.EmailAddress, user.Password);
        user.Delete();
    }

    [TestMethod]
    public void SignUpUserTooYoung()
    {
        User user = Lib.GetUserData(2, "SignupUsers");
        driver = Lib.OpenBrowser(Constants.SiteUrl);

        Lib.Signup(driver, user);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message")));
        String msg = driver.FindElement(By.Id("error-message")).Text;
        Assert.IsTrue(msg.Contains("Sorry, you must be over"), "Invalid error received on signup");
    }

    [TestMethod]
    public void SignUpNoEmail()
    {
        User user = Lib.GetUserData(3, "SignupUsers");
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Signup(driver, user);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message")));
        String msg = driver.FindElement(By.Id("error-message")).Text;
        Assert.IsTrue(msg.Contains("Email Address required"), "Invalid error received on signup with no email");
    }

    [TestMethod]
    public void SignUpInvalidEmail()
    {
        User user = Lib.GetUserData(4, "SignupUsers");
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Signup(driver, user);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message")));
        String msg = driver.FindElement(By.Id("error-message")).Text;
        Assert.IsTrue(msg.Contains("Invalid email address"), "Invalid error received on signup with invalid email");
    }

    [TestMethod]
    public void SignupPasswordsDontMatch()
    {
        User user = Lib.GetUserData(5, "SignupUsers");
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Signup(driver, user, "no match");

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message")));
        String msg = driver.FindElement(By.Id("error-message")).Text;
        Assert.IsTrue(msg.Contains("Passwords don't match"), "Invalid error received on signup with password mispatch");
    }

    [TestMethod]
    public void SignupNoZipCode()
    {
        User user = Lib.GetUserData(6, "SignupUsers");
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Signup(driver, user);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message")));
        String msg = driver.FindElement(By.Id("error-message")).Text;
        Assert.IsTrue(msg.Contains("Passwords don't match"), "Invalid error received on signup with no zip code");
    }

    [TestMethod]
    public void SingupEmailAlreadyPresent()
    {
        User user = Lib.GetUserData(7, "SignupUsers");
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Signup(driver, user);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message")));
        String msg = driver.FindElement(By.Id("error-message")).Text;
        Assert.IsTrue(msg.Contains("Email address in use by another"), "Invalid error received on signup with email address already present");
    }

    [TestMethod]
    public void SignupNoBirthdate()
    {
        User user = Lib.GetUserData(8, "SignupUsers");
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Signup(driver, user);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message")));
        String msg = driver.FindElement(By.Id("error-message")).Text;
        Assert.IsTrue(msg.Contains("Birthdate required"), "Invalid error received on signup with no birthdate");
    }

    [TestMethod]
    public void SignupPasswordTooShort()
    {
        User user = Lib.GetUserData(9, "SignupUsers");
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Signup(driver, user);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message")));
        String msg = driver.FindElement(By.Id("error-message")).Text;
        Assert.IsTrue(msg.Contains("Password must be greater than 5 characters"), "Invalid error received on signup with password too short");
    }

    [TestMethod]
    public void SignupBadCharacters()
    {
        User user = Lib.GetUserData(10, "SignupUsers");
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.Signup(driver, user);

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message")));
        String msg = driver.FindElement(By.Id("error-message")).Text;
        Assert.IsTrue(msg.Contains("Invalid characters in name"), "Invalid error received on signup with bad characters");
    }

    [TestMethod]
    public void SingupViaAccountRequiredDialog()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("hub-add-remove-big")));

        //Click 'Add this hub' button on home page
        driver.FindElement(By.ClassName("hub-add-remove-big")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("need-account")));

        //Click 'Ok' on the account required page
        driver.FindElement(By.Id("need-account")).Click();
        Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog1"))).Displayed, "Signup dialog not presented");
    }

    [TestCleanup]
    public void After()
    {
        driver.Quit();
    }
}
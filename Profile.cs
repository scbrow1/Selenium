
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

[TestClass]
public class Profile
{
    private IWebDriver driver;

    [TestMethod]
    public void UpdateName()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab6")).Click();

        //update the name
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("name-change")));
        driver.FindElement(By.Id("name-change")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("name1")));
        driver.FindElement(By.Id("name1")).SendKeys("firstname");
        driver.FindElement(By.Id("name-save")).Click();
        System.Threading.Thread.Sleep(500);
        string msg = driver.FindElement(By.Id("name-message")).Text;
        Assert.IsTrue(msg.Contains("Last Name required"), "Bad error when first name blank");
        driver.FindElement(By.Id("name2")).SendKeys("lastname");
        driver.FindElement(By.Id("name-save")).Click();
        System.Threading.Thread.Sleep(500);
        string name = driver.FindElement(By.XPath("//*[@id='name-top']/div[2]")).Text;
        Assert.IsTrue(name.Contains("firstname lastname"), "Name not updated properly");

        //change the name back
        driver.FindElement(By.Id("name-change")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("name1")));
        driver.FindElement(By.Id("name1")).SendKeys("steve");
        driver.FindElement(By.Id("name2")).SendKeys("hammond");
        driver.FindElement(By.Id("name-save")).Click();
    }

    [TestMethod]
    public void UpdateZipCode()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab6")).Click();

        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("name-change")));
        driver.FindElement(By.Id("zip-code-change")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("zip-code1")));

        //invalid the zip code
        driver.FindElement(By.Id("zip-code1")).SendKeys("!!!!!");
        driver.FindElement(By.Id("zip-code-save")).Click();
        System.Threading.Thread.Sleep(500);
        string msg = driver.FindElement(By.Id("zip-code-message")).Text;
        Assert.IsTrue(msg.Contains("Invalid Zip Code"), "Bad error when zip code invalid");

        //valid the zip code
        driver.FindElement(By.XPath("//*[@id='zip-code-change']")).Click();
        driver.FindElement(By.Id("zip-code1")).SendKeys("111111");
        driver.FindElement(By.Id("zip-code-save")).Click();
        System.Threading.Thread.Sleep(500);
        string name = driver.FindElement(By.XPath("//*[@id='zip-code-top']/div[2]")).Text;
        Assert.IsTrue(name.Contains("111111"), "Zip code not updated");

        //change the zip code back
        driver.FindElement(By.Id("zip-code-change")).Click();
        driver.FindElement(By.Id("zip-code1")).SendKeys("98112");
        driver.FindElement(By.Id("zip-code-save")).Click();
    }

    [TestMethod]
    public void UpdateGender()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab6")).Click();

        //update gender
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("gender-change")));
        driver.FindElement(By.Id("gender-change")).Click();
        SelectElement select1 = new SelectElement(driver.FindElement(By.Id("gender1")));
        select1.SelectByText("Female");
        driver.FindElement(By.Id("gender-save")).Click();
        System.Threading.Thread.Sleep(500);
        string name = driver.FindElement(By.XPath("//*[@id='gender-top']/div[2]")).Text;
        Assert.IsTrue(name.Contains("F"), "Gender not updated");

        //change the gender back
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("gender-change")));
        driver.FindElement(By.Id("gender-change")).Click();
        SelectElement select2 = new SelectElement(driver.FindElement(By.Id("gender1")));
        select2.SelectByText("Male");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("gender-save")));
        driver.FindElement(By.Id("gender-save")).Click();
    }

    [TestMethod]
    public void UpdateCountry()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab6")).Click();

        //update country
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("country-change")));
        driver.FindElement(By.Id("country-change")).Click();
        SelectElement select1 = new SelectElement(driver.FindElement(By.Id("country1")));
        select1.SelectByText("France");
        driver.FindElement(By.Id("country-save")).Click();
        System.Threading.Thread.Sleep(500);
        string name = driver.FindElement(By.XPath("//*[@id='country-top']/div[2]")).Text;
        Assert.IsTrue(name.Contains("France"), "country not updated");

        //change the country back
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("country-change")));
        driver.FindElement(By.Id("country-change")).Click();
        SelectElement select2 = new SelectElement(driver.FindElement(By.Id("country1")));
        select2.SelectByText("United States");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("country-change")));
        driver.FindElement(By.Id("country-save")).Click();
    }

    [TestMethod]
    public void UpdateLanguage()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab6")).Click();

        //update language
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("language-change")));
        driver.FindElement(By.Id("language-change")).Click();
        SelectElement select1 = new SelectElement(driver.FindElement(By.Id("language1")));
        select1.SelectByText("Japanese");
        driver.FindElement(By.Id("language-save")).Click();
        System.Threading.Thread.Sleep(500);
        string name = driver.FindElement(By.XPath("//*[@id='language-top']/div[2]")).Text;
        Assert.IsTrue(name.Contains("Japanese"), "language not updated");

        //change the language back
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("language-change")));
        driver.FindElement(By.Id("language-change")).Click();
        SelectElement select2 = new SelectElement(driver.FindElement(By.Id("language1")));
        select2.SelectByText("English");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("language-change")));
        driver.FindElement(By.Id("language-save")).Click();
    }

    [TestMethod]
    public void UpdateBirthdate()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab6")).Click();

        //update birthdate
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("birthdate-change")));
        driver.FindElement(By.Id("birthdate-change")).Click();
        SelectElement select1 = new SelectElement(driver.FindElement(By.Id("month")));
        select1.SelectByText("January");
        SelectElement select2 = new SelectElement(driver.FindElement(By.Id("day")));
        select2.SelectByText("1");
        SelectElement select3 = new SelectElement(driver.FindElement(By.Id("year")));
        select3.SelectByText("1975");
        driver.FindElement(By.Id("birthdate-save")).Click();
        System.Threading.Thread.Sleep(500);
        string name = driver.FindElement(By.XPath("//*[@id='birthdate-top']/div[2]")).Text;
        Assert.IsTrue(name.Contains("01/01/1975"), "birthdate not updated");

        //change the birthdate back
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("birthdate-change")));
        driver.FindElement(By.Id("birthdate-change")).Click();
        SelectElement select4 = new SelectElement(driver.FindElement(By.Id("month")));
        select4.SelectByText("November");
        SelectElement select5 = new SelectElement(driver.FindElement(By.Id("day")));
        select5.SelectByText("2");
        SelectElement select6 = new SelectElement(driver.FindElement(By.Id("year")));
        select6.SelectByText("1985");
        driver.FindElement(By.Id("birthdate-save")).Click();
        System.Threading.Thread.Sleep(500);
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("birthdate-change")));
        driver.FindElement(By.Id("birthdate-save")).Click();
    }

    [TestMethod]
    public void UpdateBirthdateTooYoung()
    {
        driver = Lib.OpenBrowser(Constants.SiteUrl);
        Lib.LoginSettingsUser(driver);
        driver.FindElement(By.Id("tab6")).Click();

        //update birthdate
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("birthdate-change")));
        driver.FindElement(By.Id("birthdate-change")).Click();
        SelectElement select1 = new SelectElement(driver.FindElement(By.Id("month")));
        select1.SelectByText("January");
        SelectElement select2 = new SelectElement(driver.FindElement(By.Id("day")));
        select2.SelectByText("1");
        SelectElement select3 = new SelectElement(driver.FindElement(By.Id("year")));
        select3.SelectByText("2016");
        driver.FindElement(By.Id("birthdate-save")).Click();
        System.Threading.Thread.Sleep(500);
        string msg = driver.FindElement(By.Id("birthdate-message")).Text;
        Assert.IsTrue(msg.Contains("Sorry, you must be"), "bad error message for birthdate");
    }

    [TestCleanup]
    public void After()
    {
        driver.Quit();
    }
}

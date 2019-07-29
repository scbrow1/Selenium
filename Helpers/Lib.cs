
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Edge;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class Lib
{
    public static void Signup(IWebDriver driver, User user, string specialCase = "")
    {
        driver.FindElement(By.Id("bars")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dropdown")));
        Assert.IsNotNull(driver.FindElements(By.Id("dropdown-singup")), "Singup link did not display");
        driver.FindElement(By.Id("dropdown-signup")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog1")));
        driver.FindElement(By.Id("ca-first-name")).SendKeys(user.FirstName);
        driver.FindElement(By.Id("ca-last-name")).SendKeys(user.LastName);
        driver.FindElement(By.Id("ca-email")).SendKeys(user.EmailAddress);
        driver.FindElement(By.Id("ca-password")).SendKeys(user.Password);
        if (specialCase == "no match")
            driver.FindElement(By.Id("ca-repeat-password")).SendKeys("zsadfasf");
        else
            driver.FindElement(By.Id("ca-repeat-password")).SendKeys(user.Password);
        SelectElement select1 = new SelectElement(driver.FindElement(By.Id("ca-month")));
        select1.SelectByText(user.BirthDate.ToString("MMMM"));
        SelectElement select2 = new SelectElement(driver.FindElement(By.Id("ca-day")));
        select2.SelectByText(user.BirthDate.Day.ToString());
        SelectElement select3 = new SelectElement(driver.FindElement(By.Id("ca-year")));
        select3.SelectByText(user.BirthDate.Year.ToString());
        driver.FindElement(By.CssSelector("input[value='" + user.Gender + "']")).Click();
        SelectElement select4 = new SelectElement(driver.FindElement(By.Id("ca-country")));
        select4.SelectByText(user.CountryName);
        driver.FindElement(By.Id("ca-zip-code")).SendKeys(user.ZipCode);
        driver.FindElement(By.Id("submit-ca")).Click();
    }

    public static void Login(IWebDriver driver, string username, string password)
    {
        driver.FindElement(By.Id("bars")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dropdown")));
        Assert.IsNotNull(driver.FindElements(By.Id("dropdown-login")), "Login link did not display");
        driver.FindElement(By.Id("dropdown-login")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog1")));
        System.Threading.Thread.Sleep(200);
        driver.FindElement(By.Id("login-email")).SendKeys(username);
        driver.FindElement(By.Id("login-password")).SendKeys(password);
        driver.FindElement(By.Id("login-button")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("Everything Feed"));
        wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
    }

    public static void LoginSettingsUser(IWebDriver driver)
    {
        Login(driver, "s.hammond@yahoo.com", "777777");
        driver.FindElement(By.Id("close-slide-panel1")).Click();
        driver.FindElement(By.Id("down-button")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("tabs-pane")));
    }

    public static void Logout(IWebDriver driver)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        System.Threading.Thread.Sleep(500);
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("bars")));
        driver.FindElement(By.Id("bars")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dropdown")));
        Assert.IsNotNull(driver.FindElements(By.Id("dropdown-logout")), "Login link did not display");
        driver.FindElement(By.Id("dropdown-logout")).Click();
    }

    public static void ForgotPassword(IWebDriver driver, string emailAddress)
    {
        driver.FindElement(By.Id("bars")).Click();
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dropdown")));
        driver.FindElement(By.Id("dropdown-login")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog1")));
        driver.FindElement(By.Id("forgot-password")).Click();
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("dialog2")));
        driver.FindElement(By.Id("forgot-email")).SendKeys(emailAddress);
        driver.FindElement(By.Id("send-password-button")).Click();
    }

    public static IWebDriver OpenBrowser(string url)
    {
        IWebDriver driver;
        if (Constants.BrowserName == "Chrome")
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--start-maximized");
            driver = new ChromeDriver(option);
        }
        else if (Constants.BrowserName == "Firefox")
            driver = new FirefoxDriver();
        else
        {
            string serverPath = "Microsoft Web Driver";
            if (System.Environment.Is64BitOperatingSystem)
                serverPath = Path.Combine(System.Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%"), serverPath);
            else
                serverPath = Path.Combine(System.Environment.ExpandEnvironmentVariables("%ProgramFiles%"), serverPath);

            EdgeOptions options = new EdgeOptions();
            driver = new EdgeDriver(serverPath, options);
        }
        driver.Navigate().GoToUrl(url);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        return driver;
    }

    public static User GetUserData(int row, string type)
    {
        User user = new User();

        List<string> lines = GetTestData(type);
        string[] columns = lines[row - 1].Split(',');

        user.FirstName = columns[0];
        user.LastName = columns[1];
        user.EmailAddress = columns[2];
        user.Password = columns[3];
        user.BirthDate = Convert.ToDateTime(columns[4]);
        user.Gender = columns[5];
        user.CountryName = columns[6];
        user.ZipCode = columns[7];

        return user;
    }

    public static List<string> GetTestData(string fileName)
    {
        List<String> lines = new List<string>();
        string filePath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\testdata\\" + fileName + ".csv";
        using (var sr = new StreamReader(filePath))
        {
            while (sr.Peek() >= 0)
                lines.Add(sr.ReadLine());
        }
        return lines;
    }

    public string RandomString(int size, bool lowerCase)
    {
        Random rnd = new Random();
        int length = 5;
        var str = "";
        for (var i = 0; i < length; i++)
        {
            str += ((char)(rnd.Next(1, 26) + 64)).ToString();
        }
        return str;
    }

}

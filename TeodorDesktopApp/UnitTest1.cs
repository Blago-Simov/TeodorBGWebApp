using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace TeodorDesktopApp
{
    public class SeleniumTests
    {
        IWebDriver driver; 
        const string pageUrl = "https://teodor.bg/";
        string expectedResult = "СТРУКТУРНА БОРДО ВРАТОВРЪЗКА НА СИНИ ФИГУРИ";
        IWebElement agreeButton ;
        IWebElement elementAccesories;
        IWebElement ties;
        IWebElement tieItem;
        IWebElement tieBordo;
        IWebElement addToCartButton;
        IWebElement showCartButton;
        IWebElement addedItem;
        [SetUp]
        public void Setup()
        {
           driver = new ChromeDriver();
           driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    
        }

        [Test]
        public void SucssesfullyAddProductTocart()
        {
            //Arrange
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(pageUrl);
            driver.Manage().Window.Maximize();


            //Act
            agreeButton = driver.FindElement(By.XPath("//div[5]/div/div/div/button[1]"));
            agreeButton.Click();

            elementAccesories = driver.FindElement(By.CssSelector(".has-sub:nth-child(2) > a"));
            Actions action = new Actions(driver);
            action.MoveToElement(elementAccesories).Perform();

            ties = driver.FindElement(By.LinkText("Вратовръзки"));
            ties.Click();

            tieItem = driver.FindElement(By.XPath("//div[2]/ol/li[1]/div/a/span[1]/span"));
            tieItem.Click();

            tieBordo = driver.FindElement(By.XPath("//div[4]/div/a[15]/span[1]/span/span"));
            tieBordo.Click();

            addToCartButton = driver.FindElement(By.Id("product-addtocart-button"));
            addToCartButton.Click();

            Thread.Sleep(3000);

            showCartButton = driver.FindElement(By.CssSelector(".showcart"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(showCartButton).Perform();

            addedItem = driver.FindElement(By.XPath("//li/div/div/strong/a"));

            //Assert
            Assert.AreEqual(expectedResult, addedItem.Text);
            Assert.IsNotNull(showCartButton);


        }
        [TearDown]
        public void TearDown()
        {
           driver.Quit();
        }
    }
}
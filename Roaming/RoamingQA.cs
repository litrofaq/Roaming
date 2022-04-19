using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Roaming;

public class RoamingQA
{
    private ChromeDriver driver;
    
    [SetUp]
    public void SetUp()
    {
        // Опции отображения браузера
        var options = new ChromeOptions();
        options.AddArgument("start-maximized");
        // Запуск браузера
        driver = new ChromeDriver(options);
    }
    
    [Test]
    public void _blankTest()
    {
        // Перейти по урлу
        driver.Navigate().GoToUrl(urlRoaming);

        // Ожидать 10 секунд появления поля ввода
        WebDriverWait waitVisibleInput = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisibleInput.Until(e => e.FindElement(emailInputLokator)).SendKeys(email3LevelDomain);
        // Кликнуть по кнопке
        driver.FindElement(buttonSendMeLokator).Click();


        Assert.Multiple(() =>
        {
            Assert.IsFalse(driver.FindElement(errorFormLokator).Displayed,
                $"\tОжидалась успешная валидация емэйла домена третьего уровня '{email3LevelDomain}'\n" +
                $"Фактически ошибка валидации емэйл: '{driver.FindElement(errorFormLokator).Text}'");
        });
    }
    
    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
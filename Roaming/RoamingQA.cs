using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Roaming;

public class RoamingQA
{
    private CounterAgents myFullFields = new CounterAgents("ООО 'Сладкая сказка'", "1839001744", "165701001",
        1, "Иванов Сидор Петрович", "ivanov@test.ru","89051234567");
    
    
    private ChromeDriver driver;
    private string urlRoaming = "https://qa-course.kontur.host/roaming";
    
    // первый экран
    private By titleLokator = By.Id("Company");
    private By innLokator = By.Id("Inn");   
    private By kppLokator = By.Id("Kpp");
    private By nameLokator = By.Id("Name");
    private By emailLokator =By.Id("Liame");
    private By phoneLokator = By.Id("Phone");
    private By button1Lokator = By.ClassName("js-roaming-next-step-button");


    // Второй экран
    
    private By scanTextLokator = By.Id("file-upload-text");
    
    
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
    // Ввод полной валидной формы и переход на второй шаг
    public void truFullFirstForm_jumpStep2()
    {
        // Перейти по урлу
        driver.Navigate().GoToUrl(urlRoaming);

        // Ожидать 10 секунд появления кнопки(загрузка страницы окончена)
        WebDriverWait waitVisibleInput = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisibleInput.Until(e => e.FindElement(button1Lokator));
        
        // Ввести все поля
        driver.FindElement(titleLokator).SendKeys(myFullFields.title);
        driver.FindElement(innLokator).SendKeys(myFullFields.inn);
        driver.FindElement(kppLokator).SendKeys(myFullFields.kpp);
        driver.FindElement(nameLokator).SendKeys(myFullFields.fullName);
        driver.FindElement(emailLokator).SendKeys(myFullFields.email);
        driver.FindElement(phoneLokator).SendKeys(myFullFields.telephone);
        //driver.FindElement().SendKeys();

        
        
        // Кликнуть по кнопке
        driver.FindElement(button1Lokator).Click();

        // Ожидать 10 секунд появления второго состояния
        WebDriverWait waitVisible2screen = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisibleInput.Until(e => e.FindElement(scanTextLokator));
        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(scanTextLokator).Displayed);
                //$"\tОжидалась успешная валидация и переход на второе состояние '");//{email3LevelDomain}'\n" + 
                //$"Фактически ошибка валидации емэйл: '{driver.FindElement(errorFormLokator).Text}'");
        });
    }
    
    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
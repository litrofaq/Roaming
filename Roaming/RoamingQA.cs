using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Roaming;

public class RoamingQA
{
    private CounterAgents myFullFields = new CounterAgents("ООО 'Сладкая сказка'", "1839001744", "165701001",
        1, "Иванов Сидор Петрович", "ivanov@test.ru","89051234567");
    
    private CounterAgents myFullFields_specialUserNameEmail = new CounterAgents("ООО 'Сладкая сказка'", "1839001744", "165701001",
        1, "Иванов Сидор Петрович", "ab(c)d,e:f;g<h>i[j]l@example.com","89051234567"); // err = Сервер временно недоступен
    
    
    private ChromeDriver driver;
    private string urlRoaming = "https://qa-course.kontur.host/roaming";
    
    // первый экран
    private static By titleLokator = By.Id("Company");
    private static By innLokator = By.Id("Inn");   
    private static By kppLokator = By.Id("Kpp");
    private static By nameLokator = By.Id("Name");
    private static By emailLokator =By.Id("Liame");
    private static By phoneLokator = By.Id("Phone");
    private static By button1Lokator = By.ClassName("js-roaming-next-step-button");

    public static void enterTrueAllFieldForm1(CounterAgents counterAgent, ChromeDriver drv)
    {
        drv.FindElement(titleLokator).SendKeys(counterAgent.title);
        drv.FindElement(innLokator).SendKeys(counterAgent.inn);
        drv.FindElement(kppLokator).SendKeys(counterAgent.kpp);
        drv.FindElement(nameLokator).SendKeys(counterAgent.fullName);
        drv.FindElement(emailLokator).SendKeys(counterAgent.email);
        drv.FindElement(phoneLokator).SendKeys(counterAgent.telephone);
    }


    // Второй экран
    
    private By scanTextLokator = By.Id("file-upload-text");
    private By skip2Step = By.ClassName("js-roaming-next-step-link");
    
    // Третий экран
    private By button3Lokator = By.ClassName("js-contragents-form-submit-btn");
    
    
    
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
        enterTrueAllFieldForm1(myFullFields, driver);
        

        // Кликнуть по кнопке следующий шаг (1 --> 2) 
        driver.FindElement(button1Lokator).Click();

        // Ожидать 10 секунд появления второго состояния
        WebDriverWait waitVisible2screen = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisible2screen.Until(e => e.FindElement(scanTextLokator));
        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(scanTextLokator).Displayed);
                //$"\tОжидалась успешная валидация и переход на второе состояние '");//{email3LevelDomain}'\n" + 
                //$"Фактически ошибка валидации емэйл: '{driver.FindElement(errorFormLokator).Text}'");
        });
    }
    
    [Test]
    // UserNameEmail=спецсимволы, ошибка email
    public void trueFullFirstForm_SpecialUserNameEmail_jumpStep2()
    {
        // Перейти по урлу
        driver.Navigate().GoToUrl(urlRoaming);

        // Ожидать 10 секунд появления кнопки(загрузка страницы окончена)
        WebDriverWait waitVisibleInput = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisibleInput.Until(e => e.FindElement(button1Lokator));
        
        // Ввести все поля
        enterTrueAllFieldForm1(myFullFields_specialUserNameEmail, driver);
        

        // Кликнуть по кнопке следующий шаг (1 --> 2) 
        driver.FindElement(button1Lokator).Click();

        // Ожидать 10 секунд появления ошибки
        WebDriverWait waitVisibleErrorEmail = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisibleErrorEmail.Until(e => e.FindElement(By.ClassName("field-validation-error")));
        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(By.ClassName("field-validation-error")).Text.Contains("Некорректный электронный адрес"),
            $"\tОжидалась ошибка валидации email: '{myFullFields_specialUserNameEmail.email}'\n" + 
            $"Фактическая ошибка: '{driver.FindElement(By.ClassName("field-validation-error")).Text}'");
        });
    }
    
    [Test]
    // Ввод полной валидной формы и переход на третий шаг
    public void truFullFirstForm_jumpStep2_jumpStep3()
    {
        // Перейти по урлу
        driver.Navigate().GoToUrl(urlRoaming);

        // Ожидать 10 секунд появления кнопки(загрузка 1 состояния окончена)
        WebDriverWait waitVisibleInput = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisibleInput.Until(e => e.FindElement(button1Lokator));
        
        // Ввести все поля
        enterTrueAllFieldForm1(myFullFields, driver);
        

        // Кликнуть по кнопке следующий шаг (1 --> 2) 
        driver.FindElement(button1Lokator).Click();
        
        // Ожидать 10 секунд появления ссылки(загрузка 2 состояния окончена)
        WebDriverWait waitVisible2Step = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisible2Step.Until(e => e.FindElement(skip2Step));        
        
        // Кликнуть пропустить, перейти на следующий шаг (2 --> 3) 
        driver.FindElement(skip2Step).Click();

        // Ожидать 10 секунд появления кнопки в третьем состоянии (загрузка 3 состояния окончена)
        WebDriverWait waitVisible3button = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        waitVisible3button.Until(e => e.FindElement(button3Lokator));
        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(button3Lokator).Displayed);
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
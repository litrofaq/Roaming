using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Roaming;

public class RoamingQA
{
    private CounterAgents myFullFields = new CounterAgents("ООО 'Сладкая сказка'", "1839001744", "165701001",
        1, "Иванов Сидор Петрович", "proverka_kazymov_005@test.ru","89051234567");
    
    private CounterAgents myFullFields_specialUserNameEmail = new CounterAgents("ООО 'Сладкая сказка'", "1839001744", "165701001",
        1, "Иванов Сидор Петрович", "ab(c)d,e:f;g<h>i[j]l@example.com","89051234567"); // err = Сервер временно недоступен

    private static Dictionary<int, string> roamingOperators = new Dictionary<int, string>
    {
        {5, "НТЦ СТЭК"},
        {6, "Компания Тензор (СБИС)"},
        {7, "НУЦ"},
        {8, "Эдисофт"},
        {9, "Такском (Файлер)"},
        {10, "ТаксНет (Транскрипт)"},
        {11, "Калуга-Астрал"},
        {12, "ФораПром (LeraData)"},
        {14, "УЦ ГИС"},
        {15, "НИИАС (РЖД)"},
        {16, "КРИПТЭКС (Signatura)"},
        {17, "СИСЛИНК"},
        {2, "Э-КОМ (EXIT)"},
        {4, "ДИРЕКТУМ (Synerdocs)"},
        {1, "КОРУС Консалтинг СНГ (СФЕРА)"},
        {3, "АРГОС"}
    };
    //private static CounterAgents[] _counterAgents = new CounterAgents[10];
    //_counterAgents[0]=
    private List<CounterAgents> _counterAgents = new List<CounterAgents>() //0(пустой) +16 операторов
    {
        new CounterAgents("ООО 'Привет'", "1839001744", "165701001", 1, "Иванов Сидор Петрович", "abc@example.com","89371234567"),
        new CounterAgents("Юг-Новый Век", "2320092269", "232001001", 2, "Иванова Вира Петровна","test1@test.ru","89051234567"),
        new CounterAgents("КРАЙМИА СИНЕРДЖИ 21 ВЕК", "7715446068", "775101001", 3, "Стеклов Фёдор Ильич","test2@test.ru","89061234567"),
        new CounterAgents("ОКТЯБРЬ", "6316164550", "631601001", 4, "Потапов Егор Викторович","test3@test.ru","89071234567"),
        new CounterAgents("ООО Компания Март", "2634072094", "262401001", 5, "Сахарова Софья Николаевна","test4@test.ru","89081234567"),
        new CounterAgents("ВАГОНМАШ", "7804453941", "781001001", 6, "Данилов Артём Петрович","test5@test.ru","89091234567"),
        new CounterAgents("С легким паром", "0901046025", "090101001", 7, "Иванов Илья Игоревич","test6@test.ru","89101234567"),
        new CounterAgents("ПАРОМ", "7733258099", "774301001", 8, "Сарцев Потап Васильевич","test7@test.ru","89111234567"),
        new CounterAgents("Пароходъ", "3917015966", "390601001", 9, "Озерова Олька Сергеевна","test8@test.ru","89121234567"),
        new CounterAgents("АЛЕКС ЭНЕРДЖИ", "5614086619", "561401001", 10, "Козлов Франк Игоревич","test9@test.ru","89921234567"),
        new CounterAgents("СВЕТ ЭНЕРГИЯ", "7707332500", "772601001", 11, "Петрованова Алёна Матвеевна","testA@test.ru","89631234567"),
        new CounterAgents("ПОТАП", "6678038450", "667801001", 12, "Покачук Анна Ивановна","testA@test.ru","89631222567"),
        new CounterAgents("ЛОГРУС", "7706416074", "770401001", 13, "Сколов Вагир Едросович","testB@test.ru","89631222567"),
        new CounterAgents("ФАРМСИНТЕЗ", "7801075160", "470301001", 14, "Зощук Александр Потапович","testC@test.ru","89631222567"),
        new CounterAgents("НИИК", "5249003464", "524901001", 15, "Сазонова Эльвира Евгеньевна","testD@test.ru","89631222567"),
        new CounterAgents("ИП Бакашев Ваха Элимбекович", "594807172278", "", 16, "Комаров Петр Ильич","testE@test.ru","89631222567"),
        new CounterAgents("ИП Ридош Елена Владимировна", "632122259306", "", 17, "Баглаева Ксения Витальевна","testF@test.ru","89631222567"),
    };
    


    private ChromeDriver driver;
    //private Select select = new Select();
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
    // btn btn-lg btn-lng btn-third js-contragents-form-submit-btn js-test-submit-btn
    
    
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
 
        // Переход на 3 состояние, ввод 4 контрагентов в список, пятого в форму и отправить
    [Test]
    public void jumpStep3_add4ValidKA_addKAinForm_submit()
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

        foreach (var KA in _counterAgents)
        {
            var countKA = 5; //кол-во отправляемых КА
            if (KA.label>countKA)
            {
                continue;
            }
            //Thread.Sleep(3000);
            driver.FindElement(By.Id("Form_Company")).Clear();
            driver.FindElement(By.Id("Form_Company")).SendKeys(KA.title);
            //Thread.Sleep(500);
            driver.FindElement(By.Id("Form_Inn")).Clear();
            driver.FindElement(By.Id("Form_Inn")).SendKeys(KA.inn);
            //Thread.Sleep(500);
            driver.FindElement(By.Id("Form_Kpp")).Clear();
            driver.FindElement(By.Id("Form_Kpp")).SendKeys(KA.kpp);
            //Thread.Sleep(500);
            //var selelectedObj = new SelectElement(
            driver.FindElement(By.Id("operator")).SendKeys("НТЦ СТЭК"+Keys.Enter);
            //Thread.Sleep(500);
            driver.FindElement(By.Id("Form_Name")).Clear();
            driver.FindElement(By.Id("Form_Name")).SendKeys(KA.fullName);
            //Thread.Sleep(500);
            driver.FindElement(By.Id("Form_Phone")).Clear();
            driver.FindElement(By.Id("Form_Phone")).SendKeys(KA.telephone);
            //Thread.Sleep(500);
            if (KA.label>(countKA-1))//если КА последний не добавляем в список
            {
                continue;
            }
            driver.FindElement(By.ClassName("js-test-submit-btn")).Click();
        }
        
        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(button3Lokator).Displayed);
            //$"\tОжидалась успешная валидация и переход на второе состояние '");//{email3LevelDomain}'\n" + 
            //$"Фактически ошибка валидации емэйл: '{driver.FindElement(errorFormLokator).Text}'");
        });
        driver.FindElement(button3Lokator).Click(); //отправить форму
    }  
    
    // Переход на 3 состояние, ввод 16 контрагентов с разными операторами
    [Test]
    public void jumpStep3_add16ValidKA()
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

        foreach (var KA in _counterAgents)
        {
            //Thread.Sleep(3000);
            driver.FindElement(By.Id("Form_Company")).Clear();
            driver.FindElement(By.Id("Form_Company")).SendKeys(KA.title);
            //Thread.Sleep(500);
            driver.FindElement(By.Id("Form_Inn")).Clear();
            driver.FindElement(By.Id("Form_Inn")).SendKeys(KA.inn);
            //Thread.Sleep(500);
            driver.FindElement(By.Id("Form_Kpp")).Clear();
            driver.FindElement(By.Id("Form_Kpp")).SendKeys(KA.kpp);
            //Thread.Sleep(500);
            //var selelectedObj = new SelectElement(
            driver.FindElement(By.Id("operator")).SendKeys("НТЦ СТЭК"+Keys.Enter);
            //Thread.Sleep(500);
            driver.FindElement(By.Id("Form_Name")).Clear();
            driver.FindElement(By.Id("Form_Name")).SendKeys(KA.fullName);
            //Thread.Sleep(500);
            driver.FindElement(By.Id("Form_Phone")).Clear();
            driver.FindElement(By.Id("Form_Phone")).SendKeys(KA.telephone);
            //Thread.Sleep(500);
            if (KA.label>4)
            {
                continue;
            }
            driver.FindElement(By.ClassName("js-test-submit-btn")).Click();
        }
        
        Assert.Multiple(() =>
        {
            Assert.IsTrue(driver.FindElement(button3Lokator).Displayed);
            //$"\tОжидалась успешная валидация и переход на второе состояние '");//{email3LevelDomain}'\n" + 
            //$"Фактически ошибка валидации емэйл: '{driver.FindElement(errorFormLokator).Text}'");
        });
        driver.FindElement(button3Lokator).Click(); //отправить форму
    }      
    
    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
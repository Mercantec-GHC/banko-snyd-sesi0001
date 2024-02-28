using System.Xml.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace BankoCheater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //sets driver equal to the chromedriver 
            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            //opens a chrome browser and goes to the Banko website
            driver.Navigate().GoToUrl("https://mags-template.github.io/Banko/");

            //identifies the username textbox  
            IWebElement textBox = wait.Until(drv => drv.FindElement(By.Id("tekstboks")));

            //all the usernames for the different plader 
            string[] names = { "seb1", "seb2", "seb3", "seb4", "seb5", "seb6", "seb7", "seb8", "seb9", "seb10" };

            //creates a dictionary that has the names and data as key and value pairs
            Dictionary<string, string[][]> nameToArrays = new Dictionary<string, string[][]>();


            foreach (var name in names)
            {
                //enters the username value and clears it before the next name
                textBox.Clear();
                textBox.SendKeys(name);

                //finds the "Generer plader" button and clicks it
                IWebElement button = wait.Until(drv => drv.FindElement(By.Id("knap")));
                button.Click();

                //creates 3 arrays that contains 3 emty arrays
                string[][] emptyArrays = new string[3][];


                for (int i = 0; i < emptyArrays.Length; i++)
                {
                    //creates an empty sub-array
                    emptyArrays[i] = new string[9];

                    //scrapes the plade data from the Banko website
                    emptyArrays[i][0] = driver.FindElement(By.Id($"p1{i + 1}1")).Text;
                    emptyArrays[i][1] = driver.FindElement(By.Id($"p1{i + 1}2")).Text;
                    emptyArrays[i][2] = driver.FindElement(By.Id($"p1{i + 1}3")).Text;
                    emptyArrays[i][3] = driver.FindElement(By.Id($"p1{i + 1}4")).Text;
                    emptyArrays[i][4] = driver.FindElement(By.Id($"p1{i + 1}5")).Text;
                    emptyArrays[i][5] = driver.FindElement(By.Id($"p1{i + 1}6")).Text;
                    emptyArrays[i][6] = driver.FindElement(By.Id($"p1{i + 1}7")).Text;
                    emptyArrays[i][7] = driver.FindElement(By.Id($"p1{i + 1}8")).Text;
                    emptyArrays[i][8] = driver.FindElement(By.Id($"p1{i + 1}9")).Text;

                    //prints the name, row and sub-array contents
                    Console.WriteLine($"Array: {name}, Row: {i + 1} [{string.Join(", ", emptyArrays[i])}]");
                }

                nameToArrays[name] = emptyArrays;

            }

            Console.WriteLine("Enter the banko number that that was picked:  ");
            string pickedNumber = Console.ReadLine();

            Console.WriteLine($"The picked number was: {pickedNumber}");





            Thread.Sleep(TimeSpan.FromSeconds(30));

            driver.Quit();
        }
    }
}

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

            //identifies the plade-id textbox  
            IWebElement textBox = wait.Until(drv => drv.FindElement(By.Id("tekstboks")));

            //all the plade-id's for the different plader 
            string[] names = { "seb1", "seb2", "seb3", "seb4", "seb5", "seb6", "seb7", "seb8", "seb9", "seb10" };

            //creates a dictionary that has the names and data as key and value pairs
            Dictionary<string, string[][]> nameToArrays = new Dictionary<string, string[][]>();

            foreach (var name in names)
            {
                //enters a plade-id and clears it before the next plade-id
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

            //initializes a dictionary to keep track of counts
            Dictionary<string, Dictionary<int, int>> counts = new Dictionary<string, Dictionary<int, int>>();

            //array to keep track of names that have gotten a bingo with one row
            List<string> namesWithBingo = new List<string>();

            //array to keep track of names that have gotten a bingo with two rows
            List<string> namesWith2Bingo = new List<string>();

            while (true)
            {
                Console.WriteLine("Enter the banko number that was picked (or 'quit' to exit): ");
                string pickedNumber = Console.ReadLine();

                //checks if the user wants to quit
                if (pickedNumber.ToLower() == "quit")
                    break;

                Console.WriteLine($"The picked number was: {pickedNumber}");

                //iterates over each name and their arrays
                foreach (var kvp in nameToArrays)
                {
                    string name = kvp.Key;
                    string[][] arrays = kvp.Value;

                    for (int i = 0; i < arrays.Length; i++)
                    {
                        //checks if the picked number exists in the current row
                        if (arrays[i].Contains(pickedNumber))
                        {
                            Console.WriteLine($"{pickedNumber} was found in Array: {name}, Row: {i + 1}");

                            //checks if the counts dictionary contains the array name and initializes one if it doesn't
                            if (!counts.ContainsKey(name))
                            {
                                counts[name] = new Dictionary<int, int>();
                            }

                            //checks if the dictionary contains the row number and initializes the count for that row to 0 by adding an entry with the row number as the key.
                            if (!counts[name].ContainsKey(i))
                            {
                                counts[name][i] = 0;
                            }

                            counts[name][i]++;
                            
                            //shows bingo banko and then gets rid of the row
                            if (counts[name][i] == 5 && namesWith2Bingo.Contains(name))
                            {
                                Console.WriteLine($"Bingo Banko!!! all the rows at Array: {name}, has been filed");
                                counts[name][i] = -1;
                                break;
                            }

                            //shows bingo with one row then gets rid of the row and adds the name to a list
                            if (counts[name][i] == 5 && !namesWithBingo.Contains(name))
                            {
                                Console.WriteLine($"Bingo på Array: {name}, Row: {i + 1}");
                                counts[name][i] = -1;
                                namesWithBingo.Add(name);
                                break;
                            }

                            //shows bingo with two rows then gets rid of the row and adds the name to a second list
                            if (counts[name][i] == 5 && namesWithBingo.Contains(name))
                            {
                                Console.WriteLine($"Bingo with two rows at Array: {name}, the second Row: {i + 1}");
                                counts[name][i] = -1;
                                namesWith2Bingo.Add(name);
                                break;
                            } 
                        }
                    }
                }
            }
        //quits the chrome browser 
        driver.Quit();
        }
    }
}

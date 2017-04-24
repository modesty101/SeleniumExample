using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace HelloSite
{
    class Program
    {
        static void Main(string[] args)
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArgument(@"user-data-dir=C:\Users\genius\AppData\Local\Google\Chrome\User Data\Default");

            using(ChromeDriver WebDriver = new ChromeDriver(@".\",option))
            {
                WebDriver.Navigate().GoToUrl("http://www.nbamania.com");
                WEBROWSER_LOAD_COMPLETE(WebDriver);
                Console.WriteLine(WebDriver.Title);
                Console.WriteLine(WebDriver.Url);

                //Thread.Sleep(1500);
                List<IWebElement> memu_new_member_list = new List<IWebElement>();
                //List<IWebElement> memu_new_list = new List<IWebElement>();
                List<IWebElement> push_num_list = new List<IWebElement>();

                

                // menu_new_member (로그인)
                //memu_new_member_list.AddRange(WebDriver.FindElementsByXPath("//div[@class='menu_new_member']").ToList());
                //memu_new_member_list.AddRange(WebDriver.FindElementsByXPath("//div[@class='submenu_new_total_member']").ToList());
   
                /*
                // 로그인 상태일 경우
                foreach (IWebElement memu_new_member in memu_new_member_list)
                {
                    if (!string.IsNullOrWhiteSpace(memu_new_member.Text.Trim()))
                    {
                        Console.WriteLine(memu_new_member.GetAttribute("oldtitle") + " : " + memu_new_member.Text.Trim());
                    }
                } */

                Console.WriteLine(" ");             

                //WebDriver.GetScreenshot().SaveAsFile(@".\1.png", System.Drawing.Imaging.ImageFormat.Png);

                while (true)
                {
                    Console.Clear();
                    memu_new_member_list.Clear();
                    push_num_list.Clear();

                    // menu_new_member (로그인)
                    memu_new_member_list.AddRange(WebDriver.FindElementsByXPath("//div[@class='menu_new_member']").ToList());
                    memu_new_member_list.AddRange(WebDriver.FindElementsByXPath("//div[@class='submenu_new_total_member']").ToList());

                    /* menu_new (비로그인)
                    memu_new_list.AddRange(WebDriver.FindElementsByXPath("//div[@class='menu_new']").ToList());
                    memu_new_list.AddRange(WebDriver.FindElementsByXPath("//div[@class='submenu_new_total']").ToList()); */

                    // push_num (실시간 알림)
                    push_num_list.AddRange(WebDriver.FindElementsByXPath("//div[@class='push_num box_shadow_3 eng help']").ToList());
                    push_num_list.AddRange(WebDriver.FindElementsByXPath("//*[@id='push_num_xpert']/span").ToList());

                    //push_num_list.AddRange(WebDriver.FindElementsByXPath("//div[@id='push_num_comment']").ToList());

                    // 로그인 상태
                    foreach (IWebElement memu_new in memu_new_member_list)
                    {
                        if (!string.IsNullOrWhiteSpace(memu_new.Text.Trim()))
                        {
                            Console.WriteLine(memu_new.GetAttribute("oldtitle") + " : " + memu_new.Text.Trim());
                        }
                    }

                    Console.WriteLine("");

                    // 실시간 알림
                    foreach (IWebElement push_num in push_num_list)
                    {
                        if (!string.IsNullOrWhiteSpace(push_num.Text.Trim()))
                        {
                            Console.WriteLine(push_num.GetAttribute("oldtitle") + " : " + push_num.Text.Trim());
                        }
                        
                        //else
                        //{
                        //    Console.WriteLine(push_num.GetAttribute("oldtitle") + " : " + push_num.Text.Trim());
                        //}
                    }

                    Thread.Sleep(5000);
                    WebDriver.Navigate().Refresh();
                    WEBROWSER_LOAD_COMPLETE(WebDriver);
                }
            }
        }

        static bool WEBROWSER_LOAD_COMPLETE(RemoteWebDriver webdriver, int timeout = -1)
        {
            try
            {
                DateTime begin = DateTime.Now;
                while (true)
                {
                    object result = webdriver.ExecuteScript("return document.readyState", "");
                    //object found = webdriver.FindElementByXPath("//section[@class='sc sp_noresult']");
                    if (result != null)
                    {
                        TimeSpan timespan = (DateTime.Now - begin);
                        Console.WriteLine(webdriver.Url + "Load : " + (DateTime.Now - begin).TotalMilliseconds);

                        if (result.ToString().ToUpper().Equals("COMPLETE"))
                        {
                            Console.WriteLine("COMPLETE");
                            /*
                            if(timespan.TotalMilliseconds > 20)
                            {
                                DateTime now = DateTime.Now;
                                string IMAGE_PATH = @".\" + now.ToString("yyyyMMdd") + @"\" + now.ToString("yyyyMMddHHmmss") + ".png";
                                new System.IO.FileInfo(IMAGE_PATH).Directory.Create();
                                webdriver.GetScreenshot().SaveAsFile(IMAGE_PATH, System.Drawing.Imaging.ImageFormat.Png);
                            }*/
                            /*
                            if()
                            {
                                DateTime now = DateTime.Now;
                                string IMAGE_PATH = @".\" + now.ToString("yyyyMMdd") + @"\" + now.ToString("yyyyMMddHHmmss") + ".png";
                                new System.IO.FileInfo(IMAGE_PATH).Directory.Create();
                                webdriver.GetScreenshot().SaveAsFile(IMAGE_PATH, System.Drawing.Imaging.ImageFormat.Png);
                            }
                            */
                            return true;
                        }
                        else
                        {
                            if (timeout >= 0 && (DateTime.Now - begin).TotalMilliseconds > timeout)
                            {
                                return false;
                            }
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
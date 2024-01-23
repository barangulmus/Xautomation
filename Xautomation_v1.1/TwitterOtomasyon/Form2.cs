using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace TwitterOtomasyon
{
    public partial class Form2 : Form
    {
        private Form1 form1Instance;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            form1Instance = form1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool r1 = radioButton1.Checked;
            bool r2 = radioButton2.Checked;

            bool r3 = radioButton3.Checked;
            bool r4 = radioButton4.Checked;
            bool r5 = radioButton5.Checked;
            bool r6 = radioButton6.Checked;
            if ((r3 == true || r4 == true|| r5 == true || r6==true) && (r1 == true || r2 == true))
            {
                DialogResult msg= MessageBox.Show("Bu işlem biraz zaman alabilir, devam etmek istiyor musunuz? \n Ayrıca Programın düzgün çalışabilmesi için program çalışırken mouse ve klavye kullanmayınız!", "Uyarı", MessageBoxButtons.YesNo);
                if (msg == DialogResult.Yes)
                {
                    IWebDriver driver = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                    driver.Navigate().GoToUrl("https://twitter.com/i/flow/login");
                    System.Threading.Thread.Sleep(7000);
                    IWebElement username = driver.FindElement(By.CssSelector("input[name='text']"));
                    username.Click();
                    System.Threading.Thread.Sleep(3000);
                    username.SendKeys(form1Instance.loginInfo.username);
                    System.Threading.Thread.Sleep(100);
                    SendKeys.Send("{ENTER}");
                    System.Threading.Thread.Sleep(2000);
                    Clipboard.SetText(form1Instance.loginInfo.password);
                    SendKeys.Send("^v");
                    System.Threading.Thread.Sleep(100);
                    SendKeys.Send("{ENTER}");
                    System.Threading.Thread.Sleep(7000);
                    driver.Navigate().GoToUrl("https://twitter.com/" + form1Instance.loginInfo.username);
                    System.Threading.Thread.Sleep(10000);
                    driver.Navigate().Refresh();
                    System.Threading.Thread.Sleep(10000);
                    if (r3 == true)
                    {
                        if (r1 == true)
                        {
                            for (int i = 1; i < 10; i++)
                            {
                                By reposthere = By.XPath($"((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv'])[{i}]//a[@style='text-overflow: unset; color: rgb(113, 118, 123);' and starts-with(@id, 'id__')])");
                                if (driver.FindElements(reposthere).Count>0)
                                {
                                    IWebElement repostdel = driver.FindElement(By.XPath($"((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv'])[{i}]//div[@role='button'])[3]"));
                                    repostdel.Click();
                                    System.Threading.Thread.Sleep(1000);
                                    IWebElement yestelmyrepost = driver.FindElement(By.XPath("(//div[@role='menuitem'])"));
                                    yestelmyrepost.Click();
                                    Thread.Sleep(3000);
                                }
                                else
                                {
                                    IWebElement rightbtn = driver.FindElement(By.XPath($"((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv'])[{i}]//div[@role='button'])[1]"));
                                    rightbtn.Click();
                                    System.Threading.Thread.Sleep(2000);
                                    IWebElement delbtn = driver.FindElement(By.XPath("//div[@role='menuitem'][1]"));
                                    delbtn.Click();
                                    System.Threading.Thread.Sleep(500);
                                    IWebElement delete = driver.FindElement(By.XPath("//div[@role='button' and @data-testid='confirmationSheetConfirm']"));
                                    delete.Click();
                                    System.Threading.Thread.Sleep(2000);
                                }
                                if (i % 3 == 0)
                                {
                                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                                    // Sayfanın yüksekliğini al
                                    long pageHeight = (long)js.ExecuteScript("return Math.max( document.body.scrollHeight, document.body.offsetHeight, document.documentElement.clientHeight, document.documentElement.scrollHeight, document.documentElement.offsetHeight);");

                                    // Yüzde 70 kadar aşağı kaydır
                                    long scrollTo = (long)(pageHeight * 0.6);
                                    js.ExecuteScript($"window.scrollTo(0, {scrollTo})");

                                    System.Threading.Thread.Sleep(5000);
                                }

                            }
                        }
                    }   
                }
            }
            else
            {
                MessageBox.Show("Lütfen boş alan bırakmadığınızdan emin olunuz");
            }
        }
    }
}

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
using System.Text.RegularExpressions;
using System.Xml.Schema;

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
        ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////
        int elsex = 0;
        string numericPart, followNumericPart;
        string PersonName;
        string PersonText;
        ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            bool r1 = radioButton1.Checked;
            bool r2 = radioButton2.Checked;

            bool r3 = radioButton3.Checked;
            bool r4 = radioButton4.Checked;
            if ((r3 == true || r4 == true) && (r1 == true || r2 == true))
            {
                DialogResult msg = MessageBox.Show("Bu işlem biraz zaman alabilir, devam etmek istiyor musunuz? \n Ayrıca Programın düzgün çalışabilmesi için program çalışırken mouse ve klavye kullanmayınız!", "Uyarı", MessageBoxButtons.YesNo);
                if (msg == DialogResult.Yes)
                {
                    IWebDriver driver = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                    driver.Navigate().GoToUrl("https://twitter.com/i/flow/login");
                    System.Threading.Thread.Sleep(7000);
                    IWebElement username = driver.FindElement(By.CssSelector("input[name='text']"));
                    username.Click();
                    System.Threading.Thread.Sleep(3000);
                    string us = form1Instance.loginInfo.username;
                    username.SendKeys(us);
                    System.Threading.Thread.Sleep(100);
                    SendKeys.Send("{ENTER}");
                    System.Threading.Thread.Sleep(2000);
                    string pas = form1Instance.loginInfo.password;
                    Clipboard.SetText(pas);
                    SendKeys.Send("^v");
                    System.Threading.Thread.Sleep(100);
                    SendKeys.Send("{ENTER}");
                    Clipboard.SetText(" ");
                    System.Threading.Thread.Sleep(7000);
                    if (r4) { driver.Navigate().GoToUrl("https://twitter.com/" + us + "/likes"); }
                    if (r3) { driver.Navigate().GoToUrl("https://twitter.com/" + us + "/with_replies"); }
                    System.Threading.Thread.Sleep(7000);
                    driver.Navigate().Refresh();
                    System.Threading.Thread.Sleep(7000);
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("window.scrollBy(0,350);");
                    System.Threading.Thread.Sleep(600);
                    if (r3)
                    {
                        if (r1)
                        {
                            string xpath = "(//div[contains(@class, 'r-3s2u2q r-bcqeeo r-qvutc0 r-37j5jr r-n6v787')])[1]";
                            string postText = driver.FindElement(By.XPath(xpath)).Text;
                            numericPart = new string(postText.Where(char.IsDigit).ToArray());
                        }
                        if (r2)
                        {
                            numericPart = textBox1.Text;
                        }
                        if (int.TryParse(numericPart, out int postCount))
                        {
                            for (int i = 0; i < postCount; i++)
                            {
                                try
                                {
                                    By post = By.XPath("(//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div/article]//div[@class = 'css-175oi2r r-1igl3o0 r-qklmqi r-1adg3ll r-1ny4l3l'])[1]");
                                    if (driver.FindElements(post).Count > 0)
                                    {
                                        IWebElement postElement = driver.FindElement(post);
                                        // post elementinin altındaki p elementini bulma
                                        By rElement = By.XPath($"(//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div/article]//div[@class = 'css-175oi2r r-1igl3o0 r-qklmqi r-1adg3ll r-1ny4l3l'])[1]//article//a[@href='/{us}' and @dir]");
                                        if (driver.FindElements(rElement).Count > 0)
                                        {//retwet var ise
                                            IWebElement unrepost = driver.FindElement(By.XPath($"(//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div//article//a[@href='/{us}' and @dir]])[1]//div[@data-testid='unretweet']"));
                                            unrepost.Click();
                                            System.Threading.Thread.Sleep(1000);
                                            IWebElement yesdelmyrepost = driver.FindElement(By.XPath("(//div[@role='menuitem'])"));
                                            yesdelmyrepost.Click();
                                            System.Threading.Thread.Sleep(1000);
                                        }
                                        else
                                        {//retweet yok, post var ise
                                            By pElement = By.XPath("(//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div/article]//div[@class = 'css-175oi2r r-1igl3o0 r-qklmqi r-1adg3ll r-1ny4l3l'])[1]//article//div[@aria-haspopup and @data-testid='caret']");
                                            if (driver.FindElements(pElement).Count > 0)
                                            {
                                                IWebElement delpostElement = driver.FindElement(pElement);
                                                System.Threading.Thread.Sleep(400);
                                                delpostElement.Click();
                                                System.Threading.Thread.Sleep(1000);
                                                IWebElement delbtn = driver.FindElement(By.XPath("//div[@role='menuitem'][1]//span"));
                                                if (delbtn.Text.Length < 10)
                                                {
                                                    delbtn.Click();
                                                    System.Threading.Thread.Sleep(500);
                                                    IWebElement delete = driver.FindElement(By.XPath("//div[@role='button' and @data-testid='confirmationSheetConfirm']"));
                                                    delete.Click();
                                                    System.Threading.Thread.Sleep(1000);
                                                }
                                                else
                                                {
                                                    driver.Navigate().Refresh();
                                                    System.Threading.Thread.Sleep(7000);
                                                }
                                            }
                                            else
                                            {
                                                driver.Navigate().Refresh();
                                                System.Threading.Thread.Sleep(7000);
                                            }
                                        }
                                        if (i == 5) { js.ExecuteScript("window.scrollBy(0,600);"); }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Bir hata ile karşılaşıldı" + ex);
                                    throw;
                                }
                                finally
                                {
                                    System.Threading.Thread.Sleep(500);
                                }

                                if (i == postCount - 1) // Döngü sona erdiğinde
                                {
                                    MessageBox.Show("İşlem Tamamlandı");
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Sayıya dönüştürme başarısız oldu.");
                        }
                    }
                    if (r4)
                    {
                        if (r1)
                        {
                            string xpath = "(//div[contains(@class, 'r-3s2u2q r-bcqeeo r-qvutc0 r-37j5jr r-n6v787')])[1]";
                            string postText = driver.FindElement(By.XPath(xpath)).Text;
                            numericPart = new string(postText.Where(char.IsDigit).ToArray());
                        }
                        if (r2)
                        {
                            numericPart = textBox1.Text;
                        }
                        if (int.TryParse(numericPart, out int postCount))
                        {
                            for (int j = 0; j < postCount; j++)
                            {
                                try
                                {
                                    By cellInnerDiv = By.XPath("(//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div/article]/div//div[@data-testid='unlike']/ancestor::div[@data-testid='cellInnerDiv'])[1]");
                                    long height = (long)js.ExecuteScript("return arguments[0].offsetHeight;", driver.FindElement(cellInnerDiv));
                                    System.Threading.Thread.Sleep(500);
                                    IWebElement Unlike = driver.FindElement(By.XPath("(//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div/article]/div//div[@data-testid='unlike'])[1]"));
                                    Unlike.Click();
                                    System.Threading.Thread.Sleep(800);
                                    js.ExecuteScript($"window.scrollBy(0, {height});");
                                }
                                catch (Exception)
                                {
                                    driver.Navigate().Refresh();
                                    j = 0;
                                    System.Threading.Thread.Sleep(6000);
                                    throw;
                                }
                                finally
                                {
                                    System.Threading.Thread.Sleep(500);
                                    if (j == postCount - 1)
                                    {
                                        MessageBox.Show("İşlem Tamamlandı");
                                    }
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
        string textboxLanguage, sendTextLanguage;

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://twitter.com/i/flow/login");
            System.Threading.Thread.Sleep(7000);
            IWebElement username = driver.FindElement(By.CssSelector("input[name='text']"));
            username.Click();
            System.Threading.Thread.Sleep(3000);
            string us = form1Instance.loginInfo.username;
            username.SendKeys(us);
            System.Threading.Thread.Sleep(100);
            SendKeys.Send("{ENTER}");
            System.Threading.Thread.Sleep(2000);
            string pas = form1Instance.loginInfo.password;
            Clipboard.SetText(pas);
            SendKeys.Send("^v");
            System.Threading.Thread.Sleep(100);
            SendKeys.Send("{ENTER}");
            Clipboard.SetText(" ");
            System.Threading.Thread.Sleep(7000);
            driver.Navigate().GoToUrl("https://twitter.com/i/trends");
            System.Threading.Thread.Sleep(7000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,800);");
            System.Threading.Thread.Sleep(1500);
            js.ExecuteScript("window.scrollBy(0,-800);");
            System.Threading.Thread.Sleep(1500);
            List<string> trendsList = new List<string>();
            for (int i = 1; i <= 20; i++)
            {
                string trends = $"((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='trend']/div/div[2]//span[@class='css-1qaijid r-bcqeeo r-qvutc0 r-poiln3'])[{i}]";
                string trendsText = driver.FindElement(By.XPath(trends)).Text;
                Clipboard.SetText(trendsText);
                trendsList.Add(trendsText);
                System.Threading.Thread.Sleep(300);
                if (i == 20)
                {
                    driver.Quit();
                }
            }
            listBox1.DataSource = trendsList;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                string itemsText = string.Join(" ", listBox1.Items.Cast<string>());
                Clipboard.SetText(itemsText);
                MessageBox.Show("Öğeler panoya kopyalandı!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kopyalanacak öğe yok!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.Items.Clear();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            radioButton9.Enabled = false;
            radioButton9.Checked = false;
            radioButton10.Checked = false;
            radioButton10.Enabled = false;
            radioButton13.Enabled = true;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            radioButton10.Enabled = true;
            radioButton9.Enabled = true;
            radioButton13.Enabled = false;
            radioButton13.Checked = false;
        }
        List<string> followList = new List<string>();
        private void button6_Click(object sender, EventArgs e)
        {
            bool r5 = radioButton5.Checked;
            bool r6 = radioButton6.Checked;
            bool r7 = radioButton7.Checked;
            bool r8 = radioButton8.Checked;
            bool r9 = radioButton9.Checked;
            bool r10 = radioButton10.Checked;
            bool r11 = radioButton11.Checked;
            bool r12 = radioButton12.Checked;
            bool r13 = radioButton13.Checked;

            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://twitter.com/i/flow/login");
            System.Threading.Thread.Sleep(7000);
            IWebElement username = driver.FindElement(By.CssSelector("input[name='text']"));
            username.Click();
            System.Threading.Thread.Sleep(3000);
            string us = form1Instance.loginInfo.username;
            username.SendKeys(us);
            System.Threading.Thread.Sleep(100);
            SendKeys.Send("{ENTER}");
            System.Threading.Thread.Sleep(2000);
            string pas = form1Instance.loginInfo.password;
            Clipboard.SetText(pas);
            SendKeys.Send("^v");
            System.Threading.Thread.Sleep(100);
            SendKeys.Send("{ENTER}");
            Clipboard.SetText(" ");
            System.Threading.Thread.Sleep(7000);
            if (r11)
            {
                driver.Navigate().GoToUrl("https://twitter.com/" + us);
                System.Threading.Thread.Sleep(7000);
                if (r5)
                {
                    string followCount = "((//div[@data-testid='UserName']/following::div/following::div/following::div)[1]/div[2]/a/span/span)[1]";
                    string followCountText = driver.FindElement(By.XPath(followCount)).Text;
                    followNumericPart = new string(followCountText.Where(char.IsDigit).ToArray());
                }
                if (r6)
                {
                    string followCount = "((//div[@data-testid='UserName']/following::div/following::div/following::div)[1]/div[1]/a/span/span)[1]";
                    string followCountText = driver.FindElement(By.XPath(followCount)).Text;
                    followNumericPart = new string(followCountText.Where(char.IsDigit).ToArray());
                }
            }
            if (r12)
            {
                followNumericPart = textBox2.Text;
            }
            if (r5) { driver.Navigate().GoToUrl("https://twitter.com/" + us + "/followers"); }
            if (r6) { driver.Navigate().GoToUrl("https://twitter.com/" + us + "/following"); }
            System.Threading.Thread.Sleep(7000);
            ///////////////////////////////////////////////////////////////////////////////////////////
            if (r5)
            {
                if (int.TryParse(followNumericPart, out int followCount))
                {
                    IWebElement firstPersonText = driver.FindElement(By.XPath("((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a)[1]"));
                    PersonText = firstPersonText.Text.Substring(1);
                    if (!r13) { followList.Add(PersonText); }
                    if (r13)
                    {
                        By thefollowBYY = By.XPath($"(//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a/parent::div/parent::div/parent::div/parent::div/following::div/div/div[@role='button' and contains(@aria-label, '{PersonText}') and not(contains(@data-testid, 'un'))]");
                        if (driver.FindElements(thefollowBYY).Count > 0)
                        {
                            System.Threading.Thread.Sleep(500);
                            IWebElement theFollow = driver.FindElement(By.XPath($"(//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a/parent::div/parent::div/parent::div/parent::div/following::div/div/div[@role='button' and contains(@aria-label, '{PersonText}') and not(contains(@data-testid, 'un'))]"));
                            theFollow.Click();
                            followList.Add(PersonText);
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                    for (int i = 1; i <= followCount - 1; i++)
                    {
                        try
                        {
                            string XXXPERSONTEXT = PersonText;
                            IWebElement XPerson = driver.FindElement(By.XPath($"(((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a[@href='/{PersonText}'])[1]/ancestor::div[@data-testid='cellInnerDiv']/following-sibling::div//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a)[1]"));
                            PersonText = XPerson.Text.Substring(1);
                            if (!r13) { followList.Add(PersonText); }
                            if (r13)
                            {
                                By theFollowBy = By.XPath($"(((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a[@href='/{XXXPERSONTEXT}'])[1]/ancestor::div[@data-testid='cellInnerDiv']/following-sibling::div//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a/parent::div/parent::div/parent::div/parent::div/following::div/div/div[@role='button' and contains(@aria-label, '{PersonText}') and not(contains(@data-testid, 'un'))])[1]");
                                if (driver.FindElements(theFollowBy).Count > 0)
                                {
                                    System.Threading.Thread.Sleep(500);
                                    IWebElement theFollow = driver.FindElement(By.XPath($"(((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a[@href='/{XXXPERSONTEXT}'])[1]/ancestor::div[@data-testid='cellInnerDiv']/following-sibling::div//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a/parent::div/parent::div/parent::div/parent::div/following::div/div/div[@role='button' and contains(@aria-label, '{PersonText}') and not(contains(@data-testid, 'un'))])[1]"));
                                    theFollow.Click();
                                    followList.Add(PersonText);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            listBox2.DataSource = followList;
                            MessageBox.Show("Bir Hata İle Karşılaşıldı, \n" + ex);
                            throw;
                        }
                        finally
                        {
                            System.Threading.Thread.Sleep(700);
                            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                            By boy = By.XPath($"((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a[@href='/{PersonText}'])[1]/ancestor::div[@data-testid='cellInnerDiv']");
                            long height = (long)js.ExecuteScript("return arguments[0].offsetHeight;", driver.FindElement(boy));
                            js.ExecuteScript($"window.scrollBy(0,{height});");
                        }

                    }
                    listBox2.DataSource = followList;
                }
            }
            if (r6)
            {
                if (int.TryParse(followNumericPart, out int followCount))
                {
                    //ilk kişinin kullanıcı adını bul
                    IWebElement firstPersonText = driver.FindElement(By.XPath("((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a)[1]"));
                    PersonText = firstPersonText.Text.Substring(1);
                    if (!r10 && !r9) { followList.Add(PersonText); }
                    if (r10)
                    {
                        By thefollowBYY = By.XPath($"((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a/parent::div/parent::div/parent::div/parent::div/following::div/div/div)[1]");
                        if (driver.FindElements(thefollowBYY).Count > 0)
                        {
                            System.Threading.Thread.Sleep(500);
                            IWebElement theFollow = driver.FindElement(By.XPath($"(//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a/parent::div/parent::div/parent::div/parent::div/following::div/div[@role='button' and contains(@aria-label, '{PersonText}')]"));
                            theFollow.Click();
                            System.Threading.Thread.Sleep(1000);
                            IWebElement okUnFollow = driver.FindElement(By.XPath("//div[@data-testid='confirmationSheetConfirm']/div"));
                            okUnFollow.Click();
                            followList.Add(PersonText);
                        }
                    }
                    if (r9)
                    {   
                        By thefollowBYY = By.XPath($"((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a[@href='/{PersonText}'])/parent::div/following-sibling::div");
                        if (driver.FindElements(thefollowBYY).Count > 0)
                        {
                            System.Threading.Thread.Sleep(500);
                            IWebElement theFollow = driver.FindElement(By.XPath($"(//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a/parent::div/parent::div/parent::div/parent::div/following::div/div[@role='button' and contains(@aria-label, '{PersonText}')]"));
                            theFollow.Click();
                            System.Threading.Thread.Sleep(1000);
                            IWebElement okUnFollow = driver.FindElement(By.XPath("//div[@data-testid='confirmationSheetConfirm']/div"));
                            okUnFollow.Click();
                            followList.Add(PersonText);
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                    for (int i = 1; i <= followCount - 1; i++)
                    {
                        try
                        {
                            string XXXPERSONTEXT = PersonText;
                            IWebElement XPerson = driver.FindElement(By.XPath($"(((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a[@href='/{PersonText}'])[1]/ancestor::div[@data-testid='cellInnerDiv']/following-sibling::div//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a)[1]"));
                            PersonText = XPerson.Text.Substring(1);
                            if (!r10) { followList.Add(PersonText); }
                            if (!r9) { followList.Add(PersonText); }
                            if (r10)
                            {
                                By theFollowBy = By.XPath($"(//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a/parent::div/parent::div/parent::div/parent::div/following::div/div[@role='button' and contains(@aria-label, '{PersonText}')]");
                                if (driver.FindElements(theFollowBy).Count > 0)
                                {
                                    System.Threading.Thread.Sleep(500);
                                    IWebElement theFollow = driver.FindElement(By.XPath($"(((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a[@href='/{XXXPERSONTEXT}'])[1]/ancestor::div[@data-testid='cellInnerDiv']/following-sibling::div//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a/parent::div/parent::div/parent::div/parent::div/following::div/div[@role='button' and contains(@aria-label, '{PersonText}')])[1]"));
                                    theFollow.Click();
                                    System.Threading.Thread.Sleep(1000);
                                    IWebElement okUnFollow = driver.FindElement(By.XPath("//div[@data-testid='confirmationSheetConfirm']/div"));
                                    okUnFollow.Click();
                                    followList.Add(PersonText);
                                }
                            }
                            if (r9)
                            {
                                By thefollowBYY = By.XPath($"((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a[@href='/{PersonText}'])/parent::div/following-sibling::div");
                                if (driver.FindElements(thefollowBYY).Count < 0)
                                {
                                    System.Threading.Thread.Sleep(500);
                                    IWebElement theFollow = driver.FindElement(By.XPath($"(//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a/parent::div/parent::div/parent::div/parent::div/following::div/div[@role='button' and contains(@aria-label, '{PersonText}')]"));
                                    theFollow.Click();
                                    System.Threading.Thread.Sleep(1000);
                                    IWebElement okUnFollow = driver.FindElement(By.XPath("//div[@data-testid='confirmationSheetConfirm']/div"));
                                    okUnFollow.Click();
                                    followList.Add(PersonText);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            listBox2.DataSource = followList;
                            MessageBox.Show("Bir Hata İle Karşılaşıldı, Bulunan hesap sayısı" + i + " " + PersonText + "\n" + ex);
                            throw;
                        }
                        finally
                        {
                            System.Threading.Thread.Sleep(700);
                            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                            By boy = By.XPath($"((//div[contains(@style, 'position: relative;')]//div[@data-testid='cellInnerDiv' and descendant::div/div])//div[@data-testid='UserCell']/div/div[2]/div/div/div/div[2]//a[@href='/{PersonText}'])[1]/ancestor::div[@data-testid='cellInnerDiv']");
                            long height = (long)js.ExecuteScript("return arguments[0].offsetHeight;", driver.FindElement(boy));
                            js.ExecuteScript($"window.scrollBy(0,{height});");
                        }

                    }
                    listBox2.DataSource = followList;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton8.Checked == true)
            {
                textboxLanguage = "What is happening?!";
                sendTextLanguage = "Tweet";
            }
            if (radioButton7.Checked == true)
            {
                textboxLanguage = "Neler oluyor?";
                sendTextLanguage = "Gönder";
            }
            int how_many_twits = Convert.ToInt32(textBox3.Text);
            string tweetText = richTextBox1.Text;
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://twitter.com/i/flow/login");
            System.Threading.Thread.Sleep(7000);
            IWebElement username = driver.FindElement(By.CssSelector("input[name='text']"));
            username.Click();
            System.Threading.Thread.Sleep(3000);
            string us = form1Instance.loginInfo.username;
            username.SendKeys(us);
            System.Threading.Thread.Sleep(100);
            SendKeys.Send("{ENTER}");
            System.Threading.Thread.Sleep(2000);
            string pas = form1Instance.loginInfo.password;
            Clipboard.SetText(pas);
            SendKeys.Send("^v");
            System.Threading.Thread.Sleep(100);
            SendKeys.Send("{ENTER}");
            System.Threading.Thread.Sleep(10000);
            IWebElement tweetClickLocator = driver.FindElement(By.XPath($"//span[contains(text(), '{textboxLanguage}')]"));
            By tweetButtonClickLocator = By.XPath($"//div[@dir='ltr']/span/span[contains(text(), '{sendTextLanguage}')]");
            for (int i = 1; i <= how_many_twits; i++)
            {
                tweetClickLocator.Click();
                System.Threading.Thread.Sleep(500);
                SendKeys.Send(tweetText + " " + i);
                System.Threading.Thread.Sleep(500);
                IWebElement tweetClick = driver.FindElement(tweetButtonClickLocator);
                tweetClick.Click();
                System.Threading.Thread.Sleep(3000);
            }
        }
    }
}

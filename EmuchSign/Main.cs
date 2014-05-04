using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Configuration;
using System.Threading;
using _EmuchSign.cs;
using System.Text.RegularExpressions;

namespace _EmuchSign
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            string path = Application.StartupPath;

        }
        ConHttp ch = new ConHttp();
        RegexMatch rm = new RegexMatch();
        HandleMsg hm = new HandleMsg();
        FileControl fc = new FileControl();
        DeUser du=new DeUser();
        CookieCollection ccl = new CookieCollection();

        string path = ConfigurationManager.AppSettings["path"].ToString();

        private void btnSign_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(() => { ExDo(); });
            t1.IsBackground = true;
            t1.Start();
        }
        public void ExDo()
        {
            try
            {
                ExStep();
            }
            catch (Exception err)
            {
                Invoke(new Action(() =>
                {
                    hm.AppendMsg(rtxtMsg, Color.Red,"错误:"+err.ToString(), true);
                }));
            }

        }
        #region 已经领取过的话提示信息，随机
        public string ShowMsg()
        {
            string[] str1 = new string[] { "怎么", "还", "诶,", "^_^", "真", "咳咳，" };
            string[] str2 = new string[] { "这么", "这样" };
            string[] str3 = new string[] { "贪心的", "贪的" };
            string[] str4 = new string[] { "小孩，" };
            string[] str5 = new string[] { "刚刚不是领了红包了么?", "刚刚才领取过的吧～", "不是才领过红包的么?", "今天领过红包的?", "一天只能领取一次红包额～～" };
            Random r = new Random();
            int str1Choice = r.Next(0, str1.Length);
            int str2Choice = r.Next(0, str2.Length);
            int str3Choice = r.Next(0, str3.Length);
            int str4Choice = r.Next(0, str4.Length);
            int str5Choice = r.Next(0, str5.Length);
            return str1[str1Choice] + str2[str2Choice] + str3[str3Choice] + str4[str4Choice] + str5[str5Choice];
        }
        #endregion

        public void ExStep()
        {
            Invoke(new Action(() =>
            {
                hm.AppendMsg(rtxtMsg, Color.Black, "正在登录...", true);
            }));
            Dictionary<CookieCollection, string> dic_index = new Dictionary<CookieCollection, string>();
            dic_index = Logon();
            string html_index = "";
            CookieCollection ccl_index = new CookieCollection();
            foreach (var item in dic_index)
            {
                ccl_index = item.Key;
                ccl = ccl_index;
                html_index = item.Value;
            }
            if (html_index.Contains("退出"))
            {
                Invoke(new Action(() =>
                {
                    hm.AppendMsg(rtxtMsg, Color.Black, "登录成功!", true);
                }));
            }
            else
            {
                Invoke(new Action(() =>
                {
                    hm.AppendMsg(rtxtMsg, Color.Red, "登录失败,请检查配置或网络!", true);
                }));
                return;
            }

            string[] strSign = Sign(ccl_index);
            string html_sign = strSign[0];
            string signWord = strSign[1];

            string mycoin = GetCoin(ccl_index, html_sign);
            if (mycoin == null)
            {
                Invoke(new Action(() =>
                {
                    hm.AppendMsg(rtxtMsg, Color.Red, ShowMsg(), true);
                }));
                return;
            }
            Invoke(new Action(() =>
            {
                hm.AppendMsg(rtxtMsg, Color.Black, "你目前" + mycoin, true);
            }));
            if (html_sign.Contains("今天的红包，您已经领取了，一天就一次机会"))
            {
                Invoke(new Action(() =>
                {
                    hm.AppendMsg(rtxtMsg, Color.Red, ShowMsg(), true);
                }));
                return;
            }
            Invoke(new Action(() =>
            {
                hm.AppendMsg(rtxtMsg, Color.Blue, "今日签到:\r\n    " + signWord, false);
            }));
        }
        
        #region 登录
        public Dictionary<CookieCollection, string> Logon()
        {
            string id = "";
            string userid = "";
            string pwd = "";
            string path = "";
            try
            {
                id = ConfigurationManager.AppSettings["id"].ToString();
                path = ConfigurationManager.AppSettings["path"].ToString();
                userid = du.EnDEUser(ConfigurationManager.AppSettings["userid"].ToString(), id+path, false);
                pwd = du.EnDEUser(ConfigurationManager.AppSettings["pwd"].ToString(), id+path, false);

            }
            catch (Exception)
            {
                Invoke(new Action(() =>
                {
                    hm.AppendMsg(rtxtMsg, Color.Red, "读取配置文件错误，请重新设置你的账户信息!", true);
                    return;
                }));
            }
            string url_index = "http://emuch.net/bbs/logging.php?action=login"
                + "&cookietime=31536000"
                + "&formhash=eb4b777d"
                + "&loginsubmit=%C2%BB%C3%A1%C3%94%C2%B1%C2%B5%C3%87%C3%82%C2%BC"
                + "&password=" + rm.GetEncoding(pwd, true)
                + "&referer=http%3A%2F%2Femuch.net%2Fbbs%2Findex.php%3Femobile%3D1"
                + "&username=" + rm.GetEncoding(userid, true);
            Dictionary<CookieCollection, string> dic_index = new Dictionary<CookieCollection, string>();
            dic_index = ch.GetResponsesDomain(url_index, "get", "", "application/x-www-form-urlencoded", "gbk");
            return dic_index;
        }
        #endregion

        #region 签到并发布说说
        public string[] Sign(CookieCollection ccl2)
        {
            List<string> lst_SignWord = new List<string>();
            lst_SignWord = fc.ReadAllLine(path, Encoding.Unicode, true);
            int count = lst_SignWord.Count;
            Random rd = new Random();
            int choice = rd.Next(0, count);
            string txtSign = lst_SignWord[choice];
            string postdata = "&formhash=d5df2a36&getmode=1" + "&message=" + rm.GetEncoding(txtSign, true)
                + "&creditsubmit=" + rm.GetEncoding("领取红包", true) + "&isdoing=1";
            string url = "http://emuch.net/bbs/memcp.php?action=getcredit";
            Dictionary<CookieCollection, string> dic_sign = new Dictionary<CookieCollection, string>();
            dic_sign = ch.GetResponsesDomain(url, "post", postdata, "application/x-www-form-urlencoded", "gbk", ccl2);
            string html_sign = "";
            foreach (var item in dic_sign)
            {
                html_sign = item.Value;
            }
            string[] strSign = new string[] { html_sign, txtSign };
            //hm.AppendMsg(rtxtMsg, Color.Blue, "今日签到:"+txtSign, false);
            return strSign;
        }
        #endregion

        #region 查询我的金币
        public string GetCoin(CookieCollection ccl2, string html2)
        {
            Regex r_coin = new Regex(@"<u>金币: [^<]+");
            List<string> lst_coin = new List<string>();
            lst_coin = rm.GetAims(html2, r_coin);
            if (lst_coin.Count >= 1)
            {
                return lst_coin[0].Replace("<u>", "");
            }
            return null;
        }
        #endregion

        #region 设置账户
        private void TSMenu_Set_Click(object sender, EventArgs e)
        {
            Set sfrm = new Set();
            sfrm.ShowDialog();
        }
        #endregion

        #region 记事本
        private void Menu_Tool_Note_Click(object sender, EventArgs e)
        {
            Note frmnote = new Note(ccl);
            frmnote.ShowDialog();
        }

        #endregion

   


        //窗体关闭触发事件
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Menu_Tool_About_Click(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                hm.AppendMsg(rtxtMsg, Color.Blue, "版本:1.01\r\n编译时间:20131107\r\n改进项目:\r\n    1.修复发表说说中文乱码;\r\n    2.新增小木虫私密记事本功能。\r\n", false);
            }));
        }





    }
}

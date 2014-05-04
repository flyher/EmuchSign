using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using _EmuchSign.cs;
using System.Text.RegularExpressions;
using System.Configuration;

namespace _EmuchSign
{
    public partial class Note : Form
    {
        public Note()
        {
            InitializeComponent();
        }
        public CookieCollection ccl { get; set; }
        int colorR = 0;
        int colorG = 0;
        int colorB = 0;
        public Note(CookieCollection ccl)
        {
            InitializeComponent();
            this.ccl = ccl;
        }
        
        private void Note_Load(object sender, EventArgs e)
        {
            lbMsg.Visible = false;
            cbColor.DropDownHeight = 60;
            string emuchid= GetEmuchId(ccl);
            Read(ccl, emuchid);
            LoadColor();
        }       
        
        ConHttp ch = new ConHttp();
        RegexMatch rm = new RegexMatch();
        DeUser du = new DeUser();

        private void btnSave_Click(object sender, EventArgs e)
        {
            string emuchid = GetEmuchId(ccl);
            //string a= Read(ccl, emuchid);
            if (emuchid==null)
            { 
                colorG = 0;
                colorB = 0;
                //保存时候颜色选项隐藏
                cbColor.Visible = false;
                cbox.Visible = false;
                lbMsg.Visible = true;
                lbMsg.ForeColor = Color.FromArgb(255,colorG,colorB);
                lbMsg.Text = "* 你需要先签到才能使用该功能!";
                timer2.Interval = 63;
                timer2.Enabled = true;
                timer2.Start();
                return;
            }
            else if (Save(ccl, emuchid))
            {
                colorR = 51;
                colorG = 0;
                //保存时候颜色选项隐藏
                cbColor.Visible = false;
                cbox.Visible = false;
                lbMsg.Visible = true;
                lbMsg.ForeColor = Color.FromArgb(colorR, colorG, 255);
                lbMsg.Text = "* 保存成功!";
                timer1.Interval = 63;
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                colorG = 0;
                colorB = 0;
                //保存时候颜色选项隐藏
                cbColor.Visible = false;
                cbox.Visible = false;
                lbMsg.Visible = true;
                lbMsg.ForeColor = Color.FromArgb(255, colorG, colorB);
                lbMsg.Text = "* 保存失败!";
                timer2.Interval = 63;
                timer2.Enabled = true;
                timer2.Start();
                return;
            }
        }

        //获取我的虫号
        public string GetEmuchId(CookieCollection ccl2)
        {
            string value = "";
            foreach (Cookie item in ccl2)
            {
                if (item.Name == "_discuz_uid")
                {
                    value = item.Value;
                }
            }
            if (value.Length<=0)
            {
                return null;
            }
            return value;
        }
        //加载记事本
        private void Read(CookieCollection ccl2, string emuchid)
        {
            string content="";
            try
            {
                string url = "http://emuch.net/bbs/space.php?uid=" + emuchid + "&view=notebook";
                Dictionary<CookieCollection, string> dic_note = new Dictionary<CookieCollection, string>();
                dic_note = ch.GetResponsesDomain(url, "get", "", "application/x-www-form-urlencoded", "gbk", ccl2);
                string html_note = "";
                foreach (var item in dic_note)
                {
                    html_note = item.Value;
                }
                Regex r = new Regex("99%;\">[^<]+[^>]+");
                List<string> lst_OldNote = rm.GetAims(html_note, r);
                if (lst_OldNote.Count == 1)
                {
                    content = lst_OldNote[0].Replace("99%;\">", "")
                        .Replace("</textarea", "");
                }
                rtxtNote.Text=content;
            }
            catch (Exception)
            {
                rtxtNote.Text = "* 读取错误";
            }

        }

        //加载颜色
        private void LoadColor()
        {
            cbColor.Items.Add("红色");
            cbColor.Items.Add("蓝色");
            cbColor.Items.Add("绿色");
            cbColor.Items.Add("黄色");
            cbColor.Items.Add("橙色");
            cbColor.Items.Add("粉色");
            cbColor.Items.Add("浅绿色");
            cbColor.Items.Add("天蓝色");
            cbColor.Items.Add("黑色");
            cbColor.Items.Add("银灰色");
            cbColor.Items.Add("紫色");
            cbColor.Items.Add("金色");
            cbColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbColor.BackColor = Color.White;
            string forecolor = ConfigurationManager.AppSettings["forecolor"].ToString();
            if (forecolor.Length <= 0)
            {
                cbColor.SelectedIndex = 8;
            }
            else
            {
                cbColor.SelectedIndex = Convert.ToInt32(forecolor);
            }
        }

        //保存记事本
        private bool Save(CookieCollection ccl2, string emuchid)
        {
            try
            {
                string newcontent = rtxtNote.Text;
                string url = "http://emuch.net/bbs/space.php?" + "uid=" + emuchid + "&view=notebook";
                string postdata = "&formhash=cf4a4a91" + "&newcontent=" + rm.GetEncoding(newcontent, true) + "&notesubmit=%C2%B1%C2%A3%C2%B4%C3%A6";
                Dictionary<CookieCollection, string> dic_Note = new Dictionary<CookieCollection, string>();
                dic_Note = ch.GetResponsesDomain(url, "post", postdata, "application/x-www-form-urlencoded", "gbk", ccl2);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //复选框
        private void cbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxIsCheck();
        }

        //检查cbox是否选中
        private void CheckBoxIsCheck()
        { 
            if (cbox.Checked)
            {
                cbColor.Visible = true;
            }
            else
            {
                cbColor.Visible = false;
            }            
        }

        //选项颜色改变
        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int forecolor = -1;
                if (cbColor.SelectedIndex == 0) { rtxtNote.ForeColor = Color.Red; forecolor = 0; }
                if (cbColor.SelectedIndex == 1) { rtxtNote.ForeColor = Color.Blue; forecolor = 1; }
                if (cbColor.SelectedIndex == 2) { rtxtNote.ForeColor = Color.Green; forecolor = 2; }
                if (cbColor.SelectedIndex == 3) { rtxtNote.ForeColor = Color.Yellow; forecolor = 3; }
                if (cbColor.SelectedIndex == 4) { rtxtNote.ForeColor = Color.Orange; forecolor = 4; }
                if (cbColor.SelectedIndex == 5) { rtxtNote.ForeColor = Color.Pink; forecolor = 5; }
                if (cbColor.SelectedIndex == 6) { rtxtNote.ForeColor = Color.LightGreen; forecolor = 6; }
                if (cbColor.SelectedIndex == 7) { rtxtNote.ForeColor = Color.SkyBlue; forecolor = 7; }
                if (cbColor.SelectedIndex == 8) { rtxtNote.ForeColor = Color.Black; forecolor = 8; }
                if (cbColor.SelectedIndex == 9) { rtxtNote.ForeColor = Color.Silver; forecolor = 9; }
                if (cbColor.SelectedIndex == 10) { rtxtNote.ForeColor = Color.Purple; forecolor = 10; }
                if (cbColor.SelectedIndex == 11) { rtxtNote.ForeColor = Color.Gold; forecolor = 11; }
                du.SetValue("forecolor", forecolor.ToString());
            }
            catch (Exception)
            {
                colorG = 0;
                colorB = 0;
                //保存时候颜色选项隐藏
                cbColor.Visible = false;
                cbox.Visible = false;
                lbMsg.Visible = true;
                lbMsg.ForeColor = Color.FromArgb(255, colorG, colorB);
                lbMsg.Text = "* 颜色配置失败!";
                timer2.Interval = 63;
                timer2.Enabled = true;
                timer2.Start();
                return;
            }
        }

        //提示颜色渐变(成功)
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (colorG <= 245 && colorR <= 245)
            {
                colorR += 5;
                colorG += 5;
                lbMsg.ForeColor = Color.FromArgb(colorR, colorG, 255);
            }
            else
            {
                lbMsg.ForeColor = Color.Empty;
                lbMsg.Text = "";
                //保存结束颜色选项显示出来
                cbox.Visible = true;
                lbMsg.Visible = false;
                //再检查一下cbox是否选中
                CheckBoxIsCheck();
                timer1.Stop();
            }
        }

        //颜色渐变(失败)
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (colorG <= 245 && colorB <= 245)
            {
                colorG += 5;
                colorB += 5;
                lbMsg.ForeColor = Color.FromArgb(255, colorG, colorB);
            }
            else
            {
                lbMsg.ForeColor = Color.Empty;
                lbMsg.Text = "";
                //保存结束颜色选项显示出来
                cbox.Visible = true;
                lbMsg.Visible = false;
                //再检查一下cbox是否选中
                CheckBoxIsCheck();
                timer1.Stop();
            }
        }


    }
}

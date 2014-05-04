using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Web.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using _EmuchSign.cs;

namespace _EmuchSign
{
    public partial class Set : Form
    {
        public Set()
        {
            InitializeComponent();
        }
        DeUser du = new DeUser();
        private void Set_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Opacity = 1;//不透明
            try
            {
                string path = ConfigurationManager.AppSettings["path"].ToString();
                txtSay.Text = path;
            }
            catch (Exception)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show("读取配置文件失败", "错误", MessageBoxButtons.OK);
                    this.Close();
                }));
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string userid=txtUserid.Text.Trim();
            string pwd=txtPwd.Text.Trim();
            Random r=new Random();
            string id = r.Next(100000, 999999).ToString();
            string path = txtSay.Text.Trim();
            if (userid.Length==0||pwd.Length==0||path.Length<2)
            {
                lbmsg.ForeColor = Color.Red;
                lbmsg.Text = "* 必须输入正确的账户信息!";
                return;
            }

            if (SaveUser(userid, pwd, id,path))
            {
                lbmsg.ForeColor = Color.Blue;
                lbmsg.Text = "* 保存成功,用户信息已加密!";
                timer1.Enabled = true;
                timer1.Interval = 100;
                timer1.Start();
            }
            else
            { 
                MessageBox.Show("保存失败！");
                return;                
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //对显示的提示渐变
            this.Opacity = this.Opacity - 0.1;
            if (this.Opacity==0)
            {
                timer1.Stop();
                this.Close();
            }
        }
        private bool SaveUser(string userid,string pwd,string id,string path)
        {
            string type = id + path;
            try
            {
                string newuserid = du.EnDEUser(userid, type, true);//加密
                string newpwd = du.EnDEUser(pwd, type, true);
                du.SetValue("userid", newuserid);
                du.SetValue("pwd", newpwd);
                du.SetValue("id", id);
                du.SetValue("path", path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}

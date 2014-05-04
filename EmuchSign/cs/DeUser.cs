using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Xml;

namespace _EmuchSign.cs
{
    public class DeUser
    {
        public string EnDEUser(string user, string id, bool ende)
        {
            string str = "";
            if (ende)
            {
                str = EnRSA(user, id);
            }
            else
            {
                str = DeRSA(user, id);
            }
            return str;
        }

        #region RSA加密
        public string EnRSA(string str, string keyName)
        {
            CspParameters cp;
            cp = new CspParameters();
            cp.KeyContainerName = keyName;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            byte[] bs = Encoding.Default.GetBytes(str);
            byte[] en = rsa.Encrypt(bs, false);
            return Convert.ToBase64String(en);
        }

        public string DeRSA(string str, string keyName)
        {
            CspParameters cp;
            cp = new CspParameters();
            cp.KeyContainerName = keyName;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            byte[] bs = Convert.FromBase64String(str);
            byte[] de = rsa.Decrypt(bs, false);
            return Encoding.Default.GetString(de);
        }
        #endregion

        //保存配置
        public void SetValue(string AppKey, string AppValue)
        {
            //System.Configuration.ConfigurationSettings.AppSettings.Set(AppKey,AppValue);这个没用   
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            XmlNode xNode;
            XmlElement xElem1;
            XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            if (xElem1 != null) xElem1.SetAttribute("value", AppValue);
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", AppKey);
                xElem2.SetAttribute("value", AppValue);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
        }
    }
}

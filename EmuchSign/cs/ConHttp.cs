using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.ComponentModel;//
using System.Reflection;
using System.Drawing;
using System.Runtime.InteropServices;


namespace _EmuchSign.cs
{
    /// <summary>
    /// 网络提交数据(POST或者GET)的操作
    /// Build 
    /// Again 20130717 V1.00
    /// by flyher from System.Road.Net
    /// </summary>
    public class ConHttp
    {
        #region 参数 cookie容器
        /// <summary>
        /// cookie容器
        /// </summary>
        public static CookieContainer CookieContainers = new CookieContainer();
        #endregion

        #region N8
        public static string N8 = "Mozilla/5.0(Symbian/3;Series60/5.2 NokiaN8-00/014.002rofile/MIDP-2.1 Configuration/CLDC-1.1)";
        #endregion

        #region iPhone
        public static string iPhone = "Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16";
        #endregion

        #region 提交数据，获取返回的字符串(返回带cookies,域)
        /// <summary>
        /// 提交数据，获取返回的字符串(返回带cookies,域)
        /// </summary>
        /// <param name="url">提交的地址</param>
        /// <param name="method">"POST"或者"GET"</param>
        /// <param name="data">当method为"POST"时，data会被提交到服务器；当method是"GET"时,data应该为空(string.empty)</param>
        /// <param name="contenttype">Content-Type HTTP的标头值，例如:返回json:"application/json; charset=utf8";返回web:"application/x-www-form-urlencoded"</param>
        /// <param name="encoding">提交后响应的文章编码</param>
        /// <returns>返回服务器响应的数据</returns>
        public Dictionary<CookieCollection, string> GetResponsesDomain(string url, string method, string data, string contenttype, string encoding)
        {
            Dictionary<CookieCollection, string> dic = new Dictionary<CookieCollection, string>();
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.KeepAlive = true;
                req.Method = method.ToUpper();
                req.AllowAutoRedirect = true;
                req.CookieContainer = CookieContainers;
                //req.ContentType = "application/json; charset=utf8";
                req.ContentType = contenttype;

                req.UserAgent = N8;
                req.Accept = "application/json, text/javascript, */*; q=0.01";
                req.Timeout = 50000;


                if (method.ToUpper() == "POST" && data != null)
                {
                    ASCIIEncoding encodings = new ASCIIEncoding();
                    byte[] postBytes = encodings.GetBytes(data); ;
                    req.ContentLength = postBytes.Length;
                    Stream st = req.GetRequestStream();
                    st.Write(postBytes, 0, postBytes.Length);
                    st.Close();
                }

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
                {
                    return true;
                };

                Encoding myEncoding = Encoding.GetEncoding(encoding);//编码

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream resst = res.GetResponseStream();
                StreamReader sr = new StreamReader(resst, myEncoding);
                string str = sr.ReadToEnd();
                CookieCollection ccl = new CookieCollection();
                foreach (Cookie cookie in res.Cookies)
                {
                    ccl.Add(cookie);
                }
                //CookieCollection cookies = res.Cookies;


                dic.Add(ccl, str);

                return dic;
            }
            catch (Exception)
            {
                return dic;
            }
        }
        #endregion

        #region 提交数据(带cookies,域)，获取返回的字符串(返回带cookies,域)
        /// <summary>
        /// 提交数据(带cookies,域)，获取返回的字符串(返回带cookies,域)
        /// </summary>
        /// <param name="url">提交的地址</param>
        /// <param name="method">"POST"或者"GET"</param>
        /// <param name="data">当method为"POST"时，data会被提交到服务器；当method是"GET"时,data应该为空(string.empty)</param>
        /// <param name="contenttype">Content-Type HTTP的标头值，例如:返回json:"application/json; charset=utf8";返回web:"application/x-www-form-urlencoded"</param>
        /// <param name="encoding">提交后响应的文章编码</param>
        /// <returns>返回服务器响应的数据</returns>
        public Dictionary<CookieCollection, string> GetResponsesDomain(string url, string method, string data, string contenttype, string encoding, CookieCollection ccl)
        {
            Dictionary<CookieCollection, string> dic = new Dictionary<CookieCollection, string>();
            try
            {
                CookieContainer cc = new CookieContainer();
                cc.Add(ccl);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.KeepAlive = true;
                req.Method = method.ToUpper();
                req.AllowAutoRedirect = true;
                req.CookieContainer = cc;
                //req.ContentType = "application/json; charset=utf8";
                req.ContentType = contenttype.ToString();

                req.UserAgent = N8;
                req.Accept = "application/json, text/javascript, */*; q=0.01";
                req.Timeout = 50000;


                if (method.ToUpper() == "POST" && data != null)
                {
                    ASCIIEncoding encodings = new ASCIIEncoding();
                    byte[] postBytes = encodings.GetBytes(data); ;
                    req.ContentLength = postBytes.Length;
                    Stream st = req.GetRequestStream();
                    st.Write(postBytes, 0, postBytes.Length);
                    st.Close();
                }

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
                {
                    return true;
                };

                Encoding myEncoding = Encoding.GetEncoding(encoding);//编码

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream resst = res.GetResponseStream();
                StreamReader sr = new StreamReader(resst, myEncoding);
                string str = sr.ReadToEnd();
                CookieCollection ccle = new CookieCollection();
                foreach (Cookie cookie in res.Cookies)
                {
                    ccle.Add(cookie);
                }
                dic.Add(ccle, str);
                //CookieCollection cookies = res.Cookies;
                //dic.Add(cookies, str);

                return dic;
            }
            catch (Exception)
            {
                return dic;
            }
        }
        #endregion

    }
}
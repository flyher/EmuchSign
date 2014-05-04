using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;//正则
using System.Net;
using System.Web;
using System.Net.NetworkInformation;

namespace _EmuchSign.cs
{
    /// <summary>
    /// 网页源码以及正则表达式相关操作
    /// Build 20130529 V1.00
    /// Again 20130710 V1.00
    /// by flyher from System.Road.Net
    /// </summary>
    public class RegexMatch
    {
        #region 返回匹配的数据列表
        /// <summary>
        /// 返回匹配的数据列表
        /// </summary>
        /// <param name="htmlbasic">网页源文件</param>
        /// <param name="regex">要使用的正则表达式</param>
        /// <returns>得到List类型的匹配数据,错误 0 位 是"Error:错误信息",若没匹配返回为空</returns>
        public List<string> GetAims(string htmlbasic, Regex regex)
        {
                
            List<string> list_match = new List<string>();
            try
            {
                MatchCollection regex_m = regex.Matches(htmlbasic);

                for (int i = 0; i < regex_m.Count; i++)//有匹配
                {
                    list_match.Add(regex_m[i].ToString());
                }
                return list_match;
            }
            catch (Exception err)//出现错误
            {
                list_match.Add("Error:"+err.ToString());
                return list_match;
            }
        }
        #endregion

        #region 将文本或网址转换编码(例如汉字网址互转)
        /// <summary>
        /// 将文本或网址转换编码(例如汉字网址互转)
        /// </summary>
        /// <param name="text">文本或者网页源码</param>
        /// <param name="encoding">目标编码(Encoding.Unicode等等类似可查看编码;若url 的bool为false，这里传空字符串)</param>
        /// <param name="url">目标编码是否用于url，是为true，否则false</param>
        /// <returns>转换好的编码(转换失败提示失败原因)</returns>
        public string GetEncoding(string text, string encoding, bool url)
        {
            try
            {
                if (url == true)
                {
                    return Uri.EscapeUriString(text);
                }
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                return Encoding.GetEncoding(encoding).GetString(buffer);
            }
            catch (Exception err)
            {
                return err.ToString();
            }
        }
        #endregion

        #region 将文本转换编码
        /// <summary>
        /// 将文本转换编码
        /// </summary>
        /// <param name="text">文本或者网页源码</param>
        /// <param name="encoding">目标编码(Encoding.Unicode等等类似可查看编码;)</param>
        /// <returns>转换好的编码(转换失败提示失败原因)</returns>
        public string GetEncoding(string text, string encoding)
        {
            try
            {
                byte[] buffer = Encoding.GetEncoding(encoding).GetBytes(text);
                    //Encoding.Unicode.GetBytes(text);
                return Encoding.GetEncoding(encoding).GetString(buffer);
                //encodeing.getencodeing(xxxx).getbytes;
            }
            catch (Exception err)
            {
                return err.ToString();
            }
        }
        #endregion

        #region 特殊符号数学符号或者网址编码互转
        /// <summary>
        /// 特殊符号数学符号或者网址编码互转
        /// </summary>
        /// <param name="text">特殊符号数学符号或者要转换的带有数学符号的字符串</param>
        /// <param name="url">目标编码是否用于url，编码为true，解码false</param>
        /// <returns>转换好的编码(转换失败提示失败原因)</returns>
        public string GetEncoding(string text,bool url)
        {
            try
            {
                if (url==true)
                {
                return HttpUtility.UrlEncode(text,Encoding.GetEncoding("gb2312"));//编码
                //return  Encoding.GetEncoding("%CA%D7%D2%B3%C1%F4%D1%D4%B1%BE").GetBytes;                    
                }
                else
                {
                    return HttpUtility.UrlDecode(text, Encoding.GetEncoding("gb2312")); //解码
                }
            }
            catch (Exception err)
            {
                return err.ToString();
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace _EmuchSign.cs
{
    /// <summary>
    /// 文件和文件夹操作
    /// Build 20130702 V1.00
    /// Again 20130812 V1.01
    /// by flyher from System.Road.Files
    /// </summary>
    public class FileControl
    {

        #region 逐行读取文本
        /// <summary>
        /// 逐行读取文本
        /// </summary>
        /// <param name="path">文本路径</param>
        /// <param name="encoding">读取编码</param>
        /// <param name="isEncoding">是否用自定义编码(false:encoding可为空)</param>
        /// <returns>返回读取的文本</returns>
        public List<string> ReadAllLine(string path, Encoding encoding, bool isEncoding)
        {
            List<string> lst = new List<string>();
            string[] lines = null;
            if (isEncoding == false) { lines = File.ReadAllLines(path); }
            if (isEncoding == true) { lines = File.ReadAllLines(path, encoding); }
            for (int i = 0; i < lines.Length; i++)
            {
                lst.Add(lines[i].ToString());
            }
            return lst;
        }
        #endregion
    }
}
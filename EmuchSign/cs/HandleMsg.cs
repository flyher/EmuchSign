using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;//RichTextBox
using System.Threading;//ThreadStart
using System.Drawing;

namespace _EmuchSign.cs
{
    /// <summary>
    /// 回馈消息处理类(打印文本，弹出框)
    /// Build 20130806 V1.00
    /// Again 20130812 V1.00
    /// by flyher from System.Road.Word
    /// </summary>
     public class HandleMsg
     {
         #region 在富文本上打印消息
         /// <summary>
         /// 在富文本上打印消息
         /// </summary>
         /// <param name="richTextBox1">所在打印的富文本</param>
         /// <param name="color">打印字体颜色</param>
         /// <param name="text">要打印的文本</param>
         /// <param name="AutoTime">是否在每条打印结果前追加时间</param>
         public void AppendMsg(RichTextBox richTextBox1, Color color, string text,bool AutoTime)
         {
             richTextBox1.BeginInvoke(new ThreadStart(() =>
             {
                 lock (richTextBox1)
                 {
                     //为控件输入焦点
                     richTextBox1.Focus();
                     //检查文本框过长
                     if (richTextBox1.TextLength > 100000 )
                     {
                         richTextBox1.Clear();
                     }
                     //得到有格式的文本 
                     using (var temp = new RichTextBox())
                     {
                         temp.SelectionColor = color;
                         if (AutoTime)
                             temp.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                         temp.AppendText(text);
                         //追加文本
                         richTextBox1.Select(richTextBox1.Rtf.Length, 0);
                         richTextBox1.SelectedRtf = temp.Rtf;
                     }
                     //设定光标所在位置 
                     //richTextBox1.SelectionStart = richTextBox1.TextLength;
                     //滚动到当前光标处 
                     //richTextBox1.ScrollToCaret();
                 }
             }));
         }
         #endregion

     }
}

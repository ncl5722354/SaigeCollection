using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace String_Caozuo
{
    public class string_caozuo
    {

        /// <summary>
        ///  功能整理  2017 12 21 writen by Tom Ni
        ///  Get_KongGe_String(string scr,int index)          当字符串被空格分隔，取出第index个分隔项(index从1开始)
        ///  Get_Xiahuaxian_String(string scr,int index)      当字符串被下划线分隔，取出第index个分隔项(index从1开始)
        ///  Get_Maohao_String(string scr, int index)         当字符串被冒号分隔，取出第index个分隔项(index从1开始)
        ///  Get_Send_Cmd_From_Byte(byte[] sendbyte)          将byte数组用带空格的字符串格式表示出来
        ///  Get_String_From_Byte(byte[] bytearray)           将byte数组转化为默认编码格式的字符串
        ///  Get_Byte_From_String(string scr)                 将scr字符串转化为默认编码格式的byte数组
        /// </summary>
        /// <param name="scrlist"></param>
        /// <param name="mudilist"></param>

        public static string Get_KongGe_String(string scr,int index)
        {
            // 返回下划线第index个量
            try
            {
                int count = 0;   // 下划线的数量
                int length = scr.Length;
                char[] char_arraylist = scr.ToCharArray(0, length);
                for (int i = 0; i < length; i++)
                {
                    if (char_arraylist[i] == ' ')
                    {
                        count++;
                    }
                }
                int[] _index = new int[count + 2];
                string[] substring = new string[count + 1];
                _index[0] = -1;
                int mycount = 1;
                for (int i = 0; i < length; i++)
                {
                    if (i == length - 1)
                    {
                        _index[mycount] = i + 1;
                        break;
                    }
                    if (char_arraylist[i] == ' ')
                    {
                        _index[mycount] = i;
                        mycount++;
                    }
                }
                for (int i = 0; i < count + 1; i++)
                {
                    substring[i] = scr.Substring(_index[i] + 1, _index[i + 1] - _index[i] - 1);
                }
                return substring[index - 1];
            }
            catch { return null; }
        } // 返回空格第index个量

        public static string Get_Xiahuaxian_String(string scr,int index)
        {
            // 返回下划线第index个量
            try
            {
                int count = 0;   // 下划线的数量
                int length = scr.Length;
                char[] char_arraylist = scr.ToCharArray(0, length);
                for(int i=0;i<length;i++)
                {
                    if(char_arraylist[i]=='_')
                    {
                        count++;
                    }
                }
                int[] _index = new int[count+2];
                string[] substring = new string[count+1];
                _index[0]=-1;
                int mycount=1;
                for(int i=0;i<length;i++)
                {
                    if(i==length-1)
                    {
                        _index[mycount]=i+1;
                        break;
                    }
                    if(char_arraylist[i]=='_')
                    {
                        _index[mycount] = i;
                        mycount++;
                    }
                }
                for(int i=0;i<count+1;i++)
                {
                    substring[i] = scr.Substring(_index[i] + 1, _index[i + 1] - _index[i] - 1);
                }
                return substring[index - 1];
            }
            catch { return null; }
        }                 // 用下划线分割的标志

         public static string Get_HengGang_String(string scr,int index)
        {
            // 返回下划线第index个量
            try
            {
                int count = 0;   // 下划线的数量
                int length = scr.Length;
                char[] char_arraylist = scr.ToCharArray(0, length);
                for(int i=0;i<length;i++)
                {
                    if(char_arraylist[i]=='-')
                    {
                        count++;
                    }
                }
                int[] _index = new int[count+2];
                string[] substring = new string[count+1];
                _index[0]=-1;
                int mycount=1;
                for(int i=0;i<length;i++)
                {
                    if(i==length-1)
                    {
                        _index[mycount]=i+1;
                        break;
                    }
                    if(char_arraylist[i]=='-')
                    {
                        _index[mycount] = i;
                        mycount++;
                    }
                }
                for(int i=0;i<count+1;i++)
                {
                    substring[i] = scr.Substring(_index[i] + 1, _index[i + 1] - _index[i] - 1);
                }
                return substring[index - 1];
            }
            catch { return null; }
        }

         public static string Get_Dian_String(string scr, int index)
         {
             try
             {
                 int count = 0;   // 下划线的数量
                 int length = scr.Length;
                 char[] char_arraylist = scr.ToCharArray(0, length);
                 for (int i = 0; i < length; i++)
                 {
                     if (char_arraylist[i] == '.')
                     {
                         count++;
                     }
                 }
                 int[] _index = new int[count + 2];
                 string[] substring = new string[count + 1];
                 _index[0] = -1;
                 int mycount = 1;
                 for (int i = 0; i < length; i++)
                 {
                     if (i == length - 1)
                     {
                         _index[mycount] = i + 1;
                         break;
                     }
                     if (char_arraylist[i] == '.')
                     {
                         _index[mycount] = i;
                         mycount++;
                     }
                 }
                 for (int i = 0; i < count + 1; i++)
                 {
                     substring[i] = scr.Substring(_index[i] + 1, _index[i + 1] - _index[i] - 1);
                 }
                 return substring[index - 1];
             }
             catch { return null; }
         }      // 返回点分割的字符串

        public static string Get_Table_String(string scr,int index)
        {
            try
            {
                int count = 0;   // 下划线的数量
                int length = scr.Length;
                char[] char_arraylist = scr.ToCharArray(0, length);
                for (int i = 0; i < length; i++)
                {
                    if (char_arraylist[i] == '\t')
                    {
                        count++;
                    }
                }
                int[] _index = new int[count + 2];
                string[] substring = new string[count + 1];
                _index[0] = -1;
                int mycount = 1;
                for (int i = 0; i < length; i++)
                {
                    if (i == length - 1)
                    {
                        _index[mycount] = i + 1;
                        break;
                    }
                    if (char_arraylist[i] == '\t')
                    {
                        _index[mycount] = i;
                        mycount++;
                    }
                }
                for (int i = 0; i < count + 1; i++)
                {
                    substring[i] = scr.Substring(_index[i] + 1, _index[i + 1] - _index[i] - 1);
                }
                return substring[index - 1];
            }
            catch { return null; }
        }      // 返回\t分割的字符串

        public static string Get_Maohao_String(string scr, int index)
        {
            // 返回下划线第index个量
            try
            {
                int count = 0;   // 下划线的数量
                int length = scr.Length;
                char[] char_arraylist = scr.ToCharArray(0, length);
                for (int i = 0; i < length; i++)
                {
                    if (char_arraylist[i] == ':')
                    {
                        count++;
                    }
                }
                int[] _index = new int[count + 2];
                string[] substring = new string[count + 1];
                _index[0] = -1;
                int mycount = 1;
                for (int i = 0; i < length; i++)
                {
                    if (i == length - 1)
                    {
                        _index[mycount] = i + 1;
                        break;
                    }
                    if (char_arraylist[i] == ':')
                    {
                        _index[mycount] = i;
                        mycount++;
                    }
                }
                for (int i = 0; i < count + 1; i++)
                {
                    substring[i] = scr.Substring(_index[i] + 1, _index[i + 1] - _index[i] - 1);
                }
                return substring[index - 1];
            }
            catch { return null; }
        }                    // 用：分割的标志

        public static string Get_Send_Cmd_From_Byte(byte[] sendbyte)
        {
            string cmd = "";
            foreach(byte mybyte in sendbyte)
            {
                cmd = cmd + mybyte.ToString("X").PadLeft(2, '0') + " ";
            }
            return cmd.TrimEnd();
        }

        public static string Get_String_From_Byte(byte[] bytearray)
        {
            return System.Text.Encoding.Default.GetString(bytearray);
        }                      // 转化为字符串

        public static byte[] Get_Byte_From_String(string scr)
        {
            return System.Text.Encoding.Default.GetBytes(scr);
        }


    }
}

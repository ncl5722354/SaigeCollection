using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

// ini操作
// [DllImport("kernel32")] 
// private static extern long WritePrivateProfileString(string section, string key, string val, string filePath); 
// section: 要写入的段落名
// key: 要写入的键，如果该key存在则覆盖写入
// val: key所对应的值
// filePath: INI文件的完整路径和文件名


namespace saigecollection
{
    public class IniFile
    {
        public string Path;

        // 声明INI文件的写操作函数 WritePrivateProfileString()
        [System.Runtime.InteropServices.DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        public IniFile(string path)
        {
            this.Path = path;
            try
            {
                FileCaozuo.Create_File(path); // 尝试创建这个文件
            }
            catch { }
        }
        // 声明INI文件的读操作函数 GetPrivateProfileString()

        [System.Runtime.InteropServices.DllImport("kernel32")]

        public static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);
       
        [System.Runtime.InteropServices.DllImport("kernel32")]
        public static extern int GetPrivateProfileSectionNames(byte[] buffer, int iLen, string lpFileName);

        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="section">段落</param>
        /// <param name="key">键</param>
        /// <param name="iValue">值</param>
        public void IniWriteValue(string section, string key, string iValue) 
        {
            WritePrivateProfileString(section, key, iValue, this.Path);
        }

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">段落</param>
        /// <param name="key">键</param>
        /// <returns>返回的键值</returns>
        public string IniReadValue(string section, string key) 
        { 
            StringBuilder temp = new StringBuilder(255); 

            int i = GetPrivateProfileString(section, key, "", temp, 255, this.Path); 
            return temp.ToString();
        }



        public ArrayList ReadSections()
        {
            byte[] buffer = new byte[65535];
            int rel = GetPrivateProfileSectionNames(buffer, buffer.GetUpperBound(0), this.Path);
            return Conver2ArrayList(rel, buffer);
        }
        public ArrayList Conver2ArrayList(int rel, byte[] buffer)
        {
            ArrayList arrayList = new ArrayList();
            if (rel > 0)
            {
                int iCnt, iPos;
                string tmp;
                iCnt = 0; iPos = 0;
                for (iCnt = 0; iCnt < rel; iCnt++)
                {
                    if (buffer[iCnt] == 0x00)
                    {
                        tmp = System.Text.ASCIIEncoding.Default.GetString(buffer, iPos, iCnt - iPos).Trim();
                        iPos = iCnt + 1;
                        if (tmp != "")
                            arrayList.Add(tmp);
                    }
                }
            }
            return arrayList;
        }


        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section">段，格式[]</param>
        /// <param name="Key">键</param>
        /// <returns>返回byte类型的section组或键值组</returns>
        //public byte[] IniReadValues(string section, string key)
        //{
        //    byte[] temp = new byte[255];

        //    int i = GetPrivateProfileString(section,key,"", temp,255, this.Path);
        //    return temp;
        //}
    }
}

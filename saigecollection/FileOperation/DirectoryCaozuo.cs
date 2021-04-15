using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace saigecollection
{
    /// <summary>
    /// Developed by Tom Ni
    /// 文件夹操作类，实现的功能：
    /// 1. 检测文件夹是否存在
    /// 2. 创建文件夹
    /// </summary>
    public class DirectoryCaozuo
    {
        public static bool Directory_Exits(string path)
        {
            return Directory.Exists(path);
        }

        public static void Create_Directory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}

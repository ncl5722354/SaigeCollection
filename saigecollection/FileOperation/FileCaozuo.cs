using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;

namespace saigecollection
{
    /// <summary>
    /// Developed by Tom Ni
    /// 封装文件操作的命令
    /// 1.读文件（两种方式：a 读某一行，b 全部读出）
    /// 2.写文件（两种方式：a 追加一行，b 覆盖）
    /// 3.删某一行
    /// </summary>
    public class FileCaozuo
    {
        public static string[] Read_All_Line(string filepath)
        {
            string read_string;
            
            using(StreamReader sr=File.OpenText(filepath))
            {
                ArrayList mylist = new ArrayList();        //用来装返回的字符串
                while((read_string=sr.ReadLine())!=null)
                {
                    try
                    {
                        mylist.Add(read_string);
                    }
                    catch { }
                }
                int count=mylist.Count;
                if (count <=0) return null;  // 数量不能为0
                string[] return_string_array=new string[count];
                for (int i = 0; i < count - 1; i++)
                {
                    return_string_array[i] = (string)mylist[i];
                }
                sr.Close();
                return return_string_array;
            }
        }

        

        public static void Read_All_Files_Show_List(string path,string filetype, ListBox listbox)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            listbox.Items.Clear();
            foreach (FileInfo file in folder.GetFiles(filetype))
            {
                listbox.Items.Add(file.Name);
                // 创建相关文件
                Create_File(path+"\\"+file.Name+".config");
            }
        }

        public static void Read_All_Files_Show_ComboBox(string path,string filetype,ComboBox combobox)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            
            combobox.Items.Clear();
            foreach (FileInfo file in folder.GetFiles(filetype))
            {
                combobox.Items.Add(file.Name);
                // 创建相关文件
               //Create_File(path + "\\" + file.Name + ".config");
            }
        }

        // 返回所有的文件夹的名称
        public static DirectoryInfo[] Read_All_FilesDirect(string path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            
            return folder.GetDirectories();
        }

        public static ArrayList Read_All_Dir(string path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            DirectoryInfo[] dirs = folder.GetDirectories();
            ArrayList dirslist = new ArrayList();
            foreach(DirectoryInfo info in dirs)
            {
                dirslist.Add(info);
            }
            return dirslist;
        }

        public static string Get_Line(string path,int index)
        {
            string[] allline = File.ReadAllLines(path);
            return allline[index-1];
        }
        public static ArrayList Read_All_Files(string path,string filetype)
        {
            try
            {
                ArrayList filename_list = new ArrayList();
                DirectoryInfo folder = new DirectoryInfo(path);
                foreach (FileInfo file in folder.GetFiles(filetype))
                {
                    filename_list.Add(file.Name);
                }
                return filename_list;
            }
            catch { return null; }
        }


        public static void Create_File(string file_path_name)
        {
            if(!File.Exists(file_path_name))
            {
                // 文件不存在，创建文件
                FileStream fs = new FileStream(file_path_name,FileMode.Create);
                fs.Close();
            }
            else
            {

            }
        }

        public static void Create_Dir(string dir)
        {
            if(!Directory.Exists(dir))
            {
                //如果文件夹不存在，创建文件夹
                Directory.CreateDirectory(dir);
            }
        }

        public static void Copy(string scr_file,string mudi)
        {
            try
            {
                File.Copy(scr_file, mudi,true);
                
            }
            catch { }
        }

        

        public static void Write_Lind_Add(string filepath, string add_line)
        {
            using (StreamWriter sw = new StreamWriter(filepath,true))
            {
                sw.WriteLine(add_line);
                sw.Close();
            }
        }
    }
}

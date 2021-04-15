using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using System.Collections;
using MySql.Data.MySqlClient;

namespace saigecollection
{
    class Mysql
    {
        public static string remote_ip = "58.250.100.13";
        public static string database_name = "saigedatabase";
        public static string password = "ncl5722354";
        public static string user = "root";
        public static string port = "3306";

        
        public string HelloWorld()
        {
            return "Hello World";
        }



        // 从数据库查询中返回一组查询结果
      
        // 返回
        public static ArrayList Get_Sql_Select_Return(string sql)
        {
            ArrayList return_list = new ArrayList();
            // 连接字符串
            string CONNECTIONSTRING = "datasource=" + remote_ip + ";port=" + port + ";database=" + database_name + ";user=" + user + ";pwd=" + password;          //= "datasource=192.168.56.1;port=3306;database=saigedatabase;user=root;pwd=ncl5722354;";      // 连接字符串
            MySqlConnection maindatabase;
            maindatabase = new MySqlConnection(CONNECTIONSTRING);

            try
            {
                maindatabase.Open();

                MySqlCommand mysqlcom = new MySqlCommand(sql, maindatabase);
                MySqlDataReader mysqlread = mysqlcom.ExecuteReader();

                while (mysqlread.Read())
                {
                    int FieldCount = mysqlread.FieldCount;
                    ArrayList row = new ArrayList();
                    for (int i = 0; i < FieldCount; i++)
                    {
                        row.Add(mysqlread[i].ToString());

                    }
                    return_list.Add(row);
                }

                mysqlread.Close();

            }
            catch { }


            maindatabase.Close();
            return return_list;
        }


       
        public static void Ex_Sql(string sql)
        {
            string CONNECTIONSTRING = "datasource=" + remote_ip + ";port=" + port + ";database=" + database_name + ";user=" + user + ";pwd=" + password;          //= "datasource=192.168.56.1;port=3306;database=saigedatabase;user=root;pwd=ncl5722354;";      // 连接字符串
            MySqlConnection maindatabase;
            maindatabase = new MySqlConnection(CONNECTIONSTRING);

            try
            {
                maindatabase.Open();
                MySqlCommand mysqlcom = new MySqlCommand(sql, maindatabase);
                mysqlcom.ExecuteNonQuery();
                mysqlcom.Dispose();
            }
            catch { }
            maindatabase.Close();
        }


    }
}

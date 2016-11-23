using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Proxy
{
    public static class BaseConnection
    {
       static string connString = ConfigurationManager.AppSettings["connectionString"];
       

       public static SqlConnection OpenConnection()
        {
            if (string.IsNullOrEmpty(connString))
            {
                connString = "Data Source=.;Initial Catalog=ActivityDB;User ID=sa;Password=7598115";
            }
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
           return conn;
        }


        public static List<T> Query<T>(string sql,T m)
        {
            IDbConnection conn;
            using (conn = OpenConnection())
            {
                return conn.Query<T>(sql, m).ToList();
            }
        }

        public static List<T> Query<T>(string sql)
        {
            IDbConnection conn;
            using (conn = OpenConnection())
            {
                return conn.Query<T>(sql).ToList();
            }
        }

        public static int Count(string sql)
        {
            IDbConnection conn;
            using (conn = OpenConnection())
            {
                var count = 0;
                var result = conn.Query(sql).FirstOrDefault() ;
                Int32.TryParse(result,out count);
                return count;
            }
        }

        public static int Update<T>(string sql)
        {
            IDbConnection conn;
            using (conn = OpenConnection())
            {
                return conn.Execute(sql);
            }
        }
        
        //public int Insert<T>(T model, string sql)
        //{
        //    return conn.Execute(sql, model);
        //}

        //public int Delete<T>(T model, string sql)
        //{
        //    return conn.Execute(sql, model);
        //}
    }
}

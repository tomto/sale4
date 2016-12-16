using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Sale4.Controllers.API
{
    public static class BaseConnection
    {
       static string connString = ConfigurationManager.AppSettings["connectionString"];
       

       public static SqlConnection OpenConnection()
        {
            if (string.IsNullOrEmpty(connString))
            {
                connString = "Data Source=172.17.1.106;Initial Catalog=Efruit_CN_SH;User ID=ygtest;Password=ygtest";
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

        public static T Single<T>(string sql)
        {
            IDbConnection conn;
            using (conn = OpenConnection())
            {
                return conn.QueryFirst<T>(sql);
            }
        }

        public static int Count(string sql)
        {
            IDbConnection conn;
            using (conn = OpenConnection())
            {
                return conn.ExecuteScalar<int>(sql);
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

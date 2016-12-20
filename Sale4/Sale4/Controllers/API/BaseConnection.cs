using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperExtensions;
using Sale4.Controllers.API.Models;

namespace Sale4.Controllers.API
{
    public static class BaseConnection
    {
       static string _connString = ConfigurationManager.AppSettings["connectionString"];
       
        public static SqlConnection OpenConnection()
        {
            if (string.IsNullOrEmpty(_connString))
            {
                _connString = "Data Source=172.17.1.106;Initial Catalog=Efruit_CN_SH;User ID=ygtest;Password=ygtest";
            }            
            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();
           return conn;
        }

       #region query

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

        public static T QueryFirst<T>(string sql)
        {
            IDbConnection conn;
            using (conn = OpenConnection())
            {
                return conn.QueryFirst<T>(sql);
            }
        }

        public static T QueryFirst<T>(T model) where T : class 
        {
            IDbConnection conn;
            using (conn = OpenConnection())
            {
                return conn.Get<T>(model);
            }
        }

        public static T Scalar<T>(string sql)
        {
            IDbConnection conn;
            using (conn = OpenConnection())
            {
                return conn.ExecuteScalar<T>(sql);
            }
        }

        #endregion query

        public static int Update<T>(string sql)
        {
            IDbConnection conn;
            using (conn = OpenConnection())
            {
                return conn.Execute(sql);
            }
        }

        public static int Update<T>(T model) where T : class 
        {
            IDbConnection conn;
            using (conn = OpenConnection())
            {
                return conn.Update(model) ? 1 : 0;
            }
        }

        public static int Insert<T>(T model) where T : class
        {
            IDbConnection conn;
            using (conn = OpenConnection())
            {
                return conn.Insert(model);
            }
        }

        //public int Delete<T>(T model)
        //{
        //    IDbConnection conn;
        //    using (conn = OpenConnection())
        //    {
        //        return conn.Execute(model);
        //    }
        //}
    }
}

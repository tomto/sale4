using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Proxy
{
    public class BLL_Fct_ActivityBase
    {

        public Model_Fct_ActivityBase GetBase(Model_Fct_ActivityBase m)
        {
            return BaseConnection.Query<Model_Fct_ActivityBase>("SELECT Fct_ActivityBase.* FROM dbo.Fct_ActivityBase WHERE Disabled = 0 AND ActivityId = @ActivityId", m).FirstOrDefault();
        }

        public Model_Fct_ActivityBase GetBase(string id)
        {
            var sql = "SELECT Fct_ActivityBase. FROM dbo.Fct_ActivityBase WHERE Disabled = 0 AND ActivityId = " + id;
            return BaseConnection.Query<Model_Fct_ActivityBase>(sql).FirstOrDefault();
        }

        public PageResult<Model_Fct_ActivityBase> GetBasePage(int pageSize, int index)
        {
            var page = new PageResult<Model_Fct_ActivityBase>();
            index = index < 0 ? 0 : index;
            var start = index*pageSize;
            var end =  (index + 1)*pageSize;
            var sql = @"
WITH LIST as
  (
    SELECT ROW_NUMBER() OVER(ORDER BY Fct_ActivityBase.Rec_CreateTime DESC) AS ROWNUM,
                          Fct_ActivityBase.*                   
FROM dbo.Fct_ActivityBase WHERE Fct_ActivityBase.Disabled = 0
  )  SELECT * FROM LIST WHERE ROWNUM BETWEEN {0} AND {1}";
            var sqlcount =@"
    SELECT COUNT(1) FROM dbo.Fct_ActivityBase WHERE Fct_ActivityBase.Disabled = 0";
            page.Count = BaseConnection.Count(sqlcount);
            page.List = BaseConnection.Query<Model_Fct_ActivityBase>(string.Format(sql, start, end));
            return page;
        }

        
        public string GetNewActCode()
        {
            var result = string.Empty;
            var now = DateTime.Now;
            var sql = @"
SELECT COUNT(1) FROM Fct_ActivityBase 
AND REC_CreateTime >= @REC_CreateTime1
AND	REC_CreateTime > @REC_CreateTime2";
            var num = BaseConnection.Count(string.Format(sql, now.ToString("yyyy-M-d 00:00:00"), now.ToString("yyyy-M-d 23:59:59")));
            result = now.ToString("yyyyMMdd") + num;
            return result;
        }
		
    }
}



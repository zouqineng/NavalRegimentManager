using NavalRegimentManager.Model;
using NavalRegimentManager.Util;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalRegimentManager.dao
{
    public class DataInfoDao: DbContext<DataInfo>
    {
        public List<DataInfo> getData()
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append(" select m.id as uid,m.name as memberName,r.count,m.CreateDate from member m");
            sqlStr.Append(" left join(");
            sqlStr.Append("     select memberId, count(memberId) count from record");
            sqlStr.Append("     where createDate>= @weekStart and createDate<= @weekEnd");
            sqlStr.Append("     group by memberId");
            sqlStr.Append(" ) r on m.id = r.memberId");
            sqlStr.Append(" where m.isDelete=0");
            sqlStr.Append(" order by r.count desc,m.createDate desc");
            List<SugarParameter> sugarParameters = new List<SugarParameter>();
            sugarParameters.Add(new SugarParameter("weekStart",DateTimeHelper.getWeekStartStr()));
            sugarParameters.Add(new SugarParameter("weekEnd", DateTimeHelper.getWeekEndStr()));
            return GetListBySql(sqlStr.ToString(),sugarParameters);
        }


        

    }
}

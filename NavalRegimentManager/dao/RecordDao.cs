using NavalRegimentManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalRegimentManager.dao
{
    public class RecordDao : DbContext<Record>
    {
        public int getMaxIndex()
        {
            string sqlStr = "select max(r.index) from record r";
            string maxIndex = GetSingleValue(sqlStr);
            return Convert.ToInt32(maxIndex);
        }
    }
}

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalRegimentManager.Model
{
    public class Record
    {
        [SugarColumn(IsPrimaryKey = true)] //主键
        public string Id { get; set; }
        public string MemberId { get; set; }
        public DateTime CreateDate { get; set; }
        public int Index { get; set; }

        public Record() { }

        public Record(string memberId,int index)
        {
            this.MemberId = memberId;
            this.Index = index;
            this.CreateDate = DateTime.Now;
        }

    }
}

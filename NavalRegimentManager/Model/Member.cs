using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalRegimentManager.Model
{
    public class Member
    {
        [SugarColumn(IsPrimaryKey = true)] 
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDelete { get; set; }

        public Member() { }

        public Member(string id,string name)
        {
            this.Id = id;
            this.Name = name;
            this.CreateDate = DateTime.Now;
            this.IsDelete = false;
        }
    }
}

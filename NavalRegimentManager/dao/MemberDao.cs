using NavalRegimentManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalRegimentManager.dao
{
    public class MemberDao : DbContext<Member>
    {

        public Member getMemberById(string id)
        {
            return Db.Queryable<Member>().First(t => t.Id == id);
        }

        public bool saveOrUpdate(Member member)
        {
            Member dbMember = getMemberById(member.Id);
            if (dbMember == null) return Insert(member);
            member.IsDelete = false;
            member.CreateDate = dbMember.CreateDate;
            return Update(member);
        }

        public bool delMemberById(string id)
        {
            Member dbMember = getMemberById(id);
            if (dbMember == null) return false;
            dbMember.IsDelete = true;
            return Update(dbMember);
        }



    }
}

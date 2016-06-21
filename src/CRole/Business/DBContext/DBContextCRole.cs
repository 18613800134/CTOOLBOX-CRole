
namespace CRole.Business.DBContext
{
    using System.Data.Entity;
    using CAM.Common.Data;
    using CRole.Model.Entity;

    public class DBContextCRole : BaseDBContext<DBContextCRole>
    {
        public IDbSet<CRolePopedomGroup> CRolePopedomGroup { get; set; }
        public IDbSet<CRolePopedom> CRolePopedom { get; set; }
        public IDbSet<CRoleRole> CRoleRole { get; set; }
    }
}

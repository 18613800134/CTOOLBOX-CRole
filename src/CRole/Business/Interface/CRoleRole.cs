
namespace CRole.Business.Interface
{
    using System;
    using System.Linq;
    using CAM.Core.Business.Interface;
    using Model.Entity;
    using Model.Filter;
    using DBContext;

    public interface ICRoleCommand : IBaseInterfaceCommand<DBContextCRole>
    {
        long addRole(string Name, string Description);
        void updateRole(long Id, string Name, string Description);
        void updateRolePopedom(long Id, string PopedomIdList);
        void deleteRole(long Id);
        long cloneFromRole(long Id, string NewName, string NewDescription);

        CRoleRole readRole(long Id);
        IQueryable<CRoleRole> readRoleList(CRoleFilter filter);

        string readRolePopedomKeys(string popedomIdList);
    }
}

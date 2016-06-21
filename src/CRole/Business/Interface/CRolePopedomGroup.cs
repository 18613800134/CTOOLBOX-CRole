
namespace CRole.Business.Interface
{
    using System;
    using System.Linq;
    using CAM.Core.Business.Interface;
    using Model.Entity;
    using Model.Filter;
    using DBContext;

    public interface ICPopedomGroupCommand : IBaseInterfaceCommand<DBContextCRole>
    {
        long addPopedomGroup(string Name);
        void updatePopedomGroup(long Id, string Name);
        void deletePopedomGroup(string IdList);
        void recoverPopedomGroup(string IdList);
        void movePopedomGroupToLast(long Id);
        void movePopedomGroupToNext(long Id);
        void movePopedomGroupToTop(long Id);
        void movePopedomGroupToBottom(long Id);

        CRolePopedomGroup readPopedomGroup(long Id);
        IQueryable<CRolePopedomGroup> readPopedomGroupList(CPopedomGroupFilter filter);
    }
}

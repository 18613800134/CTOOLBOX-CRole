
namespace CRole.Business.Interface
{
    using System;
    using System.Linq;
    using CAM.Core.Business.Interface;
    using Model.Entity;
    using Model.Filter;
    using DBContext;

    public interface ICPopedomCommand : IBaseInterfaceCommand<DBContextCRole>
    {
        long addPopedom(string Name, string Key, long GroupId, string Description);
        void updatePopedom(long Id, string Name, string Key, long GroupId, string Description);
        void deletePopedom(string IdList);
        void recoverPopedom(string IdList);
        void movePopedomToLast(long Id);
        void movePopedomToNext(long Id);
        void movePopedomToTop(long Id);
        void movePopedomToBottom(long Id);
        CRolePopedom readPopedom(long Id);
        IQueryable<CRolePopedom> readPopedomList(CPopedomFilter filter);
    }
}

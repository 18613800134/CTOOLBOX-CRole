
namespace CRole.Business.Aggregate
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity.SqlServer;
    using System.Collections.Generic;

    using Model.Entity;
    using Model.Factory;
    using Model.Filter;

    using CAM.Core.Business.Interface;
    using CAM.Core.Business.Aggregate;

    using Interface;
    using Rule;

    using CAM.Common.Data;
    using CAM.Common.Convert;
    using CAM.Common.Error;
    
    public partial class Aggregate : ICPopedomCommand
    {

        public long addPopedom(string Name, string Key, long GroupId, string Description)
        {
            try
            {
                IRepository<CRolePopedom> res = createRepository<CRolePopedom>();
                CRolePopedom dbObj = CPopedomFactory.createPopedom();

                dbObj.Name = Name;
                dbObj.Key = Key;
                dbObj.GroupId = GroupId;
                dbObj.Description = Description;
                dbObj.Order.Index = createNewOrderIndex<CRolePopedom>("GroupId", GroupId);

                dbObj.addValidationRule(new PopedomCannotExistsSameKeyRule(res, dbObj));
                dbObj.addValidationRule(new PopedomCannotExistsSameNameRule(res, dbObj));

                dbObj.validate();
                res.add(dbObj);
                commit();

                groupPopedomCountRefresh(GroupId);

                return dbObj.Id;
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
                return 0;
            }
        }

        public void updatePopedom(long Id, string Name, string Key, long GroupId, string Description)
        {
            try
            {
                IRepository<CRolePopedom> res = createRepository<CRolePopedom>();
                CRolePopedom dbObj = res.read(m => m.Id == Id);

                long OldGroupId = dbObj.GroupId;

                dbObj.Name = Name;
                dbObj.Key = Key;
                dbObj.GroupId = GroupId;
                dbObj.Description = Description;

                dbObj.addValidationRule(new PopedomCannotExistsSameKeyRule(res, dbObj));
                dbObj.addValidationRule(new PopedomCannotExistsSameNameRule(res, dbObj));

                dbObj.validate();
                res.update(dbObj);
                commit();

                if (OldGroupId != GroupId)
                {
                    groupPopedomCountRefresh(OldGroupId);
                    groupPopedomCountRefresh(GroupId);
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void deletePopedom(string IdList)
        {
            try
            {
                IRepository<CRolePopedom> res = createRepository<CRolePopedom>();
                res.delete(typeof(CRolePopedom), IdList);
                commit();

                groupPopedomCountRefreshAfterDeleteOrRecover(IdList);
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void recoverPopedom(string IdList)
        {
            try
            {
                IRepository<CRolePopedom> res = createRepository<CRolePopedom>();
                res.recover(typeof(CRolePopedom), IdList);
                commit();

                groupPopedomCountRefreshAfterDeleteOrRecover(IdList);
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        private void groupPopedomCountRefreshAfterDeleteOrRecover(string IdList)
        {
            try
            {
                IRepository<CRolePopedom> res = createRepository<CRolePopedom>();
                long Id = Convert.ToInt64(IdList.Split(',')[0]);
                CRolePopedom popedom = res.read(m => m.Id == Id);
                long GroupId = popedom.GroupId;
                groupPopedomCountRefresh(GroupId);
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void movePopedomToLast(long Id)
        {
            try
            {
                moveLast<CRolePopedom>(Id);
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void movePopedomToNext(long Id)
        {
            try
            {
                moveNext<CRolePopedom>(Id);
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void movePopedomToTop(long Id)
        {
            try
            {
                moveToTop<CRolePopedom>(Id);
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void movePopedomToBottom(long Id)
        {
            try
            {
                moveToBottom<CRolePopedom>(Id);
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public CRolePopedom readPopedom(long Id)
        {
            IRepository<CRolePopedom> res = createRepository<CRolePopedom>();
            return res.read(m => m.Id == Id);
        }

        public IQueryable<CRolePopedom> readPopedomList(CPopedomFilter filter)
        {
            Expression<Func<CRolePopedom, bool>> lambda = FilterToLambdaBuilder.build<CRolePopedom, CPopedomFilter>(filter);

            //SQL IN...
            if (!string.IsNullOrWhiteSpace(filter.IdList))
            {
                string[] arrIds = filter.IdList.Split(',');
                lambda = lambda.And<CRolePopedom>(m => arrIds.Contains(m.Id.ToString()));
            }
            else
            {
                lambda = lambda.And<CRolePopedom>(m => m.GroupId == filter.GroupId);
            }

            IQueryable<CRolePopedom> result = getDataByFilter<CRolePopedom, CPopedomFilter>(lambda, ref filter);
            return result;
        }
    }
}


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
    
    public partial class Aggregate : ICPopedomGroupCommand
    {

        public long addPopedomGroup(string Name)
        {
            try
            {
                IRepository<CRolePopedomGroup> res = createRepository<CRolePopedomGroup>();
                CRolePopedomGroup dbObj = CPopedomGroupFactory.createGroup();

                dbObj.Name = Name;
                dbObj.Order.Index = createNewOrderIndex<CRolePopedomGroup>();

                dbObj.addValidationRule(new PopedomGroupCannotExistsSameNameRule(res, dbObj));

                dbObj.validate();
                res.add(dbObj);
                commit();

                return dbObj.Id;
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
                return 0;
            }
        }

        public void updatePopedomGroup(long Id, string Name)
        {
            try
            {
                IRepository<CRolePopedomGroup> res = createRepository<CRolePopedomGroup>();
                CRolePopedomGroup dbObj = res.read(m => m.Id == Id);

                dbObj.Name = Name;

                dbObj.addValidationRule(new PopedomGroupCannotExistsSameNameRule(res, dbObj));

                dbObj.validate();
                res.update(dbObj);
                commit();

            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void deletePopedomGroup(string IdList)
        {
            try
            {
                IRepository<CRolePopedomGroup> res = createRepository<CRolePopedomGroup>();
                res.delete(typeof(CRolePopedomGroup), IdList);
                commit();
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void recoverPopedomGroup(string IdList)
        {
            try
            {
                IRepository<CRolePopedomGroup> res = createRepository<CRolePopedomGroup>();
                res.recover(typeof(CRolePopedomGroup), IdList);
                commit();
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void movePopedomGroupToLast(long Id)
        {
            try
            {
                moveLast<CRolePopedomGroup>(Id);
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void movePopedomGroupToNext(long Id)
        {
            try
            {
                moveNext<CRolePopedomGroup>(Id);
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void movePopedomGroupToTop(long Id)
        {
            try
            {
                moveToTop<CRolePopedomGroup>(Id);
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void movePopedomGroupToBottom(long Id)
        {
            try
            {
                moveToBottom<CRolePopedomGroup>(Id);
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        private void groupPopedomCountRefresh(long Id)
        {
            try
            {
                IRepository<CRolePopedom> res_popedom = createRepository<CRolePopedom>();
                long popedomCount = res_popedom.count(m => m.GroupId == Id && m.System.DeleteFlag == false);

                IRepository<CRolePopedomGroup> res = createRepository<CRolePopedomGroup>();
                CRolePopedomGroup dbObj = res.read(m => m.Id == Id);
                dbObj.PopedomCount = popedomCount;
                dbObj.validate();
                res.update(dbObj);
                commit();
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public CRolePopedomGroup readPopedomGroup(long Id)
        {
            IRepository<CRolePopedomGroup> res = createRepository<CRolePopedomGroup>();
            return res.read(m => m.Id == Id);
        }

        public IQueryable<CRolePopedomGroup> readPopedomGroupList(CPopedomGroupFilter filter)
        {
            Expression<Func<CRolePopedomGroup, bool>> lambda = FilterToLambdaBuilder.build<CRolePopedomGroup, CPopedomGroupFilter>(filter);

            if (!filter.withHidden)
            {
                lambda = lambda.And<CRolePopedomGroup>(m => m.IsHidden == false);
            }

            IQueryable<CRolePopedomGroup> result = getDataByFilter<CRolePopedomGroup, CPopedomGroupFilter>(lambda, ref filter);
            return result;
        }
    }
}

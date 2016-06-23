
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
    
    public partial class Aggregate : ICRoleCommand
    {

        public long addRole(long OrganizationId, string Name, string Description)
        {
            try
            {
                IRepository<CRoleRole> res = createRepository<CRoleRole>();
                CRoleRole dbObj = CRoleFactory.createRole();

                dbObj.OrganizationId = OrganizationId;
                dbObj.Name = Name;
                dbObj.Description = Description;

                dbObj.addValidationRule(new RoleCannotExistsSameNameRule(res, dbObj));

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

        public void updateRole(long Id, string Name, string Description)
        {
            try
            {
                IRepository<CRoleRole> res = createRepository<CRoleRole>();
                CRoleRole dbObj = res.read(m => m.Id == Id);

                dbObj.Name = Name;
                dbObj.Description = Description;

                dbObj.addValidationRule(new RoleCannotExistsSameNameRule(res, dbObj));

                dbObj.validate();
                res.update(dbObj);
                commit();

            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void updateRolePopedom(long Id, string PopedomIdList)
        {
            try
            {
                IRepository<CRoleRole> res = createRepository<CRoleRole>();
                CRoleRole dbObj = res.read(m => m.Id == Id);

                dbObj.PopedomIdList = PopedomIdList;

                dbObj.validate();
                res.update(dbObj);
                commit();

            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void deleteRole(long Id)
        {
            try
            {
                IRepository<CRoleRole> res = createRepository<CRoleRole>();
                res.delete(typeof(CRoleRole), Id.ToString());
                commit();
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public long cloneFromRole(long Id, string NewName, string NewDescription)
        {
            try
            {
                CRoleRole role = readRole(Id);
                if (role == null)
                {
                    ErrorHandler.ThrowException("克隆角色失败！");
                    return 0;
                }

                IRepository<CRoleRole> res = createRepository<CRoleRole>();
                CRoleRole dbObj = CRoleFactory.createRole();

                dbObj.OrganizationId = role.OrganizationId;
                dbObj.Name = NewName;
                dbObj.Description = NewDescription;
                dbObj.PopedomIdList = role.PopedomIdList;

                dbObj.addValidationRule(new RoleCannotExistsSameNameRule(res, dbObj));

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

        public CRoleRole readRole(long Id)
        {
            IRepository<CRoleRole> res = createRepository<CRoleRole>();
            return res.read(m => m.Id == Id);
        }

        public IQueryable<CRoleRole> readRoleList(CRoleFilter filter)
        {
            Expression<Func<CRoleRole, bool>> lambda = FilterToLambdaBuilder.build<CRoleRole, CRoleFilter>(filter);

            lambda = lambda.And<CRoleRole>(m => m.OrganizationId == 0);
            if (filter.OrganizationId > 0)
            {
                lambda = lambda.And<CRoleRole>(m => m.OrganizationId == filter.OrganizationId);
            }

            IQueryable<CRoleRole> result = getDataByFilter<CRoleRole, CRoleFilter>(lambda, ref filter);
            return result;
        }


        public string readRolePopedomKeys(string popedomIdList)
        {
            try
            {
                StringBuilder sbSQL = new StringBuilder("");
                sbSQL.AppendFormat("declare @popedomKeyList varchar(max);");
                sbSQL.AppendFormat("set @popedomKeyList = '';");
                sbSQL.AppendFormat("select @popedomKeyList = @popedomKeyList + p.[Key]+',' from CRolePopedom p where p.Id in ({0});", popedomIdList);
                sbSQL.AppendFormat("select @popedomKeyList;");

                string popedomKeyList = this.dbContext.Database.SqlQuery(typeof(string), sbSQL.ToString()).Cast<string>().FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(popedomKeyList))
                {
                    popedomKeyList = popedomKeyList.Substring(0, popedomKeyList.Length - 1);
                }

                return popedomKeyList;
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
                return "";
            }
        }
    }
}

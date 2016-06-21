
namespace CRole.Model.Factory
{
    using CAM.Core.Model.Entity;
    using Entity;
    using System;

    public class CRoleFactory
    {
        public static CRoleRole createRole()
        {
            CRoleRole role = EntityBuilder.build<CRoleRole>();
            role.Name = "";
            role.Description = "";
            role.PopedomIdList = "";
            return role;
        }
    }
}

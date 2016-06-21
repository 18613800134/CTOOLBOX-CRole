
namespace CRole.Model.Factory
{
    using CAM.Core.Model.Entity;
    using Entity;
    using System;

    public class CPopedomFactory
    {
        public static CRolePopedom createPopedom()
        {
            CRolePopedom popedom = EntityBuilder.build<CRolePopedom>();
            popedom.GroupId = 0;
            popedom.Key = "";
            popedom.Name = "";
            popedom.Description = "";
            return popedom;
        }
    }
}

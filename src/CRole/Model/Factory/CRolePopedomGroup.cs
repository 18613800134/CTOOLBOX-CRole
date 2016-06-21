
namespace CRole.Model.Factory
{
    using CAM.Core.Model.Entity;
    using Entity;
    using System;

    public class CPopedomGroupFactory
    {
        public static CRolePopedomGroup createGroup()
        {
            CRolePopedomGroup group = EntityBuilder.build<CRolePopedomGroup>();
            group.Name = "";
            group.IsHidden = false;
            group.PopedomCount = 0;
            return group;
        }
    }
}

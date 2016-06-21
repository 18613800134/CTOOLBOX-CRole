
namespace CRole.Business.Rule
{
    using System.ComponentModel.DataAnnotations;
    using CAM.Core.Business.Rule;
    using CAM.Core.Model.Validation;
    using CAM.Common.Data;
    using Model.Entity;

    public class RoleCannotExistsSameNameRule : BaseRule<CRoleRole>
    {
        public RoleCannotExistsSameNameRule(IRepository<CRoleRole> res, CRoleRole checkObj)
            : base(res, checkObj)
        {

        }

        public override ValidationResult validate()
        {
            ValidationResult result = ValidationResult.Success;
            if (_res.exists(m => m.Id != _checkObj.Id && m.System.DeleteFlag == false && m.Name == _checkObj.Name && (m.OrganizationId == _checkObj.OrganizationId || m.OrganizationId == 0)))
            {
                result = createValidationResult("Name", string.Format("【{0}】这个角色已经存在，请使用其他角色名！", _checkObj.Name));
            }
            return result;
        }
    }
}

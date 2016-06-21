
namespace CRole.Business.Rule
{
    using System.ComponentModel.DataAnnotations;
    using CAM.Core.Business.Rule;
    using CAM.Core.Model.Validation;
    using CAM.Common.Data;
    using Model.Entity;

    public class PopedomGroupCannotExistsSameNameRule : BaseRule<CRolePopedomGroup>
    {
        public PopedomGroupCannotExistsSameNameRule(IRepository<CRolePopedomGroup> res, CRolePopedomGroup checkObj)
            : base(res, checkObj)
        {

        }

        public override ValidationResult validate()
        {
            ValidationResult result = ValidationResult.Success;
            if (_res.exists(m => m.Id != _checkObj.Id && m.System.DeleteFlag == false && m.Name == _checkObj.Name))
            {
                result = createValidationResult("Name", string.Format("【{0}】已经存在，请使用不同的组别名称！", _checkObj.Name));
            }
            return result;
        }
    }
}

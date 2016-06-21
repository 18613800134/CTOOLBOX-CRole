
namespace CRole.Business.Rule
{
    using System.ComponentModel.DataAnnotations;
    using CAM.Core.Business.Rule;
    using CAM.Core.Model.Validation;
    using CAM.Common.Data;
    using Model.Entity;

    public class PopedomCannotExistsSameKeyRule : BaseRule<CRolePopedom>
    {
        public PopedomCannotExistsSameKeyRule(IRepository<CRolePopedom> res, CRolePopedom checkObj)
            : base(res, checkObj)
        {

        }


        public override ValidationResult validate()
        {
            ValidationResult result = ValidationResult.Success;
            if (_res.exists(m => m.Id != _checkObj.Id && m.System.DeleteFlag == false && m.Key == _checkObj.Key))
            {
                result = createValidationResult("Key", string.Format("权限KEY值【{0}】已经存在，请使用唯一的KEY值！", _checkObj.Key));
            }
            return result;
        }
    }

    public class PopedomCannotExistsSameNameRule : BaseRule<CRolePopedom>
    {
        public PopedomCannotExistsSameNameRule(IRepository<CRolePopedom> res, CRolePopedom checkObj)
            : base(res, checkObj)
        {

        }


        public override ValidationResult validate()
        {
            ValidationResult result = ValidationResult.Success;
            if (_res.exists(m => m.Id != _checkObj.Id && m.System.DeleteFlag == false && m.Name == _checkObj.Name))
            {
                result = createValidationResult("Name", string.Format("权限名称【{0}】已经存在，请使用唯一的名称！", _checkObj.Name));
            }
            return result;
        }
    }

}

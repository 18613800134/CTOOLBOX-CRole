
namespace CRole.Model.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using CAM.Core.Model.Entity;

    public class CRoleRole : BaseEntityNormal
    {
        /// <summary>
        /// 角色的所属组织机构，如果为0，表示为系统定义的角色，无法被编辑或删除
        /// </summary>
        [Required]
        [Index(IsClustered = false, IsUnique = false)]
        public long OrganizationId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} 的长度为 {2} 到 {1} 个字符。", MinimumLength = 1)]
        public string Name { get; set; }


        [MaxLength(100)]
        public string Description { get; set; }

        [MaxLength(4000)]
        public string PopedomIdList { get; set; }
    }
}

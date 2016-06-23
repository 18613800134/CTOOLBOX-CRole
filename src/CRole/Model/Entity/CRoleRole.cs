
namespace CRole.Model.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using CAM.Core.Model.Entity;

    public class CRoleRole : BaseEntityNormal
    {
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

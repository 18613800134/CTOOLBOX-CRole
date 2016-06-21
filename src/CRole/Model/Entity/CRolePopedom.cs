
namespace CRole.Model.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using CAM.Core.Model.Entity;

    public class CRolePopedom : BaseEntityOrder
    {
        [Required]
        [Index(IsClustered = false, IsUnique = false)]
        public long GroupId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} 的长度为 {2} 到 {1} 个字符。", MinimumLength = 1)]
        [Index(IsClustered = false, IsUnique = false)]
        public string Key { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} 的长度为 {2} 到 {1} 个字符。", MinimumLength = 1)]
        public string Name { get; set; }


        [MaxLength(100)]
        public string Description { get; set; }
    }
}

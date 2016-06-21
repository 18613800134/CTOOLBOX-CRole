
namespace CRole.Model.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using CAM.Core.Model.Entity;

    public class CRolePopedomGroup : BaseEntityOrder
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} 的长度为 {2} 到 {1} 个字符。", MinimumLength = 1)]
        public string Name { get; set; }

        [Index(IsClustered = false, IsUnique = false)]
        public bool IsHidden { get; set; }

        public long PopedomCount { get; set; }
    }
}

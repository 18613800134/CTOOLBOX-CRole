
namespace CRole.Model.Filter
{
    using System;
    using CAM.Core.Model.Filter;

    public class CPopedomFilter : BaseFilter
    {
        public long GroupId { get; set; }
        public string IdList { get; set; }
    }
}

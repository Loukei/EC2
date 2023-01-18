using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EC2.Models
{
    /// <summary>
    /// A model for ProductController GetAll parameter
    /// </summary>
    public class ProductsRequestModel
    {
        [StringLength(40)]
        [DefaultValue(null)]
        public string? name { get; set; } = null;

        /// TODO: use guid rather than direct mapping to pk
        [Range(1, int.MaxValue)]
        [DefaultValue(null)]
        public int? supplierID { get; set; } = null;

        /// TODO: use guid rather than direct mapping to pk
        [Range(1, int.MaxValue)]
        [DefaultValue(null)]
        public int? categoryID { get; set; } = null;
        
        [Range(1,int.MaxValue)]
        [DefaultValue(1)]
        public int pageIndex { get; set; } = 1;

        [Range(1, int.MaxValue)]
        [DefaultValue(10)]
        public int pageSize { get; set; } = 10;
    }
}

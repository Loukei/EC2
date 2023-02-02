using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EC2.Controllers.Implement;

namespace EC2.Models
{
    /// <summary>
    /// An view model for <see cref="ProductController.GetAll(ProductPageQueryVM)"/> parameter
    /// </summary>
    public class ProductPageQueryVM
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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EC2.Models
{
    /// <summary>
    /// 驗證Category
    /// </summary>
    public class CategoryViewModel
    {
        //public int CategoryID { get; set; }
        [StringLength(15)]
        [DefaultValue("")]
        public string CategoryName { get; set; } = String.Empty;
        [DefaultValue("")]
        public string Description { get; set; } = String.Empty;
        //public IFormFile? Picture { get; set; } = null;
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EC2.Models
{
    /// <summary>
    /// ViewModel
    /// - 用來限制API開放出來的資料型式，有些欄位不需要給API user看到
    /// - 驗證資料邏輯
    /// </summary>
    public class ProductViewModel
    {

        //[Required]
        //[Range(1, int.MaxValue)]
        //[DefaultValue(1)]
        //public int ProductID { get; set; } = 1;
        
        [Required]
        [StringLength(40)]
        [DefaultValue("")]
        public string ProductName { get; set; } = string.Empty;
        
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int SupplierID { get; set; } = 1;
        
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int CategoryID { get; set; } = 1;
        
        [StringLength(20)]
        [DefaultValue("")]
        public string QuantityPerUnit { get; set; } = string.Empty;
        
        [Range(0, int.MaxValue)] //range deta anotation accept only int
        [DefaultValue(1)]
        public decimal UnitPrice { get; set; } = decimal.Zero;
        
        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public int UnitsInStock { get; set; } = 0;
        
        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public int UnitsOnOrder { get; set; } = 0;
        
        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public int ReorderLevel { get; set; } = 0;

        public byte Discontinued { get; set; } = 0;

        // When object deleted, simply set status to false, default is true
        //public bool Status { get; set; }

        // Updated by user, default to admin
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public int UpdatedBy { get; set; } = 1;

        // default to admin
        //public DateTime UpdatedDate { get; set; }
    }
    
}

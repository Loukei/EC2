namespace EC2.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public int? UnitsOnOrder { get; set; }
        public int? ReorderLevel { get; set; }
        public byte? Discontinued { get; set; }
        // When object deleted, simply set status to false, default is true
        public bool Status { get; set; }
        // Updated by user, default to admin
        public int UpdatedBy { get; set; }
        // Modify datetime
        public DateTime UpdatedDate { get; set; }
    }
}

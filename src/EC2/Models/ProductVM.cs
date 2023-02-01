using EC2.Models.DTOs.Northwind;

namespace EC2.Models
{
    /// <summary>
    /// An DTO class for <see cref="Repository.ProductRepository"/> as return value.
    /// This DTO blocks some Property from <see cref="Product"/>
    /// Relate Model:
    /// <seealso cref="Supplier"/>
    /// <seealso cref="Category"/>
    /// </summary>
    public class ProductVM
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        public string? QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        //public bool? Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// A foreign attribute follow by <see cref="ProductVM.CategoryId"/>.
        /// see also <seealso cref="Models.DTOs.Northwind.Category"/>
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// A foreign attribute follow by <see cref="ProductVM.SupplierId"/>.
        /// see also <seealso cref="Models.DTOs.Northwind.Supplier"/>
        /// </summary>
        public string? SupplierName { get; set; }

        //public int CreatedBy { get; set; }

        //public int? UpdatedBy { get; set; }

        //public virtual Category? Category { get; set; }

        //public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

        //public virtual Supplier? Supplier { get; set; }
    }
}

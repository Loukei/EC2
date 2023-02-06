using NorthWindEFLibrary.Context;
using NorthWindEFLibrary.DTOs;

namespace EC2.Repository.Implement
{
    public class SuppilierRepository : ISuppilierRepository
    {
        private readonly NorthwindContext _northwindContext;

        public SuppilierRepository(NorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }

        public Supplier GetSuppilierByID(int supplierId)
        {
            try
            {
                var supplier = _northwindContext.Suppliers.Single(s => s.SupplierId == supplierId);
                return supplier;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

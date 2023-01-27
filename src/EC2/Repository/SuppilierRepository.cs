using EC2.Context;
using EC2.Models.EFCore;

namespace EC2.Repository
{
    public interface ISuppilierRepository
    {
        Supplier GetSuppilierByID(int supplierId);
    }

    public class SuppilierRepository: ISuppilierRepository
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

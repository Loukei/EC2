using EC2.Models.DTOs.Northwind;

namespace EC2.Repository
{
    public interface ISuppilierRepository
    {
        Supplier GetSuppilierByID(int supplierId);
    }
}

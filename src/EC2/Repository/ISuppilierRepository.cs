using NorthWindLibrary.DTOs;

namespace EC2.Repository
{
    public interface ISuppilierRepository
    {
        Supplier GetSuppilierByID(int supplierId);
    }
}

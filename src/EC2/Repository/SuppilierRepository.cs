using EC2.Context;
using EC2.Models.EFcore;
using Dapper;

namespace EC2.Repository
{
    public interface ISuppilierRepository
    {
        Supplier GetSuppilierByID(int supplierId);
    }

    public class SuppilierRepository: ISuppilierRepository
    {
        //private readonly DapperContext _dapperContext;
        private readonly NorthwindContext _northwindContext;

        public SuppilierRepository(//DapperContext dapperContext, 
            NorthwindContext northwindContext)
        {
            //_dapperContext = dapperContext;
            _northwindContext = northwindContext;
            //_northwindContext = northwindContext;
        }

        public Supplier GetSuppilierByID(int supplierId)
        {
            //using (var conn = _dapperContext.CreateConnection())
            //{
            //    string sql = @"SELECT * FROM Suppliers WHERE [SupplierID] = @supplierId";
            //    var result = conn.QuerySingleOrDefault<Supplier>(sql, new { supplierId = supplierId });
            //    return result;
            //}
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

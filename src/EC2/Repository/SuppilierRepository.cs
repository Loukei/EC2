using EC2.Context;
using EC2.Models;
using Dapper;

namespace EC2.Repository
{
    public interface ISuppilierRepository
    {
        Suppilier GetSuppilierByID(int supplierId);
    }

    public class SuppilierRepository: ISuppilierRepository
    {
        private readonly DapperContext _dapperContext;

        public SuppilierRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public Suppilier GetSuppilierByID(int supplierId)
        {
            using (var conn = _dapperContext.CreateConnection())
            {
                string sql = @"SELECT * FROM Suppliers WHERE [SupplierID] = @supplierId";
                var result = conn.QuerySingleOrDefault<Suppilier>(sql, new { supplierId = supplierId });
                return result;
            }
        }
    }
}

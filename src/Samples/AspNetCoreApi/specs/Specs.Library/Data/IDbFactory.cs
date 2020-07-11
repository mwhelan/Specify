using ApiTemplate.Api.Infrastructure.Persistence;

namespace Specs.Library.Data
{
    public interface IDbFactory
    {
        AppDbContext CreateContext(bool beginTransaction = false);
        void CreateDatabase();
        void DeleteDatabase();
        void ResetData();
    }
}
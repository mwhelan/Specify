using Specify.Configuration;

namespace Specs.Library.Data
{
    public class DatabaseApplicationAction : IPerApplicationAction
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDb _db;

        public DatabaseApplicationAction(IDbFactory dbFactory, IDb db)
        {
            _dbFactory = dbFactory;
            _db = db;
        }

        public void Before()
        {
            if (TestSettings.ShouldCreateDatabase)
            {
                _dbFactory.CreateDatabase();
            }

            EntityExtensions.Db = _db;
        }

        public void After()
        {

        }

        public int Order { get; set; } = 3;
    }
}
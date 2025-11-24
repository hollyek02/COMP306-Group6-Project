using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using COMP306_Group_Project_Group_6.Models;

namespace COMP306_Group_Project_Group_6.Repositories
{
    public class DynamoDbRepository<T> : IRepository<T> where T : class
    {
        private readonly IDynamoDBContext _context;
        private readonly string _tableName;

        public DynamoDbRepository(IAmazonDynamoDB dynamoDbClient, string tableName)
        {
            _context = new DynamoDBContext(dynamoDbClient);
            _tableName = tableName;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var conditions = new List<ScanCondition>();
            var config = new DynamoDBOperationConfig { OverrideTableName = _tableName };
            return await _context.ScanAsync<T>(conditions, config).GetRemainingAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var config = new DynamoDBOperationConfig { OverrideTableName = _tableName };
            return await _context.LoadAsync<T>(id, config);
        }

        public async Task<T> CreateAsync(T entity)
        {
            var config = new DynamoDBOperationConfig { OverrideTableName = _tableName };
            await _context.SaveAsync(entity, config);
            return entity;
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            var config = new DynamoDBOperationConfig { OverrideTableName = _tableName };
            await _context.SaveAsync(entity, config);
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var config = new DynamoDBOperationConfig { OverrideTableName = _tableName };
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            await _context.DeleteAsync<T>(id, config);
            return true;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }
    }
}

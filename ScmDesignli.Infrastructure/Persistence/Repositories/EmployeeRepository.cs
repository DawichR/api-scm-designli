using ScmDesignli.Application.Interfaces.Repositories;
using ScmDesignli.Domain.Entities;

namespace ScmDesignli.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Employee repository implementation with specific business logic
    /// </summary>
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            var employees = await GetAllAsync();

            return employees.Any(e =>
                !e.IsDeleted &&
                e.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                (!excludeId.HasValue || e.Id != excludeId.Value));
        }

        public async Task<Employee?> GetByEmailAsync(string email)
        {
            var employees = await GetAllAsync();
            return employees.FirstOrDefault(e =>
                !e.IsDeleted &&
                e.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public override Task<IEnumerable<Employee>> GetAllAsync()
        {
            // Only return non-deleted employees
            var activeEmployees = _entities.Values.Where(e => !e.IsDeleted).ToList();
            return Task.FromResult<IEnumerable<Employee>>(activeEmployees);
        }

        public override Task<Employee?> GetByIdAsync(int id)
        {
            _entities.TryGetValue(id, out var entity);

            // Return null if entity is deleted
            if (entity?.IsDeleted == true)
            {
                return Task.FromResult<Employee?>(null);
            }

            return Task.FromResult(entity);
        }

        public override Task<Employee> AddAsync(Employee entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.IsDeleted = false;
            return base.AddAsync(entity);
        }

        public override Task<Employee?> UpdateAsync(int id, Employee entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            return base.UpdateAsync(id, entity);
        }

        public override Task<bool> DeleteAsync(int id)
        {
            if (!_entities.TryGetValue(id, out var entity))
            {
                return Task.FromResult(false);
            }

            // Soft delete
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            _entities[id] = entity;

            return Task.FromResult(true);
        }

        public override Task<int> CountAsync()
        {
            return Task.FromResult(_entities.Values.Count(e => !e.IsDeleted));
        }
    }
}

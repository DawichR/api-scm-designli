using ScmDesignli.Domain.Entities;

namespace ScmDesignli.Application.Interfaces.Repositories
{
    /// <summary>
    /// Employee specific repository interface
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// Check if an email already exists in the system
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <param name="excludeId">Employee ID to exclude from the check (for updates)</param>
        /// <returns>If email exists</returns>
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);

        /// <summary>
        /// Get employee by email
        /// </summary>
        /// <param name="email">Email to search</param>
        /// <returns>Employee by email if exists</returns>
        Task<Employee?> GetByEmailAsync(string email);
    }
}

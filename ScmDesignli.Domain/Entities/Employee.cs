using ScmDesignli.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ScmDesignli.Domain.Entities
{
    /// <summary>
    /// Employee Class
    /// </summary>
    public sealed class Employee
    {
        /// <summary>
        /// Identity Number
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Name of employee
        /// </summary>
        /// <example>Juan</example>
        public required string Name { get; set; }

        /// <summary>
        /// Lastname of employee
        /// </summary>
        /// <example>Fernandez</example>
        public required string LastName { get; set; }

        /// <summary>
        /// Birthday of employee
        /// </summary>
        /// <example>11/29/2000</example>
        public required DateTime Birthday { get; set; }

        /// <summary>
        /// Email of employee
        /// </summary>
        /// <example>juanfernandez@designli.co</example>
        [EmailAddress]
        public required string Email { get; set; }

        /// <summary>
        /// Department of employee
        /// </summary>
        /// <example>IT</example>
        public required Department Department { get; set; }

        /// <summary>
        /// Indicates if the employee is deleted (soft delete)
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Date and time when the employee was deleted
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Date and time when the employee was created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Date and time when the employee was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}

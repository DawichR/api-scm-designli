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
        /// Derpament of employee
        /// </summary>
        /// <example>IT</example>
        public required Department Department { get; set; }
    }
}

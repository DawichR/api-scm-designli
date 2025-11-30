using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScmDesignli.Domain.Enums
{
    /// <summary>
    /// Deparments for employees
    /// </summary>
    public enum Department
    {
        /// <summary>
        /// Information Technology
        /// </summary>
        [Description("Information Technology")]
        IT = 1,
        
        /// <summary>
        /// Human Resources
        /// </summary>
        [Description("Human Resources")]
        HumanResources = 2,
        
        /// <summary>
        /// Finance
        /// </summary>
        [Description("Finance")]
        Finance = 3,
        
        /// <summary>
        /// Operations
        /// </summary>
        [Description("Operations")]
        Operations = 4,
        
        /// <summary>
        /// Sales
        /// </summary>
        [Description("Sales")]
        Sales = 5,
        
        /// <summary>
        /// Marketing
        /// </summary>
        [Description("Marketing")]
        Marketing = 6,
        
        /// <summary>
        /// Management
        /// </summary>
        [Description("Management")]
        Management = 7,
        
        /// <summary>
        /// Customer Service
        /// </summary>
        [Description("Customer Service")]
        CustomerService = 8
    }
}

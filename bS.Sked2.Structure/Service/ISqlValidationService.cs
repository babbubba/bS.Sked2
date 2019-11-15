using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Structure.Service
{
    public interface ISqlValidationService
    {
        /// <summary>
        /// Determines whether [is SQL query valid] [the specified SQL].
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>
        ///   <c>true</c> if [is SQL query valid] [the specified SQL]; otherwise, <c>false</c>.
        /// </returns>
        bool IsSQLQueryValid(string sql, out List<string> errors);
    }
}

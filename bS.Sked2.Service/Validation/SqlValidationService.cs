using bS.Sked2.Structure.Service;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace bS.Sked2.Service.Validation
{
   

    public class SqlValidationService : IService, ISqlValidationService
    {
        private readonly ILogger logger;

        public SqlValidationService(ILogger logger)
        {
            this.logger = logger;
        }

        public bool IsSQLQueryValid(string sql, out List<string> errors)
        {
            errors = new List<string>();
            TSql140Parser parser = new TSql140Parser(false);
            TSqlFragment fragment;
            IList<ParseError> parseErrors;

            using (TextReader reader = new StringReader(sql))
            {
                fragment = parser.Parse(reader, out parseErrors);
                if (parseErrors != null && parseErrors.Count > 0)
                {
                    errors = parseErrors.Select(e => e.Message).ToList();
                    return false;
                }
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Connection
{
    public sealed class Command
    {
        internal string Query { get; private set; }
        internal bool IsStoredProcedure { get; private set; }
        internal Dictionary<string, object> Parameters { get; private set; }

        public Command(string query, bool isStoredProcedure = false)
        {
            Query = query;
            IsStoredProcedure = isStoredProcedure;
            Parameters = new Dictionary<string, object>();
        }

        public void AddParameter(string parmeterName, object value)
        {
            Parameters.Add(parmeterName, value ?? DBNull.Value);
        }
    }
}

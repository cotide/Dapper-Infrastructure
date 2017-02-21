using System;
using System.ComponentModel;
using System.Text;

namespace DapperInfrastructure.DapperWrapper.Helpers
{
    public class ParameterHelper
    {
        public enum QueryType
        {
            Insert,
            Update
        }

        public static string BuildParams(string parms, QueryType queryType)
        {
            var inParms = parms.Trim().Replace("[", String.Empty).Replace("]", String.Empty).Split(',');
            var outStr = new StringBuilder();
            foreach (var inStr in inParms)
            {
                if (outStr.Length > 0) outStr.Append(", ");
                switch (queryType)
                {
                    case QueryType.Insert:
                        outStr.Append("@");
                        break;
                    case QueryType.Update:
                        outStr.Append(inStr.Trim());
                        outStr.Append("=@");
                        break;
                    default:
                        throw new InvalidEnumArgumentException("QueryType not found.");
                }
                outStr.Append(inStr.Trim());
            }
            return outStr.ToString();


        }
    }
}
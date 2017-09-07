using System;

namespace DapperInfrastructure.Extensions.Extensions
{
    public static class GuidExtensions
    {
        public static string ToId(this Guid guid)
        {
            return guid.ToString("N");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class JWTSettings
    {
        public const string SectionName = "JwtSettings";
        public string Secret { get; init; } = null!;
        public double ExpiryDays { get; init; }
        public string Issuer { get; init; } = null!;
        public string Audience { get; init; } = null!;
    }
}

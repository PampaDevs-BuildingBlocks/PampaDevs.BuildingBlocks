using System;
using System.Collections.Generic;
using System.Text;

namespace PampaDevs.BuildingBlocks.Security.Jwt
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int Expiration { get; set; }
        public string Issuer { get; set; }
        public List<string> Issuers { get; set; }
        public string Audience { get; set; }
        public List<string> Audiences { get; set; }
    }
}

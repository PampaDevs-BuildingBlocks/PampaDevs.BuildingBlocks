using System;
using System.Collections.Generic;
using System.Text;

namespace PampaDevs.BuildingBlocks.Identity.Jwt
{
    public class UserToken
    {
        public string Token { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }
}

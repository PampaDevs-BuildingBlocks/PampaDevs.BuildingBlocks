using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PampaDevs.BuildingBlocks.Security.Jwt
{
    public class JwtTokenBuilder<TIdentityUser, TKey>
        where TIdentityUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        private JwtIdentityUserTokenBuilder<TIdentityUser, TKey> _identityTokenBuilder;
        private List<Claim> _claims;

        public ClaimsIdentity IdentityClaims
        {
            get
            {
                var claimsIdentity = new ClaimsIdentity(_claims);
                
                if (_identityTokenBuilder != null)
                {
                    claimsIdentity.AddClaims(_identityTokenBuilder.Claims);
                }

                return claimsIdentity;
            }
        }

        public JwtTokenBuilder()
        {
            _claims = new List<Claim>();
        }

        public JwtTokenBuilder<TIdentityUser, TKey> AddClaims(ICollection<Claim> claims)
        {
            _claims.AddRange(claims);

            return this;
        }

        public JwtIdentityUserTokenBuilder<TIdentityUser, TKey> AddIdentityUserById(UserManager<IdentityUser<TKey>> userManager, string userId)
        {
            var user = userManager.FindByIdAsync(userId).Result;
            return CreateIdentityUserTokenBuilder(userManager, user);
        }

        public JwtIdentityUserTokenBuilder<TIdentityUser, TKey> AddIdentityUserByUserName(UserManager<IdentityUser<TKey>> userManager, string userName)
        {
            var user = userManager.FindByNameAsync(userName).Result;
            return CreateIdentityUserTokenBuilder(userManager, user);
        }

        public JwtIdentityUserTokenBuilder<TIdentityUser, TKey> AddIdentityUserByEmail(UserManager<IdentityUser<TKey>> userManager, string userEmail)
        {
            var user = userManager.FindByEmailAsync(userEmail).Result;
            return CreateIdentityUserTokenBuilder(userManager, user);
        }

        private JwtIdentityUserTokenBuilder<TIdentityUser, TKey> CreateIdentityUserTokenBuilder(UserManager<IdentityUser<TKey>> userManager, IdentityUser<TKey> user)
        {
            _identityTokenBuilder = new JwtIdentityUserTokenBuilder<TIdentityUser, TKey>(userManager, user);

            return _identityTokenBuilder;
        }
    }

    public sealed class JwtIdentityUserTokenBuilder<TIdentityUser, TKey>
        where TIdentityUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly UserManager<IdentityUser<TKey>> _userManager;
        private readonly IdentityUser<TKey> _user;
        internal List<Claim> Claims { get; private set; }

        public JwtIdentityUserTokenBuilder(UserManager<IdentityUser<TKey>> userManager, IdentityUser<TKey> user)
        {
            _userManager = userManager;
            _user = user;
            Claims = new List<Claim>();
        }

        public JwtIdentityUserTokenBuilder<TIdentityUser, TKey> AddUserRoles()
        {
            var userRoles = _userManager.GetRolesAsync(_user).Result;
            userRoles.ToList().ForEach(r => Claims.Add(new Claim("role", r)));

            return this;
        }

        public JwtIdentityUserTokenBuilder<TIdentityUser, TKey> AddUserClaims()
        {
            var userClaims = _userManager.GetClaimsAsync(_user).Result;
            Claims.AddRange(userClaims);

            return this;
        }
    }

    public sealed class JwtTokenBuilder : JwtTokenBuilder<IdentityUser> { }

    public class JwtTokenBuilder<TIdentityUser> : JwtTokenBuilder<TIdentityUser, string> where TIdentityUser : IdentityUser<string> { }
}

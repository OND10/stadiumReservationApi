using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using Reservationpitch.Domain.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Reservationpitch.Infustracture.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<SystemUser> _userManager;
        private readonly IUserManagerRepository _userManagerRepository;
        //I used IOptions to get the configuration of the JwtOptions instrance plus to help registering DI
        public TokenRepository(
            IConfiguration configuration, 
            IOptions<JwtOptions> jwtOptions, 
            UserManager<SystemUser> userManager,
            IUserManagerRepository userManagerRepository
            )
        {
            _configuration = configuration;
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
            _userManagerRepository = userManagerRepository;
        }
        public async Task<string> CreateJWTTokenAsync(SystemUser user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Name, user.UserName!),
                new Claim("userid", user.Id)
            };

            // ✅ Add role claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // ✅ 🔥 Add user claims from Identity (your stadium permissions)
            //var identityClaims = await _userManager.GetClaimsAsync(user);
            //claims.AddRange(identityClaims);

            var dbClaims = await _userManagerRepository.GetUserAssignedClaimsAsync(user.Id);
            claims.AddRange(dbClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpiration),
                signingCredentials: credentials
            );

            return tokenHandler.WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        //This to return user principle from the expired refresh token
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true, // You might want to validate the audience and issuer
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
                ValidateLifetime = true, // Here we are saying that we don't care about the token's expiration date
                //because idea of it is to make access token short-lived
                //refresh the token before it expires
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}

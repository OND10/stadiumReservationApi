using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using Reservationpitch.Domain.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CMS.Infustracture.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;
        private readonly JwtOptions _jwtOptions;
        //I used IOptions to get the configuration of the JwtOptions instrance plus to help registering DI
        public TokenRepository(IConfiguration configuration, IOptions<JwtOptions> jwtOptions)
        {
            _configuration = configuration;
            _jwtOptions = jwtOptions.Value;
        }
        public string CreateJWTTokenAsync(SystemUser user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            //Initiliaze Claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Name,user.UserName)
            };

            //Adding Claims to the roles for that user
            claims.AddRange(roles.Select(role=> new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));

            var credentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
           
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpiration),
                SigningCredentials = credentails
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
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

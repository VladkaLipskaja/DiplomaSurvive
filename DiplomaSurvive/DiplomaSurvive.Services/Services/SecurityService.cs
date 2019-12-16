using System;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using DiplomaSurvive.Models;
using Microsoft.IdentityModel.Tokens;

namespace DiplomaSurvive.Services
{  
     /// <summary>
    /// Security service
    /// </summary>
    /// <seealso cref="ISecurityService" />
    public class SecurityService : ISecurityService
    {
        /// <summary>
        /// The application settings
        /// </summary>
        private readonly AppSettings _appSettings;

        /// <summary>
        /// The iteration count
        /// </summary>
        private readonly int iterationCount;

        /// <summary>
        /// The number bytes requested
        /// </summary>
        private readonly int numBytesRequested;

        // <summary>
        // Initializes a new instance of the<see cref="SecurityService"/> class.
        // </summary>
        // <param name = "appSettings" > The application settings.</param>
        public SecurityService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            iterationCount = 10000;
            numBytesRequested = 32;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The user identifier.
        /// </returns>
        public int GetUserId(ClaimsPrincipal user)
        {
            Claim userId = user.FindFirst(ClaimTypes.Sid);

            if (userId == null)
            {
                throw new SecurityException(SecurityErrorCode.UnrecognizedUser);
            }

            int id = Convert.ToInt32(userId.Value);

            return id;
        }

        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The hashed value.
        /// </returns>
        public string GetHash(string value)
        {
            byte[] salt = Encoding.ASCII.GetBytes(_appSettings.Salt);
            byte[] hash = KeyDerivation.Pbkdf2(value, salt, KeyDerivationPrf.HMACSHA512, iterationCount, numBytesRequested);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The token.
        /// </returns>
        public string GetToken(int id)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            Claim claim = new Claim(ClaimTypes.Sid, id.ToString());
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            DateTime expires = DateTime.UtcNow.AddDays(7);
            ClaimsIdentity subject = new ClaimsIdentity(new Claim[] { claim });
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                SigningCredentials = signingCredentials
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
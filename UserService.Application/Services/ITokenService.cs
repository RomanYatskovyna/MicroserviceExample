using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace UserService.Application.Services;
public interface ITokenService
{
    public string GenerateAccessToken(string username, string role);
    public string GenerateRefreshToken(string username);
    /// <summary>
    /// Reads and validates a 'JSON Web Token' (JWT) encoded as a JWS or JWE in Compact Serialized Format.
    /// </summary>
    /// <param name="token">the JWT encoded as JWE or JWS</param>
    /// <param name="validationParameters">Contains validation parameters for the <see cref="JwtSecurityToken"/>.</param>
    /// <param name="validatedToken">The <see cref="JwtSecurityToken"/> that was validated.</param>
    /// <exception cref="ArgumentNullException"><paramref name="token"/> is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="validationParameters"/> is null.</exception>
    /// <exception cref="ArgumentException"><paramref name="token"/>.Length is greater than <see cref="TokenHandler.MaximumTokenSizeInBytes"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="token"/> does not have 3 or 5 parts.</exception>
    /// <exception cref="ArgumentException"><see cref="CanReadToken(string)"/> returns false.</exception>
    /// <exception cref="SecurityTokenDecryptionFailedException"><paramref name="token"/> was a JWE was not able to be decrypted.</exception>
    /// <exception cref="SecurityTokenEncryptionKeyNotFoundException"><paramref name="token"/> 'kid' header claim is not null AND decryption fails.</exception>
    /// <exception cref="SecurityTokenException"><paramref name="token"/> 'enc' header claim is null or empty.</exception>
    /// <exception cref="SecurityTokenExpiredException"><paramref name="token"/> 'exp' claim is &lt; DateTime.UtcNow.</exception>
    /// <exception cref="SecurityTokenInvalidAudienceException"><see cref="TokenValidationParameters.ValidAudience"/> is null or whitespace and <see cref="TokenValidationParameters.ValidAudiences"/> is null. Audience is not validated if <see cref="TokenValidationParameters.ValidateAudience"/> is set to false.</exception>
    /// <exception cref="SecurityTokenInvalidAudienceException"><paramref name="token"/> 'aud' claim did not match either <see cref="TokenValidationParameters.ValidAudience"/> or one of <see cref="TokenValidationParameters.ValidAudiences"/>.</exception>
    /// <exception cref="SecurityTokenInvalidLifetimeException"><paramref name="token"/> 'nbf' claim is &gt; 'exp' claim.</exception>
    /// <exception cref="SecurityTokenInvalidSignatureException"><paramref name="token"/>.signature is not properly formatted.</exception>
    /// <exception cref="SecurityTokenNoExpirationException"><paramref name="token"/> 'exp' claim is missing and <see cref="TokenValidationParameters.RequireExpirationTime"/> is true.</exception>
    /// <exception cref="SecurityTokenNoExpirationException"><see cref="TokenValidationParameters.TokenReplayCache"/> is not null and expirationTime.HasValue is false. When a TokenReplayCache is set, tokens require an expiration time.</exception>
    /// <exception cref="SecurityTokenNotYetValidException"><paramref name="token"/> 'nbf' claim is &gt; DateTime.UtcNow.</exception>
    /// <exception cref="SecurityTokenReplayAddFailedException"><paramref name="token"/> could not be added to the <see cref="TokenValidationParameters.TokenReplayCache"/>.</exception>
    /// <exception cref="SecurityTokenReplayDetectedException"><paramref name="token"/> is found in the cache.</exception>
    /// <returns> A <see cref="ClaimsPrincipal"/> from the JWT. Does not include claims found in the JWT header.</returns>
    /// <remarks> 
    /// Many of the exceptions listed above are not thrown directly from this method. See <see cref="Validators"/> to examine the call graph.
    /// </remarks>
    public ClaimsPrincipal ValidateAccessToken(string token);
    /// <summary>
    /// Reads and validates a 'JSON Web Token' (JWT) encoded as a JWS or JWE in Compact Serialized Format.
    /// </summary>
    /// <param name="token">the JWT encoded as JWE or JWS</param>
    /// <param name="validationParameters">Contains validation parameters for the <see cref="JwtSecurityToken"/>.</param>
    /// <param name="validatedToken">The <see cref="JwtSecurityToken"/> that was validated.</param>
    /// <exception cref="ArgumentNullException"><paramref name="token"/> is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="validationParameters"/> is null.</exception>
    /// <exception cref="ArgumentException"><paramref name="token"/>.Length is greater than <see cref="TokenHandler.MaximumTokenSizeInBytes"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="token"/> does not have 3 or 5 parts.</exception>
    /// <exception cref="ArgumentException"><see cref="CanReadToken(string)"/> returns false.</exception>
    /// <exception cref="SecurityTokenDecryptionFailedException"><paramref name="token"/> was a JWE was not able to be decrypted.</exception>
    /// <exception cref="SecurityTokenEncryptionKeyNotFoundException"><paramref name="token"/> 'kid' header claim is not null AND decryption fails.</exception>
    /// <exception cref="SecurityTokenException"><paramref name="token"/> 'enc' header claim is null or empty.</exception>
    /// <exception cref="SecurityTokenExpiredException"><paramref name="token"/> 'exp' claim is &lt; DateTime.UtcNow.</exception>
    /// <exception cref="SecurityTokenInvalidAudienceException"><see cref="TokenValidationParameters.ValidAudience"/> is null or whitespace and <see cref="TokenValidationParameters.ValidAudiences"/> is null. Audience is not validated if <see cref="TokenValidationParameters.ValidateAudience"/> is set to false.</exception>
    /// <exception cref="SecurityTokenInvalidAudienceException"><paramref name="token"/> 'aud' claim did not match either <see cref="TokenValidationParameters.ValidAudience"/> or one of <see cref="TokenValidationParameters.ValidAudiences"/>.</exception>
    /// <exception cref="SecurityTokenInvalidLifetimeException"><paramref name="token"/> 'nbf' claim is &gt; 'exp' claim.</exception>
    /// <exception cref="SecurityTokenInvalidSignatureException"><paramref name="token"/>.signature is not properly formatted.</exception>
    /// <exception cref="SecurityTokenNoExpirationException"><paramref name="token"/> 'exp' claim is missing and <see cref="TokenValidationParameters.RequireExpirationTime"/> is true.</exception>
    /// <exception cref="SecurityTokenNoExpirationException"><see cref="TokenValidationParameters.TokenReplayCache"/> is not null and expirationTime.HasValue is false. When a TokenReplayCache is set, tokens require an expiration time.</exception>
    /// <exception cref="SecurityTokenNotYetValidException"><paramref name="token"/> 'nbf' claim is &gt; DateTime.UtcNow.</exception>
    /// <exception cref="SecurityTokenReplayAddFailedException"><paramref name="token"/> could not be added to the <see cref="TokenValidationParameters.TokenReplayCache"/>.</exception>
    /// <exception cref="SecurityTokenReplayDetectedException"><paramref name="token"/> is found in the cache.</exception>
    /// <returns> A <see cref="ClaimsPrincipal"/> from the JWT. Does not include claims found in the JWT header.</returns>
    /// <remarks> 
    /// Many of the exceptions listed above are not thrown directly from this method. See <see cref="Validators"/> to examine the call graph.
    /// </remarks>
    public ClaimsPrincipal ValidateRefreshToken(string token);
}

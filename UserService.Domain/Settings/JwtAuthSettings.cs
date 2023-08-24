using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Settings;
public class JwtAuthSettings
{
    public required string AccessSecret { get; init; }
    public required string RefreshSecret { get; init; }
    public int AccessTokenLifetimeInMinutes { get; init; }
    public int RefreshTokenLifetimeInDays { get; init; }
    public required string Issuer { get; set; }

}

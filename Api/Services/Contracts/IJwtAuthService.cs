using System;
using System.Collections.Immutable;
using System.Security.Claims;
using Mongoose.Api.Models;

namespace Mongoose.Api.Services.Contracts
{
    public interface IJwtAuthService
    {
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
        JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);
    }
}
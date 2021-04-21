using System;
using System.Collections.Immutable;
using System.Security.Claims;
using Api.Models;

namespace Api.Services
{
    public interface IJwtAuthService
    {
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
        JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);
    }
}
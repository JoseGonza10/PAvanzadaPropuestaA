﻿namespace SiteAsientos.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string userName);
    }
}
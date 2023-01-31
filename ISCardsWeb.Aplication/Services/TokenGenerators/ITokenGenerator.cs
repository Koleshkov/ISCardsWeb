﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ISCardsWeb.Aplication.Services.TokenGenerators
{
    public interface ITokenGenerator
    {
        string GenerateToken(string secretKey, string issuer,
            string audience, double expirationTime,
            IEnumerable<Claim>? claims = null);
    }
}

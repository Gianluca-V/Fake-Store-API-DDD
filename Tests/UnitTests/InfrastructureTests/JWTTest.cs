using Application.Common.Interfaces.Authentication;
using Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.UnitTests.InfrastructureTest;
using Xunit;

namespace Tests.UnitTests.InfrastructureTests
{
    public class JWTTest
    {
        [Fact]
        public void Generate_Token_Should_Return_String_Token()
        {
            JWTTokenGenerator tokenGenerator = new JWTTokenGenerator(Options.Create(InfrastructureUtils.JwtSettings));

            string token = tokenGenerator.GenerateToken(InfrastructureUtils.User);
            Assert.IsType<string>(token);
            Assert.NotEmpty(token);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using NiceToes.AuthAPI.Models;
using NiceToes.AuthAPI.Service;
using Xunit;

namespace NiceToes.Tests
{
    public class ServiceTests
    {
        [Fact]
        void TestJwtGenerator()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>()
                {
                    ["Secret"] = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET",
                    ["Issuer"] = "nicetoes-auth-api",
                    ["Audience"] = "nicetoes-client",
                })
                .Build();

            //var options = new OptionsManager<JwtOptions>(configuration);

            var jwtOptions = new JwtOptions
            {
                Audience = "nicetoes-client",
                Issuer = "nicetoes-auth-api",
                Secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET"
            };

            var options = Options.Create(jwtOptions);

            var expected = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImFAZ21haWwuY29tIiwic3V" +
                           "iIjoiMiIsIm5hbWUiOiJBbGV4Iiwicm9sZSI6IkFETUlOIiwibmJmIjoxNzAxOTk1NTQ0LCJle" +
                           "HAiOjE3MDI2MDAzNDQsImlhdCI6MTcwMTk5NTU0NCwiaXNzIjoibmljZXRvZXMtYXV0aC1hcGk" +
                           "iLCJhdWQiOiJuaWNldG9lcy1jbGllbnQifQ.KHjDLoa1p0pBfdVK_ezGw_8NF4Hk0mmzvi9wSWibcCQ";

            var jwtTokenGenerator = new JwtTokenGenerator(options);

            var applicationUser = new ApplicationUser();

            applicationUser.Email = "a@gmail.com";
            applicationUser.Id = "2";
            applicationUser.UserName = "Alex";

            var actualResult = jwtTokenGenerator.GenerateToken(applicationUser, new string[] {"ADMIN" });

            Assert.Equal(expected.Substring(0, 30), actualResult.Substring(0, 30));
        }
    }
}

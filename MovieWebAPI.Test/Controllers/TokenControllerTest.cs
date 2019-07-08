using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using MovieWebAPI.Controllers;
using MovieWebAPI.Infrastructure.AppSettings;
using MovieWebAPI.Model.User;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieWebAPI.Test.Controllers
{
    

    [TestFixture]
    public class TokenControllerTest
    {
        private Mock<IConfiguration> _configuration;
        private IOptions<Jwt> _jwtOptions;
        private IOptions<Authentication> _authenticationOptions;
        Jwt _jwt;
        Authentication _authentication;

        [SetUp]
        public void Setup()
        {
            _configuration = new Mock<IConfiguration>();
            _jwtOptions = Options.Create(new Jwt { Audience= "http://localhost:4200/",Issuer= "http://localhost:13388",   Key= "wdcpr5Ff5YahZdc4gfubb7pDasU9yb97"});
            _authenticationOptions = Options.Create(new Authentication { UserName = "johndoe" , Password = "Ab123456*-" });
            _jwt = _jwtOptions.Value;
            _authentication = _authenticationOptions.Value;
        }

        [Test]
        public void TokenController_IsInstanceOf_Test()
        {
            //Arrange
            var controller = new TokenController(_configuration.Object,_jwtOptions,_authenticationOptions);

            //Act | Assert
            Assert.IsInstanceOf<TokenController>(controller);
        }

        [Test]
        public void TokenController_Get_Token_Should_Return_200_And_Data_Not_Null_Test()
        {
            //Arrange
            var user = GetUser();
            var controller = new TokenController(_configuration.Object, _jwtOptions, _authenticationOptions);

            //Act
            var result = controller.GetToken(user);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TokenController_Get_Token_Should_Return_401_Test()
        {
            //Arrange
            var user = GetUser();
            user.Username = "a";
            var controller = new TokenController(_configuration.Object, _jwtOptions, _authenticationOptions);

            //Act
            var result = controller.GetToken(user);

            //Assert
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        private UserModel GetUser()
        {
            return new UserModel()
            {
               Username="johndoe",
               Password="Ab123456*-"
            };
        }
    }
}

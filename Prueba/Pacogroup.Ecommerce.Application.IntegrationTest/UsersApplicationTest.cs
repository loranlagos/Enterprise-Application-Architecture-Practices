using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pacogroup.Ecommerce.Aplication.IntegrationTest;
using Pacogroup.Ecommerce.Application.Interfaces;

namespace Pacogroup.Ecommerce.Application.Test
{
    [TestClass]
    public class UsersApplicationTest
    {
        private static WebApplicationFactory<Program>? _factory;
        private static IServiceScopeFactory? _scopeFactory;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        [TestMethod]
        public void Authenticate_CuandoNoSeEnvianParametros_RetornarMensajeErrorValidacion()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IAuthApplication>();

            // Arrange
            var userName = string.Empty;
            var password = string.Empty;
            var expected = "Autenticación fallida por uno o más errores";

            // Act            
            var result = context.SignInAsync(new DTO.SingInDTO { Email = userName, Password = password }).Result;
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Authenticate_CuandoSeEnvianParametrosCorrectos_RetornarMensajeExito()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IAuthApplication>();

            // Arrange
            var userName = "lagosariasa343@gmail.com";
            var password = "Manino_10";
            var expected = "Autenticación exitosa";

            // Act
            var result = context.SignInAsync(new DTO.SingInDTO { Email = userName, Password = password }).Result;
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Authenticate_CuandoSeEnvianParametrosIncorrectos_RetornarMensajeUsuarioNoExiste()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IAuthApplication>();

            // Arrange
            var userName = "lorrlagos@proton.me";
            var password = "Manino";
            var expected = "Email no existe o no se encuentra registrado";

            // Act
            var result = context.SignInAsync(new DTO.SingInDTO { Email = userName, Password = password }).Result;
            var actual = result.Message;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}

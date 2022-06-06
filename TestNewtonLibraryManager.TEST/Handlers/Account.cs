using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace TestNewtonLibraryManager.TEST.Handlers
{
    public class Account
    {
        [Fact]
        public void LogIn()
        {
            var testActual = NewtonLibraryManager.Handlers.AccountHandler.LogIn("test.user@newton.com", "xunit");
            Assert.True(testActual);

        }

        [Fact]
        public void CreateUser()
        {
            var testActual = NewtonLibraryManager.Handlers.AccountHandler.CreateUser("Test", "User", "test.user@newton.com", "xunit");
            Assert.True(testActual);

        }

        [Fact]
        public void CreateAdmin()
        {
            var testActual = NewtonLibraryManager.Handlers.AccountHandler.CreateAdmin("TestAdmin", "User", "test.user@newton.com", "xunit");
            Assert.True(testActual);
        }

        [Fact]
        public void ValidatePassword()
        {
            string? lmao;

            //empty password
            var testActual = NewtonLibraryManager.Handlers.AccountHandler.ValidatePassword(" ",out lmao);
            Assert.False(testActual);

            //ingen liten bokstav
            testActual = NewtonLibraryManager.Handlers.AccountHandler.ValidatePassword("ABCD5678", out lmao);
            Assert.False(testActual);
            
            //ingen stor bokstav
            testActual = NewtonLibraryManager.Handlers.AccountHandler.ValidatePassword("abcd5678", out lmao);
            Assert.False(testActual);

            //inte 8+ tecken
            testActual = NewtonLibraryManager.Handlers.AccountHandler.ValidatePassword("AbCd567", out lmao);
            Assert.False(testActual);

            //ingen siffra eller special karaktär
            testActual = NewtonLibraryManager.Handlers.AccountHandler.ValidatePassword("ABCDefgh", out lmao);
            Assert.False(testActual);

            //giltigt lösenord
            testActual = NewtonLibraryManager.Handlers.AccountHandler.ValidatePassword("SuperSafe1337", out lmao);
            Assert.True(testActual);
        }
    }
}

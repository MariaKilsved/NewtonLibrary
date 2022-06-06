using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtonLibraryManager;
using Xunit;

namespace TestNewtonLibraryManager.TEST.METHODS
{
    public class Create
    {
        
        [Fact]
        public void CreateAuthor()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Create.CreateHandler.CreateAuthor("Kalle", "Bengt");
            Assert.IsType<int>(testActual);
            Assert.NotEqual(0, testActual);

            NewtonLibraryManager.EntityFramework.Delete.DeleteHandler.DeleteAuthor(testActual);
        }

        [Fact]
        public void CreateProduct()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Create.CreateHandler.CreateProduct("TestBok", 1, 1, 1, 001, "det här är en beskrivning", "9781111111111", 0);
            Assert.IsType<int>(testActual);
            Assert.NotEqual(0, testActual);

            NewtonLibraryManager.EntityFramework.Delete.DeleteHandler.DeleteProduct(testActual);
        }

        [Fact]
        public void CreateAuthorDetail()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Create.CreateHandler.CreateAuthorDetail(40, 62);
            Assert.True(testActual);

            testActual = NewtonLibraryManager.EntityFramework.Create.CreateHandler.CreateAuthorDetail(-20, 1000);
            Assert.False(testActual);
        }

        [Fact]
        public void CreateCategory()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Create.CreateHandler.CreateCategory("TestCategory");
            Assert.True(testActual);

        }

        [Fact]
        public void CreateLanguage()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Create.CreateHandler.CreateLanguage("TestLanguage");
            Assert.IsType<int>(testActual);
            Assert.NotEqual(0, testActual);

        }

        [Fact]
        public void CreateUser()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Create.CreateHandler.CreateUser("Test", "User", "test.user@newton.com", "xunit", false);
            Assert.True(testActual);
        }

        [Fact]
        public void CreateLendingDetail()
        {
            DateTime dt1 = new DateTime(2022, 12, 30);
            DateTime dt2 = new DateTime(2022, 12, 31);
            var testActual = NewtonLibraryManager.EntityFramework.Create.CreateHandler.CreateLendingDetail(45, dt1, dt2, 50);
        }

        [Fact]
        public void CreateReservationDetail()
        {
            DateTime dt1 = new DateTime(2022, 12, 31);
            var testActual = NewtonLibraryManager.EntityFramework.Create.CreateHandler.CreateReservationDetail(45, dt1, 50);
        }

        [Fact]
        public void CreateType()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Create.CreateHandler.CreateType("TestType");
            Assert.True(testActual);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewtonLibraryManager;
using Xunit;

namespace TestNewtonLibraryManager.TEST.METHODS
{
    public class Delete
    {
        [Fact]
        public void No()
        {
            int expected = 5;

            int actual = 4;

            Assert.NotEqual(expected, actual);
        }

        [Fact]
        public void DeleteAuthor()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Delete.DeleteHandler.DeleteAuthor(42);
            Assert.True(testActual);

        }

        [Fact]
        public void DeleteProduct()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Delete.DeleteHandler.DeleteProduct(54);
            Assert.IsType<int>(testActual);
        }

        [Fact]
        public void DeleteAuthorDetail()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Delete.DeleteHandler.DeleteAuthorDetail(62);
            Assert.IsType<int>(testActual);
        }

        [Fact]
        public void DeleteCategory()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Delete.DeleteHandler.DeleteCategory(29);
            Assert.True(testActual);

        }

        [Fact]
        public void DeleteLanguage()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Delete.DeleteHandler.DeleteLanguage(6);
            Assert.True(testActual);
        }

        [Fact]
        public void DeleteUser()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Delete.DeleteHandler.DeleteUser(48);
            Assert.True(testActual);
        }

        [Fact]
        public void DeleteLendingDetail()
        {

        }

        [Fact]
        public void DeleteReservationDetail()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Delete.DeleteHandler.DeleteReservationDetail(12);
            Assert.True(testActual);
        }

        [Fact]
        public void DeleteType()
        {
            var testActual = NewtonLibraryManager.EntityFramework.Delete.DeleteHandler.DeleteType(9);
            Assert.True(testActual);
        }


    }
}

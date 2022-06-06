using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestNewtonLibraryManager.TEST.Handlers
{
    public class Product
    {
        [Fact]
        public void ReBorrowProduct()
        {
            var testActual = NewtonLibraryManager.Handlers.ProductHandler.ReBorrowProduct(48, 54);
            Assert.True(testActual);

        }

        [Fact]
        public void ReturnProduct()
        {
            var testActual = NewtonLibraryManager.Handlers.ProductHandler.ReturnProduct(54);
            Assert.True(testActual);

        }

        [Fact]
        public void CancelReservation()
        {
            var testActual = NewtonLibraryManager.Handlers.ProductHandler.CancelReservation(54);
            Assert.False(testActual);

        }

        [Fact]
        public void BorrowProduct()
        {
            var testActual = NewtonLibraryManager.Handlers.ProductHandler.BorrowProduct(48, 54);
            Assert.True(testActual);

        }

        [Fact]
        public void ReserveProduct()
        {
            var testActual = NewtonLibraryManager.Handlers.ProductHandler.BorrowProduct(48, 54);
            Assert.True(testActual);

        }

        [Fact]
        public void GetProductIdFromIsbn()
        {
            var testActual = NewtonLibraryManager.Handlers.ProductHandler.GetProductIdFromIsbn("9781111111111");
            Assert.IsType<int>(testActual);

        }

    }
}

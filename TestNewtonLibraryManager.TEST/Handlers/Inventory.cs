using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestNewtonLibraryManager.TEST.Handlers
{
    public class Inventory
    {
        [Fact]
        public void GetInventoryAmount()
        {
            var testActual = NewtonLibraryManager.Handlers.InventoryHandler.GetInventoryAmount();
            Assert.IsType<int>(testActual);
            Assert.NotEqual(0, testActual);

        }
    }
}

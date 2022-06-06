using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestNewtonLibraryManager.TEST.Handlers
{
    public class Statistics
    {
        [Fact]
        public void GetMostActiveBorrower()
        {
            var testActual = NewtonLibraryManager.Handlers.StatisticHandler.GetMostActiveBorrower();
            Assert.IsType<int>(testActual);

        }
    }
}

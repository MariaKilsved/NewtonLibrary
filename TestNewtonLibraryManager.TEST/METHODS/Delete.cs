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

        public void DeleteAuthor()
        {

        }

        public void DeleteAuthorDetail()
        {

        }

        public void DeleteCategory()
        {
    
        }

        public void DeleteLanguage()
        {

        }

        public void DeleteLendingDetail()
        {

        }

        public void DeleteReservationDetail()
        {

        }

        public void DeleteProduct()
        {

        }

        public void DeleteType()
        {

        }

        public void DeleteUser()
        {

        }

    }
}

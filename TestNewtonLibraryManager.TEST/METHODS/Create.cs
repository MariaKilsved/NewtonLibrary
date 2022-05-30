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
        public void test()
        {
            bool expected = true;
            bool actual = false;

            if (1 == 1)
            {
                actual = true;
            }

            Assert.Equal(expected, actual);
        }

        public void CreateAuthor()
        {

        }

        public void CreateAuthorDetail()
        {

        }

        public void CreateCategory()
        {

        }

        public void CreateLanguage()
        {

        }

        public void CreateLendingDetail()
        {

        }

        public void CreateReservationDetail()
        {

        }

        public void CreateProduct()
        {

        }

        public void CreateType()
        {

        }

        public void CreateUser()
        {

        }

    }
}

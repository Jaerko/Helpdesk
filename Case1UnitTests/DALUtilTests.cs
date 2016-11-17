using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpdeskDAL;

namespace Case1UnitTests
{

    [TestClass]

    public class DALUtilTests
    {
        [TestMethod]

        public void TestLoadCollectionsShouldReturnTrue()
        {
            DALUtilsV2 util = new DALUtilsV2();
            Assert.IsTrue(util.LoadCollections());
        }
    }



}
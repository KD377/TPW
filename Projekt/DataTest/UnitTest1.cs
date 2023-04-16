using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;

namespace DataTests
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void TestCreateDataAPI()
        {
            // Arrange
            DataAPI dataAPI = null;
        
            dataAPI = DataAPI.CreateDataAPI();
            Assert.IsNotNull(dataAPI);
            Assert.IsInstanceOfType(dataAPI, typeof(DataAPI));
        }
    }
}

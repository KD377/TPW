using Projekt;
namespace Test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        Projekt.Calculator calc = new Calculator();
        Assert.AreEqual(calc.multiply(2, 3), 6);
    }
}

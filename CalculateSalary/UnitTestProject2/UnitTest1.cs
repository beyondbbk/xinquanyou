using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySoft.ResManger.Services;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            AnalyExcelService.GetView(@"c:/1.xlsx");
        }
    }
}

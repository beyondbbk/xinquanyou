using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mysoft.Tjqxj.Services;
using MySoft.Common;
using MySoft.ResManger.Services;
using Remotion.Linq.Clauses.ResultOperators;

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

        [TestMethod]
        public void GetToken()
        {
            //var result = WxCommonService.GetAccessToken();
        }

        [TestMethod]
        public void GetJsSign()
        {
            //var result = WxCommonService.GetJsSign("http://yangjian.site");
        }
    }
}

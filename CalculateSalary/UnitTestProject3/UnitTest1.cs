using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mysoft.Tjqxj.Models;
using Mysoft.Tjqxj.Models.ViewModel;
using Mysoft.Tjqxj.Services;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var temp = new FeedBackTemplate();
            //temp.FeedbackTime.Value=DateTime.Now.ToString();
            //temp.Msg.Value = "十分感谢";
            //temp.Title.Value = "我是标题";
            //WxCommonService.SendMsgToUser("ofbAr1AtymN6KJS7dnzrCyNcJq30", "U_H9C_qev8jAh6iKQYmbnbSpO5RBjt4G0ZWqIXYZ6xk",
            //    temp);
        }

        [TestMethod]
        public void GetTemplateId()
        {
            var id =WxCommonService.GetTemplateId("手动");
        }

        [TestMethod]
        public void GetRealTime()
        {
             var result= CaiyunService.GetRealtimeInfo("104.06476", "30.5702");
        }

        [TestMethod]
        public void GetPreInfo()
        {
            var result =CaiyunService.GetPrediction(new RealtimeClimateInfo(), "104.06476", "30.5702");
        }
    }
}

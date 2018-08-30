using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySoft.Weeding.Models;
using MySoft.Weeding.Services;

namespace WeedUTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSms()
        {
            SmsHelper.SendSms("13320911211", "—ÓΩ°");
        }

        [TestMethod]
        public void TestTypeCpunt()
        {
            var friendInfo = new FriendInfo();
            friendInfo.NameInfos.Add("¿ÓÃŒ∏Á");
            friendInfo.NameInfos.Add("¥ÛÃŒ");
            friendInfo.NameInfos.Add("¥ÛÃŒ¿—“Ø");
            var type = friendInfo.TypeSpeed;
        }
    }
}

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
            SmsHelper.SendSms("13320911211", "�");
        }

        [TestMethod]
        public void TestTypeCpunt()
        {
            var friendInfo = new FriendInfo();
            friendInfo.NameInfos.Add("���θ�");
            friendInfo.NameInfos.Add("����");
            friendInfo.NameInfos.Add("������ү");
            var type = friendInfo.TypeSpeed;
        }
    }
}

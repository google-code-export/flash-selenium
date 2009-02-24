using NUnit.Framework;
using Selenium;

namespace IntegrationTests
{
    [TestFixture]
    public class FlashSeleniumIntegrationTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            flashSelenium.Stop();
            flashSelenium = null;
            selenium = null;
        }

        private ISelenium GetSelenium(string browserString)
        {
            return new DefaultSelenium("localhost", 4444, browserString, URL);
        }

        private ISelenium selenium;
        private FlashSelenium.FlashSelenium flashSelenium;
        private const string flashObjectId = "clickcolors";
        private const string URL = "http://localhost/colors.html";
//        private const string URL = "http://www.geocities.com/paulocaroli/flash/colors.html";

        [Test]
        public void ShouldTestFlashSeleniumInFirefox3()
        {
            selenium = GetSelenium("*firefox");
            flashSelenium = new FlashSelenium.FlashSelenium(selenium, flashObjectId);
            flashSelenium.Start();
            flashSelenium.Open(URL);
            flashSelenium.Call("click");
            Assert.AreEqual("BLUE", flashSelenium.Call("getColor"));
        }

        [Test]
        public void ShouldTestFlashSeleniumInMSIE()
        {
            selenium = GetSelenium("*iexplore");
            flashSelenium = new FlashSelenium.FlashSelenium(selenium, flashObjectId);
            flashSelenium.Start();
            flashSelenium.Open(URL);
            flashSelenium.Call("click");
            Assert.AreEqual("BLUE", flashSelenium.Call("getColor"));
        }

        [Test]
        public void ShouldTestFlashSeleniumInSafari()
        {
            selenium = GetSelenium("*safari");
            flashSelenium = new FlashSelenium.FlashSelenium(selenium, flashObjectId);
            flashSelenium.Start();
            flashSelenium.Open(URL);
            flashSelenium.Call("click");
            Assert.AreEqual("BLUE", flashSelenium.Call("getColor"));
        }

        [Test]
        public void ShouldTestFlashSeleniumInOpera()
        {
            selenium = GetSelenium("*opera");
            flashSelenium = new FlashSelenium.FlashSelenium(selenium, flashObjectId);
            flashSelenium.Start();
            flashSelenium.Open(URL);
            flashSelenium.Call("click");
            Assert.AreEqual("BLUE", flashSelenium.Call("getColor"));
        }
    }
}
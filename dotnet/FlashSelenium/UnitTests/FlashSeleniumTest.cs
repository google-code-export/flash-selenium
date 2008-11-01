//
// Flash Selenium - .NET Client
// 
// Date: 3 April 2008
// Paulo Caroli, Sachin Sudheendra
// http://code.google.com/p/flash-selenium
// -----------------------------------------
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 

using NMock;
using NUnit.Framework;
using Selenium;

namespace UnitTests
{
    [TestFixture]
    public class FlashSeleniumTest
    {
        private DynamicMock mockProcessor;
        private const string Firefox3UserAgentInfo = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.1b1) Gecko/20081007 Firefox/3.1b1";
        private const string Firefox2UserAgentInfo = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.13) Gecko/20080311 Firefox/2.0.0.13";
        private const string IEUserAgentInfo = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727)";


        [SetUp]
        public void SetUp()
        {
            mockProcessor = new DynamicMock(typeof(ISelenium));
        }


        [TearDown]
        private void TearDown()
        {
            mockProcessor.Verify();
        }

        [Test]
        public void shouldInvokeStart()
        {
            mockProcessor.Expect("Start");
            ISelenium selenium = (ISelenium) mockProcessor.MockInstance;
            FlashSelenium.FlashSelenium flashSelenium = new FlashSelenium.FlashSelenium(selenium, "test");
            try
            {
                flashSelenium.Start();
            }
            catch
            {
                Assert.Fail("Should Not Fail");
            }
        }
        
        [Test]
        public void shouldInvokeStop()
        {
            mockProcessor.Expect("Stop");
            ISelenium selenium = (ISelenium) mockProcessor.MockInstance;
            FlashSelenium.FlashSelenium flashSelenium = new FlashSelenium.FlashSelenium(selenium, "test");
            try
            {
                flashSelenium.Stop();
            }
            catch
            {
                Assert.Fail("Should Not Fail");
            }
        }

        [Test]
        public void shouldOpenUrl()
        {
            mockProcessor.Expect("Open", new object[] {"http://blah.com"});
            ISelenium selenium = (ISelenium)mockProcessor.MockInstance;
            FlashSelenium.FlashSelenium flashSelenium = new FlashSelenium.FlashSelenium(selenium, "test");
            try
            {
                flashSelenium.Open("http://blah.com");
            }
            catch
            {
                Assert.Fail("Should Not Fail");
            }
        }
        
        [Test]
        public void shouldWaitForPageLoad()
        {
            ISelenium selenium = (ISelenium) mockProcessor.MockInstance;
            FlashSelenium.FlashSelenium flashSelenium = new FlashSelenium.FlashSelenium(selenium, "test");
            mockProcessor.Expect("WaitForPageToLoad", new string[] { "5000" });
            try
            {
                flashSelenium.WaitForPageToLoad("5000");
            }
            catch
            {
                Assert.Fail("Should Not Fail");
            }
        }

        [Test]
        public void shouldReturnJSPrefixForFirefox2()
        {
            
            mockProcessor.ExpectAndReturn("GetEval", Firefox2UserAgentInfo, new object[] { "navigator.userAgent" });
            ISelenium selenium = (ISelenium) mockProcessor.MockInstance;
            FlashSeleniumExtensionForTest flashSelenium = new FlashSeleniumExtensionForTest(selenium, "test");
            Assert.AreEqual("document['test'].", flashSelenium.checkBrowserAndReturnJSPrefix());
        }

        [Test]
        public void shouldReturnJSPrefixForIE()
        {
            mockProcessor.ExpectAndReturn("GetEval", IEUserAgentInfo, new object[] { "navigator.userAgent" });
            ISelenium selenium = (ISelenium) mockProcessor.MockInstance;
            FlashSeleniumExtensionForTest flashSelenium = new FlashSeleniumExtensionForTest(selenium, "test");
            Assert.AreEqual("window.document['test'].", flashSelenium.checkBrowserAndReturnJSPrefix());
        }

        [Test]
        public void shouldConstructProperJSFunctionCallWithParamsForFirefox2()
        {
            mockProcessor.ExpectAndReturn("GetEval", Firefox2UserAgentInfo, new object[] { "navigator.userAgent" });
            ISelenium selenium = (ISelenium) mockProcessor.MockInstance;
            FlashSeleniumExtensionForTest flashSeleniumExtensionForTest = new FlashSeleniumExtensionForTest(selenium, "test");
            string actual = flashSeleniumExtensionForTest.jsForFunction("functionName", "Param1", "Param2");
            Assert.AreEqual("document['test'].functionName('Param1','Param2');", actual);
        }

        [Test]
        public void shouldConstructProperJSFunctionCallWithSingleParamForFirefox2()
        {
            mockProcessor.ExpectAndReturn("GetEval", Firefox2UserAgentInfo, new object[] { "navigator.userAgent" });
            ISelenium selenium1 = (ISelenium) mockProcessor.MockInstance;
            FlashSeleniumExtensionForTest flashSeleniumExtensionForTest = new FlashSeleniumExtensionForTest(selenium1, "test");
            string actual = flashSeleniumExtensionForTest.jsForFunction("functionName", "Param1");
            Assert.AreEqual("document['test'].functionName('Param1');", actual);
        }

        [Test]
        public void shouldConstructProperJSFunctionCallWithNoParamForFirefox2()
        {
            mockProcessor.ExpectAndReturn("GetEval", Firefox2UserAgentInfo, new object[] { "navigator.userAgent" });
            ISelenium selenium1 = (ISelenium) mockProcessor.MockInstance;
            FlashSeleniumExtensionForTest flashSeleniumExtensionForTest = new FlashSeleniumExtensionForTest(selenium1, "test");
            string actual = flashSeleniumExtensionForTest.jsForFunction("functionName");
            Assert.AreEqual("document['test'].functionName();", actual);
        }
        
        [Test]
        public void shouldConstructProperJSFunctionCallWithMultipleParamsCastedToStringForFirefox2()
        {
            mockProcessor.ExpectAndReturn("GetEval", Firefox2UserAgentInfo, new object[] { "navigator.userAgent" });
            ISelenium selenium1 = (ISelenium) mockProcessor.MockInstance;
            FlashSeleniumExtensionForTest flashSeleniumExtensionForTest = new FlashSeleniumExtensionForTest(selenium1, "test");
            string actual = flashSeleniumExtensionForTest.jsForFunction("functionName",42.ToString(), 'S'.ToString(), (42.42).ToString());
            Assert.AreEqual("document['test'].functionName('42','S','42.42');", actual);
        }

        [Test]
        public void shouldReturnJSPrefixForFirefox3()
        {
            mockProcessor.ExpectAndReturn("GetEval", Firefox3UserAgentInfo, new object[] { "navigator.userAgent" });
            ISelenium selenium = (ISelenium)mockProcessor.MockInstance;
            FlashSeleniumExtensionForTest flashSelenium = new FlashSeleniumExtensionForTest(selenium, "test");
            Assert.AreEqual("window.document['test'].", flashSelenium.checkBrowserAndReturnJSPrefix());
        }

    }

    internal class FlashSeleniumExtensionForTest : FlashSelenium.FlashSelenium
    {
        public FlashSeleniumExtensionForTest(ISelenium selenium, string flashObjectId) : base(selenium, flashObjectId)
        {
        }


        public string checkBrowserAndReturnJSPrefix()
        {
            return base.checkBrowserAndReturnJSPrefix();
        }

        public string jsForFunction(string functionName, params string[] parameters)
        {
            return base.jsForFunction(functionName, parameters);
        }
    }
}
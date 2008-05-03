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

using System;
using NMock;
using NUnit.Framework;

namespace Selenium.UnitTests
{
    [TestFixture]
    public class FlashSeleniumTest
    {
        private FlashSelenium flashSelenium;
        private readonly String URL = "http://localhost:1978/wiki/test.html";
        private DynamicMock mockProcessor;

        [SetUp]
        public void setUp()
        {
            mockProcessor = new DynamicMock(typeof(ISelenium));
            ISelenium selenium = (ISelenium) mockProcessor.MockInstance;
            flashSelenium = new FlashSelenium(selenium, "ebrochure");
        }

        [TearDown]
        public void tearDown()
        {
            mockProcessor.Verify();
            try
            {
                flashSelenium.Stop();
            }
            catch (Exception)
            {
                Console.Write("Flash Selenium Instance has stopped prematurely.");
            }
        }

        [Test]
        public void shouldStopSeleniumInstance()
        {
            mockProcessor.Expect("Start");
            flashSelenium.Start();
            try
            {
                mockProcessor.Expect("Stop");
                flashSelenium.Stop();
            }
            catch (Exception)
            {
                Assert.Fail("Should Not Fail");
            }
        }

        [Test]
        public void shouldOpenURL()
        {
            mockProcessor.Expect("Start");
            mockProcessor.Expect("Open", new object[] {URL});
            flashSelenium.Start();
            try
            {
                flashSelenium.Open(URL);
            }
            catch (Exception e)
            {
                if (e.Message != null) Assert.Fail(e.Message);
            }
        }

        [Test]
        public void shouldReturnJSPrefixForFirefox()
        {
            DynamicMock mockProcessor = new DynamicMock(typeof (ISelenium));
            mockProcessor.ExpectAndReturn("GetEval", "-1",
                                          new object[] {"navigator.appName.indexOf(\"Microsoft Internet\")"});
            ISelenium selenium1 = (ISelenium) mockProcessor.MockInstance;
            FlashSeleniumExtensionForTest flashSelenium1 = new FlashSeleniumExtensionForTest(selenium1, "test");
            Assert.AreEqual("document['test'].", flashSelenium1.checkBrowserAndReturnJSPrefix());
            mockProcessor.Verify();
        }

        [Test]
        public void shouldReturnJSPrefixForIE()
        {
            DynamicMock mockProcessor = new DynamicMock(typeof (ISelenium));
            mockProcessor.ExpectAndReturn("GetEval", "0",new object[] {"navigator.appName.indexOf(\"Microsoft Internet\")"});
            ISelenium selenium1 = (ISelenium) mockProcessor.MockInstance;
            FlashSeleniumExtensionForTest flashSelenium1 = new FlashSeleniumExtensionForTest(selenium1, "test");
            Assert.AreEqual("window.document['test'].", flashSelenium1.checkBrowserAndReturnJSPrefix());
            mockProcessor.Verify();
        }

        [Test] 
        public void shouldConstructProperJSFunctionCallWithParams()
        {
            DynamicMock mockProcessor = new DynamicMock(typeof(ISelenium));
            mockProcessor.ExpectAndReturn("GetEval", "-1", new object[] { "navigator.appName.indexOf(\"Microsoft Internet\")" });
            ISelenium selenium1 = (ISelenium)mockProcessor.MockInstance;
            FlashSeleniumExtensionForTest flashSeleniumExtensionForTest = new FlashSeleniumExtensionForTest(selenium1, "test");
            string actual = flashSeleniumExtensionForTest.jsForFunction("functionName", "Param1", "Param2");
            Assert.AreEqual("document['test'].functionName('Param1','Param2');", actual);
        }
        
        [Test] 
        public void shouldConstructProperJSFunctionCallWithSingleParam()
        {
            DynamicMock mockProcessor = new DynamicMock(typeof(ISelenium));
            mockProcessor.ExpectAndReturn("GetEval", "-1", new object[] { "navigator.appName.indexOf(\"Microsoft Internet\")" });
            ISelenium selenium1 = (ISelenium)mockProcessor.MockInstance;
            FlashSeleniumExtensionForTest flashSeleniumExtensionForTest = new FlashSeleniumExtensionForTest(selenium1, "test");
            string actual = flashSeleniumExtensionForTest.jsForFunction("functionName", "Param1");
            Assert.AreEqual("document['test'].functionName('Param1');", actual);
        }

        [Test]
        public void shouldConstructProperJSFunctionCallWithNoParam()
        {
            DynamicMock mockProcessor = new DynamicMock(typeof(ISelenium));
            mockProcessor.ExpectAndReturn("GetEval", "-1", new object[] { "navigator.appName.indexOf(\"Microsoft Internet\")" });
            ISelenium selenium1 = (ISelenium)mockProcessor.MockInstance;
            FlashSeleniumExtensionForTest flashSeleniumExtensionForTest = new FlashSeleniumExtensionForTest(selenium1, "test");
            string actual = flashSeleniumExtensionForTest.jsForFunction("functionName");
            Assert.AreEqual("document['test'].functionName();", actual);
        }
    }

    internal class FlashSeleniumExtensionForTest : FlashSelenium
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
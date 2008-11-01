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
    public class FlashSeleniumTellTargetMethodsTest
    {
        private DynamicMock mockProcessor;
        private FlashSelenium.FlashSelenium flashSelenium;
        private readonly string flashObjectId = "test";

        [SetUp]
        public void SetUp()
        {
            mockProcessor = new DynamicMock(typeof(ISelenium));
            ISelenium selenium = (ISelenium) mockProcessor.MockInstance;
            flashSelenium = new FlashSelenium.FlashSelenium(selenium, flashObjectId);
        }

        [TearDown]
        public void TearDown()
        {
            mockProcessor.Verify();
        }

        private void addJSPrefixCallToMock()
        {
            mockProcessor.ExpectAndReturn("GetEval", "-1",
                                          new object[] { "navigator.userAgent" });
        }

        [Test]
        public void shouldGetCurrentFrameNumber()
        {
            addJSPrefixCallToMock();
            mockProcessor.ExpectAndReturn("GetEval", "42", new object[] { "document['" + flashObjectId + "'].TCurrentFrame('/');" });
            Assert.AreEqual(42,flashSelenium.TCurrentFrame("/"));
        }

        [Test]
        public void shouldCallFrame()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].TCallFrame('/','42');" });
            flashSelenium.TCallFrame("/", 42);
        }

        [Test]
        public void shouldCallLabel()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].TCallLabel('/','fortytwo');" });
            flashSelenium.TCallLabel("/", "fortytwo");
        }

        [Test]
        public void shouldGetCurrentLabel()
        {
            addJSPrefixCallToMock();
            mockProcessor.ExpectAndReturn("GetEval", "fortytwo", new object[] { "document['" + flashObjectId + "'].TCurrentLabel('/fortytwo');" });
            Assert.AreEqual("fortytwo", flashSelenium.TCurrentLabel("/fortytwo"));
        }

        [Test]
        public void shouldGetProperty()
        {
            addJSPrefixCallToMock();
            mockProcessor.ExpectAndReturn("GetEval", "fortytwo", new object[] { "document['" + flashObjectId + "'].TGetProperty('/','42');" });
            Assert.AreEqual("fortytwo", flashSelenium.TGetProperty("/", 42));
        }

        [Test]
        public void shouldGetPropertyAsNumber()
        {
            addJSPrefixCallToMock();
            mockProcessor.ExpectAndReturn("GetEval", "42", new object[] { "document['" + flashObjectId + "'].TGetPropertyAsNumber('/','42');" });
            Assert.AreEqual(42, flashSelenium.TGetPropertyAsNumber("/", 42));
        }
        
        [Test]
        public void shouldGotoFrame()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].TGotoFrame('/','42');" });
            flashSelenium.TGotoFrame("/", 42);
        }

        [Test]
        public void shouldGotoLabel()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].TGotoLabel('/','fortytwo');" });
            flashSelenium.TGotoLabel("/", "fortytwo");
        }

        [Test]
        public void shouldPlayTimeline()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].TPlay('/MovieClip');" });
            flashSelenium.TPlay("/MovieClip");
        }
        
        [Test]
        public void shouldSetProperty()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].TSetProperty('/MovieClip','property','value');" });
            flashSelenium.TSetProperty("/MovieClip", "property", "value");
        }

        [Test]
        public void shouldStopPlay()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].TStopPlay('MovieClip');" });
            flashSelenium.TStopPlay("MovieClip");
        }

    }
}
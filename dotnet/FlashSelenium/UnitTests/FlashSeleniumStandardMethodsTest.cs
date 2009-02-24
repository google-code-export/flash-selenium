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
using Selenium;

namespace UnitTests
{
    [TestFixture]
    public class FlashSeleniumStandardMethodsTest
    {
        private FlashSelenium.FlashSelenium flashSelenium;
        private readonly string flashObjectId = "clickcolors";
        private DynamicMock mockProcessor;

        [SetUp]
        public void SetUp()
        {
            mockProcessor = new DynamicMock(typeof (ISelenium));
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
                                          new object[] {"navigator.userAgent"});
        }

        [Test]
        public void shouldCallToReturnPercentLoaded()
        {
            addJSPrefixCallToMock();
            mockProcessor.ExpectAndReturn("GetEval", "100",
                                          new object[] {"document['" + flashObjectId + "'].PercentLoaded();"});
            Assert.AreEqual("100", flashSelenium.PercentLoaded());
        }

        [Test]
        public void shouldCallIsPlaying()
        {
            addJSPrefixCallToMock();
            mockProcessor.ExpectAndReturn("GetEval", "true", new object[] {"document['" + flashObjectId + "'].IsPlaying();"});
            Assert.AreEqual("true", flashSelenium.IsPlaying());
        }


        [Test]
        public void shouldCallGetVariable()
        {
            addJSPrefixCallToMock();
            mockProcessor.ExpectAndReturn("GetEval", "42",
                                          new object[] {"document['" + flashObjectId + "'].GetVariable('Foo');"});
            Assert.AreEqual("42", flashSelenium.GetVariable("Foo"));
        }

        [Test]
        public void shouldCallSetVariable()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] {"document['" + flashObjectId + "'].SetVariable('Foo','42');"});
            try
            {
                flashSelenium.SetVariable("Foo", "42");
            }
            catch(Exception e)
            {
                Assert.Fail("Should Not Fail\n" + e.Message);
            }
        }

        [Test]
        public void shouldGotoSpecifiedFrame()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].GotoFrame('42');" });
            flashSelenium.GotoFrame(42);
        }

        [Test]
        public void shouldLoadMovie()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].LoadMovie('42','/');" });
            flashSelenium.LoadMovie(42, "/");
        }

        [Test]
        public void shouldPan()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].Pan('42','24','0');" });
            flashSelenium.Pan(42, 24, 0);
        }

        [Test]
        public void shouldPlay()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].Play();" });
            flashSelenium.Play();
        }

        [Test]
        public void shouldRewind()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].Rewind();" });
            flashSelenium.Rewind();
        }
        
        [Test]
        public void shouldStopPlay()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].StopPlay();" });
            flashSelenium.StopPlay();
        }

        [Test]
        public void shouldReturnTotalFrames()
        {
            addJSPrefixCallToMock();
            mockProcessor.ExpectAndReturn("GetEval", "42", new object[] { "document['" + flashObjectId + "'].TotalFrames();" });
            Assert.AreEqual(42, flashSelenium.TotalFrames());
        }

        [Test]
        public void shouldZoomByPercent()
        {
            addJSPrefixCallToMock();
            mockProcessor.Expect("GetEval", new object[] { "document['" + flashObjectId + "'].Zoom('50');" });
            flashSelenium.Zoom(50);
        }

    }
}
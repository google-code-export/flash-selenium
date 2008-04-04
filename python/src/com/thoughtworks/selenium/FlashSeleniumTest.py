""" 
Flash Selenium - Python Client

Date: 30 March 2008
Paulo Caroli, Sachin Sudheendra
http://code.google.com/p/flash-selenium
-----------------------------------------

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
"""

from selenium import selenium
import unittest
from FlashSelenium import FlashSelenium

class FlashSeleniumTest(unittest.TestCase):
    
    URL = "http://www.geocities.com/paulocaroli/flash/colors.html"
    #URL = "http://localhost:1978/wiki/colors.html"
    
    def setUp(self):
        self.seleniumObj = selenium("localhost", 4444, "*firefox", self.URL)
        self.seleniumObj.set_speed(1000)
        self.flashSeleniumObj = FlashSelenium(self.seleniumObj, "clickcolors")
        self.flashSeleniumObj.start()
        
    def tearDown(self):
        self.flashSeleniumObj.stop()

    def testShouldOpenGoogleHomePage(self):
        seleniumObj = selenium("localhost", 4444, "*firefox", "http://www.google.co.in")
        seleniumObj.start()
        seleniumObj.open("http://www.google.co.in")
        self.assertEquals("Google", seleniumObj.get_title())
        seleniumObj.stop()
    
    def testShouldCreateFlashSeleniumObject(self):
        self.assertTrue(self.flashSeleniumObj is not None)
        self.assertEquals(FlashSelenium(None, "").__class__, self.flashSeleniumObj.__class__)

    def testShouldLoadMovie100Percent(self):
        self.flashSeleniumObj.open(self.URL)
        self.assertEquals('100', self.flashSeleniumObj.percent_loaded());
        
    def testShouldCheckIfMovieIsPlaying(self):
        self.flashSeleniumObj.open(self.URL)
        self.assertTrue(self.flashSeleniumObj.is_playing())
        
    def testShouldReturnVariableValueFromMovie(self):
        self.flashSeleniumObj.open(self.URL)
        self.assertEquals('GREEN', self.flashSeleniumObj.call('getColor'))
        self.flashSeleniumObj.call("click")
        self.assertEquals('BLUE', self.flashSeleniumObj.call('getColor'))
        
    def testShouldGetVariable(self):
        self.flashSeleniumObj.open(self.URL)
        self.flashSeleniumObj.set_variable("FooBar", "42")
        self.assertEquals("42", self.flashSeleniumObj.get_variable("FooBar"))
       
    def testShouldClickAndProceedToNextFrame(self):
        self.flashSeleniumObj.open(self.URL)
        self.flashSeleniumObj.call("click")
        self.assertEquals('BLUE', self.flashSeleniumObj.call('getColor'))
        self.flashSeleniumObj.call("click")
        self.assertEquals('RED', self.flashSeleniumObj.call('getColor'))
        
#    def testShouldJumpToSpecifiedFrame(self):
#        self.flashSeleniumObj.open(self.URL)
#        self.flashSeleniumObj.goto_frame(3)
#        self.assertEquals('BLUE', self.flashSeleniumObj.call('getColor'))
        
    def testShouldPanMovie(self):
        self.flashSeleniumObj.open(self.URL)
        try:
            self.flashSeleniumObj.zoom(10)
            self.flashSeleniumObj.pan(20, 20, 1)
        except:
            self.fail("Should not fail")
            
    def testShouldFailWhenInvalidArgumentsAreSentToPanMethod(self):
        self.flashSeleniumObj.open(self.URL)
        try:
            self.flashSeleniumObj.pan("Invalid", "Arguments", "And Strings")
            self.fail("Should have thrown exception")
        except:
            self.assertTrue(True)
        
    def testShouldZoomMovieBy10Percent(self):
        self.flashSeleniumObj.open(self.URL)
        self.flashSeleniumObj.zoom(10)
        
    def testShouldReturnTotalNumberOfFramesInMovie(self):
        self.flashSeleniumObj.open(self.URL)
        while (self.flashSeleniumObj.percent_loaded() < 100):
            pass
        self.assertEquals('1', self.flashSeleniumObj.total_frames())
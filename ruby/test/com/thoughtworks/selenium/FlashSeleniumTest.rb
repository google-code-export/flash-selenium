#
#Flash Selenium - Ruby Client
#
#Date: 30 March 2008
#Paulo Caroli, Sachin Sudheendra
#http://code.google.com/p/flash-selenium
#-----------------------------------------
#
#Licensed under the Apache License, Version 2.0 (the "License");
#you may not use this file except in compliance with the License.
#You may obtain a copy of the License at
#
#    http://www.apache.org/licenses/LICENSE-2.0
#
#Unless required by applicable law or agreed to in writing, software
#distributed under the License is distributed on an "AS IS" BASIS,
#WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
#See the License for the specific language governing permissions and
#limitations under the License.
#

require 'test/unit'
require 'ruby/src/com/thoughtworks/selenium/selenium.rb'
require 'ruby/src/com/thoughtworks/selenium/FlashSelenium.rb'

class FlashSeleniumTest < Test::Unit::TestCase
    
    URL = "http://www.geocities.com/paulocaroli/flash/colors.html"
    #URL = "http://localhost:1978/wiki/colors.html"
    
    def setup()
        @selenium = Selenium::SeleniumDriver.new("localhost", 4444, "*firefox", URL);
        @flashSelenium = FlashSelenium.new(@selenium, 'clickcolors')
        @flashSelenium.start()
        @flashSelenium.open(URL)
    end
    
    def teardown()
        @flashSelenium.stop()
    end
    
    def testShouldCheckIfMovieIsPlaying()
        assert_equal("true", @flashSelenium.is_playing())
    end
    
    def testShouldReturnPercentLoaded()
        assert_equal("100", @flashSelenium.percent_loaded())
    end
    
end
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
  
  #URL = "http://localhost:1978/wiki/colors.html"
  URL = "http://localhost:1978/wiki/test.html"
  
  def setup()
    @selenium = Selenium::SeleniumDriver.new("localhost", 4444, "*chrome", "http://localhost:4444");
    @flashSelenium = FlashSelenium.new(@selenium, 'ebrochure')
    @flashSelenium.start
    @flashSelenium.open(URL)
    @selenium.wait_for_page_to_load(2000)
  end
  
  def teardown()
    @flashSelenium.stop
  end
  
  def testShouldCheckIfMovieIsPlaying()
    assert_equal("true", @flashSelenium.is_playing)
  end
  
  def testShouldReturnFlashMoviePercentLoaded()
    assert_equal("100", @flashSelenium.percent_loaded)
  end
  
  def testShouldSetVariableIntoFlash()
    begin
      @flashSelenium.set_variable("Foo", "Bar")
    rescue
      flunk("Should Not have thrown exception")
    end
    assert_equal("Bar", @flashSelenium.get_variable("Foo"))
  end
  
  def testShouldReturnTotalFramesInFlashMovie()
    assert_equal("330", @flashSelenium.total_frames)
  end
  
  def testShouldGotoFrameSpecified()
    @flashSelenium.goto_frame(@flashSelenium.total_frames)
    assert_equal("329", @flashSelenium.t_current_frame("/"))
  end
  
  def testShouldRewindMovie()
    @flashSelenium.goto_frame(@flashSelenium.total_frames)
    assert_equal("329", @flashSelenium.t_current_frame("/"))
    @flashSelenium.rewind
    assert_equal("0", @flashSelenium.t_current_frame("/"))
  end
  
end
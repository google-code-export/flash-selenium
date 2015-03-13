Note: All clients have been updated to be compatible with SRC 1.0. Java and .NET Clients are available on the Downloads tab. Rest can be checked out from version control.

## The flash-selenium project ##
The flash-selenium project aims to extend the Selenium RC clients for adding Flash communication capabilities.

Due to the high number of requests for making FlashSelenium directly test the UI components of a Flex application, we got involved in a new open source project: [flex-ui-selenium](http://code.google.com/p/flex-ui-selenium/). The new component, FlexUISelenium, is used with Selenium RC for testing Flex UI interactions.

The Flash/Selenium RC client extension is available for the following Selenium RC client drivers: Java, .Net, Ruby and Phyton. The Selenium RC client extensions --FlashSelenium components—are available [in the Downloads session](http://code.google.com/p/flash-selenium/downloads/list).

## The FlashSelenium component ##

The FlashSelenium is the component adding Flash communication capabilities to the Selenium framework.

Basically, the FlashSelenium is a Selenium RC Client driver extension for helping exercise the tests against the Flash component.


The following is the [ColorsTest](http://code.google.com/p/flash-selenium/source/browse/trunk/java/flashselenium/test/com/thoughtworks/selenium/samples/ColorsTest.java)-–a Seleniun based JUnit test case testing [colors.html](http://code.google.com/p/flash-selenium/source/browse/trunk/flash/changingcolors/colors.html), a Web application containing the [ColoredSquare](http://code.google.com/p/flash-selenium/source/browse/trunk/flash/changingcolors/src/ColoredSquare.as) flash component. Try out the web application [here](http://www.geocities.com/paulocaroli/flash/colors.html).

```
package com.thoughtworks.selenium.samples;

import com.thoughtworks.selenium.DefaultSelenium;
import com.thoughtworks.selenium.FlashSelenium;
import com.thoughtworks.selenium.Selenium;
import com.thoughtworks.selenium.SeleniumException;
import junit.framework.TestCase;

public class ColorsTest extends TestCase {

	private FlashSelenium flashApp;
	private Selenium selenium;

	private final static String GREEN = "GREEN";
	private final static String BLUE = "BLUE";
	private final static String RED = "RED";
	private final static String URL = "http://www.geocities.com/paulocaroli/flash/colors.html";
	
	public void setUp() {
		selenium = new DefaultSelenium("localhost", 4444, "*firefox",URL);
		selenium.start();
		flashApp = new FlashSelenium(selenium, "clickcolors");
		selenium.open(URL);
		assertEquals(100, flashApp.PercentLoaded());
	}

	public void tearDown() {
		selenium.stop();
	}
	
	public void testColorTransition() {
		assertEquals("Clicking Colors", selenium.getTitle());
		assertEquals(GREEN, flashApp.call("getColor"));
		assertEquals("(Click here)", flashApp.call("getSquareLabel"));
		flashApp.call("click");
		assertEquals(BLUE, flashApp.call("getColor"));
		assertEquals(BLUE, flashApp.call("getSquareLabel"));
		flashApp.call("click");
		assertEquals(RED, flashApp.call("getColor"));
		assertEquals(RED, flashApp.call("getSquareLabel"));
		flashApp.call("click");
		assertEquals(GREEN, flashApp.call("getColor"));
		assertEquals(GREEN, flashApp.call("getSquareLabel"));
	}
	
	public void testRectangleLabel() {
		assertEquals("(Click here)", flashApp.call("getSquareLabel"));
		flashApp.call("setSquareLabel", "Dummy Label");
		assertEquals("Dummy Label", flashApp.call("getSquareLabel"));
	}
}
```

## Selenium RC / Flash Integration ##

Selenium RC uses JavaScript to communicate with the browser. And Flash ExternalInterface provides a mechanism for which you can use JavaScript to call an ActionScript function in the Flash Player. Therefore we can use JavaScript as the conduit between Selenium RC and the Flash application.

This [figure](http://www.geocities.com/paulocaroli/flash/SRClientServerCommunication.jpg) shows the usage of JavaScript for connecting Selenium RC and the Flash application.


With Flash ExternalInterface you can expose specific Flash object functions.For example, the following code adds external invocation capabilities to some of the [ColoredSquare](http://code.google.com/p/flash-selenium/source/browse/trunk/flash/changingcolors/src/ColoredSquare.as) Flash component methods.
```
      // functions available for JavaSript call
      ExternalInterface.addCallback("getColor", getColor);
      ExternalInterface.addCallback("click", changeColor);
      ExternalInterface.addCallback("getSquareLabel", getSquareLabel);
      ExternalInterface.addCallback("setSquareLabel", setSquareLabel);
```

On the testing side, The FlashSelenium is the component adding Flash communication capabilities to the Selenium framework. Basically,  the FlashSelenium is a Selenium RC Client driver extension for helping exercise the tests against the Flash component. The FlashSelenium constructor takes a Selenium instance and a flash object id as parameters. An instance of FlashSelenium is used to invoke the functions on the Flash component.

You can invoke functions which were externalized by the ExternalInterface, as well as the default functions of any flash object (e.g, PercentLoaded(), IsPlaying(), etc). The following are code snapshots from the [ColorsTest](http://code.google.com/p/flash-selenium/source/browse/trunk/java/flashselenium/test/com/thoughtworks/selenium/samples/ColorsTest.java)—the sample Seleniun based JUnit test.

```
	assertEquals(100, flashApp.PercentLoaded());
	assertEquals("(Click here)", flashApp.call("getSquareLabel"));
```
package com.thoughtworks.selenium.samples;

import com.thoughtworks.selenium.DefaultSelenium;
import com.thoughtworks.selenium.FlashSelenium;
import com.thoughtworks.selenium.Selenium;
import junit.framework.TestCase;


public class ColorsTest extends TestCase {

	private FlashSelenium flashApp;
	private Selenium selenium;

	private final static String GREEN = "GREEN";
	private final static String BLUE = "BLUE";
	private final static String RED = "RED";
	private final static String BASE_URL = "http://www.geocities.com/";
	private final static String PAGE = "paulocaroli/flash/colors.html";
	
	public void setUp() {
		selenium = new DefaultSelenium("localhost", 4444, "*iexplore",BASE_URL);
		selenium.start();
		flashApp = new FlashSelenium(selenium, "clickcolors");
		selenium.open(PAGE);
		assertEquals(100, flashApp.PercentLoaded());
	}

	public void tearDown() {
		selenium.stop();
	}
	
//	public void testColorTransition() {
//		assertEquals("Clicking Colors", selenium.getTitle());
//		assertEquals(GREEN, flashApp.call("getColor"));
//		assertEquals("(Click here)", flashApp.call("getSquareLabel"));
//		flashApp.call("click");
//		assertEquals(BLUE, flashApp.call("getColor"));
//		assertEquals(BLUE, flashApp.call("getSquareLabel"));
//		flashApp.call("click");
//		assertEquals(RED, flashApp.call("getColor"));
//		assertEquals(RED, flashApp.call("getSquareLabel"));
//		flashApp.call("click");
//		assertEquals(GREEN, flashApp.call("getColor"));
//		assertEquals(GREEN, flashApp.call("getSquareLabel"));
//	}
	
	public void testRectangleLabel() {
		assertEquals("(Click here)", flashApp.call("getSquareLabel"));
		flashApp.call("setSquareLabel", "Dummy Label");
		assertEquals("Dummy Label", flashApp.call("getSquareLabel"));
	}


}

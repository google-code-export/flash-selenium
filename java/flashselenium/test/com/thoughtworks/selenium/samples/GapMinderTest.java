package com.thoughtworks.selenium.samples;

import com.thoughtworks.selenium.DefaultSelenium;
import com.thoughtworks.selenium.FlashSelenium;
import com.thoughtworks.selenium.Selenium;
import com.thoughtworks.selenium.SeleniumException;

import junit.framework.TestCase;


public class GapMinderTest extends TestCase {
	private FlashSelenium flashApp;
	private Selenium selenium;
	private final static String URL = "http://www.gapminder.org/world/";
	


	public void setUp() {
		selenium = new DefaultSelenium("localhost", 4444, "*chrome", URL);
		selenium.start();
		flashApp = new FlashSelenium(selenium, "uid");
	}

	public void tearDown() {
		try {
			selenium.waitForPageToLoad("20000");
		} catch (SeleniumException e) {
		}
		selenium.stop();
	}

	public void testGapMinderFlashHRef() {
		selenium.open(URL);
		assertTrue(flashApp
				.GetVariable("href").startsWith(URL));
		assertFalse(flashApp.IsPlaying());
		assertEquals(100, flashApp.PercentLoaded());
		assertEquals(4, flashApp.TotalFrames());
	}



}

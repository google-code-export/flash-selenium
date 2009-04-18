package com.thoughtworks.selenium.samples;

import com.thoughtworks.selenium.DefaultSelenium;
import com.thoughtworks.selenium.FlashSelenium;
import com.thoughtworks.selenium.Selenium;
import junit.framework.TestCase;

public class NikeTest extends TestCase {
	private FlashSelenium flashApp;
	private Selenium selenium;


	private final static String URL = "http://www.nike.com/g1/eu/index.jhtml?lang=1,4";

	public void setUp() {
		selenium = new DefaultSelenium("localhost", 4444, "*chrome", URL);
		selenium.start();
		flashApp = new FlashSelenium(selenium, "flid");
		selenium.open(URL);
	}

	public void tearDown() {
		selenium.stop();
	}

	public void testColorTransition() {
		assertEquals("Nike.com", selenium.getTitle());
		// flash movie is fully loaded
		assertEquals(100, flashApp.PercentLoaded());
		// still not playing when page is loaded
		assertFalse(flashApp.IsPlaying());
		// ensures the movie has one frame only
		assertEquals(20, flashApp.TotalFrames());

	}




}
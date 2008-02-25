package com.thoughtworks.selenium;

import junit.framework.TestCase;

/*
 * Tests the JS string to be passed to Selenium GetEval()
 * 
 */
public class TestJSStringPrefix extends TestCase {

	private FlashSelenium flashApp;

	private static final String FLASH_OBJ_ID = "FLASH_OBJ_ID";
	private static final String FUNCTION = "FUNCTION";
	private static final String PARAM1 = "PARAM1";
	private static final String PARAM2 = "PARAM2";
	
	private String flashMovieObj;
	
	public void setUp() {
		flashApp = FlashSelenium.createFlashSeleniumFlashObjAsWindowDocument(null, FLASH_OBJ_ID);
		flashMovieObj = flashApp.flashJSStringPrefix();
	}

	public void tearDown() {
		flashApp = null;
	}
	
	public void testFlashJSStringPrefixFunctionWithoutParameters() {
		assertEquals(flashMovieObj + "FUNCTION();", flashApp.jsForFunction("FUNCTION"));
	}

	public void testFlashJSStringPrefixFunctionWithOneParameter() {
		assertEquals(flashMovieObj + "FUNCTION('PARAM1');", flashApp.jsForFunction(FUNCTION, PARAM1));
	}
	
	public void testFlashJSStringPrefixFunctionWithSeveralParameters() {
		assertEquals(flashMovieObj + "FUNCTION('PARAM1','PARAM2');", flashApp.jsForFunction(FUNCTION, PARAM1, PARAM2));
	}	

}

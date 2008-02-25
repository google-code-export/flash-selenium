package com.thoughtworks.selenium;

import static org.easymock.EasyMock.createMock;
import static org.easymock.EasyMock.expect;
import static org.easymock.EasyMock.replay;
import static org.easymock.EasyMock.verify;
import junit.framework.TestCase;

/*
 * Test FlashSelenium calls to the any function 
 * on a Flash object
 * 
 * Functions must be "externalized" (e.g., by means of ExternalInterface)
 * by the Flash component in order to be successfully called.
 * 
 */
public class TestFlashFunctionCall extends TestCase {

	private FlashSelenium flashApp;
	private Selenium selenium;

	private static final String FLASH_OBJ_ID = "FLASH_OBJ_ID";
	private static final String FUNCTION = "FUNCTION";
	private static final String PARAM1 = "PARAM1";
	private static final String PARAM2 = "PARAM2";
	private static final String RETURN_VALUE = "RETURN_VALUE";
	
	private String flashMovieObj;
	
	public void setUp() {
		selenium = createMock(Selenium.class);
		flashApp = FlashSelenium.createFlashSeleniumFlashObjAsWindowDocument(selenium, FLASH_OBJ_ID);
		flashMovieObj = flashApp.flashJSStringPrefix();
	}

	public void tearDown() {
		selenium = null;
		flashApp = null;
	}
	
	public void testCallFunctionShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "FUNCTION('PARAM1','PARAM2');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(RETURN_VALUE);
		replay(selenium);
		assertEquals(RETURN_VALUE, flashApp.call(FUNCTION, PARAM1, PARAM2));
		verify(selenium);
	}		
	

}

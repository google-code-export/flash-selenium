package com.thoughtworks.selenium;

import com.thoughtworks.selenium.Selenium;

import junit.framework.TestCase;
import static org.easymock.EasyMock.*;

/*
 * Test FlashSelenium calls to the Flash Methods
 * 
 * A Flash method is JavaScript function that is specific 
 * to Flash movies. Use Flash methods to send JavaScript 
 * calls to Flash movies from a scripting environment. 
 * Each method has a name and most methods take arguments.
 *  An argument specifies a value that the method operates 
 *  upon. The calculation performed by some methods returns 
 *  a value that can be used by the scripting environment.
 *  
 *  Please find a full description of the Flash methods at:
 *  http://www.adobe.com/support/flash/publishexport/scriptingwithflash/scriptingwithflash_03.html
 */
public class TestFlashMethodCalls extends TestCase {

	private FlashSelenium flashApp;
	private Selenium selenium;

	private static final String FLASH_OBJ_ID = "FLASH_OBJ_ID";
	private static final String STR_PARAM1 = "PARAM1";
	private static final String STR_PARAM2 = "PARAM2";
	private static final String STR_PARAM3 = "PARAM3";
	private static final String STR_RETURN_VALUE = "STR_RETURN_VALUE";
	private static final int INT_PARAM1 = 1;
	private static final int INT_PARAM2 = 2;
	private static final int INT_PARAM3 = 3;
	private static final int INT_PARAM4 = 4;
	private static final String EMPTY_STR = "";
	
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

	public void testGetVariableShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "GetVariable('" 
												+ STR_PARAM1 
												+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(STR_RETURN_VALUE);
		replay(selenium);
		assertEquals(STR_RETURN_VALUE, flashApp.GetVariable(STR_PARAM1));
		verify(selenium);
	}
	

	public void testGotoFrameShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "GotoFrame('" 
												+ INT_PARAM1 
												+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(STR_RETURN_VALUE);
		replay(selenium);
		flashApp.GotoFrame(INT_PARAM1);
		verify(selenium);
	}
	
	public void testIsPlayingShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "IsPlaying();"; 
		expect(selenium.getEval(expectedEvalArg)).andReturn("true");
		replay(selenium);
		assertTrue(flashApp.IsPlaying());
		verify(selenium);
	}
	
	public void testLoadMovieShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "LoadMovie('" 
											+ INT_PARAM1 
											+ "','" 
											+ STR_PARAM1 
											+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.LoadMovie(INT_PARAM1, STR_PARAM1);
		verify(selenium);
	}
	
	public void testPanShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "Pan('" 
											+ INT_PARAM1 
											+ "','" 
											+ INT_PARAM2 
											+ "','" 
											+ INT_PARAM3 
											+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.Pan(INT_PARAM1, INT_PARAM2, INT_PARAM3);
		verify(selenium);
	}
	
	public void testPercentLoadedShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "PercentLoaded();"; 
		expect(selenium.getEval(expectedEvalArg)).andReturn("20");
		replay(selenium);
		assertEquals(20, flashApp.PercentLoaded());
		verify(selenium);
	}
	
	public void testPlayShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "Play();"; 
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.Play();
		verify(selenium);
	}
	
	public void testRewindShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "Rewind();"; 
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.Rewind();
		verify(selenium);
	}
	
	public void testSetVariableShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "SetVariable('" 
												+ STR_PARAM1 
												+ "','" 
												+ STR_PARAM2 
												+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.SetVariable(STR_PARAM1, STR_PARAM2);
		verify(selenium);
	}
	
	
	public void testSetZoomRectShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "SetZoomRect('" 
											+ INT_PARAM1 
											+ "','" 
											+ INT_PARAM2 
											+ "','" 
											+ INT_PARAM3 
											+ "','" 
											+ INT_PARAM4 
											+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.SetZoomRect(INT_PARAM1, INT_PARAM2, INT_PARAM3, INT_PARAM4);
		verify(selenium);
	}
	
	public void testStopPlayShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "StopPlay();"; 
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.StopPlay();
		verify(selenium);
	}
	
	public void testTotalFramesShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "TotalFrames();"; 
		expect(selenium.getEval(expectedEvalArg)).andReturn("20");
		replay(selenium);
		assertEquals(20, flashApp.TotalFrames());
		verify(selenium);
	}
	
	public void testZoomShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "Zoom('" 
											+ INT_PARAM1 
											+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.Zoom(INT_PARAM1);
		verify(selenium);
	}
	
	public void testTCallFrameShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "TCallFrame('" 
											+ STR_PARAM1 
											+ "','" 
											+ INT_PARAM1 
											+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.TCallFrame(STR_PARAM1, INT_PARAM1);
		verify(selenium);
	}
	
	public void testTCallLabelShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "TCallLabel('" 
											+ STR_PARAM1 
											+ "','" 
											+ STR_PARAM2 
											+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.TCallLabel(STR_PARAM1, STR_PARAM2);
		verify(selenium);
	}
	
	public void testTCurrentFrameShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "TCurrentFrame('" 
											+ STR_PARAM1 
											+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn("3");
		replay(selenium);
		assertEquals(3, flashApp.TCurrentFrame(STR_PARAM1));
		verify(selenium);
	}
	
	public void testTCurrentLabelShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "TCurrentLabel('" 
											+ STR_PARAM1 
											+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(STR_RETURN_VALUE);
		replay(selenium);
		assertEquals(STR_RETURN_VALUE, flashApp.TCurrentLabel(STR_PARAM1));
		verify(selenium);
	}
	
	public void testTGetPropertyShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "TGetProperty('" 
										+ STR_PARAM1 
										+ "','" 
										+ STR_PARAM2 
										+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(STR_RETURN_VALUE);
		replay(selenium);
		assertEquals(STR_RETURN_VALUE, flashApp.TGetProperty(STR_PARAM1, STR_PARAM2));
		verify(selenium);
	}
	
	public void testTGetPropertyAsNumberShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "TGetPropertyAsNumber('" 
										+ STR_PARAM1 
										+ "','" 
										+ STR_PARAM2 
										+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn("4");
		replay(selenium);
		assertEquals(4, flashApp.TGetPropertyAsNumber(STR_PARAM1, STR_PARAM2));
		verify(selenium);
	}

	public void testTGotoFrameShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "TGotoFrame('" 
										+ STR_PARAM1 
										+ "','" 
										+ INT_PARAM1 
										+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.TGotoFrame(STR_PARAM1, INT_PARAM1);
		verify(selenium);
	}
	
	public void testTGotoLabelShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "TGotoLabel('" 
										+ STR_PARAM1 
										+ "','" 
										+ STR_PARAM2 
										+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.TGotoLabel(STR_PARAM1, STR_PARAM2);
		verify(selenium);
	}
	
	public void testTPlayShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "TPlay('" 
										+ STR_PARAM1 
										+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.TPlay(STR_PARAM1);
		verify(selenium);
	}
	
	public void testTSetPropertyShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "TSetProperty('" 
										+ STR_PARAM1 
										+ "','" 
										+ STR_PARAM2 
										+ "','" 
										+ STR_PARAM3 
										+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.TSetProperty(STR_PARAM1, STR_PARAM2, STR_PARAM3);
		verify(selenium);
	}
	
	public void testTStopPlayShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "TStopPlay('" 
										+ STR_PARAM1 
										+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.TStopPlay(STR_PARAM1);
		verify(selenium);
	}
	
	public void testOnProgressShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "OnProgress('" 
											+ INT_PARAM1 
											+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.OnProgress(INT_PARAM1);
		verify(selenium);
	}
	
	
	public void testOnReadyStateChangeShouldCallJSWithSeleniumGetEval() {
		String expectedEvalArg = flashMovieObj + "OnReadyStateChange('" 
											+ INT_PARAM1 
											+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(EMPTY_STR);
		replay(selenium);
		flashApp.OnReadyStateChange(INT_PARAM1);
		verify(selenium);
	}
	
	public void testFSCommandShouldCallJSWithSeleniumGetEval() {
		String COMMAND = "COMMAND";
		String [] args = {STR_PARAM1, STR_PARAM2};
		String expectedEvalArg = flashMovieObj + "FSCommand('" 
										+ COMMAND 
										+ "','" 
										+ STR_PARAM1 
										+ "','" 
										+ STR_PARAM2 
										+ "');";
		expect(selenium.getEval(expectedEvalArg)).andReturn(STR_RETURN_VALUE);
		replay(selenium);
		assertEquals(STR_RETURN_VALUE, flashApp.FSCommand(COMMAND, args));
		verify(selenium);
	}
	
}

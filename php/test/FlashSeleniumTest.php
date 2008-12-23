<?php
	
require_once '..\src\FlashSelenium.php';
require_once( '..\src\Selenium.php' );
require_once ('PHPUnit\Framework.php');

class FlashSeleniumTest extends PHPUnit_Framework_TestCase
{
	
	public function testShouldReturnDocumentPrefix ()
	{
		$flashSelenium  = new FlashSeleniumStub(null, "4242"); 
		$this->assertEquals("document['4242'].", $flashSelenium->createJSPrefix_document());
	}
	
	public function testShouldReturnWindowDocumentPrefix ()
	{
		$flashSelenium  = new FlashSeleniumStub(null, "4242");
		$this->assertEquals("window.document['4242'].", $flashSelenium->createJSPrefix_window_document());
	}
	
	public function testShouldReturnJSForFunctionForSingleFunctionParameter ()
	{
		$flashSelenium  = new FlashSeleniumStub(null, "4242");
		$retVal = $flashSelenium->jsForFunction("Function1", "42");
		$this->assertEquals("Function1('42');", $retVal);
	}
	
	public function testShouldReturnJSForFunctionForTwoFunctionParameters ()
	{
		$flashSelenium  = new FlashSeleniumStub(null, "4242");
		$retVal = $flashSelenium->jsForFunction("Function1", "42", "24");
		$this->assertEquals("Function1('42','24');", $retVal);
	}
	
	public function testShouldReturnJSForFunctionWithNoParameter ()
	{
		$flashSelenium  = new FlashSeleniumStub(null, "4242");
		$retVal = $flashSelenium->jsForFunction("Function1");
		$this->assertEquals("Function1();", $retVal);
	}
	
	public function testShouldReturnJSFunctionCallForFirefox2 ()
	{
		$seleniumMock = $this->getMock('Testing_Selenium', array('getEval'), array('*chrome','http://localhost'), '', false);
		$seleniumMock->expects($this->any())->method('getEval')->with($this->equalTo('navigator.userAgent'))->will($this->returnValue('Mozilla/5.0 (Windows; U; Windows NT 6.0; en-GB; rv:1.8.1.19) Gecko/20081201 Firefox/2.0.0.19'));
		$flashSelenium = new FlashSelenium($seleniumMock, "blah");
		$retVal = $flashSelenium->checkBrowserAndReturnJSPrefix();
		$this->assertEquals("document['blah'].", $retVal);
	}
	
	public function testShouldReturnJSFunctionCallForFirefox3 ()
	{
		$seleniumMock = $this->getMock('Testing_Selenium', array('getEval'), array('*chrome','http://localhost'), '', false);
		$seleniumMock->expects($this->any())->method('getEval')->with($this->equalTo('navigator.userAgent'))->will($this->returnValue('Mozilla/5.0 (Windows; U; Windows NT 6.0; en-GB; rv:1.8.1.19) Gecko/20081201 Firefox/3.0.0.1'));
		$flashSelenium = new FlashSelenium($seleniumMock, "blah");
		$retVal = $flashSelenium->checkBrowserAndReturnJSPrefix();
		$this->assertEquals("window.document['blah'].", $retVal);
	}
	
	public function testShouldReturnJSFunctionCallForMSIE ()
	{
		$seleniumMock = $this->getMock('Testing_Selenium', array('getEval'), array('*iexplore','http://localhost'), '', false);
		$seleniumMock->expects($this->any())->method('getEval')->with($this->equalTo('navigator.userAgent'))->will($this->returnValue('MSIE'));
		$flashSelenium = new FlashSelenium($seleniumMock, "blah");
		$retVal = $flashSelenium->checkBrowserAndReturnJSPrefix();
		$this->assertEquals("window.document['blah'].", $retVal);
	}
	
	public function testBrowserComparison ()
	{
		$browserConstants = new BrowserConstants();
		$this->assertTrue(strripos('Mozilla/5.0 (Windows; U; Windows NT 6.0; en-GB; rv:1.8.1.19) Gecko/20081201 Firefox/3.0.0.1', $browserConstants->Firefox3()) > 0);
		$this->assertTrue(strripos('Mozilla/5.0 (Windows; U; Windows NT 6.0; en-GB; rv:1.8.1.19) Gecko/20081201 Firefox/3.0.0.1', 'Mozilla') == 0);
		$this->assertTrue(strripos('Mozilla/5.0 (Windows; U; Windows NT 6.0; en-GB; rv:1.8.1.19) Gecko/20081201 Firefox/3.0.0.1', 'UU') == false);
		$this->assertTrue(strripos('Mozilla/5.0 (Windows; U; Windows NT 6.0; en-GB; rv:1.8.1.19) Gecko/20081201 Firefox/2.0.0.1', $browserConstants->Firefox2()) > 0);
	}
	

}

#doc
#	classname:	FlashSeleniumStub
#	scope:		PUBLIC
#
#/doc

class FlashSeleniumStub extends FlashSelenium
{
	#	internal variables
	
	#	Constructor
	function __construct ( $selenium, $flashObjectId )
	{
		parent::__construct($selenium, $flashObjectId);
	}
	###	
	
	public function createJSPrefix_document ()
	{
		return parent::createJSPrefix_document();
	}
	
	public function createJSPrefix_window_document ()
	{
		return parent::createJSPrefix_window_document();
	}
	
}
###
?>

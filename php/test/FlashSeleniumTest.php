<?php
	
require_once '..\src\FlashSelenium.php';
require_once ('PHPUnit\Framework.php');

class FlashSeleniumTest extends PHPUnit_Framework_TestCase
{
	
	public function testShouldReturnDocumentPrefix ()
	{
		$flashSelenium  = new FlashSeleniumStub(null, "UngaBunga"); 
		$this->assertEquals("document['UngaBunga'].", $flashSelenium->createJSPrefix_document());
	}
	
	public function testShouldReturnWindowDocumentPrefix ()
	{
		$flashSelenium  = new FlashSeleniumStub(null, "UngaBunga");
		$this->assertEquals("window.document['UngaBunga'].", $flashSelenium->createJSPrefix_window_document());
	}
	
	public function testShouldReturnJSForFunctionForSingleFunctionParameter ()
	{
		$flashSelenium  = new FlashSeleniumStub(null, "UngaBunga");
		$retVal = $flashSelenium->jsForFunction("Function1", "42");
		$this->assertEquals("Function1('42');", $retVal);
	}
	
	public function testShouldReturnJSForFunctionForTwoFunctionParameters ()
	{
		$flashSelenium  = new FlashSeleniumStub(null, "UngaBunga");
		$retVal = $flashSelenium->jsForFunction("Function1", "42", "24");
		$this->assertEquals("Function1('42','24');", $retVal);
	}
	
	public function testShouldReturnJSForFunctionWithNoParameter ()
	{
		$flashSelenium  = new FlashSeleniumStub(null, "UngaBunga");
		$retVal = $flashSelenium->jsForFunction("Function1");
		$this->assertEquals("Function1();", $retVal);
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

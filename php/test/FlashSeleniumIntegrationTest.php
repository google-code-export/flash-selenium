<?php
require_once '..\src\FlashSelenium.php';
require_once( '..\src\Selenium.php' );
require_once ('PHPUnit\Framework.php');

class FlashSeleniumIntegrationTest extends PHPUnit_Framework_TestCase
{

	private $flashSelenium; 
	private $selenium;

	public function setUp ()
	{
		$this->selenium = new Testing_Selenium('*chrome', 'http://localhost');
		$this->flashSelenium = new FlashSelenium($this->selenium, null);
		$this->flashSelenium->start();
	}	
	public function testShouldPerformSeleniumOperations ()
	{
		$this->flashSelenium->open('http://www.google.co.in');
		$retVal = $this->selenium->getTitle();
		$this->assertEquals('Google', $retVal);
	}
	
	public function tearDown ()
	{
		$this->flashSelenium->stop();
	}
}
?>
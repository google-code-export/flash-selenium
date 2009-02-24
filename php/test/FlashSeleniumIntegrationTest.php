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
		$this->flashSelenium = new FlashSelenium($this->selenium, 'clickcolors');
		$this->flashSelenium->start();
	}	
	
	public function xtestShouldPerformSeleniumOperations ()
	{
		$this->flashSelenium->open('http://localhost');
		$retVal = $this->selenium->getTitle();
		$this->assertEquals('IIS7', $retVal);
	}
	
	public function testShouldReturnTrueIfFlashMovieIsPlaying ()
	{
		$this->flashSelenium->open('http://localhost/colors.html');
		print $this->flashSelenium->isPlaying();
	}

	public function tearDown ()
	{
		$this->flashSelenium->stop();
	}
}
?>
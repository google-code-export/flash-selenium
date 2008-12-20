<?php

require_once 'Selenium.php';

class FlashSelenium
{

    private $selenium;
    private $flashObjectId;

    public function __construct($selenium, $flashObjectId) {
        $this->selenium = $selenium;
        $this->flashObjectId = $flashObjectId;
    }

    public function start()
    {
        $this->selenium.start();
    }

    public function stop()
    {
        $this->selenium.stop();
    }
    
    public function open($url) {
    	$this->selenium.open($url);
    }
    
    public function waitForPageLoad($timeout) {
		$this->selenium.waitForPageToLoad($timeout);
	}
	
	public function call($function, $args = array()) {
		@this->selenium.getEval(jsForFunction($function, $args));
	}

}
?>
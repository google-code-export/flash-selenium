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

}
?>
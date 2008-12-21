<?php

/*
Flash Selenium - PHP Client

Date: 20 December 2008
Paulo Caroli, Sachin Sudheendra
http://code.google.com/p/flash-selenium
-----------------------------------------

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

#require_once( 'Selenium.php' );
require_once( 'BrowserConstants.php' );

class FlashSelenium
{

    private $selenium;
    private $flashObjectId;
    private $jsPrefix;

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
	
	public function call($function) {
        $this->jsPrefix = checkBrowserAndReturnJSPrefix();
		return $this->selenium.getEval(jsForFunction($function, func_get_args()));
	}
    
    
    # Internal Functions
    public function jsForFunction ($functionName)
    {
        $functionArgs = "";
        $params = func_get_args();
        if ( count($params) > 1  )
        {
        	for ( $i=1; $i < count($params); ++$i )
            { 
            	$functionArgs = $functionArgs . "'" . $params[$i] . "',";
            }
        }
        return $this->jsPrefix . $functionName . "(" . substr($functionArgs, 0, -1) . ");"; 
    }
    
    private function checkBrowserAndReturnJSPrefix ()
    {
    	$appName = $this->selenium.getEval("navigator.userAgent");
        $browserConstants = new BrowserConstants();
        if (substr($appName, $browserConstants->Firefox3()) or substr($appName, $browserConstants->IE()))
        {
        	return createJSPrefix_window_document();
        }
        return createJSPrefix_document();
    }
    
    protected function createJSPrefix_document ()
    {
    	return "document['" . $this->flashObjectId . "'].";
    }
    
    protected function createJSPrefix_window_document ()
    {
    	return "window.document['" . $this->flashObjectId . "'].";
    }
    
}
?>
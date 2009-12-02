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

require_once( 'Selenium.php' );
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
        $this->selenium->start();
    }

    public function stop()
    {
        $this->selenium->stop();
    }
    
    public function open($url) {
    	$this->selenium->open($url);
    }
    
    public function waitForPageLoad($timeout) {
		$this->selenium->waitForPageToLoad($timeout);
    }

	
	public function call() {
        	$params = func_get_args();
        	$this->jsPrefix = $this->checkBrowserAndReturnJSPrefix();
        	$function = $this->jsForFunction($params[0], $params);
		return $this->selenium->getEval($function);
	}
    
    #Standard Methods
    public function isPlaying ()
    {
    	return $this->call('IsPlaying');
    }
    
    public function percentLoaded() 
    {
        return $this->call('PercentLoaded');
    }
    
    public function getVariable ($name)
    {
    	return $this->call("GetVariable", $name);
    }
    
    public function setVariable ($name, $value)
    {
    	return $this->call("SetVariable", $name, $value);
    }
    
    public function gotoFrame ($value)
    {
    	return $this->call("GotoFrame", $value);
    }
    
    public function loadMovie ($layerNumber, $url)
    {
    	return $this->call("LoadMovie", $layerNumber, $url);
    }
    
    public function pan ($x, $y, $mode)
    {
    	return $this->call("Pan", $x, $y, $mode);
    }
    
    public function play ()
    {
    	return $this->call("Play");
    }
    
    public function rewind ()
    {
    	return $this->call("Rewind");
    }
    
    public function setZoomRect ($left, $top, $right, $bottom)
    {
    	return $this->call("SetZoomRect", $left, $top, $right, $bottom);
    }
    
    public function stopPlay ()
    {
    	return $this->call("StopPlay");
    }
    
    public function totalFrames ()
    {
    	return $this->call("TotalFrames");
    }
    
    public function zoom ($percent)
    {
    	return $this->call("Zoom", $percent);
    }
    
    # TellTarget Methods
    public function tCallFrame ($target, $frameNumber)
    {
    	return $this->call("TCallFrame", $target, $frameNumber);
    }
    
    public function tCallLabel ($target, $label)
    {
    	return $this->call("TCallLabel", $target, $label);
    }
    
    public function tCurrentFrame($target) 
    {
        return $this->call("TCurrentFrame", $target);
    }

    public function tCurrentLabel($target)
    {
        return $this->call("TCurrentLabel", $target);
    }

    public function tGetProperty($target, $property)
    {
        return $this->call("TGetProperty", $target, $property);
    }

    public function tGetPropertyAsNumber($target, $property)
    {
        return $this->call("TGetPropertyAsNumber", $target, $property);
    }
    
    public function tGotoFrame($target, $frameNumber)
    {
        return $this->call("TGotoFrame", $target, $frameNumber);
    }

    public function tGotoLabel($target, $label)
    {
        return $this->call("TGotoLabel", $target, $label);
    }

    public function tPlay($target)
    {
        return $this->call("TPlay", $target);
    }

    public function tSetProperty($property, $value)
    {
        return $this->call("TSetProperty", $property, $value);
    }

    public function tStopPlay($target)
    {
        return $this->call("TStopPlay", $target);
    }
    
    # Standard Events
    public function onProgress ($percent)
    {
    	return $this->call("OnProgress", $percent);
    }
    
    public function onReadyStateChange ($state)
    {
    	return $this->call("OnReadyStateChange", $state);
    }
    
    # Internal Functions
    public function jsForFunction ($functionName, $params)
    {
        $functionArgs = "";
        #$params = func_get_args();
        if ( count($params) > 1 and $params != NULL )
        {
        	for ( $i=1; $i < count($params); $i++ )
            { 
            	$functionArgs = $functionArgs . "'" . $params[$i] . "',";
            }
        }
        return $this->jsPrefix . $functionName . '(' . substr($functionArgs, 0, -1) . ');'; 
    }
    
    public function checkBrowserAndReturnJSPrefix ()
    {
	return $this->createJSPrefix_browserbot();
    }
    
    protected function createJSPrefix_browserbot ()
    {
    	return "this.browserbot.findElement(\"" . $this->flashObjectId . "\").";
    }
    
}
?>

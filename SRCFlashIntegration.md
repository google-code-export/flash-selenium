# Selenium RC / Flash Integration #

Selenium RC uses JavaScript to communicate with the browser. And Flash ExternalInterface provides a mechanism for which you can use JavaScript to call an ActionScript function in the Flash Player. Therefore we can use JavaScript as the conduit between Selenium RC and the Flash application.

The [figure](http://www.caroli.org/flash/SRClientServerCommunication.jpg) shows the usage of JavaScript for connecting Selenium RC and the Flash application.


With Flash ExternalInterface you can expose specific Flash object functions.
```
      // functions available for JavaSript call
      ExternalInterface.addCallback("getColor", getColor);
      ExternalInterface.addCallback("click", changeColor);
      ExternalInterface.addCallback("getSquareLabel", getSquareLabel);
      ExternalInterface.addCallback("setSquareLabel", setSquareLabel);
```

The FlashSelenium is the component adding Flash communication capabilities to the Selenium framework. Basically,  the FlashSelenium is a Selenium RC Client driver extension for helping exercise the tests against the Flash component. The FlashSelenium constructor takes a Selenium instance and a flash object id as parameters. An instance of FlashSelenium is used to invoke the functions on the Flash component.

You can invoke functions which were externalized by the ExternalInterface, as well as the default functions of any flash object (e.g, PercentLoaded(), IsPlaying(), etc).

```
	assertEquals(100, flashApp.PercentLoaded());
	assertEquals("(Click here)", flashApp.call("getSquareLabel"));
```

The FlashSelenium is currently available for Java. Analogous components will be created for the other Selenium RC client drivers--.Net, ruby, Phyton and PHP.

package {
  import flash.display.Sprite;
  import flash.events.MouseEvent;  
  import flash.external.ExternalInterface;
  import flash.text.TextField;
  import flash.text.TextFieldAutoSize;
  import flash.text.TextFormat;

  public class ColoredSquare extends Sprite {
    private const COLOR_HEX_VALUES:Array = [0x00ff00,0x0000ff, 0xff0000];
    private const COLORS:Array = ["GREEN","BLUE", "RED"];
    private const SQUARE_SIDE:uint = 500;
    private var currentColor:uint;
    private var currentColorIndex:uint = 0;
    private var buttonSprite:Sprite = new Sprite();
    private var squareLabel:String = "(Click here)";
    private var label:TextField = new TextField();

    public function ColoredSquare() {
      currentColor = COLOR_HEX_VALUES[currentColorIndex % 3];
      updateButtonLabel();
      drawButton();
      addEventListener(MouseEvent.CLICK, buttonClicked);
      
      // functions available for JavaSript call
      ExternalInterface.addCallback("getColor", getColor);
      ExternalInterface.addCallback("click", changeColor);
      ExternalInterface.addCallback("getSquareLabel", getSquareLabel);
      ExternalInterface.addCallback("setSquareLabel", setSquareLabel);
    }

    private function buttonClicked(event:MouseEvent):void {
      changeColor();    
    }

    private function changeColor():void {
      currentColorIndex++;
      currentColor = COLOR_HEX_VALUES[currentColorIndex % 3];
      this.squareLabel = COLORS[currentColorIndex % 3];
      updateButtonLabel();
      drawButton();
    }

    private function updateButtonLabel():void {
      var format:TextFormat = new TextFormat();
      format.size = SQUARE_SIDE / 10;
      var label:TextField = new TextField();
      label.autoSize = TextFieldAutoSize.LEFT;
      label.selectable = false;
      label.defaultTextFormat = format;
      label.text = squareLabel;
      label.x = SQUARE_SIDE / 5;
      label.y = SQUARE_SIDE / 2.5;
      if (buttonSprite.contains(this.label)) {
        buttonSprite.removeChild(this.label);
      }
      this.label = label;   
      buttonSprite.addChild(this.label);    
    }

    private function drawButton():void {
      buttonSprite.graphics.beginFill(currentColor);
      buttonSprite.graphics.drawRect(0, 0, SQUARE_SIDE, SQUARE_SIDE);
      buttonSprite.graphics.endFill();
      addChild(buttonSprite);
    }

    public function setSquareLabel(squareLabel:String):void {
      this.squareLabel = squareLabel;
      updateButtonLabel();
      drawButton();
    }

    public function getColor():String {
      return COLORS[currentColorIndex % 3];
    }

    public function getSquareLabel():String {
      return this.squareLabel;
    }
  }
}

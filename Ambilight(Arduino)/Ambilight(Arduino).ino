#include <Adafruit_NeoPixel.h>

int PIN = 6;
int numPixels = 23;

Adafruit_NeoPixel strip = Adafruit_NeoPixel(numPixels, PIN, NEO_GRB + NEO_KHZ800);

String hexstring;
String data;
int led;
long number;
long r;
long g;
long b;

void setup() {
	strip.begin();

	for(int i = 0; i < numPixels; i++){
	    SetLed(i, 0, 0, 0);
	}

	Serial.begin(115200);
}

void loop() {
	data = Serial.readStringUntil('\n');

	if(data != ""){
		ProcessData(data);
	}
}

//00 FFFFFF
void ProcessData(String data){
	led = data.substring(0, 2).toInt();
	hexstring = data.substring(3);

	number = strtol( &hexstring[0], NULL, 16);

	// Split them up into r, g, b values
	r = number >> 16;
	g = number >> 8 & 0xFF;
	b = number & 0xFF;

	SetLed(led, r, g, b);
}

void SetLed(int led, int red, int green, int blue){
	strip.setPixelColor(led, strip.Color(red, green, blue));
	strip.show();
}
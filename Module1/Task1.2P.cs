// C++ code
//
int state = LOW;
int red = 11;
int blue = 10;
int green = 9;
int PIR = 2;

void setup()
{
    pinMode(PIR, INPUT);

    pinMode(red, OUTPUT);
    pinMode(blue, OUTPUT);
    pinMode(green, OUTPUT);
    Serial.begin(9600);

    attachInterrupt(digitalPinToInterrupt(PIR), MotionDetection, CHANGE);
}

void loop()
{
}

void MotionDetection()
{
    state = digitalRead(2);
    //If motion is detected, change LED colour to green
    if (state)
    {
        analogWrite(red, 0);
        analogWrite(blue, 0);
        analogWrite(green, 255);
        Serial.println("Motion detected!");
    }
    //If motion is not detected, changle LED colour to red
    else
    {
        analogWrite(red, 255);
        analogWrite(blue, 0);
        analogWrite(green, 0);
        Serial.println("No motion detected");
    }

    delay(500); // Wait for 500 millisecond(s)
}

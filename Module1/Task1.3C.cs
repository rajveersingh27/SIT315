// C++ code
//
uint8_t state1 = LOW;
uint8_t state2 = LOW;
const uint8_t red = 11;
const uint8_t blue = 10;
const uint8_t green = 9;
const uint8_t PIR1 = 2;
const uint8_t PIR2 = 3;

void setup()
{
    pinMode(PIR1, INPUT);
    pinMode(PIR2, INPUT);
    pinMode(red, OUTPUT);
    pinMode(blue, OUTPUT);
    pinMode(green, OUTPUT);
    Serial.begin(9600);

    attachInterrupt(digitalPinToInterrupt(PIR1), PIR1Motion, CHANGE);
    attachInterrupt(digitalPinToInterrupt(PIR2), PIR2Motion, CHANGE);
}

void loop()
{
}

void PIR1Motion()
{
    MotionDetection(PIR1, state1, "PIR1");
}

void PIR2Motion()
{
    MotionDetection(PIR2, state2, "PIR2");
}

void MotionDetection(const uint8_t &input, uint8_t &state, const String &device)
{
    state = digitalRead(input);
    //If motion is detected, change LED colour to green
    if (state)
    {
        analogWrite(red, 0);
        analogWrite(blue, 0);
        analogWrite(green, 255);
        Serial.println("Motion detected on " + device + "!");
    }
    //If motion is not detected, change LED colour to red
    else
    {
        analogWrite(red, 255);
        analogWrite(blue, 0);
        analogWrite(green, 0);
        Serial.println("No motion detected");
    }

    delay(500); // Wait for 500 millisecond(s)
}

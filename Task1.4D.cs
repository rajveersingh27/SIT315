// C++ code
//
uint8_t motion_state = LOW;
uint8_t button_state = LOW;
const uint8_t red = 11;
const uint8_t blue = 10;
const uint8_t green = 9;
const uint8_t PIR1 = 2;
const uint8_t PIR2 = 3;
const uint8_t Button = 6;

void setup()
{
    pinMode(PIR1, INPUT);
    pinMode(PIR2, INPUT);
    pinMode(Button, INPUT);
    pinMode(red, OUTPUT);
    pinMode(blue, OUTPUT);
    pinMode(green, OUTPUT);

    PCICR |= B00000100;         // Using PCIE2
    PCMSK2 |= B01001100;        // Interrupt will be triggered by D2, D3, D6

    noInterrupts();             // Stop interrupts 
    TCCR1A = 0;                 // Clear registers
    TCCR1B = 0;
    TCNT1 = 0;

    TCCR1B |= (1 << CS12);      // Set bits for 256 prescaler
    TCCR1B &= ~(1 << CS11);
    TCCR1B &= ~(1 << CS10);

    OCR1A = 31250;

    TIMSK1 = (1 << OCIE1A);     // Enable timer and compare interrupt  TIMSK1 = (1 << OCIE1A);		// Enable timer and compare interrupt
    TCCR1B |= (1 << WGM12);

    interrupts();               // Allow interrupts
    Serial.begin(9600);
}

void loop()
{
}

ISR(PCINT2_vect)	// Includes digital pins 0-7
{
    // Change LED colour to Green for PIR1 sensor
    if (digitalRead(PIR1))
    {
        motion_state = HIGH;
        digitalWrite(green, motion_state);
        Serial.println();
        Serial.print("Motion detected from PIR1!");
    }
    else
    {
        motion_state = LOW;
        digitalWrite(green, motion_state);
    }

    // Change LED colour to RED when button is pressed
    if (digitalRead(Button))
    {
        button_state = HIGH;
        digitalWrite(red, button_state);
        Serial.println();
        Serial.print("Button Pressed!");
    }
    else
    {
        button_state = LOW;
        digitalWrite(red, button_state);
    }

    // Change LED colour to blue for PIR2 sensor
    if (digitalRead(PIR2))
    {
        motion_state = HIGH;
        digitalWrite(blue, motion_state);
        Serial.println();
        Serial.print("Motion detected from PIR2!");
    }
    else
    {
        motion_state = LOW;
        digitalWrite(blue, motion_state);
    }
}

ISR(TIMER1_COMPA_vect)
{
    // Timer constantly blinks red LED at 500ms intervals
    digitalWrite(red, digitalRead(red) ^ 1);
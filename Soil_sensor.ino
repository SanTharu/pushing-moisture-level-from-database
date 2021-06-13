int soil = A0; // potentiometer wiper (middle terminal) connected to analog pin 3
                    // outside leads to ground and +5V
  // variable to store the value read


void setup() {
  pinMode (soil,INPUT);
  Serial.begin(9600);//  setup serial
  
}

void loop() {
  unsigned int analog_val = 0;
  analog_val = analogRead(soil);  // read the input pin
  Serial.println(analog_val); 
  delay (30000);
}


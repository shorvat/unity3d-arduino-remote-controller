#include<Wire.h>
#include <SoftwareSerial.h>
#include "MegunoLink.h"
#include "Filter.h"

// We are using MegunoLink exponential filter.
// GY-521 accelerometer module produces some noise
// so we need to smooth this noise out.
//
// URL: https://www.megunolink.com/documentation/arduino-libraries/exponential-filter/

ExponentialFilter<long> ADCFilterX(50, 0); // (weight = 50, startValue=0)
ExponentialFilter<long> ADCFilterY(50, 0); // (weight = 50, startValue=0)

const int MPU=0x68; 
int16_t AcX,AcY,AcZ,Tmp,GyX,GyY,GyZ;
 
SoftwareSerial mySerial(2, 3); // RX | TX 

void setup() {
  Wire.begin();
  Wire.beginTransmission(MPU);
  Wire.write(0x6B); 
  Wire.write(0);    
  Wire.endTransmission(true);
  Serial.begin(9600);
  mySerial.begin(9600);
}

void loop() {
  
  // request the data from GY-521
  Wire.beginTransmission(MPU);
  Wire.write(0x3B);  
  Wire.endTransmission(false);
  Wire.requestFrom(MPU,12,true); 

   // get all the values from GY-521
  AcX=Wire.read()<<8|Wire.read();    
  AcY=Wire.read()<<8|Wire.read();  
  AcZ=Wire.read()<<8|Wire.read();  
  GyX=Wire.read()<<8|Wire.read();  
  GyY=Wire.read()<<8|Wire.read();  
  GyZ=Wire.read()<<8|Wire.read();

  // apply ExponentialFilter to raw data
  ADCFilterX.Filter(AcX);
  ADCFilterY.Filter(AcY);

  // send filtered AcX and AcY data to Unity3D game via HC-05 bluetooth module
  mySerial.print(ADCFilterX.Current());
  mySerial.print(",");
  mySerial.println(ADCFilterY.Current());

  // wait for 0.1s - feed rate of 10 values per second is enough for our demo
  delay(100);
}

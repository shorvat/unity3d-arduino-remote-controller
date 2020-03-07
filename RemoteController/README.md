# Arduino: Remote Controller

## Parts:

* Arduino Nano: [Arduino/Official](https://store.arduino.cc/arduino-nano), [ebay](https://www.ebay.com/itm/Nano-V3-0-Mini-USB-ATmega328-5V-16MHz-Micro-Controller-CH340G-Driver-For-Arduino/273743114225)
* HC-05 Bluetooth module: [Amazon](https://www.amazon.com/DSD-TECH-SH-H3-Compatible-Replacement/dp/B072LX3VG1/ref=sr_1_16?keywords=hc-05&qid=1572771443&sr=8-16), [ebay](https://www.ebay.com/itm/HC-05-HC-06-Wireless-Bluetooth-RF-Transceiver-Module-Serial-RS232-TTL-Base-Board/113343568594)
* GY-521 Accelerometer: [Amazon](https://www.amazon.com/Gy-521-MPU-6050-MPU6050-Sensors-Accelerometer/dp/B008BOPN40), [ebay](https://www.ebay.com/itm/GY521-MPU-6050-Module-3-Axis-Gyroscope-Accelerometer-Module-for-Arduino-MPU-6050/170881535422)
* Resistor 1k ohm
* Resistor 2k ohm

## Wiring:

![Wiring](images/arduino-wiring.png?raw=true "Wiring")

_This wiring diagram is only for a develomplent use. Do not use it in production. It needs at least a better power supply for GY-521 and HC-05 modules and a better voltage devider between HC-05 RXD pin and Arduino Nano D3 pin._

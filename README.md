MEEPROMMER
==========

(E)EPROM programmer based on Arduino hardware


The MEEPROMMER is a combination of hardware and software that lets you read and write 
data from (and to) 28Cxxx EEPROMS. Maybe later there will be enhancements to use it
also for 27Cxxx EPROMS. 

At the moment we have an working prototype on a PCB that uses an Arduino Nano and an 
Arduino-shield that can directly plugged to an Arduino Uno. 

The Arduino firmware provides a serial interface with simple commands to transfer data 
between a host computer and an eeprom.

For the host computer a Java based GUI application is available that uses the RXTX 
library to interface the programmer.


## Updates
* Created a .NET winforms app (dotBurn) for communicating with the device
* Modified the Arduino code, to accept 32bit addresses
* This of course meant that I had to modify the schematic too, by adding another shift register
_Note: the schematic files are not modified yet!_

## TODO
 * I really don't like the idea that a lot of eeproms have slightly different pinouts, and that would require different hardware, so I will update the app and the Arduino code, so that the pinout is configured from the gui. Will this slow down the read/write process? Maybe. We will see once it is done.
    * Modify the app, so it has a new form for setting the pins
    * Pinoutns should be saveable/loadable in the app
    * The arduino code needs a new command for uploading the pinout before each eeprom access
 * Use Skidlz's modified firware: [MEEPROMMERfirmware.ino](https://github.com/Skidlz/MEEPROMMER/blob/master/Arduino/MEEPROMMERfirmware/MEEPROMMERfirmware.ino)

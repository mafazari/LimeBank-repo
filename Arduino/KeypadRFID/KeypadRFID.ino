#include <Keypad.h>
#include <SPI.h>
#include <MFRC522.h>
#define SS_PIN 10
#define RST_PIN 9
MFRC522 rfid(SS_PIN, RST_PIN);
MFRC522::MIFARE_Key key;
byte nuidPICC[3];
const byte ROWS = 4; 
const byte COLS = 4; 

//define the symbols on the buttons of the keypad
char hexaKeys[ROWS][COLS] = {
  {'1','2','3','A'},
  {'4','5','6','B'},
  {'7','8','9','C'},
  {'*','0','#','D'}
};
byte rowPins[ROWS] = {4, 3, 2, 0}; //row pins on arduino
byte colPins[COLS] = {8, 7, 6, 5}; //column pins on arduino

Keypad kp = Keypad(makeKeymap(hexaKeys), rowPins, colPins, ROWS, COLS); //initialize keypad instance
int timeout = 0;
bool newcard = false;

void setup() {
  Serial.begin(9600);
  SPI.begin();
  rfid.PCD_Init();

  for (byte i = 0; i < 6; i++)
  {
    key.keyByte[i] = 0xFF;//keyByte is defined in the "MIFARE_Key" 'struct' definition in the .h file of the library
  }
}

void dump_byte_array(byte *buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    Serial.print(buffer[i] < 0x10 ? " 0" : "");
    Serial.print(buffer[i], HEX);
  }
}

void loop() { 
  char customKey = kp.getKey();
  if (customKey) {
    Serial.println(customKey);
  }
  if ( ! rfid.PICC_IsNewCardPresent()){   //Look for new cards
    return;
  }

  if ( ! rfid.PICC_ReadCardSerial()){     //Verify that the card has been read
    return;
  }

  MFRC522::PICC_Type piccType = rfid.PICC_GetType(rfid.uid.sak); // Check if the PICC or Classic MIFARE type
  if (piccType != MFRC522::PICC_TYPE_MIFARE_MINI &&  
    piccType != MFRC522::PICC_TYPE_MIFARE_1K &&
    piccType != MFRC522::PICC_TYPE_MIFARE_4K) {
    Serial.println(F("RFID: Error: Not a MIFARE Type card"));
    return;
  }  
  
  printHex(rfid.uid.uidByte, rfid.uid.size);
  Serial.print("NUID");
  Serial.println();

  rfid.PICC_HaltA();
  rfid.PCD_StopCrypto1();
}
void printHex(byte *buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    Serial.print(buffer[i], HEX);
  }
}

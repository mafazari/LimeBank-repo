using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace bankgui
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Hash hash = new Hash();
            //Error.show(hash.makeHash("F049897C", "1234"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Begin());
        }
    }
}
/*public class DtbConnection
{
    MySqlConnection cn;
    
    public Object GetKlant()
    {
        cn = new MySqlConnection("server=Lime-Bank;uid=root;pwd=Antonio01!;database=lime-bank");
        cn.Open();
        MySqlCommand cmd = new MySqlCommand("SELECT * FROM `lime-bank`.klant where KlantID = 1;", cn);
        MySqlDataReader reader = cmd.ExecuteReader();
        string info;
        while (reader.Read())
        {
            Object obj = new Object();
            obj = reader.GetString(3);
            info = (string)obj;
            cn.Close();
            
        }
        return ;
    }
    
}*/

/*public class ArduinoClass
{
    static SerialPort Arduino = new SerialPort();
    static String portName = "COM4";
    //String amount;

    public bool makePort(String s)
    {
        portName = s;
        bool status = false;
        Arduino.BaudRate = 9600;
        Arduino.PortName = portName;
        // Arduino.DataReceived

        try
        {
            Arduino.Open();
            status = true;
        }
        catch (System.IO.IOException)
        {
            Error.show("Poort niet gevonden", "Error");
            // Console.WriteLine(e.Message);
        }
        catch (System.UnauthorizedAccessException)
        {
            Error.show("Toegang tot de compoort is geweigerd", "Error");
            // Console.WriteLine(ex.Message);
        }
        catch (Exception)
        {
            Error.show("No port selected", "Error");
            // Console.WriteLine(exc.Message);   
        }
        return status;
    }
    public void closePort(String s)
    {
        Arduino.Close();
    }
    public static SerialPort getPort()
    {
        return Arduino;
    }

    public static string strInput()
    {
        string str = "";

        while (str.Equals(""))
        {
            str = Arduino.ReadLine().ToString().Trim();
        }

        return str;
    }

    // public void dispense(int a)
    // {
    //   amount = Convert.ToString(a);
    //Arduino.Write(amount);
    //}*/

    public static class Error
    {
        public static void show(String s, String x)
        {
            MessageBox.Show(s, x,
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void show(String s)
        {
            MessageBox.Show(s, s,
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
public class Hash
{
    /*public bool checkHash(string RekeningID, String pincode)
    {
        int RekeningIDcv;
        int pincodecv;
        Int32.TryParse(RekeningID, out RekeningIDcv);
        Int32.TryParse(pincode, out pincodecv);
        bool status = false;
        //HTTPget temporary = new HTTPget();
        //Error.show(makeHash(RekeningID, pincode));
        //Error.show(temporary.getHash(RekeningID));
        string Hash = makeHash(RekeningID, pincode);
        if (Hash.Equals(temporary.getHash(RekeningID)))
        {
            status = true;
        }
        return status;
    }*/

    public String makeHash(String RekeningID, String pincode)
    {
        string input = String.Concat(RekeningID, pincode);
        byte[] bytes = Encoding.UTF8.GetBytes(input);
        SHA512Managed hashstring = new SHA512Managed();
        byte[] hash = hashstring.ComputeHash(bytes);
        string hashString = string.Empty;
        foreach (byte x in hash)
        {
            hashString += String.Format("{0:x2}", x);
        }
        return hashString;
    }
}

//}
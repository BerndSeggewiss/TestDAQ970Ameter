/*
 * Test program to test the basic communication of a DAQ970A meter from Keysight. The identity data
 * and temperature values of channels 101 to 110 are read out once. The thermocouple of type T is 
 * configured.
 * Written: Bernd Seggewiß
 * Published: 18/08/2023
 * Version 1.0.0
 */ 


using System;
using Ivi.Visa.Interop;

// Test
namespace TestKeysightDAQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ivi.Visa.Interop.ResourceManager rm = new Ivi.Visa.Interop.ResourceManager();
            Ivi.Visa.Interop.FormattedIO488 myDMM = new Ivi.Visa.Interop.FormattedIO488();
            try
            {
                string DUTAddr = "USB0::0x2A8D::0x5101::MY58020662::0::INSTR";
                myDMM.IO = (IMessage)rm.Open(DUTAddr, AccessMode.NO_LOCK, 2000, "");
                myDMM.IO.Clear();
                myDMM.WriteString("RST", true);
                myDMM.WriteString("*IDN?", true);
                string IDN = myDMM.ReadString();
                Console.WriteLine(IDN);
                myDMM.WriteString(":CONFigure:TEMPerature:TCouple T,(@101:110)", true);
                myDMM.WriteString(":UNIT:TEMPerature C", true);
                myDMM.WriteString("READ?", true);
                string TempResponse = myDMM.ReadString();
                Console.WriteLine(TempResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured: " + e.Message);
            }
        }
    }
}

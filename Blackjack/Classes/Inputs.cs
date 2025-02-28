using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public static class Inputs
    {
        
        // String
        public static string String(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        // Char
        public static char Char(string prompt)
        {
            string str = String(prompt);
            return str.Length > 0 ? str[0] : ' ';
        }

        // Int
        public static void Int(string prompt, out int value)
        {
            string str = String(prompt);
            int.TryParse(str, out value);
        }
        public static int Int(string prompt, int defaultValue)
        {
            string str = String(prompt);
            int value = defaultValue;
            int.TryParse(str, out value);
            return value;
        }

        // Float
        public static void Float(string prompt, out float value)
        {
            string str = String(prompt);
            float.TryParse(str, out value);
        }
        public static float Float(string prompt, float defaultValue)
        {
            string str = String(prompt);
            float value = defaultValue;
            float.TryParse(str, out value);
            return value;
        }

        // Double
        public static void Double(string prompt, out double value)
        {
            string str = String(prompt);
            double.TryParse(str, out value);
        }
        public static double Double(string prompt, double defaultValue)
        {
            string str = String(prompt);
            double value = defaultValue;
            double.TryParse(str, out value);
            return value;
        }

        // Long
        public static void Long(string prompt, out long value)
        {
            string str = String(prompt);
            long.TryParse(str, out value);
        }
        public static long Long(string prompt, long defaultValue)
        {
            string str = String(prompt);
            long value = defaultValue;
            long.TryParse(str, out value);
            return value;
        }
    }
}

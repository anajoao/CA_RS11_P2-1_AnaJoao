using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Utility
{
    public class RSGymUtility
    {
        public static void SetUnicodeConsole()
        {
            //Console.WriteLine("á Á à À ã Ã â Â ç Ç º ª");

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Console.WriteLine("á Á à À ã Ã â Â ç Ç º ª");
        }

        // parameter vs argument 
        public static void WriteTitle(string title, string beginTitle = "", string endTitle = "")
        {

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"{beginTitle}{new string('-', 60)}");

            Console.WriteLine(title.ToUpper());    // UPPER(title)

            Console.WriteLine($"{new string('-', 60)}{endTitle}");

           

            Console.ForegroundColor = ConsoleColor.White;

        }


        // receber 3 valores
        public static void WriteMessage_v0(string message1, string message2, string message3)
        {
            Console.WriteLine($"\n{message1}");
            Console.WriteLine($"{message2}");
            Console.WriteLine($"{message3}\n\n\n");
        }

        public static void WriteMessage(string message, string beginMessage = "", string endMessage = "")
        {
            Console.Write($"{beginMessage}{message}{endMessage}");
        }

        public static void PauseConsole()
        {

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.Write("\n\n\n\nPrime qualquer tecla para continuares.");

            Console.ForegroundColor = ConsoleColor.White;

            Console.ReadKey();

            Console.Clear();

        }

        public static void TerminateConsole()
        {

            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write("\n\n\n\nPrime qualquer tecla para terminares.");

            Console.ForegroundColor = ConsoleColor.White;

            Console.ReadKey();

            Console.Clear();

        }



    }
}

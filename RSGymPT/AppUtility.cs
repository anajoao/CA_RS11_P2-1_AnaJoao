using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace RSGymPT
{
    internal class AppUtility
    {
        internal static int ShowMenuLogin()
        {
            int option;

            string[,] menuLogin =
            {
                {"1.", " Login"},
                {"2.", " Exit"},
            };


            Console.Clear();

            RSGymUtility.WriteTitle("Menu LOGIN", "", "\n\n");

            for (int r = 0; r < 2; r++)
            {
                for (int c = 0; c < 2; c++)
                {
                    RSGymUtility.WriteMessage($"{menuLogin[r, c]}");
                }

                RSGymUtility.WriteMessage("\n");
            }

            do
            {
                RSGymUtility.WriteMessage("> ", "\n");

            } while (!int.TryParse(Console.ReadLine(), out option));

            return option;
        }

        internal static int ShowUserMenu(User user)
        {
            int option;

            string[,] userMenu =
            {
                {"", "Request"},
                {"1.", "Register"},
                {"2.", "Alter"},
                {"3.", "Delete"},
                {"4.", "Consult"},
                {"5.", "End"},
                {"", "Personal Trainer"},
                {"6.", "Search"},
                {"7.", "Consult"},
                {"", "User"},
                {"8.", "Consult"},
                {"9.", "Logout"},
            };


            Console.Clear();

            RSGymUtility.WriteTitle("User Menu", "", "\n\n");

            for (int r = 0; r < userMenu.GetLength(0); r++)
            {
                if (string.IsNullOrWhiteSpace(userMenu[r, 0]))
                {
                    Console.WriteLine(userMenu[r, 1]);
                }
                else
                {
                    Console.WriteLine($"    {userMenu[r, 0]} {userMenu[r, 1]}");
                }
            }

            do
            {
                RSGymUtility.WriteMessage($"{user.Username}> ", "\n");

            }while(!int.TryParse(Console.ReadLine(), out option));

            return option;

        }

        internal static int ShowRequestMenu(User user)
        {
            int option;

            string[,] menuAlter =
                {
                    {"1.", " PTCode"},
                    {"2.", " Date"},
                    {"3.", " Cancel"},
                    {"4.", " Exit"}
                };

            
            for (int r = 0; r < menuAlter.GetLength(0); r++)
            {
                for (int c = 0; c < 2; c++)
                {
                    RSGymUtility.WriteMessage($"{menuAlter[r, c]}");
                }

                RSGymUtility.WriteMessage("\n");
            }

            do
            {
                RSGymUtility.WriteMessage($"{user.Username}> ", "\n");
            } while (!int.TryParse(Console.ReadLine(), out option));

            return option;

        }

        // (https://stackoverflow.com/questions/3404421/password-masking-console-application) método para ler a senha do User enquanto a oculta, substituindo os caracteres por asteriscos (*).

        internal static string ReadPassword()
        {
            string password = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);             // lê a tecla pressionada, sem a exibir e armazana a info na variavel
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");                                 // \b move o cursor para trás uma posição  
                    password = password.Substring(0, password.Length - 1);  // remove o ultimo caracter da senha
                }
                else if (!char.IsControl(keyInfo.KeyChar))                  // Verifica se a tecla pressionada não é uma tecla de controle (como Enter)
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);                              // loop para continuar enquanto o user não primir a tecla enter.

            return password;
        }

    }
}

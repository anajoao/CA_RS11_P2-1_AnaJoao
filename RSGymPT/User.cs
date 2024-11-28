using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace RSGymPT
{
    internal class User
    {


        #region Fields
        private string password;
        private string username;
        #endregion

        #region Properties

        internal static int NextId { get; set; } = 1;
        internal int Id { get; set; }
        internal string Name { get; set; }
        internal DateTime BirthDate { get; set; }
        internal string Username
        {
            get { return username; }
            set
            {
                if (value.Length != 5)
                {
                    throw new Exception("Username must have exactly 5 characters.");
                }

                username = value;
            }
        }
        internal string Password
        {
            get { return password; }
            set
            {
                if (value.Length < 9)
                {
                    throw new Exception("Password must be at least 9 characters long.");
                }
                password = value;
            }
        }

        #endregion

        #region Constructores

        internal User()
        {
            Id = 0;
            Name = string.Empty;
            BirthDate = DateTime.Now;
            Username = string.Empty;
            Password = string.Empty;
        }

        internal User(string name, DateTime birthDate, string userCode, string password)
        {
            Id = NextId++;
            Name = name;
            BirthDate = birthDate;
            Username = userCode;
            Password = password;
        }


        #endregion

        #region Methods

        // Método para validar o login. É um método não void que devolve uma instancia da classe, caso os dados sejam validados.
        internal static User Login(List<User> userList)
        {

            User loggedUser;

            RSGymUtility.WriteMessage("User Code: ", "", "");
            string code = Console.ReadLine();

            RSGymUtility.WriteMessage("Password: ", "", "");
            string password = AppUtility.ReadPassword();

            loggedUser = userList.Find(user => user.Username == code && user.Password == password);

            if (loggedUser == null)
            {
                RSGymUtility.WriteMessage("Login failed. Try again.", "\n\n", "");
                RSGymUtility.PauseConsole();
            }
            
            return loggedUser;
        }

        // Método para listar os Users existentes na user List.
        internal static void ListUser(List<User> userList)
        {
            Console.Clear();

            RSGymUtility.WriteTitle("User List", "", "\n\n");

            RSGymUtility.WriteMessage("List of Users:", "", "\n\n");

            foreach (User item in userList)
            {

                RSGymUtility.WriteMessage($"{item.Id}. {item.Username}: {item.Name}, Birth date ({item.BirthDate.ToShortDateString()})", "", "\n");

            }

        }

        #endregion

    }
}

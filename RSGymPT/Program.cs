using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace RSGymPT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RSGymUtility.SetUnicodeConsole();

            try
            {
                bool exit = false;
                bool loggedIn = false;
                User currentUser = null;

                List<Request> requests = new List<Request>();


                List<User> userList = new List<User>
                    {
                        new User("Ana Oliveira", new DateTime(1994, 03, 30), "a_jro", "abc123cba"),
                        new User("João Ribeiro", new DateTime(1990, 06, 12), "jPmro", "fgh987hgf")
                    };
                
                List<PersonalTrainer> ptList = new List<PersonalTrainer>
                    {
                        new PersonalTrainer("José Carlos Monteiro", "915678934", "Jcm_1"),
                        new PersonalTrainer("Maria Luís Carvalho", "914659876", "Mlc_2"),
                        new PersonalTrainer("António Simões Antunes", "912056489", "Asa_3")
                    };

                do
                {
                    
                    while (!loggedIn)
                    {
                        int loginOption = AppUtility.ShowMenuLogin();
                       

                        switch (loginOption)
                        {

                            case 1:
                                Console.Clear();
                                RSGymUtility.WriteTitle("Login", "", "\n");
                                currentUser = User.Login(userList);
                                if (currentUser != null)
                                {
                                    loggedIn = true;
                                }
                                break;
                            case 2:
                                exit = true;
                                break;
                        }

                        if (exit)
                        {
                            break;
                        }
                    }

                    while (loggedIn)
                    {
                        
                        int option = AppUtility.ShowUserMenu(currentUser);

                        switch (option)
                        {

                            case 1:
                                do
                                {
                                    Console.Clear();
                                    RSGymUtility.WriteTitle("Register Request", "", "\n");
                                    PersonalTrainer.ListPersonalTrainers(ptList);
                                    Request.Register(requests, ptList, currentUser);
                                    RSGymUtility.WriteMessage($"Do you want to register another session?(y/n): {currentUser.Username}> ", "\n\n");
                                } while (Console.ReadLine().ToUpper() == "Y");
                                break;
                            case 2:
                                do
                                {
                                    Console.Clear();
                                    RSGymUtility.WriteTitle("Alter Request", "", "\n");
                                    Request.ListRequests(requests);
                                    Request.AlterRequest(requests, ptList, currentUser);
                                    RSGymUtility.WriteMessage($"Do you want to change more requests?(y/n): {currentUser.Username}> ", "\n\n");
                                } while (Console.ReadLine().ToUpper() == "Y"); 
                                break;
                            case 3:
                                Console.Clear();
                                RSGymUtility.WriteTitle("Remove Request", "", "\n");
                                Request.ListRequests(requests);
                                Request.RemoveRequest(requests, currentUser);
                                RSGymUtility.PauseConsole();
                                break;
                            case 4:
                                Console.Clear();
                                RSGymUtility.WriteTitle("List requests", "", "\n");
                                Request.ListRequests(requests);
                                RSGymUtility.PauseConsole();
                                break;
                            case 5:
                                Console.Clear();
                                RSGymUtility.WriteTitle("End Session", "", "\n");
                                Request.ListRequests(requests);
                                Request.EndSession(requests, currentUser);
                                RSGymUtility.PauseConsole();
                                break;
                            case 6:
                                Console.Clear();
                                RSGymUtility.WriteTitle("Search Personal Trainer", "", "\n");
                                PersonalTrainer.FindPersonalTrainer(ptList, currentUser);
                                RSGymUtility.PauseConsole();
                                break;
                            case 7:
                                Console.Clear();
                                RSGymUtility.WriteTitle("Personal Trainers", "", "\n");
                                PersonalTrainer.ListPersonalTrainers(ptList);
                                RSGymUtility.PauseConsole();
                                break;
                            case 8:
                                Console.Clear();
                                RSGymUtility.WriteTitle("Personal Trainer List", "", "\n");
                                User.ListUser(userList);
                                RSGymUtility.PauseConsole();
                                break;
                            case 9:
                                loggedIn = false;
                                break;
                            default:
                                RSGymUtility.WriteMessage("Invalid option");
                                break;
                        }
                    }
                } while (!loggedIn && !exit);
            }

            catch (Exception ex)
            {
                RSGymUtility.WriteMessage($"Erro: {ex.Message}", "\n\n");
            }
 

            RSGymUtility.TerminateConsole();

        }
    }
}

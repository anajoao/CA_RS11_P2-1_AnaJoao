using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utility;

namespace RSGymPT
{
    internal class Request
    {

        #region Enum
        internal enum EnumStatus
        {

            Scheduled,
            Completed,
            Canceled

        }
        #endregion

        #region Properties
        internal static int NextId { get; set; } = 1;
        internal int Id { get; set; }
        internal User UserId { get; set; }
        internal PersonalTrainer PtCode { get; set; }
        internal DateTime RequestDate { get; set; }
        internal DateTime EndDate { get; set; }
        internal EnumStatus Status { get; set; }
        internal string Reason { get; set; }
        #endregion

        #region Constructors
        internal Request()
        {
            Id = 0;
            UserId = null;
            PtCode = null;
            RequestDate = DateTime.Now;
            EndDate = DateTime.Now;
            Reason = string.Empty;
        }

        internal Request(User userId, PersonalTrainer ptCode, DateTime requestDate, EnumStatus status)
        {
            Id = NextId++;
            UserId = userId;
            PtCode = ptCode;
            RequestDate = requestDate;
            Status = status;
        }
        #endregion

        #region Methods
        // Método para registar um pedido na lista. Regista o Código do PT solicitado e a data e hora da marcação pretendida (que tem que ser superior à data atual)  
        internal static void Register(List<Request> requests, List<PersonalTrainer> ptList, User user)
        {
            DateTime dateRequest;

            RSGymUtility.WriteMessage($"Write the PT Code you want to request: {user.Username}> ", "\n\n", "");
            string ptCode = Console.ReadLine();

            PersonalTrainer findPT = ptList.Find(pt => pt.PTCode == ptCode);

            if (findPT != null)
            {
                do
                {
                    RSGymUtility.WriteMessage($"Date and time of the session workout (format: dd-mm-yyyy HH:mm): {user.Username}> ");

                }while (!DateTime.TryParse(Console.ReadLine(), out dateRequest));

                if (dateRequest > DateTime.Now)
                {
                    Request request = new Request(user, findPT, dateRequest, Request.EnumStatus.Scheduled);
                    requests.Add(request);
                    RSGymUtility.WriteMessage($"Session scheduled for {dateRequest} with {ptCode} PT.", "\n\n", "");
                }
                else
                {
                    RSGymUtility.WriteMessage("ERROR: You can´t schedule for a past date.", "\n\n", "");
                }
            }
            else
            {
                RSGymUtility.WriteMessage("PT not found.", "\n", "\n");
            }
        }

        // Método para apagar um pedido existente na lista de pedidos.
        internal static void RemoveRequest(List<Request> requests, User user)
        {

            int removeId;

            do
            {
                RSGymUtility.WriteMessage($"Id to remove: {user.Username}> ", "\n\n", "");
            }
            while (!int.TryParse(Console.ReadLine(), out removeId));


            Request requestToRemove = requests.Find(r => r.Id == removeId);

            if (requestToRemove != null)
            {
                if (requestToRemove.Status == Request.EnumStatus.Scheduled)
                {
                    requests.Remove(requestToRemove);
                    RSGymUtility.WriteMessage($"Id {removeId} successfully removed.", "\n", "\n");
                }
                else
                {
                    RSGymUtility.WriteMessage("Only requests with status 'Scheduled' can be removed.", "\n", "\n");
                }
            }
            else
            {
                RSGymUtility.WriteMessage($"Request with Id {removeId} not found.", "\n", "\n");
            }

        }

        private static void AlterPT(List<PersonalTrainer> ptList, User user, Request requestToAlter)
        {

            RSGymUtility.WriteMessage($"Enter the new PT Code: {user.Username}> ", "\n");
            string ptCode = Console.ReadLine();

            PersonalTrainer findPT = ptList.Find(pt => pt.PTCode == ptCode);

            if (findPT != null)
            {

                RSGymUtility.WriteMessage($"PT was changed to {findPT.PTCode}", "\n", "\n");

                requestToAlter.PtCode = findPT;
            }
            else
            {
                RSGymUtility.WriteMessage("PT not found.", "", "\n");
            }
        }

        private static void AlterDate(User user, Request requestToAlter)
        {
            DateTime newDate;
            do
            {
                RSGymUtility.WriteMessage($"Enter new date and time (format: dd-mm-yyyy HH:mm): {user.Username}> ", "\n");

            } while (!DateTime.TryParse(Console.ReadLine(), out newDate));

            requestToAlter.RequestDate = newDate;

            RSGymUtility.WriteMessage($"The request date was seccessfully changed to {newDate}.", "\n");
        }

        private static void CancelSession(User user, Request requestToCancel, int requestId)
        {

            requestToCancel.Status = EnumStatus.Canceled;
            requestToCancel.EndDate = DateTime.Now;

            RSGymUtility.WriteMessage($"Reason: {user.Username}> ", "\n");
            requestToCancel.Reason = Console.ReadLine();

            RSGymUtility.WriteMessage($"Session {requestId} was cancelled. Reason: '{requestToCancel.Reason}', Cancelation date: {requestToCancel.EndDate}", "\n", "");

        }

        // Método para alterar um pedido. Este método valida o Id do pedido e posteriormente dá a opção de alterações, podendo alterar o PT através do código, alterar a data e hora, e cancelar o pedido.
        internal static void AlterRequest(List<Request> requests, List<PersonalTrainer> ptList, User user)
        {
            int requestId;
            int option;

            do
            {
                RSGymUtility.WriteMessage($"Select the ID of the request you want to alter: {user.Username}> ", "\n\n", "");
            } while (!int.TryParse(Console.ReadLine(), out requestId));

            Request requestToAlter = requests.Find(r => r.Id == requestId);

            if (requestToAlter != null && requestToAlter.Status == EnumStatus.Scheduled)
            {
                do
                {
                    Console.Clear();
                    RSGymUtility.WriteTitle("Alter request", "", "\n\n");

                    RSGymUtility.WriteMessage($"Selected request - PT: {requestToAlter.PtCode.PTCode}, Date: {requestToAlter.RequestDate}", "", "\n\n");                  
                    option = AppUtility.ShowRequestMenu(user);

                    switch (option)
                    {
                        case 1:
                            Console.Clear();
                            RSGymUtility.WriteTitle($"Alter PT - {requestToAlter.PtCode.PTCode}", "", "\n");
                            PersonalTrainer.ListPersonalTrainers(ptList);
                            AlterPT(ptList, user, requestToAlter);
                            RSGymUtility.PauseConsole();
                            break;
                        case 2:
                            Console.Clear();
                            RSGymUtility.WriteTitle($"Alter Date - {requestToAlter.RequestDate}", "", "\n");
                            AlterDate(user, requestToAlter);
                            RSGymUtility.PauseConsole();
                            break;
                        case 3:
                            Console.Clear();
                            RSGymUtility.WriteTitle("Cancel session", "", "\n");
                            CancelSession(user, requestToAlter, requestToAlter.Id);
                            RSGymUtility.PauseConsole();
                            break;
                        case 4:
                            break;
                        default:
                            RSGymUtility.WriteMessage("Invalid option");
                            break;
                    }
                } while (option != 4);
            }
            else
            {
                RSGymUtility.WriteMessage("Couldn´t find a request to change", "\n\n");
            }
        }

        // método para terminar a sessão validada, marcando a hora do fim da mesma.
        internal static void EndSession(List<Request> requests, User user)
        {
            int requestId;

            do
            {
                RSGymUtility.WriteMessage($"Enter the ID of the session to end: {user.Username}> ", "\n", "");
            } while (!int.TryParse(Console.ReadLine(), out requestId));

            Request requestToEnd = requests.Find(r => r.Id == requestId);

            if (requestToEnd != null)
            {
                if (requestToEnd.Status == EnumStatus.Scheduled)
                {
                    requestToEnd.Status = EnumStatus.Completed;
                    requestToEnd.EndDate = DateTime.Now;

                    RSGymUtility.WriteMessage($"Session {requestId} successfully ended. End date: {requestToEnd.EndDate}", "\n", "");
                }
                else
                {
                    RSGymUtility.WriteMessage("You can't end a session that has already been ended or canceled", "\n", "");
                }
            }
            else
            {
                RSGymUtility.WriteMessage("Session not found.", "\n", "");
            }
        }

        
        // Método para listar todos os pedidos dentro da lista de pedidos.
        internal static void ListRequests(List<Request> requests)
        {

            if (requests.Count == 0)
            {
                Console.WriteLine("No people in the list.");
            }
            else
            {
                RSGymUtility.WriteMessage("Requests List:", "", "\n\n");

                foreach (Request item in requests)
                {

                    RSGymUtility.WriteMessage($"{item.Id}: UserId: {item.UserId.Id}, PT: {item.PtCode.PTCode}, Date: {item.RequestDate}, Status: {item.Status}", "", "\n");

                    if (item.Status == EnumStatus.Completed)
                    {
                        RSGymUtility.WriteMessage($"   End Date: {item.EndDate}", "", "\n\n");
                    }
                    if (item.Status == EnumStatus.Canceled)
                    {
                        RSGymUtility.WriteMessage($"   Cancelation Date: {item.EndDate}, Reason: '{item.Reason}'", "", "\n\n");
                    }
                }

            }

        }

    }
    #endregion

}


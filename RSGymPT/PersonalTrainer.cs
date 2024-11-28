using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace RSGymPT
{
    internal class PersonalTrainer
    {

        #region Fields
        private string ptCode;
        #endregion

        #region Properties
        internal static int NextId { get; set; } = 1;
        internal int Id { get; set; }
        internal string Name { get; set; }
        internal string PhoneNumber { get; set; }
        internal string PTCode
        {
            get { return ptCode; }
            set
            {
                if (value.Length != 5)
                {
                    throw new Exception("UserCode must have exactly 5 characters.");    
                }

                ptCode = value;
            }
        }
        #endregion

        #region Constructores
        internal PersonalTrainer()
        {
            Id = 0;
            Name = string.Empty;
            PhoneNumber = string.Empty;
            PTCode = string.Empty;
        }

        internal PersonalTrainer(string name, string phoneNumber, string ptCode)
        {
            Id = NextId++;
            Name = name;
            PhoneNumber = phoneNumber;
            PTCode = ptCode;
        }
        #endregion

        #region Methods
        // Método para procurar um PT através do seu Código. Caso exista, lista a sua informação.
        internal static void FindPersonalTrainer(List<PersonalTrainer> ptList, User user)
        {
            string ptCode;
 
            Console.Clear();

            RSGymUtility.WriteTitle("Search Personal Trainer", "", "\n\n");

            RSGymUtility.WriteMessage($"PT code: {user.Username}> ");
            ptCode = Console.ReadLine();

            PersonalTrainer findPT = ptList.Find(pt => pt.PTCode == ptCode);

            if (findPT != null)
            {

                RSGymUtility.WriteMessage($"{findPT.PTCode} - {findPT.Name} (Phone number: {findPT.PhoneNumber})", "\n", "\n");
            }
            else
            {
                RSGymUtility.WriteMessage("Person not found.", "", "\n");
            }
        }

        
        // Método para listar os Personal Trainers existentes na lista de PT.
        internal static void ListPersonalTrainers(List<PersonalTrainer> ptList)
        {

            if (ptList.Count == 0)
            {
                Console.WriteLine("No people in the list.");
            }
            else
            {
                RSGymUtility.WriteMessage("Personal Trainers List:", "\n", "\n\n");

                OrderByName(ptList);

                foreach (PersonalTrainer item in ptList)
                {

                    RSGymUtility.WriteMessage($"{item.Id} - PT Code: {item.PTCode}, {item.Name}, {item.PhoneNumber}", "", "\n");

                }

            }
           
        }

        internal static void OrderByName(List<PersonalTrainer> ptList)
        {
            ptList.Sort((pt1, pt2) => pt1.Name.CompareTo(pt2.Name));
        }
    }

    #endregion

}

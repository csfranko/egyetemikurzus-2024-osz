using System.Runtime.CompilerServices;

using StudentRegistrySystem.Controller;
using StudentRegistrySystem.DAO;
using StudentRegistrySystem.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            string path = "./../../../students.json";
            var controller = new StudentController();

            controller.LoadData(path);
            controller.DisplayHelps();
            while (true)
            {
                string[] command = Console.ReadLine().Split(" ");

                if (command[0] == "Stop")
                {
                    break;
                }

                switch (command[0]) 
                {
                    case "AddStudent":
                        break;
                    case "AddSubject":
                        break;
                    case "AddGrade":
                        break;
                    case "BestAverageGrade":
                        break;
                    case "BestAverageGrades":
                        break;
                    case "DisplayStudents":
                        controller.DisplayStudents();
                        break;
                    case "Save":
                        controller.SaveData(path);
                        break;
                    case "Help":
                        controller.DisplayHelps();
                        break;
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
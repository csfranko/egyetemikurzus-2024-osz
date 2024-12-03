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
                    Console.WriteLine("A program leáll...");
                    Environment.Exit(0); 

                }

                switch (command[0]) 
                {
                    case "AddStudent":
                        if (command.Length >= 3)
                        {
                            string studentClass = command[^1];
                            string studentName = string.Join(" ", command.Skip(1).Take(command.Length - 2));

                            var rtv = controller.AddStudent(new Student
                            {
                                Name = studentName,
                                Class = studentClass,
                                Subjects = new List<SubjectGrades>()
                            });

                            if (rtv)
                            {
                                Console.WriteLine("A diák hozzáadása sikeres volt!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Hiba: Rosszul adtad meg a paramétereket!");
                        }
                        break;
                    case "AddSubject":
                        if(command.Length == 3)
                        {
                            var rtv = controller.AddSubject(Convert.ToInt32(command[1]), command[2]);
                            if (rtv)
                            {
                                Console.WriteLine("A tárgy hozzáadása sikeres volt!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Hiba: Rosszul adtad meg a paramétereket!");
                        }
                        break;
                    case "AddGrade":
                        if (command.Length == 4)
                        {
                            var rtv = controller.AddGrade(Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), command[3]);

                            if (rtv)
                            {
                                Console.WriteLine("A jegy hozzáadása sikeres volt!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Hiba: Rosszul adtad meg a paramétereket!");
                        }
                            break;
                    case "BestAndAverageGrade":
                        if (command.Length == 3)
                        {
                            controller.StudentPerformanceInSubject(Convert.ToInt32(command[1]), command[2]);
                        }
                        else
                        {
                            Console.WriteLine("Hiba: Rosszul adtad meg a paramétereket!");
                        }
                            break;
                    case "BestAverageGrades":
                        if (command.Length == 2)
                        {
                            controller.BestAverageStudentsGradeInSubject(command[1]);
                        }
                        else
                        {
                            Console.WriteLine("Hiba: Rosszul adtad meg a paramétereket!");
                        }
                            break;
                    case "RemoveStudent":
                        if (command.Length == 2)
                        {
                           var rtv = controller.RemoveStudent(Convert.ToInt32(command[1]));
                            if (rtv)
                            {
                                Console.WriteLine("A törlés sikeres volt!");
                            }
                            else
                            {
                                Console.WriteLine("Hiba: Hiba történt a törlés közben!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Hiba: Rosszul adtad meg a paramétereket!");
                        }                       
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
                    default:
                        Console.WriteLine("Ismeretlen parancs!");
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using StudentRegistrySystem.Model;

namespace StudentRegistrySystem.DAO
{
    internal class StundentMemoryDAO : IStundentDAO
    {
        List<Student> _students = new List<Student>();
        List<SubjectGrades> _subjects = new List<SubjectGrades>();

        public bool AddGrade(int id, int grade, string subject)
        {
            throw new NotImplementedException();
        }

        public bool AddStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public bool AddSubject(int id, string subjectName)
        {
            throw new NotImplementedException();
        }

        public int BestAverageStudentGradeInSubject(string subject)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetStudents()
        {
            return _students;
        }

        public void LoadStudents(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                _students = JsonSerializer.Deserialize<List<Student>>(jsonString);

                if (_students == null)
                {
                    Console.WriteLine("Hiba: Az adatstruktúra üres vagy érvénytelen.");
                }

                Console.WriteLine("Az adatok sikeresen betöltésre kerültek.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Hiba: A fájl nem található ({filePath}).");
                Console.WriteLine($"Részletek: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Hiba: A fájl tartalma nem megfelelő JSON formátumú.");
                Console.WriteLine($"Részletek: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Hiba: A fájl olvasása során I/O hiba történt.");
                Console.WriteLine($"Részletek: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba: Ismeretlen hiba történt az adatok betöltésekor.");
                Console.WriteLine($"Részletek: {ex.Message}");
            }
        }

        public bool RemoveStudent(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveStudents(string filePath, IEnumerable<Student> students)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(students, options);
                File.WriteAllText(filePath, jsonString);
                Console.WriteLine("Az adatok sikeresen elmentésre kerültek.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Hiba: Nincs jogosultság a fájl írásához ({filePath}).");
                Console.WriteLine($"Részletek: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Hiba: A fájl mentése során I/O hiba történt.");
                Console.WriteLine($"Részletek: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba: Ismeretlen hiba történt az adatok mentésekor.");
                Console.WriteLine($"Részletek: {ex.Message}");
            }
        }

        public int StudentSpecificAverageSubjectGrade(int id, string subject)
        {
            throw new NotImplementedException();
        }
    }
}

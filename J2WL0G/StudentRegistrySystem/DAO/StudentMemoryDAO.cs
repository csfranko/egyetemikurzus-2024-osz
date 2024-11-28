using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using StudentRegistrySystem.Model;

namespace StudentRegistrySystem.DAO
{
    internal class StudentMemoryDAO : IStudentDAO
    {
        List<Student> _students = new List<Student>();
        List<SubjectGrades> _subjects = new List<SubjectGrades>();
        public StudentMemoryDAO()
        { 
        
        }

        public bool AddGrade(int id, int grade, string subjectName)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                Console.WriteLine("Hiba: A diák nem található.");
                return false;
            }

            var subject = student.Subjects.FirstOrDefault(s => s.SubjectName == subjectName);

            if (subject == null)
            {
                Console.WriteLine("Hiba: A tantárgy nem található.");
                return false;
            }

            subject.Grades.Add(grade);
            return true;
        }

        public bool AddStudent(Student student)
        {   
            bool rtv = false;

            if(student == null || String.IsNullOrEmpty(student.Class) || String.IsNullOrEmpty(student.Name)) 
            {
                Console.WriteLine("Hiba: Az adatstruktúra üres vagy érvénytelen.");
                return rtv;
            }

            _students.Add(student);
            rtv = true;
            return rtv;



        }

        public bool AddSubject(int id, string subjectName)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                Console.WriteLine("Hiba: A diák nem található.");
                return false;
            }

            if (student.Subjects.Any(s => s.SubjectName == subjectName))
            {
                Console.WriteLine("Hiba: Ez a tantárgy már létezik ennél a diáknál.");
                return false;
            }
            var subject = new SubjectGrades { 
                SubjectName = subjectName,
                Grades = new List<int>()
            };
            student.Subjects.Add(subject);
            return true;
        }

        public int BestAverageStudentGradeInSubject(string subject)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetStudents()
        {
            return _students;
        }
        public void DisplayStudents()
        {          
            if (_students.Count == 0)
            {
                Console.WriteLine("Nincs diák az adatbázisban.");
                return;
            }

            Console.WriteLine("Diákok listája:");
            foreach (var student in _students)
            {
                Console.WriteLine($"ID: {student.Id}, Név: {student.Name}, Osztály: {student.Class}");
                foreach (var subject in student.Subjects)
                {
                    Console.WriteLine($"  Tantárgy: {subject.SubjectName}, Jegyek: {string.Join(", ", subject.Grades)}");
                }
            }
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
                    return;
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
                Console.WriteLine("Hiba: A fájl tartalma nem megfelelő JSON formátumú.");
                Console.WriteLine($"Részletek: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine("Hiba: A fájl olvasása során I/O hiba történt.");
                Console.WriteLine($"Részletek: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: Ismeretlen hiba történt az adatok betöltésekor.");
                Console.WriteLine($"Részletek: {ex.Message}");
            }
        }

        public bool RemoveStudent(int id)
        {
            bool rtv = false;
            foreach (Student student in _students) 
            {
                if (student.Id == id)
                {
                    _students.Remove(student);
                    rtv = true;
                }
            }
            return rtv;
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
                Console.WriteLine("Hiba: A fájl mentése során I/O hiba történt.");
                Console.WriteLine($"Részletek: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: Ismeretlen hiba történt az adatok mentésekor.");
                Console.WriteLine($"Részletek: {ex.Message}");
            }
        }

        public int StudentSpecificAverageSubjectGrade(int id, string subject)
        {
            throw new NotImplementedException();
        }

        public void DisplayHelps()
        {
            Console.WriteLine(@"Üdvözöllek Geptun-ban!
                                Az alábbi parancsok elérhetők: 
                                    (A '[]' között található elnevezések a megadható paraméterek)
                                    - AddStudent [Tanuló neve] [Tanuló osztálya]: Az alábbi paranccsal hozzá adhatsz egy tanulót az adatbázishoz!
                                    - AddSubject [Tanuló azonosítója] [Tantárgy neve]: Az alábbi paranccsal hozzá adhatsz egy tantárgyat a tanuló tantárgyai közé, 
                                        amit még nem tanul, ha az adatbázisban megtalálható a tanuló!
                                    - AddGrade [Tanuló azonosítója] [Jegy] [Tantárgy neve]: Az alábbi paranccsal hozzá adhatsz egy jegyet a tanuló tantárgyához!
                                    - BestAverageGrade [Tanuló azonosítója] [Tantárgy neve]:
                                    - BestAverageGrades [Tantárgy neve]:
                                    - DisplayStudents: Kilistázza az adatbázisban lévő diákokat!
                                    - Save: A program futása közben létrehozott vagy módosított adatokat lehet menteni!
                                    - Help: A parancsok listáját és azok funkcióját iratja ki a képernyőre!");


            Console.Beep();
        }
    }
}

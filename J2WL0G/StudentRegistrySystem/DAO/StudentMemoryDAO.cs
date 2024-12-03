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
        List<Student> _students;
        List<SubjectGrades> _subjects;
        public StudentMemoryDAO()
        {
            List<Student> _students = new List<Student>();
            List<SubjectGrades> _subjects = new List<SubjectGrades>();
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
            Console.WriteLine("Sikeresen hozzá adtál egy diákot a rendszerhez!");
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
                SubjectName = subjectName
            };
            student.Subjects.Add(subject);
            return true;
        }

        public void BestAverageStudentsGradeInSubject(string subject)
        {

            var studentAverages = _students
                .Select(s => new
                {
                    Student = s,
                    Average = s.Subjects
                        .Where(sub => sub.SubjectName == subject && sub.Grades.Any())
                        .SelectMany(sub => sub.Grades)
                        .DefaultIfEmpty(0) 
                        .Average()
                })
                .Where(sa => sa.Average > 0) 
                .OrderByDescending(sa => sa.Average)
                .Take(5)
                .ToList();

            if (!studentAverages.Any())
            {
                Console.WriteLine($"Nincs adat az alábbi tárgyból: {subject}");
                return;
            }

            Console.WriteLine($"Az első 5 legjobb átlag a(z) {subject} tárgyból:");
            foreach (var entry in studentAverages)
            {
                Console.WriteLine($"ID: {entry.Student.Id}, Név: {entry.Student.Name}, Osztály: {entry.Student.Class}, Átlag: {entry.Average:F2}");
            }
        }


        public List<Student> GetStudents()
        {
            return _students;
        }
        public void DisplayStudents()
        {          
            if (_students.Count == 0)
            {
                Console.WriteLine("Hiba: Nincs diák az adatbázisban.");
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
            int removedCount = _students.RemoveAll(s => s.Id == id); 
            return removedCount > 0;
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

        public void StudentPerformanceInSubject(int id, string subject)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                Console.WriteLine($"Hiba: A diák az adott ID-val ({id}) nem található.");
                return;
            }

            var subjectGrades = student.Subjects
                .Where(s => s.SubjectName == subject) 
                .SelectMany(s => s.Grades) 
                .ToList();

            if (!subjectGrades.Any())
            {
                Console.WriteLine($"Hiba: A diák ({student.Name}) nem rendelkezik jegyekkel a(z) {subject} tárgyból.");
                return;
            }

            double average = subjectGrades.Average(); 
            int bestGrade = subjectGrades.Max(); 

            Console.WriteLine($"Diák adatai - ID: {student.Id}, Név: {student.Name}, Osztály: {student.Class}");
            Console.WriteLine($"Tantárgy: {subject}, Átlag: {average:F2}, Legjobb jegy: {bestGrade}");
        }


        public void DisplayHelps()
        {
            Console.WriteLine(
@"Üdvözöllek Geptun-ban!
Az alábbi parancsok elérhetők:
FONTOS: A változtatások csak a 'Save' parancs beírása után mentődnek el!
(A '[]' között található elnevezések a megadható paraméterek)
- AddStudent [Tanuló neve] [Tanuló osztálya]: Az alábbi paranccsal hozzá adhatsz egy tanulót az adatbázishoz!
- AddSubject [Tanuló azonosítója] [Tantárgy neve]: Az alábbi paranccsal hozzá adhatsz egy tantárgyat a tanuló tantárgyai közé, 
    amit még nem tanul, ha az adatbázisban megtalálható a tanuló!
- AddGrade [Tanuló azonosítója] [Jegy] [Tantárgy neve]: Az alábbi paranccsal hozzá adhatsz egy jegyet a tanuló tantárgyához!
- BestAndAverageGrade [Tanuló azonosítója] [Tantárgy neve]: Kiírja a választott tanuló legjobb jegyét és átlagát az adott tantárgyból!
- BestAverageGrades [Tantárgy neve]: Kiírja az adott tantárgyból az első 5 legjobb tanuló átlagát!
- DisplayStudents: Kilistázza az adatbázisban lévő diákokat!
- RemoveStudent [Tanuló azonosítója]: Törli a diákot az adatbázisból!
- Save: A program futása közben létrehozott vagy módosított adatokat lehet menteni!
- Stop: A program leáll!
- Help: A parancsok listáját és azok funkcióját iratja ki a képernyőre!");

            //hihi
            Console.Beep();
        }
    }
}

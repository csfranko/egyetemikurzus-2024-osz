using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentRegistrySystem.Model;

namespace StudentRegistrySystem.DAO
{
    internal interface IStudentDAO
    {
        public void StudentPerformanceInSubject(int id, string subject);
        public List<Student> GetStudents();
        public void DisplayStudents();
        public void DisplayHelps();
        public bool AddStudent(Student student);
        public bool RemoveStudent(int id);
        public bool AddGrade(int id, int grade, string subjectName);
        public bool AddSubject(int id, string subjectName);
        public void BestAverageStudentsGradeInSubject(string subject);
        public void SaveStudents(string filePath, IEnumerable<Student> students);
        public void LoadStudents(string filePath);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StudentRegistrySystem.DAO;
using StudentRegistrySystem.Model;

namespace StudentRegistrySystem.Controller
{
    public class StudentController
    {
        private  IStudentDAO studentDAO;
        public StudentController() 
        { 
            studentDAO = new StudentMemoryDAO();
        }
        public bool AddStudent(Student student)
        {
            return studentDAO.AddStudent(student);
        }

        public bool AddSubject(int id, string subjectName)
        {

            return studentDAO.AddSubject(id, subjectName);
            
        }

        public bool AddGrade(int id, int grade, string subjectName)
        {

            return studentDAO.AddGrade(id, grade, subjectName);
            
        }

        public void DisplayStudents()
        {
            studentDAO.DisplayStudents();
        }

        public void BestAverageStudentsGradeInSubject(string subject)
        {

            studentDAO.BestAverageStudentsGradeInSubject(subject);

        }

        public void StudentPerformanceInSubject(int id, string subjectName) 
        { 
            studentDAO.StudentPerformanceInSubject(id, subjectName);
        }

        public void DisplayHelps()
        {
            studentDAO.DisplayHelps();
        }

        public bool RemoveStudent(int id) 
        { 
            return studentDAO.RemoveStudent(id);
        }

        public void SaveData(string filePath)
        {          
            studentDAO.SaveStudents(filePath, studentDAO.GetStudents());           
        }

        public void LoadData(string filePath)
        {
            studentDAO.LoadStudents(filePath);
        }
    }
}

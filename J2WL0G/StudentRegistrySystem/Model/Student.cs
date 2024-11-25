using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrySystem.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string Name { get; set; }
        public List<SubjectGrades> Subjects { get; set; }
        public Student(string _class, string _name) 
        {

        }

    }
}

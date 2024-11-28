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
        public Student(int id,string _name, string _class) 
        {
            Id = id;
            Name = _name;
            Class = _class;
            Subjects = new List<SubjectGrades>();
        }
        public Student() { }

    }
}

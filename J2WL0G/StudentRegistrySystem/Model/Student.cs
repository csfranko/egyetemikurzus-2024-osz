using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        public Student(int Id,string Name, string Class) 
        {
            this.Id = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0) & int.MaxValue;
            this.Name = Name;
            this.Class = Class;
            this.Subjects = new List<SubjectGrades>();
        }
        public Student() {
            this.Id = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0) & int.MaxValue;
        }

    }
}

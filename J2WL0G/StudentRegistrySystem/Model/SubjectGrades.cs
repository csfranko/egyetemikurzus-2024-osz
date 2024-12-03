using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrySystem.Model
{
    public record SubjectGrades
    {
        public string SubjectName { get; set; }
        public List<int> Grades { get; set; }

        public SubjectGrades(string _subjectName) 
        {
            SubjectName = _subjectName;
            Grades = new List<int>();
        }
        public SubjectGrades() {
            Grades = new List<int>();
        }
    }
}

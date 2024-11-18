using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrySystem.Model
{
    public class SubjectGrades
    {
        public string SubjectGrade { get; set; }
        public List<int> Grades { get; set; }

        public SubjectGrades() { }
    }
}

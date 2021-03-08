using System.Collections.Generic;
using System.Linq;

namespace StudentenausweisHalter.Models
{
    public class Semester
    {
        public Semester(int semesterNumber)
        {
            Number = semesterNumber;
            Subjects = new List<Subject>();
        }

        public int Number { get; set; }
        public IList<Subject> Subjects { get; set; }

        public Subject AddSubject(Subject subject)
        {
            Subjects.Add(subject);
            return subject;
        }

        public int TotalStudyHours => Subjects.Sum(s => s.TotalStudyHours);
    }
}
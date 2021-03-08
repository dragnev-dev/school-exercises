using System.Collections.Generic;
using System.Linq;

namespace StudentenausweisHalter.Models
{
    public class Semester
    {
        public Semester(int semesterNumber)
        {
            if (semesterNumber < 1 || semesterNumber > 12)
                throw new ApplicationException("Грешка при създаване на семестър:\n" +
                                               "Стойността за номер може да бъде между 1 и 12.");
                
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
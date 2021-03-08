using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentenausweisHalter.Models
{
    public class Student
    {
        public Student(string name)
        {
            Name = name;
            Semesters = new List<Semester>();
        }

        public string Name { get; set; }
        public IList<Semester> Semesters { get; set; }

        public Semester AddNewSemester(int semesterNumber)
        {
            var semester = new Semester(semesterNumber);
            Semesters.Add(semester);
            // TODO should the semesters be sorted here?
            return semester;
        }

        public Semester GetSemesterIfExistent(int semesterNumber)
        {
            return Semesters.FirstOrDefault(s => s.Number == semesterNumber);
        }

        public override string ToString()
        {
            /*1, ОП, 25, 30, проф. И.Иванов, 6;
             1, ЛААГ, 25, 30, проф. Г.Панайотова, 4;
             1, ДМ, 25, 30, проф. Р.Павлов, 3;
             2, ООП, 25, 30, проф. И.Иванов, 4;
             3, КМК, 25, 30, В.Иванов, 5*/
            var sb = new StringBuilder();
            sb.AppendLine($"Студент: {Name}");
            sb.AppendLine($"Общо изучавани дисциплини: {TotalSubjectCount}");
            sb.AppendLine("Среден успех по семестри и общ хорариум:");
            for (var i = 0; i < Semesters.Count; i++)
            {
                // TODO: are the subjects enumerated twice here?
                sb.AppendLine($"  {i+1}. Семестър {Semesters[i].Number}, {Semesters[i].TotalStudyHours}: {Semesters[i].Subjects.Select(s => s.Grade).Average():N2}");
            }
            sb.AppendLine($"Общ среден успех: {GradePointAverage:N2}");
            // TODO remove the last \n here 
            return sb.ToString();
        }

        private int TotalSubjectCount => Semesters.SelectMany(s => s.Subjects).Count();
        private double GradePointAverage => Semesters.SelectMany(s => s.Subjects.Select(su => su.Grade)).Average();
    }
}
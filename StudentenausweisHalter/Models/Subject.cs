using System;

namespace StudentenausweisHalter.Models
{
    public class Subject
    {
        public Subject(string subjectName, string teacherName, int lecturesStudyHours, int exercisesStudyHours, int grade)
        {
            if (grade < 2 || grade > 6)
                throw new ApplicationException("Грешка при създаване на дисциплина:\n" +
                                               "Допустимите стойности за оценка са между 2 и 6.");

            if (subjectName.Length < 2 || teacherName.Length < 2 || subjectName.Length > 50 || teacherName.Length > 50)
                throw new ApplicationException("Грешка при създаване на дисциплина:\n" +
                                               "Допустимата дължина за имена е между 2 и 50 символа.");
            
            if (lecturesStudyHours < 0 || exercisesStudyHours < 0  || lecturesStudyHours > 3000 || exercisesStudyHours > 3000)
                throw new ApplicationException("Грешка при създаване на дисциплина:\n" +
                                               "Допустимите стойности за хорариум са между 0 и 3000.");
            
            Name = subjectName;
            Teacher = teacherName;
            LecturesStudyHours = lecturesStudyHours;
            ExercisesStudyHours = exercisesStudyHours;
            Grade = grade;
        }

        public string Name { get; set; }
        public string Teacher { get; set; }
        public int LecturesStudyHours { get; set; }
        public int ExercisesStudyHours { get; set; }
        public int Grade { get; set; }
        public int TotalStudyHours => LecturesStudyHours + ExercisesStudyHours;
    }
}
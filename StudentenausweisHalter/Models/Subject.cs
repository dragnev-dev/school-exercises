namespace StudentenausweisHalter.Models
{
    public class Subject
    {
        public Subject(string subjectName, string teacher, int lecturesStudyHours, int exercisesStudyHours, int grade)
        {
            Name = subjectName;
            Teacher = teacher;
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
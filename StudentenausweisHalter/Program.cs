using System;
using System.Linq;
using StudentenausweisHalter.Models;

namespace StudentenausweisHalter
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Write("Студент: ");
                var input1 = ReadInputString();
                var input2 = ReadInputString();
                
                try
                {
                    // TODO check argument count
                    var student = new Student(input1);
                    var subjectInputChunks = input2.Split(";").Select(s => s.Trim());
                    foreach (var subjectInputChunk in subjectInputChunks)
                    {
                        var arguments = subjectInputChunk.Split(",").Select(s => s.Trim()).ToList();
                        var semesterNumber = ParseUserEnteredNumbers(arguments[0]);
                        var subjectName = arguments[1];
                        var lecturesStudyHours = ParseUserEnteredNumbers(arguments[2]);
                        var exercisesStudyHours = ParseUserEnteredNumbers(arguments[3]);
                        var teacher = arguments[4];
                        var grade = ParseUserEnteredNumbers(arguments[5]);

                        var semester = student.GetSemesterIfExistent(semesterNumber) ?? 
                                       student.AddNewSemester(semesterNumber);

                        if (semester.Subjects.Any(s => s.Name == subjectName))
                            throw new ApplicationException("Грешка при добавянето на дисциплина:\n" +
                                                           "Дисциплина с такова име вече съществува.");

                        var subject = new Subject(subjectName, teacher, lecturesStudyHours, exercisesStudyHours, grade);
                        semester.AddSubject(subject);
                    }

                    Console.WriteLine(student.ToString());
                }
                catch (ApplicationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static string ReadInputString()
        {
            return Console.ReadLine();
        }

        private static int ParseUserEnteredNumbers(string input)
        {
            var isParsable = int.TryParse(input, out int parsedValue);
            
            return isParsable ? parsedValue : throw new ApplicationException($"Невалидна стойност: {input}");
        }
    }
}
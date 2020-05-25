using System;
using System.Collections.Generic;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            List<double> listOfAverageGrades = new List<double>();
            int numberOfStudent = Students.Count;
            if (numberOfStudent < 5) throw new InvalidOperationException();

            foreach (Student student in Students)
            {
                listOfAverageGrades.Add(student.AverageGrade);
            }
            // now our list of Average grade will be sorted
            listOfAverageGrades.Sort();
            int rank = listOfAverageGrades.IndexOf(averageGrade);
            rank +=1;

            if (rank == -1)
            { // if for some reason we could not find the rank yet
                rank = 0;
                foreach (double studentAverageGrade in listOfAverageGrades)
                {
                    rank += 1;
                    if (studentAverageGrade <= averageGrade) break;
                }
            }

            int hundred = 100;
            double top20PercentA = 20;
            double top40PercentB = 40;
            double top60PercentC = 60;
            double top80PercentD = 80;

            double topNPercent = (rank / numberOfStudent) * hundred;

            if (topNPercent <= top20PercentA)
                return 'A';
            else if (topNPercent <= top40PercentB)
                return 'B';
            else if (topNPercent <= top60PercentC)
                return 'C';
            else if (topNPercent <= top80PercentD)
                return 'D';
            else
                return 'F';


        }

        private double TotalAverageGrade()
        {
            double result = 0.0;

            foreach (Student student in Students)
            {
                result += student.AverageGrade;
            }
            result /= Students.Count;

            return result;
        }

    }
}
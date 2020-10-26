using System;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class Book
    {
        private List<double> grades;

        readonly string category = "Science";
        public string Name
        {
            get;
            set;
        }
        public Book(string name)
        {
            grades = new List<double>();
            Name = name;
        }
        public void AddGrade(char letter)
        {
            switch(letter)
            {
                case 'A':
                    AddGrade(90.0);
                    break;
                case 'B':
                    AddGrade(80.0);
                    break;
                case 'C':
                    AddGrade(70.0);
                    break;
                default:
                    AddGrade(0.0);
                    break;
            }
        }
        public void AddGrade(double grade)
        {
            if(grade >=0 && grade <=100)
            {
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public event GradeAddedDelegate GradeAdded;
        public Statistics GetStatistics()
        {
            var result = new Statistics();
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;

            foreach(var grade in grades){
                result.Low = Math.Min(grade, result.Low);
                result.High = Math.Max(grade, result.High);
                result.Average += grade;
            }

            result.Average /= grades.Count;

            switch(result.Average)
            {
                case var d when d>=90.0:
                    result.Letter = 'A';
                    break;

                case var d when d>=80.0:
                    result.Letter = 'B';
                    break;
                
                case var d when d>=70.0:
                    result.Letter = 'C';
                    break;

                case var d when d>=60.0:
                    result.Letter = 'D';
                    break;

                default:
                    result.Letter = 'F';
                    break;
            }
            return result;
         }
    }
}
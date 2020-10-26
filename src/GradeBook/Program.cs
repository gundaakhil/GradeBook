﻿using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            //var p = new Program;
            //p.Main(args);
            //Book.AddGrade(55.5);
            
            var book = new Book("AKhil's Grade Book");

            book.GradeAdded += OnGradeAdded;
            book.GradeAdded += OnGradeAdded;

            var done = false;
            
            while(!done)
            {
                Console.WriteLine("Enter a grade or 'q' to quit");
                var input = Console.ReadLine();

                if(input == "q")
                {
                    done = true;
                    continue;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }
            }

            var stats = book.GetStatistics();

            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The Lowest grade is {stats.Low}");
            Console.WriteLine($"The Highest grade is {stats.High}");
            Console.WriteLine($"The Average grade is {stats.Average}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("GradeAdded");
        }
    }
}

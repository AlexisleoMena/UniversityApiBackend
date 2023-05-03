using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León",
            };

            /// 1. SELECT * of cars
            var carList = from car in cars select car;
            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE car is Audi
            var audiList = from car in cars where car.Contains("Audi") select car;
            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }
        }

        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Each Number Multiplied by 3
            // Take all numbers, but 9
            // Order number by ascending value
            var processedNumberList = numbers
                .Select(num => num * 3) // { 3, 6, 9, etc }
                .Where(num => num != 9) // { all but the 9}
                .OrderBy(num => num); // at the end, we order ascending
        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string> { "a", "bx", "c", "d", "e", "cj", "f", "c" };

            // 1. First of all elements
            var first = textList.First();

            // 2. First element that is "c"
            var cText = textList.First(text => text.Equals("c"));

            // 3. First element that contains "j"
            var jText = textList.First(text => text.Equals("j"));

            // 4. First element that contains "z" or default
            var firstOrDefaultText = textList.FirstOrDefault(text => text.Contains("z")); // "" or element that contains "z"

            // 5. Last element that contains "z" or default
            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z")); // "" or element that contains "z"

            // 6. Sigle Values
            var uniqueTexts = textList.Single();
            var uniqueOrDefaultTexts = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            // Obtain { 4, 8 }
            var myEventNumbers = evenNumbers.Except(otherEvenNumbers);
        }

        static public void MultipleSelects()
        {
            // SELECT MANY
            string[] myOpinions =
            {
                "Opínión 1",
                "text 1",
                "Opínión 2",
                "text 2",
                "Opínión 3",
                "text 3",
            };

            var nyOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Alexis",
                            Email = "alexismena@gmail.com",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Juan",
                            Email = "juanmena@gmail.com",
                            Salary = 4000
                        },
                        new Employee
                        {
                            Id = 3,
                            Name = "Kevin",
                            Email = "kevinmena@gmail.com",
                            Salary = 1500
                        },
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id = 1,
                            Name = "Pedro",
                            Email = "pedromena@gmail.com",
                            Salary = 2000
                        },
                        new Employee
                        {
                            Id = 2,
                            Name = "Oscar",
                            Email = "oscarmena@gmail.com",
                            Salary = 6000
                        },
                        new Employee
                        {
                            Id = 3,
                            Name = "Leandro",
                            Email = "leandromena@gmail.com",
                            Salary = 500
                        },
                    }
                }
            };

            // Obtain all Employees of all Enterprises
            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Know if ana list is empty
            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // All enterprises at least emplyees with more tahn 1000 of salary
            bool hasEmployeeWithSalaryMoreThan1000 = enterprises.Any(
                enterprises => enterprises.Employees.Any(employee => employee.Salary >= 1000)
            );
        }

        static public void linqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondtList = new List<string>() { "a", "b", "d" };

            // INNER JOIN
            var commonResult =
                from element in firstList
                join secondElement in secondtList on element equals secondElement
                select new { element, secondElement };

            var commonResult2 = firstList.Join(
                secondtList,
                element => element,
                secondElement => secondElement,
                (element, secondElement) => new { element, secondElement }
            );

            // OUTER JOIN - LEFT
            var leftOuterJoin =
                from element in firstList
                join secondElement in secondtList on element equals secondElement into temporalList
                from temporalElement in temporalList.DefaultIfEmpty()
                where element != temporalElement
                select new { Element = element };

            var leftOuterJoin2 =
                from element in firstList
                from secondElement in secondtList.Where(s => s == element).DefaultIfEmpty()
                select new { Element = element, SecondElement = secondElement };

            // OUTER JOIN - RIGHT
            var rightOuterJoin =
                from secondElement in secondtList
                join element in firstList on secondElement equals element into temporalList
                from temporalElement in temporalList.DefaultIfEmpty()
                where secondElement != temporalElement
                select new { Element = secondElement };

            // UNION
            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }

        static public void skipTakeLinq()
        {
            var myList = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, };

            // SKIP

            var skipTwoFirstValues = myList.Skip(2); // {3,4,5,6,7,8,9,10,11}
            var skipTwoLastValues = myList.SkipLast(2); // {1,2,3,4,5,6,7,8,9}
            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4); // {4,5,6,7,8,9,10,11}

            // TAKE

            var takeTwoFirstValues = myList.Take(2); // {1,2}
            var takeTwoLastValues = myList.TakeLast(2); // {10,11}
            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); // {1,2,3}
        }

        // Paging
        public IEnumerable<T> GetPage<T>(
            IEnumerable<T> collection,
            int pageNumber,
            int resultsPerPage
        )
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }

        // Variables
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage =
                from number in numbers
                let average = numbers.Average()
                let bSquared = Math.Pow(number, 2)
                where bSquared > average
                select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Number: {0} Square: {1}", number, Math.Pow(number, 2));
            }
        }

        // ZIP
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            // { "1=one", "2=two", ...}
            IEnumerable<string> zipNumbers = numbers.Zip(
                stringNumbers,
                (number, word) => number + "=" + word
            );
        }

        // Repeat && Range
        static public void repeatRangeLinq()
        {
            // Generate collection from 1 - 1000
            //var first1000 = Enumerable.Range(1, 1000);
            IEnumerable<int> first1000 = Enumerable.Range(1, 1000);

            // Repeat a value N times
            //var fiveXs = Enumerable.Repeat("X", 5);
            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5); // {"X", "X", "X", "X", "X"}
        }

        static public void studentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Alexis",
                    Grade = 98,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false,
                },
                new Student
                {
                    Id = 3,
                    Name = "Kevin",
                    Grade = 80,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Pedro",
                    Grade = 70,
                    Certified = true,
                },
            };

            var certifiedStudents =
                from student in classRoom
                where student.Certified
                select student;

            var notCetifiedStudents =
                from student in classRoom
                where student.Certified == false
                select student;

            var approvedStudentsNames =
                from student in classRoom
                where student.Grade >= 50 && student.Certified == true
                select student.Name;
        }

        // ALL
        static public void AllLinq()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

            bool allAreSmallerThan10 = numbers.All(x => x < 10); // true

            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2); // false

            var emptyList = new List<int>();

            bool allNumbersAreGreaterThan0 = numbers.All(x => x >= 0); // true
        }

        // Aggregate
        static public void aggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Sum all numbers
            int sum = numbers.Aggregate((prev, current) => prev + current);

            string[] words = { "Hello, ", "my", "name", "is", "Alexis" };
            string greeting = words.Aggregate((prev, current) => prev + current);
        }

        // Distinct
        static public void distincValues()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };
            IEnumerable<int> distinctValues = numbers.Distinct();
        }

        // GroupBy
        static public void groupByExamples()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Obtain only even numbers and generate two group
            var grouped = numbers.GroupBy(x => x % 2 == 0);

            // We will hae two group:
            // 1. The group that doesnt fit the condition
            // 2. The group that fits the condition (even numbers)
            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value); //1,3,5,7,9, ..., 2,4,6,8 (first the odds and then the even)
                }
            }

            // Another example
            var classRoom = new[]
            {
                new Student
                {
                    Id = 1,
                    Name = "Alexis",
                    Grade = 98,
                    Certified = true,
                },
                new Student
                {
                    Id = 2,
                    Name = "Juan",
                    Grade = 50,
                    Certified = false,
                },
                new Student
                {
                    Id = 3,
                    Name = "Kevin",
                    Grade = 80,
                    Certified = true,
                },
                new Student
                {
                    Id = 4,
                    Name = "Pedro",
                    Grade = 70,
                    Certified = true,
                },
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified);

            // We obtain two group
            // 1- Not certified students
            // 2- Certified students

            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("------------- {0} -----------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student.Name);
                }
            }
        }

        static public void relationLinq()
        {
            List<Post> posts = new List<Post>
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My content"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My second comment",
                            Content = "My content"
                        },
                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My content"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My second comment",
                            Content = "My content"
                        },
                    }
                }
            };

            var commentsContent = posts.SelectMany(
                post => post.Comments,
                (post, comment) => new { PostId = post.Id, CommentContent = comment.Content }
            );
        }
    }
}

using System;

namespace St
{
    class Program
    {
        static void Main(string[] args) {
            try
            {
                var student = new Student("1222", "2", "3", "4", 3, "12345");
                object student2 = new Student("123", "12", "1", "1", 3, "1");
                var rbn = student.RecordBookNumber;

                Console.WriteLine(student2.Equals(student));

                foreach (var fieldValue in student)
                {
                    Console.WriteLine(fieldValue);
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}
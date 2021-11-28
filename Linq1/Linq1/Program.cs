using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq1
{
    class Program
    {
        static void Main(string[] args)
        {
            UniversityManager um = new UniversityManager();

            um.MaleStudents();
            um.FemaleStudents();

            Console.ReadKey();
        }
    }

    class UniversityManager
    {
        public List<University> universities;
        public List<Student> students;

        public UniversityManager()
        {
            universities = new List<University>();
            students = new List<Student>();

            universities.Add(new University { Id = 1, Name = "Yale" });
            universities.Add(new University { Id = 2, Name = "Beijing Tech" });

            students.Add(new Student { Id = 1, Name = "Carla", Gender = "female", Age = 17, UniverityId = 1 });
            students.Add(new Student { Id = 2, Name = "Toni", Gender = "male", Age = 21, UniverityId = 1 });
            students.Add(new Student { Id = 3, Name = "Leyla", Gender = "female", Age = 19, UniverityId = 2 });
            students.Add(new Student { Id = 4, Name = "James", Gender = "trans-gender", Age = 25, UniverityId = 2 });
            students.Add(new Student { Id = 5, Name = "Linda", Gender = "female", Age = 22, UniverityId = 2 });
        }

        public void MaleStudents()
        {
            IEnumerable<Student> maleStudents = from student in students where student.Gender == "male" select student;
            Console.WriteLine("Male - Students: ");

            foreach (Student student in maleStudents)
            {
                student.Print();
            }
        }

        public void FemaleStudents()
        {
            IEnumerable<Student> femaleStudents = from student in students where student.Gender == "female" select student;
            Console.WriteLine("Female - Students: ");

            foreach (Student student in femaleStudents)
            {
                student.Print();
            }
        }
    }

    class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public void Print()
        {
            Console.WriteLine("Univeristy {0} with Id {1}", Name, Id);
        }
    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        // Foreign Key
        public int UniverityId { get; set; }

        public void Print()
        {
            Console.WriteLine("Student {0} with Id {1}, Gender {2} and age {3} from University with Id {4}", Name, Id, Gender, Age, UniverityId);
        }
    }
}

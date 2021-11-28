using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqWithXML
{
    class Program
    {
        static void Main(string[] args)
        {
            string studentsXML =
                @"<Students>
                    <Student>
                        <Name>Toni</Name>
                        <Age>21</Age>
                        <University>Yale</University>
                        <Year>5</Year>
                    </Student>
                    <Student>
                        <Name>Carla</Name>
                        <Age>17</Age>
                        <University>Yale</University>
                        <Year>1</Year>
                    </Student>
                    <Student>
                        <Name>Leyla</Name>
                        <Age>19</Age>
                        <University>Beijing Tech</University>
                        <Year>3</Year>
                    </Student>
                    <Student>
                        <Name>Chris</Name>
                        <Age>20</Age>
                        <University>Beijing Tech</University>
                        <Year>4</Year>
                    </Student>
                 </Students>";

            XDocument studentXdoc = new XDocument();
            studentXdoc = XDocument.Parse(studentsXML);

            var students = from student in studentXdoc.Descendants("Student")
                           select new
                           {
                               Name = student.Element("Name").Value,
                               Age = student.Element("Age").Value,
                               University = student.Element("University").Value,
                               Year = student.Element("Year").Value
                           };

            foreach (var student in students)
            {
                Console.WriteLine("Student {0} with age {1} from university {2} in year {3}", student.Name, student.Age, student.University, student.Year);
            }

            var studentsSortedByAge = from student in students
                                      orderby student.Age
                                      select student;

            foreach (var student in studentsSortedByAge)
            {
                Console.WriteLine("Student {0} with age {1} from university {2} in year {3}", student.Name, student.Age, student.University, student.Year);
            }

            Console.ReadKey();
        }
    }
}

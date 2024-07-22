using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using static Music_School.Configuration.AppConfiguration;
using Music_School.Model;
using System.Reflection.Metadata;

namespace Music_School.Service
{
    internal static class MusicSchoolService
    {
        public static void save()
        {                     
                XDocument.Load(MusicSchoolPath).Save(MusicSchoolPath);                   
        }

        public static XElement? GetXElementByNameAttribute(string elementname, string elementvalue) =>
                 XDocument.Load(MusicSchoolPath)
                .Descendants(elementname)
                .FirstOrDefault(element => element.Attribute("name")?.Value == elementvalue);



        public static XElement StudentToXMl(Student student) =>
                    new XElement("student",
                    new XAttribute("name", student.Name),
                    new XElement("instrument", student.Instrument.InsturmentName));

        public static XElement TeacherToXMl(Teacher teacher) =>
                    new XElement("teacher",
                    new XAttribute("name", teacher.Name));
        public static XElement InsturmentToXMl(Insturment insturment) =>
                    new XElement("instrument",
                    new XAttribute("name", insturment.InsturmentName));
                                   
        

        public static void CreateXmlIfNotExist()
        {
            if (!File.Exists(MusicSchoolPath))
            {
                XDocument document = new ();
                XElement musicschool = new("music-school");
                document.Add(musicschool);
                document.Save(MusicSchoolPath);
            }

        }



        public static void InsertClassRoom(string classroomname)
        {

            XDocument document = XDocument.Load(MusicSchoolPath);
            var root = document.Descendants("music-school")
            .FirstOrDefault();
            if (root != null)
            {
                XElement classroom = new(
                    "class-room",
                    new XAttribute("name", classroomname)

                    );

                root.Add(classroom);
                document.Save(MusicSchoolPath);
            }
        }

        public static void InsertTeacher(Teacher teacher, string classroomname)
        {

            if (GetXElementByNameAttribute("class-room", classroomname) is XElement classroom)
            {              
                classroom.Add(TeacherToXMl(teacher));
                save();
            }
        }

        public static void InsertStudent(Student student, string classroomname)
        {

            if (GetXElementByNameAttribute("class-room", classroomname) is XElement classroom)
            {               
                classroom.Add(StudentToXMl(student));
                save();
            }
        }



        public static void InsertStudents(List<Student> students, string classroomname)
        {

            if (GetXElementByNameAttribute("class-room", classroomname) is XElement classroom)
            {
                classroom.Add(
                students.Select(StudentToXMl).ToList());
                save();
            }
        }



        public static bool StudentExists(Student student)
        {
            return XDocument.Load(MusicSchoolPath)
            .Descendants("student")
            .Any(s => s.Attribute("name")?.Value == student.Name);      
        }


        public static void UpdateInsturment(Student student, Insturment instrument)
        {
            if (GetXElementByNameAttribute("student", student.Name) is XElement XStudent)
            {
                XStudent.Descendants("instrument").FirstOrDefault().SetValue(instrument.InsturmentName);               
                save(); 
                
            }


        }


    }
}

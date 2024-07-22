using Music_School.Model;
using static Music_School.Service.MusicSchoolService;



namespace Music_School
{
    public partial class Form1 : Form
    {
        static Insturment guitar = new("guitar");
        static Insturment piano = new("piano");
        static Student s1 = new ("me", piano);
        static Student s2 = new ("you", guitar);
        static List<Student> students = [s1, s2];
        public Form1()
        {
            InitializeComponent();
            CreateXmlIfNotExist();
            InsertStudents(students, "new-class");
            UpdateInsturment(s2, piano);
            Close();

        }
    }
}


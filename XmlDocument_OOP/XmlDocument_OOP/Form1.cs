using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XmlDocument_OOP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("../../Students.xml");
            XmlNode students = xdoc.SelectSingleNode("Students");
            XmlNodeList list = students.SelectNodes("Student");
            lstStudents.Items.Clear();
            foreach (XmlNode student in list)
            {
                Student s = new Student();
                s.Name = student.Attributes["name"].Value;
                s.Surname = student.Attributes["surname"].Value;
                s.Node = student;
                lstStudents.Items.Add(s);
            }
        }

        private void lstStudents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstStudents.SelectedItem == null) return;
            Student selected = (Student)lstStudents.SelectedItem;
            lstCourses.Items.Clear();
            foreach (XmlNode course in selected.Node.SelectNodes("Course"))
            {
                Course c = new Course();
                c.Node = course;
                lstCourses.Items.Add(c);
            }

        }

        private void lstCourses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstCourses.SelectedItem == null) return;
            Course selected = (Course)lstCourses.SelectedItem;
            lstGrades.Items.Clear();

            foreach (XmlNode exam in selected.Node.SelectNodes("Exams/Exam"))
            {
                Exam ex = new Exam();
                ex.Node = exam;
                lstGrades.Items.Add(ex);
            }
            lstProjects.Items.Clear();
            foreach (XmlNode project in selected.Node.SelectNodes("Projects/Project"))
            {
                Project p = new Project();
                p.Node = project;
                lstProjects.Items.Add(p);
            }

        }
    }
}

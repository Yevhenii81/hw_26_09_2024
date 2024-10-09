using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Class_Student_hw_25_06_2024;
using static System.Formats.Asn1.AsnWriter;

namespace Class_Group_hw_27_06
{
    enum Speciallization { SoftwareDeveloper, Designer }

    internal class Group : ICloneable, IEnumerable
    {
        private List<Student> students = new List<Student>();
        private string name;
        private int courseNumber;
        private Speciallization groupSpeciallization;

        public event EventHandler<GroupEventArgs> StudentAdded;
        public event EventHandler<GroupEventArgs> StudentRemoved;

        public Group() : this("", 0, Speciallization.SoftwareDeveloper, new List<Student>()) { }
        public Group(string name, int courseNumber, Speciallization groupSpeciality, List<Student> students)
        {
            SetStudentsList(students);
            SetName(name);
            SetCourseNumber(courseNumber);
            SetSpeciallization(groupSpeciality);
        }

        public Group(Group group)
        {
            this.name = group.name;
            this.courseNumber = group.courseNumber;
            this.groupSpeciallization = group.groupSpeciallization;
            this.students = new List<Student>();
            foreach (Student student in group.students)
            {
                this.students.Add((Student)student.Clone());
            }
        }

        public Group Copy()
        {
            return new Group(this);
        }

        //метод для вызова события добавления студента

        public void OnStudentAdded(Student student)
        {
            StudentAdded?.Invoke(this, new GroupEventArgs { Info = $"{student.GetName()} added to the group {name}" });
        }

        //метод для вызова событися удаления студента

        public void OnStudentRemoved(Student student)
        {
            StudentRemoved?.Invoke(this, new GroupEventArgs { Info = $"{student.GetName()} removed from the group {name}" });
        }

        //Clone
        public object Clone()
        {
            return this.Copy();
        }

        ////CompareTo
        //public int CompareTo(Group group)
        //{
        //    if (group == null) return 1;
        //    int studentCountComparison = this.GetStudentList().Count.CompareTo(group.GetStudentList().Count);

        //    if (studentCountComparison != 0)
        //        return studentCountComparison;
        //    return string.Compare(this.GetName(), group.GetName(), StringComparison.Ordinal);
        //}

        //ToString
        public override string ToString()
        {
            var studentsInfo = students.Count > 0
                ? string.Join("\n", students)
                : "No students in the group.";

            return $"Group Name: {name}\n" +
                   $"Speciality: {groupSpeciallization}\n" +
                   $"Course: {courseNumber}\n" +
                   $"Number of Students: {students.Count}\n" +
                   $"Students:\n{studentsInfo}";
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return this.name;
        }
        public void SetCourseNumber(int courseNumber)
        {
            this.courseNumber = courseNumber;
        }

        public int GetCourseNumber()
        {
            return this.courseNumber;
        }

        public void SetSpeciallization(Speciallization speciallization)
        {
            this.groupSpeciallization = speciallization;
        }

        public Speciallization GetSpeciallization()
        {
            return this.groupSpeciallization;
        }

        public void SetStudentsList(List<Student> students)
        {
            this.students = students;
        }

        public List<Student> GetStudentList()
        {
            return this.students;
        }

        public void AddStudent(Student student)
        {
            students.Add(student);
            OnStudentAdded(student); 
        }

        public void RemoveStudent(Student student)
        {
            if (students.Remove(student))
            {
                OnStudentRemoved(student); 
            }
            else
            {
                Console.WriteLine($"Student {student.GetName()} not found in group {name}");
            }
        }

        public void DeleteStudent(in Student student)
        {
            this.students.Remove(student);
            OnStudentRemoved(student);
        }
        public void TransferStudent(in Student student, Group group)
        {
            this.DeleteStudent(student);
            group.AddStudent(student);
        }

        public void ExpulsionFallingStudents()
        {
            foreach (var item in this.students)
            {
                double average = 0;
                for (int i = 0; i < item.GetExamRates().Count; i++)
                    average += item.GetExamRates()[i];
                average /= item.GetExamRates().Count;
                if (average < 6)
                {
                    this.DeleteStudent(item);
                }
            }
        }

        public void StudentsExpulsion()
        {
            for (int i = 0; i < this.students.Count; i++)
            {
                for (int j = 0; j < students[i].GetExamRates().Count; j++)
                {
                    if (students[i].GetExamRates()[j] < 6)
                    {
                        this.DeleteStudent(students[i]);
                        i--;
                        break;
                    }
                }
            }
        }

        public IEnumerator<Student> GetEnumerator() => students.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        //overload

        //public static bool operator ==(Group left, Group right)
        //{
        //    string averageGrade1 = left.GetName();
        //    string averageGrade2 = right.GetName();

        //    return averageGrade1 == averageGrade2;
        //}

        //public static bool operator !=(Group left, Group right)
        //{
        //    return !(left == right);
        //}
    }

    class GroupePrinter
    {
        static public void Print(in Group group)
        {
            Console.WriteLine(group.GetName());
            Console.WriteLine(group.GetCourseNumber());
            Console.WriteLine(group.GetSpeciallization());

            foreach(var student in group)
            {
                StudentPrinter.Print(student);
            }
            Console.WriteLine();
        }
    }

    public class GroupEventArgs : EventArgs
    {
        public string Info { get; set; }
    }
}
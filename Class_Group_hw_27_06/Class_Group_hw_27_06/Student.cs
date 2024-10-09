using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
interface IPrint
{
    void Print();
}

namespace Class_Student_hw_25_06_2024
{
    internal class Student : IComparable<Student>, ICloneable
    {
        string name;
        string secondname;
        string lastname;
        DateTime birthday;
        List<int> homeworkRates = new List<int>();
        List<int> classworkRates = new List<int>();
        List<int> examRates = new List<int>();
        int averageGrade;

        public event EventHandler<StudentEventArgs> GradeAchieved;
        public event EventHandler<StudentEventArgs> CourseCompleted;

        public Student() : this("Student", "Studentov") { }
        public Student(string name, string lastname) : this(name, " ", lastname, new DateTime(5534, 12, 3)) { }
        public Student(string name, string secondname, string lastname, DateTime birthday) : this(name, secondname, lastname, new DateTime(2222, 12, 12), new List<int>(), new List<int>(), new List<int>()) { }
        public Student(string name, string secondname, string lastname, DateTime birthday, List<int> homeworkRates, List<int> classworkRates, List<int> examRates)
        {
            SetName(name);
            SetSecondName(secondname);
            SetLastName(lastname);
            SetBirthday(birthday);
            SetHomeworkRates(homeworkRates);
            SetClassWorkRates(classworkRates);
            SetExamRates(examRates);
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetLastName(string lastname)
        {
            this.lastname = lastname;
        }

        public string GetLastName()
        {
            return this.lastname;
        }

        public void SetSecondName(string secondname)
        {
            this.secondname = secondname;
        }

        public string GetSecondName()
        {
            return this.secondname;
        }

        public void SetBirthday(DateTime birthday)
        {
            this.birthday = birthday;
        }

        public DateTime GetBirthday()
        {
            return this.birthday;
        }

        public void SetHomeworkRates(List<int> homeworkRates)
        {
            this.homeworkRates = homeworkRates;
        }

        public void SetHomeworkRates(params int[] homeworkRates)
        {
            for (int i = 0; i < homeworkRates.Length; i++)
            {
                this.homeworkRates.Add(homeworkRates[i]);
            }
        }

        public List<int> GetHomeworkRates()
        {
            return this.homeworkRates;
        }

        public void SetClassWorkRates(List<int> classworkRates)
        {
            this.classworkRates = classworkRates;
        }

        public void SetClassWorkRates(params int[] classworkRates)
        {
            for (int i = 0; i < classworkRates.Length; i++)
            {
                this.classworkRates.Add(classworkRates[i]);
            }
        }

        public List<int> GetClassworkRates()
        {
            return this.classworkRates;
        }

        public void SetExamRates(List<int> examRates)
        {
            this.examRates = examRates;
        }

        public void SetExamRates(params int[] examRates)
        {
            for (int i = 0; i < examRates.Length; i++)
            {
                this.examRates.Add(examRates[i]);
            }
        }

        public List<int> GetExamRates()
        {
            return this.examRates;
        }

        public int GetAverageGrade()
        {
            return averageGrade;
        }

        //вычесление среднего балла

        //public void PlusAverageGrade(int number)
        //{
        //    this.averageGrade += number;
        //}

        //public int AverageGrade(Student student)
        //{
        //    for (int i = 0; i < student.GetExamRates().Count; i++)
        //    {
        //        int r = student.GetExamRates()[i] / student.GetExamRates().Capacity;
        //        student.PlusAverageGrade(r);
        //    }
        //    return student.GetAverageGrade();
        //}

        //public void AddHomeworkGrades(Student student, int grade)
        //{
        //    student.GetHomeworkRates().Add(grade);
        //}

        //public void AddExamGrades(Student student, int grade)
        //{
        //    student.GetExamRates().Add(grade);
        //}

        public class AverageGradeComparatorAZ : IComparer<Student>
        {
            public int Compare(Student? x, Student? y)
            {
                if (x == null || y == null) throw new ArgumentNullException("Student is null.");
                return x.averageGrade.CompareTo(y.averageGrade); // Возрастание
            }
        }

        //вложенные классы для сравнения студентов

        public class NameComparatorAZ : IComparer<Student>
        {
            public int Compare(Student? x, Student? y)
            {
                if (x == null || y == null) throw new ArgumentNullException("Student is null.");
                return string.Compare(x.GetName(), y.GetName(), StringComparison.Ordinal); // Сравнение по имени
            }
        }

        public class NameComparatorZA : IComparer<Student>
        {
            public int Compare(Student? x, Student? y)
            {
                if (x == null || y == null) throw new ArgumentNullException("Student is null.");
                return string.Compare(y.GetName(), x.GetName(), StringComparison.Ordinal); // Обратное сравнение по имени
            }
        }

        //реализация IComparable<Student>
        public int CompareTo(Student other)
        {
            if (other == null) return 1;
            return this.averageGrade.CompareTo(other.averageGrade); //сравнение по среднему баллу
        }

        //реализация ICloneable
        public object Clone()
        {
            return new Student(name, secondname, lastname, birthday, new List<int>(homeworkRates), new List<int>(classworkRates), new List<int>(examRates));
        }

        public int CalculateAverageGrade()
        {
            int totalRates = homeworkRates.Count + classworkRates.Count + examRates.Count;
            if (totalRates == 0) return 0;

            int sumRates = homeworkRates.Sum() + classworkRates.Sum() + examRates.Sum();
            return sumRates / totalRates;
        }

        //события

        public void OnGradeAchieved(int grade)
        {
            GradeAchieved?.Invoke(this, new StudentEventArgs { Info = $"Student {this.GetName()} achieved grade {grade}" });
        }

        public void OnCourseCompleted(string courseName)
        {
            CourseCompleted?.Invoke(this, new StudentEventArgs { Info = $"Student {this.GetName()} completed the course {courseName}" });
        }


        //ToString
        public override string ToString()
        {
            return $"Surname: {GetSecondName()}\n" +
                   $"Name: {GetName()}\n" +
                   $"Lastname: {GetLastName()}\n" +
                   $"Birthday: {GetBirthday()}\n" +
                   //$"Average Grade: {studentManager.AverageGrade(this)}\n" +
                   $"Homework Rates: {string.Join(", ", homeworkRates)}\n" +
                   $"Classwork Rates: {string.Join(", ", classworkRates)}\n" +
                   $"Classwork Rates:  {string.Join(", ", classworkRates)}\n" +
                    $"Average Grade: {CalculateAverageGrade()}\n";
        }

        //overload

        public static bool operator true(Student original)
        {
            return original.averageGrade != 0;
        }

        public static bool operator false(Student original)
        {
            return original.averageGrade == 0;
        }

        public static bool operator ==(Student left, Student right)
        {
            int averageGrade1 = left.averageGrade;
            int averageGrade2 = right.averageGrade;

            return averageGrade1 == averageGrade2;
        }

        public static bool operator !=(Student left, Student right)
        {
            return !(left == right);
        }

        public static bool operator >(Student a, Student b)
        {
            return a.averageGrade > b.averageGrade;
        }

        public static bool operator <(Student a, Student b)
        {
            return a.averageGrade < b.averageGrade;
        }

        public override bool Equals(object obj)
        {
            if (obj is Student student)
            {
                return this.averageGrade == student.averageGrade;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.averageGrade.GetHashCode();
        }
    }

    //классы для обработки событий

    public class StudentEventArgs : EventArgs
    {
        public string Info { get; set; }
    }

    class StudentPrinter
    {
        public static void Print(Student student) 
        {
            Console.WriteLine(student.ToString());
        }
    }
}
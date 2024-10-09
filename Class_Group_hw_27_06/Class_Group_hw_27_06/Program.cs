using Class_Student_hw_25_06_2024;
using Class_Group_hw_27_06;

namespace Class_Group_hw_27_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student("Student", "Studenkov", "Studenkovich", DateTime.Now, new List<int> { 8, 7, 9 }, new List<int> { 9, 9, 9 }, new List<int> { 8, 7, 9 });
            Student student2 = new Student("Petya", "Petrovkin", "Vasilievich", DateTime.Now, new List<int> { 6, 5, 6 }, new List<int> { 5, 6, 6 }, new List<int> { 6, 5, 6 });
            Student student3 = new Student("Mikhail ", "Zubenko", "Petrovich", DateTime.Now, new List<int> { 9, 10, 9 }, new List<int> { 10, 9, 9 }, new List<int> { 9, 10, 9 });

            Group group = new Group("P26", 1, Speciallization.SoftwareDeveloper, new List<Student>());

            student1.GradeAchieved += (sender, e) =>
            {
                Console.WriteLine($"Notification: Student {student1.GetName()} achieved grade - {e.Info}");
            };
            student1.CourseCompleted += (sender, e) =>
            {
                Console.WriteLine($"Notification: Student {student1.GetName()} completed course - {e.Info}");
            };

            group.StudentAdded += (sender, e) =>
            {
                Console.WriteLine($"Notification: Student added - {e.Info}");
            };
            group.StudentRemoved += (sender, e) =>
            {
                Console.WriteLine($"Notification: Student removed - {e.Info}");
            };

            group.AddStudent(student1);
            group.AddStudent(student2);
            group.AddStudent(student3);

            Console.WriteLine("\nStudents in the group:");
            foreach (Student student in group)
            {
                Console.WriteLine(student.GetName());
            }

            student1.GetHomeworkRates().Add(10);
            student1.OnGradeAchieved(10);

            group.RemoveStudent(student2);
        }
    }
}
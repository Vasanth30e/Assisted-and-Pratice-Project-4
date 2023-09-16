using NUnit.Framework;
using System.Collections.Generic;

namespace SchoolManagementTest
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void StudentTest()
        {
            var studentList = new List<Student>();
            var student = new Student { StudentId = 1, StudentName = "Sam", StudentEmail = "sam@email.com", StudentPhone = 9876523456};

            studentList.Add(student);
            Assert.AreEqual(1, studentList.Count);
            Assert.AreEqual(2,studentList.Count);
        }

        [Test]
        public void SubjectTest()
        {
            var subjectList = new List<Subject>();
            var subject = new Subject { SubjectCode = 101, SubjectName = "Biology"};

            subjectList.Add(subject);
            Assert.AreEqual(1, subjectList.Count);
        }

        [Test]
        public void TeacherTest()
        {
            var teacherList = new List<Teacher>();
            var teacher = new Teacher { TeacherId = 1, TeacherName = "John", TeacherDescription = "Biology Teacher"};

            teacherList.Add(teacher);
            Assert.AreEqual(1, teacherList.Count);
        }
    }
}

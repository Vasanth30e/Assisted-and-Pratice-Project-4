using NUnit.Framework;
using Moq;

namespace SchoolManagementTest
{
    [TestFixture]
    public class MockTest
    {
        [Test]
        public void GetById()
        {
            // Arrange
            var studentRepositoryMock = new Mock<IStudentRepository>();
            studentRepositoryMock.Setup(repo => repo.GetById(1)).Returns(new Student { StudentId = 1, StudentName = "Alice", StudentEmail = "alice@email.com", StudentPhone = 9765425487 });

            var subjectRepositoryMock = new Mock<ISubjectRepository>();
            subjectRepositoryMock.Setup(repo => repo.GetById(1)).Returns(new Subject { SubjectCode = 1, SubjectName = "Math" });

            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(repo => repo.GetById(1)).Returns(new Teacher { TeacherId = 1, TeacherName = "John Doe", TeacherDescription = "Biology" });

            var repository = new Repository(studentRepositoryMock.Object, subjectRepositoryMock.Object, teacherRepositoryMock.Object);

            // Act
            var student = repository.GetStudentById(1);
            var subject = repository.GetSubjectById(1);
            var teacher = repository.GetTeacherById(1);

            // Assert
            Assert.NotNull(student);
            Assert.NotNull(subject);
            Assert.NotNull(teacher);

            Assert.AreEqual("Alice", student.StudentName);
            Assert.AreEqual("Math", subject.SubjectName);
            Assert.AreEqual("John Doe", teacher.TeacherName);
        }
    }
}

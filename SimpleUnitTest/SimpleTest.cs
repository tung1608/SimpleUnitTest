using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SimpleUnitTest.Utils;

namespace SimpleUnitTest
{
    public class SimpleTest
    {
        [Test]
        [AutoMoqData]
        public void RegisterSubjectTest_WithInValidSubject(StudentService service, Mock<ISubjectManager> subjectManager, Mock<IStudentManager> studentManager)
        {
            subjectManager.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);
            var result = service.RegisterSubject(new Student(), "test");
            result.Should().Be(-1);
        }

        [Test]
        [AutoMoqData]
        public void RegisterSubjectTest_WithInValidSubject_Banned(StudentService service, Mock<ISubjectManager> subjectManager, Mock<IStudentManager> studentManager)
        {
            subjectManager.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);
            studentManager.Setup(i => i.IsBanned(It.IsAny<string>())).Returns(true);
            var result = service.RegisterSubject(new Student(), "test");
            result.Should().Be(-2);
        }

        [Test]
        [AutoMoqData]
        public void RegisterSubjectTest_WithInValidSubject_NotBanned(StudentService service, Mock<ISubjectManager> subjectManager, Mock<IStudentManager> studentManager)
        {
            subjectManager.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);
            studentManager.Setup(i => i.IsBanned(It.IsAny<string>())).Returns(false);
            var result = service.RegisterSubject(new Student(), "test");
            result.Should().Be(0);
        }
    }

    public class Student
    {
        public string Name;
    }

    public interface IStudentService
    {
        int RegisterSubject(Student student, string subject);
    }

    public interface ISubjectManager
    {
        bool IsValid(string subject);
    }

    public interface IStudentManager
    {
        bool IsBanned(string student);
    }

    public class StudentService : IStudentService
    {
        readonly ISubjectManager _subjectManager;
        readonly IStudentManager _studentManager;

        public StudentService(ISubjectManager subjectManager, IStudentManager studentManager)
        {
            _subjectManager = subjectManager;
            _studentManager = studentManager;
        }

        public int RegisterSubject(Student student, string subject)
        {
            if (!_subjectManager.IsValid(subject)) return -1;
            if (_studentManager.IsBanned(student.Name)) return -2;
            return 0;
        }
    }
}

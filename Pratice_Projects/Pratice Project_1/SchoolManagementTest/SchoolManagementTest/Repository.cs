namespace SchoolManagementTest
{
    public interface IStudentRepository
    {
        Student GetById(int id);
    }

    public interface ISubjectRepository
    {
        Subject GetById(int id);
    }

    public interface ITeacherRepository
    {
        Teacher GetById(int id);
    }
    public class Repository
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;

        public Repository(IStudentRepository studentRepository, ISubjectRepository subjectRepository, ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
        }

        public Student GetStudentById(int id)
        {
            return _studentRepository.GetById(id);
        }

        public Subject GetSubjectById(int id)
        {
            return _subjectRepository.GetById(id);
        }

        public Teacher GetTeacherById(int id)
        {
            return _teacherRepository.GetById(id);
        }
    }
}

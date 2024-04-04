using Exam.Interfaces;
using Exam.Models;

namespace Exam.Services
{
    public class ExamService : IExamService
    {
        private readonly List<ExamModel> _exams;

        public ExamService()
        {
            _exams = new List<ExamModel>();
        }

        public IEnumerable<ExamModel> GetAllExams()
        {
            return _exams;
        }

        public ExamModel GetExamById(int id)
        {
            return _exams.Find(exam => exam.Id == id);
        }

        public void AddExam(ExamModel exam)
        {
            exam.Id = _exams.Count + 1;
            _exams.Add(exam);
        }

        public void UpdateExam(int id, ExamModel updatedExam)
        {
            var index = _exams.FindIndex(exam => exam.Id == id);
            if (index != -1)
            {
                _exams[index] = updatedExam;
            }
            else
            {
                throw new ArgumentException("Exam not found");
            }
        }

        public void DeleteExam(int id)
        {
            var exam = _exams.Find(exam => exam.Id == id);
            if (exam != null)
            {
                _exams.Remove(exam);
            }
            else
            {
                throw new ArgumentException("Exam not found");
            }
        }
    }
}

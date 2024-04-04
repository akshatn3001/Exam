using Exam.Models;

namespace Exam.Interfaces
{
    public interface IExamService
    {
        IEnumerable<ExamModel> GetAllExams();
        ExamModel GetExamById(int id);
        void AddExam(ExamModel exam);
        void UpdateExam(int id, ExamModel exam);
        void DeleteExam(int id);
    }
}

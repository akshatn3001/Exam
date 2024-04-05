using Exam.Interfaces;
using Exam.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam.Services
{
    public class ExamService : IExamService
    {
        private readonly ExamDbContext _context;

        public ExamService(ExamDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ExamModel> GetAllExams()
        {
            return _context.Exams.ToList();
        }

        public ExamModel GetExamById(int id)
        {
            ExamModel exam= _context.Exams.FirstOrDefault(x => x.Id == id);
            return exam;
            
        }

        public void AddExam(ExamModel exam)
        {
            
            _context.Exams.Add(exam);
            _context.SaveChanges();
        }

        public void UpdateExam(int id, ExamModel updatedExam)
        {
            
            var exam = _context.Exams.FirstOrDefault(x=>x.Id == id);
            if (exam != null)
            {
                exam.Name = updatedExam.Name;
                exam.Marks = updatedExam.Marks;
                exam.Description = updatedExam.Description;
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Exam not found");
            }
        }

        public void DeleteExam(int id)
        {
            var exam = _context.Exams.FirstOrDefault(x=>x.Id == id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }
            else
            {
                throw new ArgumentException("Exam not found");
            }
        }
    }
}

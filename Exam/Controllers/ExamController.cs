using System;
using Microsoft.AspNetCore.Mvc;
using Exam.Models;
using Exam.Services;
using Exam.Interfaces;

namespace Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService ?? throw new ArgumentNullException(nameof(examService));
        }

        [HttpGet]
        public IActionResult GetAllExams()
        {
            var exams = _examService.GetAllExams();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public IActionResult GetExamById(int id)
        {
            var exam = _examService.GetExamById(id);
            if (exam == null)
            {
                return NotFound();
            }
            return Ok(exam);
        }

        [HttpPost]
        public IActionResult AddExam([FromBody] ExamModel exam)
        {
            _examService.AddExam(exam);
            return CreatedAtAction(nameof(GetExamById), new { id = exam.Id }, exam);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExam(int id, [FromBody] ExamModel exam)
        {
            try
            {
                _examService.UpdateExam(id, exam);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExam(int id)
        {
            try
            {
                _examService.DeleteExam(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}

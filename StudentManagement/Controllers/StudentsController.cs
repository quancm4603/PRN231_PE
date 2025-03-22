using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Dtos;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            return await _context.Students
                .Include(s => s.Class)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Fullname = s.Fullname,
                    Age = s.Age,
                    Mark = s.Mark,
                    IsActive = s.IsActive,
                    ClassId = s.ClassId,
                    ClassName = s.Class.ClassName
                })
                .ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudent(int id)
        {
            var student = await _context.Students
                .Include(s => s.Class)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Fullname = s.Fullname,
                    Age = s.Age,
                    Mark = s.Mark,
                    IsActive = s.IsActive,
                    ClassId = s.ClassId,
                    ClassName = s.Class.ClassName
                })
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, UpdateStudentDto updateStudentDto)
        {
            if (id != updateStudentDto.Id)
            {
                return BadRequest();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            student.Fullname = updateStudentDto.Fullname;
            student.Age = updateStudentDto.Age;
            student.Mark = updateStudentDto.Mark;
            student.IsActive = updateStudentDto.IsActive;
            student.ClassId = updateStudentDto.ClassId;

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(CreateStudentDto createStudentDto)
        {
            var student = new Student
            {
                Fullname = createStudentDto.Fullname,
                Age = createStudentDto.Age,
                Mark = createStudentDto.Mark,
                IsActive = createStudentDto.IsActive,
                ClassId = createStudentDto.ClassId
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}

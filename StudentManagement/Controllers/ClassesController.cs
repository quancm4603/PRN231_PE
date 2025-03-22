using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Dtos;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetClasses()
        {
            var classes = await _context.Classes
                .Include(c => c.Students)
                .Select(c => new ClassDto
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    Students = c.Students.Select(s => new StudentDto
                    {
                        Id = s.Id,
                        Fullname = s.Fullname,
                        Age = s.Age,
                        Mark = s.Mark,
                        IsActive = s.IsActive
                    }).ToList()
                })
                .ToListAsync();

            return Ok(classes);
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassDto>> GetClass(int id)
        {
            var classEntity = await _context.Classes
                .Include(c => c.Students)
                .Where(c => c.ClassId == id)
                .Select(c => new ClassDto
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    Students = c.Students.Select(s => new StudentDto
                    {
                        Id = s.Id,
                        Fullname = s.Fullname,
                        Age = s.Age,
                        Mark = s.Mark,
                        IsActive = s.IsActive
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (classEntity == null)
            {
                return NotFound();
            }

            return Ok(classEntity);
        }

        // PUT: api/Classes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, UpdateClassDto updateClassDto)
        {
            var classEntity = await _context.Classes.FindAsync(id);

            if (classEntity == null)
            {
                return NotFound();
            }

            classEntity.ClassName = updateClassDto.ClassName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        // POST: api/Classes
        [HttpPost]
        public async Task<ActionResult<ClassDto>> PostClass(CreateClassDto createClassDto)
        {
            var classEntity = new Class
            {
                ClassName = createClassDto.ClassName
            };

            _context.Classes.Add(classEntity);
            await _context.SaveChangesAsync();

            var classDto = new ClassDto
            {
                ClassId = classEntity.ClassId,
                ClassName = classEntity.ClassName,
                Students = new List<StudentDto>()
            };

            return CreatedAtAction("GetClass", new { id = classDto.ClassId }, classDto);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var classEntity = await _context.Classes.FindAsync(id);
            if (classEntity == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentMgmt.Data.Interfaces;
using StudentMgmt.Models;

namespace StudentMgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await studentRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Student>> GetById(Guid id)
        {
            try
            {
                var result = await studentRepository.GetById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Add(Student student)
        {
            try
            {
                if (student == null)
                    return BadRequest();

                var createdStudent = await studentRepository.Add(student);

                return CreatedAtAction(nameof(GetById),
                    new { id = createdStudent.StudentId }, createdStudent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Student?>> Update(Guid id, Student student)
        {
            try
            {
                if (id != student.StudentId)
                    return BadRequest("Student ID mismatch");

                var studentToUpdate = await studentRepository.GetById(id);

                if (studentToUpdate == null)
                    return NotFound($"Student with Id = {id} not found");

                return await studentRepository.Update(student);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Student?>> Delete(Guid id)
        {
            try
            {
                var studentToDelete = await studentRepository.GetById(id);

                if (studentToDelete == null)
                {
                    return NotFound($"Student with Id = {id} not found");
                }

                return await studentRepository.Delete(studentToDelete);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}

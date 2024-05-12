using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StudentMgmt.Data.Interfaces;
using StudentMgmt.Models;
using System;
using System.Text;

namespace StudentMgmt.Data.Repositories
{
    public class StudentRepository: IStudentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await dbContext.Student.Include(s => s.Course).ToListAsync();
        }

        public async Task<Student?> GetById(Guid studentId)
        {
            return await dbContext.Student.Include(s=>s.Course)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);
        }

        public async Task<Student> Add(Student student)
        {
            var result = await dbContext.Student.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Student?> Update(Student student)
        {
            var result = await dbContext.Student
                .FirstOrDefaultAsync(s => s.StudentId == student.StudentId);

            if (result != null)
            {
                result.FirstName = student.FirstName;
                result.LastName = student.LastName;
                result.Email = student.Email;
                result.Mobile = student.Mobile;
                result.StudentType = student.StudentType;
                result.CourseId = student.CourseId;

                await dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Student?> Delete(Student student)
        {
                dbContext.Student.Remove(student);
                await dbContext.SaveChangesAsync();
                return null; 
        }

    }
}

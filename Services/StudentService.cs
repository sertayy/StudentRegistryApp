using Microsoft.EntityFrameworkCore;
using StudentRegistryApp.Data;
using StudentRegistryApp.Helper;
using StudentRegistryApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentRegistryApp.Services
{
    public class StudentService: IStudentService
    {
        private readonly StudentContext _context;

        public StudentService(StudentContext context)
        {
            _context = context;
        }

        public Task<List<Student>> GetAll()
        {
            return _context.students.AsNoTracking().ToListAsync();
        }

        public Student GetById(int id)
        {
            Student student = _context.students.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (student is null)
            {
                throw new KnownException($"Student not found with the given id {id}!");
            }
            return student;
        }

        public void Create(Student newStudent)
        {
            HelperFunctions.isStudentValid(newStudent, _context);
            _context.students.Add(newStudent);
            _context.SaveChanges();
        }

        public void Update(Student data)
        {
            Student studentToUpdate = GetById(data.Id);
            HelperFunctions.isStudentValid(data);
            studentToUpdate.Name = data.Name;
            studentToUpdate.Surname = data.Surname;
            studentToUpdate.Birthplace = data.Birthplace;
            studentToUpdate.Birthday = data.Birthday;
            _context.students.Update(studentToUpdate);
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            Student studentToDelete = GetById(id);
            _context.students.Remove(studentToDelete);
            _context.SaveChanges();
        }
    }
}

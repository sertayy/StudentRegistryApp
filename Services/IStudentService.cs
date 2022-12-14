using StudentRegistryApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentRegistryApp.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAll();
        Student GetById(int id);
        void Create(Student newStudent);
        void Update(Student data);
        void DeleteById(int id);
    }
}

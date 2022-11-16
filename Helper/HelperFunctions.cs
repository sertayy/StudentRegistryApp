using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using StudentRegistryApp.Data;
using StudentRegistryApp.Models;
using System.Linq;

namespace StudentRegistryApp.Helper
{
    public class HelperFunctions
    {
        public static void isStudentValid(Student student, StudentContext _context = null)
        {
            if (_context != null)
            {
                if (_context.students.FirstOrDefault(p => p.Id == student.Id) != null) 
                    throw new KnownException($"Another student exists with the same id {student.Id}");
            }
            if (student.Name.IsNullOrWhiteSpace())
            {
                throw new KnownException("Student name is not correct!");
            }
            else if (student.Surname.IsNullOrWhiteSpace())
            {
                throw new KnownException("Student surname is not correct!");
            }
            else if (student.Birthplace.IsNullOrWhiteSpace())
            {
                throw new KnownException("Student birthplace is not correct!");
            }
            else if (student.Birthday.ToString().IsNullOrWhiteSpace()) // TODO: A detailed check for the birthday field can be added as a new helper function.
            {
                throw new KnownException("Student birthday is not correct!");
            }
        }
    }
}

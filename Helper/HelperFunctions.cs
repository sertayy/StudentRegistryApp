using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using StudentRegistryApp.Models;

namespace StudentRegistryApp.Helper
{
    public class HelperFunctions
    {
        public static void isStudentValid(Student student)
        {
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

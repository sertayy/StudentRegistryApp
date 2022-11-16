using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using StudentRegistryApp.Helper;
using StudentRegistryApp.Models;
using StudentRegistryApp.Services;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace StudentRegistryApp.Controllers
{
    public class StudentController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService; // ASK: Call the student service from here but avoid to reference dbContext from the service layer??
        }

        [OpenApiOperation(operationId: "GetAllStudents", tags: new[] { "GetAllStudents" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Returning students as a response.")]
        [FunctionName("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
        HttpRequest req, ILogger logger)
        {
            try   //ASK: Try-Catch clause might be redundant in here since we are sure that the function getAll won't throw any exceptions.
            {
                logger.LogInformation("Getting all Student items.");
                List<Student> students = await _studentService.GetAll();
                return new OkObjectResult(students);
            }
            catch
            {
                logger.LogError("An error occured while getting all students.");
                throw;
            }
        }

        //[OpenApiOperation(operationId: "GetStudentById", tags: new[] { "GetStudentById" })]
        //[OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **id** parameter")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Returning student with the corresponding id.")]
        //[FunctionName("GetStudentById")]
        //public IActionResult GetStudentById(  //ASK: Should it return Task<IActionResult> by using await keyword as in the previous func?
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{id}")]
        //HttpRequest req, ILogger logger, string id)
        //{
        //    try
        //    {
        //        logger.LogInformation($"Getting student item from the database with the id {id}.");
        //        Student student = _studentService.GetById(int.Parse(id));
        //        return new OkObjectResult(student);
        //    }
        //    catch (KnownException)
        //    {
        //        logger.LogError($"An error occured while getting student with the id {id}.");
        //        throw;
        //    }
        //}


        [OpenApiOperation(operationId: "AddStudent", tags: new[] { "AddStudent" })]
        [OpenApiRequestBody("application/json", typeof(Student), Description = "JSON request body containing {name, surname, birthday, birthplace}")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Student), Description = "The student succesfully added.")]
        [FunctionName("AddStudent")]
        public async Task<IActionResult> AddStudent(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "add")]
        HttpRequest req, ILogger logger)
        {
            try
            {
                logger.LogInformation("Adding a new student item.");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Student data = JsonConvert.DeserializeObject<Student>(requestBody);
                _studentService.Create(data);
                return new OkObjectResult(new { Message = "Record Saved SuccessFully", Data = data });
            }
            catch
            {
                logger.LogError("An error occured while adding student.");
                throw;
            }
        }

        [OpenApiOperation(operationId: "UpdateStudent", tags: new[] { "UpdateStudent" })]
        [OpenApiRequestBody("application/json", typeof(Student), Description = "JSON request body containing {name, surname, birthday, birthplace}")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **id** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Updating the student information with the corresponding id.")]
        [FunctionName("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "update")]
        HttpRequest req, ILogger logger)
        {
            try
            {
                //Improvement: When the user enters the id, the information to the related student can be returned instead of the resetted values.
                logger.LogInformation($"Updating the student item.");
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Student data = JsonConvert.DeserializeObject<Student>(requestBody);
                _studentService.Update(data);
                return new OkObjectResult(new { Message = "Record Updated SuccessFully", Data = data });
            }
            catch
            {
                logger.LogError("An error occured while updating the student.");
                throw;
            }
        }


        [OpenApiOperation(operationId: "DeleteStudent", tags: new[] { "DeleteStudent" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **id** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Deleting the student information with the corresponding id.")]
        [FunctionName("DeleteStudent")]
        public IActionResult DeleteStudent(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "{id}")] HttpRequest req,
            ILogger logger, string id)
        {
            try
            {
                logger.LogInformation($"Deleting student item from the database with the id {id}.");
                _studentService.DeleteById(int.Parse(id));
                return new OkObjectResult("Record Deleted!");
            }
            catch (KnownException)
            {
                logger.LogError("An error occured while deleting the student.");
                throw;
            }
        }
    }
}
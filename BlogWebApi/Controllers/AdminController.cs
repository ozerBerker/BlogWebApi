using BlogWebApi.Contracts.Interfaces;
using BlogWebApi.Core.Models;
using BlogWebApi.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace BlogWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly ILogger<AdminController> logger;
        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            this.adminService = adminService;
            this.logger = logger;
        }

        [HttpGet("GetArticles")]
        public async Task<ActionResult> GetArticles()
        {
            try
            {
                var result = await adminService.GetUsers();
                return Ok(result);
            }
            catch (ValidationException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Validation Error", (int)HttpStatusCode.BadRequest);
                return new BadRequestObjectResult(response);
            }
            catch (FileNotFoundException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Object not found", (int)HttpStatusCode.BadRequest);
                return new NotFoundObjectResult(response);
            }
            catch (BadHttpRequestException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Bad Request", exception.StatusCode);
                return new BadRequestObjectResult(response);
            }
            catch (UnauthorizedAccessException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Only owners can create article!", (int)HttpStatusCode.Unauthorized);
                return new UnauthorizedObjectResult(response);
            }
            catch (Exception)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
                var result = await adminService.Delete(id);
                return Ok(result);
            }
            catch (ValidationException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Validation Error", (int)HttpStatusCode.BadRequest);
                return new BadRequestObjectResult(response);
            }
            catch (FileNotFoundException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Object not found", (int)HttpStatusCode.BadRequest);
                return new NotFoundObjectResult(response);
            }
            catch (BadHttpRequestException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Bad Request", exception.StatusCode);
                return new BadRequestObjectResult(response);
            }
            catch (UnauthorizedAccessException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Only owners can create article!", (int)HttpStatusCode.Unauthorized);
                return new UnauthorizedObjectResult(response);
            }
            catch (Exception)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> Read(int id)
        {
            try
            {
                var result = await adminService.Read(id);
                return Ok(result);
            }
            catch (ValidationException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Validation Error", (int)HttpStatusCode.BadRequest);
                return new BadRequestObjectResult(response);
            }
            catch (FileNotFoundException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Object not found", (int)HttpStatusCode.BadRequest);
                return new NotFoundObjectResult(response);
            }
            catch (BadHttpRequestException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Bad Request", exception.StatusCode);
                return new BadRequestObjectResult(response);
            }
            catch (UnauthorizedAccessException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Only owners can create article!", (int)HttpStatusCode.Unauthorized);
                return new UnauthorizedObjectResult(response);
            }
            catch (Exception)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost("Update")]
        public async Task<ActionResult> Update(int id, AppUserModel user)
        {
            try
            {
                var result = await adminService.Update(id, user);
                return Ok(result);
            }
            catch (ValidationException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Validation Error", (int)HttpStatusCode.BadRequest);
                return new BadRequestObjectResult(response);
            }
            catch (FileNotFoundException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Object not found", (int)HttpStatusCode.BadRequest);
                return new NotFoundObjectResult(response);
            }
            catch (BadHttpRequestException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Bad Request", exception.StatusCode);
                return new BadRequestObjectResult(response);
            }
            catch (UnauthorizedAccessException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Only owners can create article!", (int)HttpStatusCode.Unauthorized);
                return new UnauthorizedObjectResult(response);
            }
            catch (Exception)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("AddRoleToUser")]
        public async Task<ActionResult> AddRoleToUser(int id, string role)
        {
            try
            {
                var result = await adminService.AddRoleToUser(id, role);
                return Ok(result);
            }
            catch (ValidationException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Validation Error", (int)HttpStatusCode.BadRequest);
                return new BadRequestObjectResult(response);
            }
            catch (FileNotFoundException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Object not found", (int)HttpStatusCode.BadRequest);
                return new NotFoundObjectResult(response);
            }
            catch (BadHttpRequestException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Bad Request", exception.StatusCode);
                return new BadRequestObjectResult(response);
            }
            catch (UnauthorizedAccessException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Only owners can create article!", (int)HttpStatusCode.Unauthorized);
                return new UnauthorizedObjectResult(response);
            }
            catch (Exception)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("TakeOutRolesFromUser")]
        public async Task<ActionResult> TakeOutRolesFromUser(int id, IEnumerable<string> roles)
        {
            try
            {
                var result = await adminService.TakeOutRolesFromUser(id, roles);
                return Ok(result);
            }
            catch (ValidationException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Validation Error", (int)HttpStatusCode.BadRequest);
                return new BadRequestObjectResult(response);
            }
            catch (FileNotFoundException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Object not found", (int)HttpStatusCode.BadRequest);
                return new NotFoundObjectResult(response);
            }
            catch (BadHttpRequestException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Bad Request", exception.StatusCode);
                return new BadRequestObjectResult(response);
            }
            catch (UnauthorizedAccessException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Only owners can create article!", (int)HttpStatusCode.Unauthorized);
                return new UnauthorizedObjectResult(response);
            }
            catch (Exception)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}

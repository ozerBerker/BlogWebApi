using Microsoft.AspNetCore.Mvc;
using BlogWebApi.Contracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using BlogWebApi.Core.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using BlogWebApi.Domain;
using BlogWebApi.Domain.Entities;
using BlogWebApi.Services.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;

namespace BlogWebApi.Controllers
{
    //[Authorize(Roles ="Admin")]
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly ILogger<CategoryController> logger;
        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            this.categoryService = categoryService;
            this.logger = logger;
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                var result = await categoryService.GetCategories();
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
            catch (AuthenticationException exception)
            {
                var response = new ErrorResponseModel(exception.Message, "Only owners can create article!", (int)HttpStatusCode.Unauthorized);
                return new UnauthorizedObjectResult(response);
            }
            catch (Exception)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CategoryModel paramCategory)
        {
            try
            {
                var result = await categoryService.Create(paramCategory);
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
            catch (AuthenticationException exception)
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
                var result = await categoryService.Delete(id);
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
            catch (AuthenticationException exception)
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
                var result = await categoryService.Read(id);
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
            catch (AuthenticationException exception)
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
        public async Task<ActionResult> Update(int categoryId, CategoryModel paramCategory)
        {
            try
            {
                var result = await categoryService.Update(categoryId, paramCategory);
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
            catch (AuthenticationException exception)
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

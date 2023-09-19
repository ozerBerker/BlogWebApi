using BlogWebApi.Contracts.Interfaces;
using BlogWebApi.Core.DTOs;
using BlogWebApi.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;

namespace BlogWebApi.Controllers
{
    [Authorize(Roles = "Admin, Author")]
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ILogger<ArticleController> logger;

        public ArticleController(IArticleService articleService, ILogger<ArticleController> logger)
        {
            this.articleService = articleService;
            this.logger = logger;
        }

        [HttpGet("GetArticles")]
        public async Task<ActionResult> GetArticles()
        {
            try
            {
                var result = await articleService.GetArticles();
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
        public async Task<ActionResult> Create(ArticleModel paramArticle)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
                var result = await articleService.Create(paramArticle, userId);
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
                var result = await articleService.Delete(id, userId);
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
                var result = await articleService.Read(id);
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
        public async Task<ActionResult> Update(int id, ArticleModel paramArticle)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
                var result = await articleService.Update(id, paramArticle, userId);
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

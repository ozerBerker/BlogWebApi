using Azure.Core;
using BlogWebApi.Contracts.Interfaces;
using BlogWebApi.Core.DTOs;
using BlogWebApi.Core.Models;
using BlogWebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly ILogger logger;

        public AuthController(IAuthService appUserService, ILogger logger)
        {
            this.authService = appUserService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(UserSignInModel paramUser)
        {
            if (ModelState.IsValid)
            {
                var userToLogin = await authService.Login(paramUser);
                if (userToLogin !=null) return Ok(userToLogin);
            }
            return BadRequest((AuthenticationResponseDTO)null);
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(UserSignUpModel paramUser)
        {
            if (ModelState.IsValid)
            {
                var result = await authService.Register(paramUser);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            return BadRequest((AccessTokenModel)null);
        }
    }
}

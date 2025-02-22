using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper; 
using BRHomeWebApi.Dtos;
using BRHomeWebApi.Errors;
using BRHomeWebApi.Extensions;
using BRHomeWebApi.Models;
using BRHomeWebApi.Pattern.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BRHomeWebApi.Controllers
{

    public class AccountController : BaseController
    {
         private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountController(IUnitOfWork uow, 
                                 IMapper mapper,
                                 IConfiguration configuration)
        {  
            this._uow = uow;
            this._mapper = mapper;
            this._configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginReqDto loginReqDto)
        { 
            ApiError apiError = new ApiError();
            if(loginReqDto.UserName.IsEmpty() || 
                loginReqDto.Password.IsEmpty()){                
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "User name or password cannot be empty.";
                return BadRequest(apiError);
            }
           var user = await _uow.userRepository.Authenticate(loginReqDto.UserName,loginReqDto.Password);
           
           if(user ==  null)
           {
               apiError.ErrorCode = Unauthorized().StatusCode;
               apiError.ErrorDetails = "This error appears when provided username or password doesn't exists";
               apiError.ErrorMessage = "Invalid user id or password.";
               return Unauthorized(apiError);
           }
           var loginResDto = new LoginResDto()
           {
               UserName = user.UserName,
               Token = CreateToken(user)
           };
            return Ok(loginResDto);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterReqDto registerReqDto)
        { 
            ApiError apiError = new ApiError();
             if(registerReqDto.UserName.IsEmpty() || 
                registerReqDto.Password.IsEmpty()){                
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "User name or password cannot be empty.";
                return BadRequest(apiError);
            }

            if( await _uow.userRepository.IsUserExistAlready(registerReqDto.UserName)){                
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "User Already Exists,Please try something else";
                return BadRequest(apiError);
            }
                
            _uow.userRepository.Register(registerReqDto.UserName,
                                         registerReqDto.Password,
                                         registerReqDto.Email,
                                         registerReqDto.Mobile);
            await _uow.SaveAsync();

            return StatusCode(201);
        }

        private string CreateToken(User user)
        {
            // There are two types of Security Key
            // 1) Asymmetric 2) Symmetric
            // 1) Asymmetric has public & private key 
            //    It is more secure. 
            // 2) Symmetric has only one key
            //    when we are using only one server to encrypt and decyrpt 
            //    we can use Symmetric keys
            var secertKey = _configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secertKey));

            var claims = new Claim[]{
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var signCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256Signature 
            );

            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(5),
                SigningCredentials = signCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
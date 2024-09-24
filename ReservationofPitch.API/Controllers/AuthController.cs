using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnMapper;
using OnMapper.Common.Exceptions;
using Reservationpitch.Application.DTOs.UserDtos.Request;
using Reservationpitch.Application.DTOs.UserDtos.Response;
using Reservationpitch.Application.Services.Image.Interface;
using Reservationpitch.Application.Services.User.Interface;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Infustractur.Database;

namespace ReservationofPitch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IEmailSender _emailSender;
        private readonly OnMapping _mapper;
        private readonly IImageService _imageService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IUserService service, 
            IEmailSender emailSender, 
            OnMapping mapper, 
            IImageService imageService,
            ILogger<AuthController> logger
            )
        {
            _service = service;
            _emailSender = emailSender;
            _mapper = mapper;
            _imageService = imageService;
            _logger = logger;
        }



        [HttpGet]
        public async Task<Result<IEnumerable<SystemUser>>> Get()
        {
            var result = await _service.GetAllAsync();

            _logger.Log(logLevel: LogLevel.Debug, "");

            return await Result<IEnumerable<SystemUser>>.SuccessAsync(result.Data, "Get All users Successfully", true);
        }

        [HttpGet("getUser/{userId}")]
        public async Task<Result<UserResponseDto>> Get(string userId)
        {
            var result = await _service.GetByIdAsync(userId);

            return await Result<UserResponseDto>.SuccessAsync(result.Data, "User is found Successfully", true);
        }

        [HttpPost]
        [Route("login")]
        public async Task<Result<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            var response = await _service.Login(request);

            if (response.IsSuccess)
            {
                return await Result<LoginResponseDto>.SuccessAsync(response.Data, "Logged Successfully", true);
            }

            return await Result<LoginResponseDto>.FaildAsync(false, "Username or Password are incorrect");
        }

        [HttpPost]
        [Route("register")]
        public async Task<Result<UserResponseDto>> Register([FromBody] RegisterRequestDto request)
        {

            //if (request.file == null)
            //{
            //    //return BadRequest(new { message = "The file field is required." });
            //    throw new Exception();
            //}

            //var imageUrl = await _imageService.UploadImage(request, request.file);

            //request.ImageUrl = imageUrl.Data;
           
            var response = await _service.Register(request);


            if (response.IsSuccess)
            {

                var mappedUser = await _mapper.Map<UserResponseDto, SystemUser>(response.Data);
                var code = await _service.GenerateUserEmailConfirmationTokenAsync(mappedUser.Data);
                //var callbackUrl = Url.Action("ConfirmEmail", "Auth", new
                //{
                //    userid = mappedUser.Data.Id,
                //    code
                //}, protocol: HttpContext.Request.Scheme);


                ////Method for sending email to 
                //await _emailSender.SendEmailAsync(request.Email, "Confirm Email",
                //    $"Please confirm your email by clicking here : <a href='{callbackUrl}'>Link</a>");
                return await Result<UserResponseDto>.SuccessAsync(response.Data, "Account is Created Successfully", true);
            }

            return await Result<UserResponseDto>.FaildAsync(false, "Account is already in use");
        }

        [HttpPost]
        [Route("roleAssign")]
        public async Task<Result<bool>> UserRole([FromBody] UserRoleRequestDto request)
        {
            var response = await _service.AddUserToRole(request);
            if (response.IsSuccess)
            {
                return await Result<bool>.SuccessAsync(response.Data, "Role is Added Successfully", true);
            }

            return await Result<bool>.FaildAsync(false, "Role not added");
        }


        [HttpPost]
        [Route("emailConfirmation")]
        public async Task<Result<string>> ConfirmEmail(string code, string userId)
        {
            if (ModelState.IsValid)
            {
                var userEmail = await _service.FindUserByIdAsync(userId);

                if (userEmail is null)
                {
                    return await Result<string>.FaildAsync(false, "Not Found");
                }

                var result = await _service.ConfirmUserEmailAsync(userEmail.Data, code);
                if (result.IsSuccess)
                {
                    return await Result<string>.SuccessAsync("EmailConfirmed Successfully", true);
                }
            }
            return await Result<string>.FaildAsync(false, "ModelStateError");
        }


        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
        {
            var response = await _service.RefreshTokenAsync(request);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Message);
        }

        [HttpPut("updateUser/{userId}")]
        public async Task<Result<UserResponseDto>>Put(string userId, UpdateUserRequestDto user)
        {
            var response = await _service.UpdateAsync(userId, user);

            if(response.IsSuccess)
            {
                return await Result<UserResponseDto>.SuccessAsync(response.Data, "Account is Updated Successfully", true);
            }


            return await Result<UserResponseDto>.FaildAsync(false, "Account is not iupdated");
        }
    }
}

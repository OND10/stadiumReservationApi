using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using OnMapper;
using OnMapper.Common.Exceptions;
using Reservationpitch.Application.DTOs.UserDtos.Request;
using Reservationpitch.Application.DTOs.UserDtos.Response;
using Reservationpitch.Application.Services.Image.Interface;
using Reservationpitch.Application.Services.User.Interface;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Shared;
using System.IdentityModel.Tokens.Jwt;
using Reservationpitch.Application.Common.Handling;
using Microsoft.AspNetCore.Authorization;
using Reservationpitch.Infustractur.Database;
using Microsoft.EntityFrameworkCore;


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
        private readonly ApplicationDbContext _context;
        private readonly IClaimService _claimService;

        public AuthController(IUserService service,
            IEmailSender emailSender,
            OnMapping mapper,
            IImageService imageService,
            ILogger<AuthController> logger,
            ApplicationDbContext context,
            IClaimService claimService
            )
        {
            _service = service;
            _emailSender = emailSender;
            _mapper = mapper;
            _imageService = imageService;
            _logger = logger;
            _context = context;
            _claimService = claimService;
        }


        [HttpGet]    
        public async Task<Reservationpitch.Application.Common.Handling.Result<IEnumerable<SystemUser>>> Get()
        {
            try
            {
                var user = User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)?.Value;

                _logger.LogInformation($"User {user} is attempting to retrieve all user records");

                var result = await _service.GetAllAsync();

                _logger.LogInformation($"User {user} successfully retrieved all user records");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching users: {ex.Message}");
                throw;
            }
        }


        //[Authorize]
        //[HttpGet("getUser/{userId}")]
        //public async Task<Reservationpitch.Application.Common.Handling.Result<UserResponseDto>> Get(string userId)
        //{
        //    var result = await _service.GetByIdAsync(userId);

        //    return await Reservationpitch.Application.Common.Handling.Result<UserResponseDto>.SuccessAsync(result.Data, "User is found Successfully", true);
        //}

        [HttpGet("getUserRoles/{userId}")]
        public async Task<Reservationpitch.Application.Common.Handling.Result<IList<string>>> GetUserRoles(string userId)
        {
            List<string> userrolesList = [];
            var userRoles = await _context.UserRoles.Where(ur => ur.UserId == userId).ToListAsync();

            foreach (var role in userRoles)
            {
                var roles = await _context.Roles.Where(r => r.Id == role.RoleId).ToListAsync();
                foreach (var item in roles)
                {
                    userrolesList.Add(item.Name);
                }
            }

            //var userRoles = await (from ur in _context.UserRoles
            //                       join r in _context.Roles
            //                       on ur.RoleId equals r.Id
            //                       where ur.UserId == userId
            //                       select r.Name)
            //                       .ToListAsync();

            return await Reservationpitch.Application.Common.Handling.Result<IList<string>>.SuccessAsync(userrolesList, "Get All UserRoles successfully");
        }

        [HttpPost]
        [Route("login")]
        public async Task<Reservationpitch.Application.Common.Handling.Result<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            var response = await _service.Login(request);

            if (response.IsSuccess)
            {
                return await Reservationpitch.Application.Common.Handling.Result<LoginResponseDto>.SuccessAsync(response.Data, "Logged Successfully", true);
            }

            return await Reservationpitch.Application.Common.Handling.Result<LoginResponseDto>.FaildAsync(false, "Username or Password are incorrect");
        }

        [HttpPost]
        [Route("register")]
        public async Task<Reservationpitch.Application.Common.Handling.Result<UserResponseDto>> Register([FromBody] RegisterRequestDto request)
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
                return await Reservationpitch.Application.Common.Handling.Result<UserResponseDto>.SuccessAsync(response.Data, "Account is Created Successfully", true);
            }

            return await Reservationpitch.Application.Common.Handling.Result<UserResponseDto>.FaildAsync(false, "Account is already in use");
        }

        [HttpPost]
        [Route("roleAssign")]
        public async Task<Reservationpitch.Application.Common.Handling.Result<bool>> UserRole([FromBody] UserRoleRequestDto request)
        {
            var response = await _service.AddUserToRole(request);
            if (response.IsSuccess)
            {
                return await Reservationpitch.Application.Common.Handling.Result<bool>.SuccessAsync(response.Data, "Role is Added Successfully", true);
            }

            return await Reservationpitch.Application.Common.Handling.Result<bool>.FaildAsync(false, "Role not added");
        }


        [HttpPost]
        [Route("emailConfirmation")]
        public async Task<Reservationpitch.Application.Common.Handling.Result<string>> ConfirmEmail(string code, string userId)
        {
            if (ModelState.IsValid)
            {
                var userEmail = await _service.FindUserByIdAsync(userId);

                if (userEmail is null)
                {
                    return await Reservationpitch.Application.Common.Handling.Result<string>.FaildAsync(false, "Not Found");
                }

                var result = await _service.ConfirmUserEmailAsync(userEmail.Data, code);
                if (result.IsSuccess)
                {
                    return await Reservationpitch.Application.Common.Handling.Result<string>.SuccessAsync("EmailConfirmed Successfully", true);
                }
            }
            return await Reservationpitch.Application.Common.Handling.Result<string>.FaildAsync(false, "ModelStateError");
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
        public async Task<Reservationpitch.Application.Common.Handling.Result<UserResponseDto>> Put(string userId, UpdateUserRequestDto user)
        {
            var response = await _service.UpdateAsync(userId, user);

            if (response.IsSuccess)
            {
                return await Reservationpitch.Application.Common.Handling.Result<UserResponseDto>.SuccessAsync(response.Data, "Account is Updated Successfully", true);
            }


            return await Reservationpitch.Application.Common.Handling.Result<UserResponseDto>.FaildAsync(false, "Account is not iupdated");
        }

        [HttpDelete("deleteUser/{userId}")]
        public async Task<Reservationpitch.Application.Common.Handling.Result<bool>> Delete(string userId)
        {
            var response = await _service.DeleteAsync(userId);

            if (response.IsSuccess)
            {
                return await Reservationpitch.Application.Common.Handling.Result<bool>.SuccessAsync(response.Data, response.Message, true);
            }

            return await Reservationpitch.Application.Common.Handling.Result<bool>.FaildAsync(false, "User is not deleted");
        }

        [HttpPost("sendMessage")]
        public async Task<Reservationpitch.Application.Common.Handling.Result<bool>> Send([FromBody] string message)
        {
            var response = await _service.SendToAllUsersAsync(message);

            if (response.IsSuccess)
            {
                return await Reservationpitch.Application.Common.Handling.Result<bool>.SuccessAsync(true, ResponseStatus.SendedSuccess, true);
            }

            return await Reservationpitch.Application.Common.Handling.Result<bool>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpGet("getUserMessage/{userId}")]
        public async Task<Reservationpitch.Application.Common.Handling.Result<IEnumerable<string>>> GetMessage(string userId)
        {
            var response = await _service.GetUserMessagesAsync(userId);

            if (response.IsSuccess)
            {

                return await Reservationpitch.Application.Common.Handling.Result<IEnumerable<string>>.SuccessAsync(response.Data, ResponseStatus.GetSuccess, true);
            }

            return await Reservationpitch.Application.Common.Handling.Result<IEnumerable<string>>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpGet("getUserMessagesCount/{userId}")]
        public async Task<Reservationpitch.Application.Common.Handling.Result<int>> GetMessageCount(string userId)
        {
            var response = await _service.GetUserMessagesCount(userId);

            if (response.IsSuccess)
            {
                return await Reservationpitch.Application.Common.Handling.Result<int>.SuccessAsync(response.Data, ResponseStatus.GetSuccess, true);
            }

            return await Reservationpitch.Application.Common.Handling.Result<int>.FaildAsync(false, ResponseStatus.Faild);
        }

        [HttpPost("generate-claims")]
        public async Task<Reservationpitch.Application.Common.Handling.Result<bool>> GenerateClaims()
        {
            var result = await _claimService.GenerateAllClaimsAsync();
            return result;
        }

        [HttpGet("get-claims")]
        public async Task<Reservationpitch.Application.Common.Handling.Result<IEnumerable<ClaimEntity>>> GetClaims()
        {
            var result = await _claimService.GetAllClaimsAsync();
            return result;
        }

        [HttpPost("assign-claims")]
        public async Task<Reservationpitch.Application.Common.Handling.Result<bool>> AssignClaims(string userId, [FromBody] List<Guid> claimIds)
        {
            var result = await _claimService.AssignClaimsToUserAsync(userId, claimIds);
            return result;
        }

    }
}

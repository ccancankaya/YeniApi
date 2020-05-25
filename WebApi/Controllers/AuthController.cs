using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.LoggerService;

namespace WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        private IMapper _mapper;
        private ILoggerManager _logger;
        private IConfiguration _configuration;
        public AuthController(IRepositoryWrapper wrapper, IMapper mapper, ILoggerManager logger, IConfiguration configuration)
        {
            _wrapper = wrapper;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                if (await _wrapper.Auth.UserExists(userRegisterDto.Email))
                {
                    ModelState.AddModelError("Email", "User already exists");
                    _logger.LogError($"Auth controller icinde register methodunda kullanıcı zaten var hatası");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Auth controller icinde register methodunda model yanlis hatasi");
                    return BadRequest(ModelState);
                }

                var userToCreate = new User
                {
                    Name = userRegisterDto.Name,
                    Surname = userRegisterDto.Surname,
                    Email = userRegisterDto.Email,
                    Age = userRegisterDto.Age,
                    Gender = userRegisterDto.Gender,
                    Description = "açıklama",
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };

                _logger.LogInfo($"{userRegisterDto.Name} adlı kullanıcının kayıt islemi yapildi");
                var createdUser = await _wrapper.Auth.Register(userToCreate, userRegisterDto.Password);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Auth controller Register methodunda hata : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var user = await _wrapper.Auth.Login(userLoginDto.Email, userLoginDto.Password);
                if (user == null)
                {
                    _logger.LogError($"Auth controller login methodunda kullanıcı adı veya sifre hatali");
                    return Unauthorized("Email or password is incorrect");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim(ClaimTypes.Name,user.Name)
                    }),
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                _logger.LogInfo($"Auth controller kullanıcı giris yaptı");
                return Ok(tokenString);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Auth controller login methodu icinde hata : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetKitchenId(int id)
        {
            try
            {
                var kitchenId = _wrapper.Auth.CurrentKitchenId(id);

                return Ok(kitchenId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Aktif muftak ıd döndürülken bir hata meydana geldi : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("user/{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _wrapper.User.GetById(id);
                if (user == null)
                {
                    _logger.LogError($"Kullanıcı bulunamadı");
                    return NotFound("User not found");
                }

                var userResult = _mapper.Map<UserDto>(user);
                _logger.LogInfo($"İstenilen kullanıcı veritabanından döndürüldü");

                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Auth controller da userbyıd de sorun var : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody]UserUpdateDto user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("User object sent null from client");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("User model doğru dönmedi");
                    return BadRequest("Invalid model object");
                }

                var userEntity = _wrapper.User.GetById(id);

                if (userEntity == null)
                {
                    _logger.LogError($"UserId: {id} olan kullanıcı bulunamadı");
                    return NotFound("User is not exist");
                }


                _mapper.Map(user, userEntity);

                _wrapper.User.UpdateUser(userEntity);
                _wrapper.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Kullanıcı güncelleme işleminde birşeyler ters gitti : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _wrapper.User.GetById(id);
                if (user == null)
                {
                    _logger.LogError($"Id si : {id} olan bir kullanıcı bulunamadı");
                    return NotFound();
                }

                _wrapper.User.DeleteUser(user);
                _wrapper.Save();

                _logger.LogInfo("Kullanıcı silindi");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Kullanıcı silinirken bir hata meydana geldi : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
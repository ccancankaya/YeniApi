using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.LoggerService;

namespace WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        private IMapper _mapper;
        private ILoggerManager _logger;
        public UserController(IRepositoryWrapper wrapper, IMapper mapper, ILoggerManager logger)
        {
            _wrapper = wrapper;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _wrapper.User.GetAllUsers();
                var userResult = _mapper.Map<List<UserDto>>(users);
                _logger.LogInfo($"User tablosundan tüm kayıtlar çekildi {users.Count()} tane kayıt döndü.");
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"user controller GetAll methodunda hata : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDescription(int id, [FromBody]UpdateDescriptionDto updateDescriptionDto)
        {
            try
            {
                if (updateDescriptionDto == null)
                {
                    _logger.LogError("Kullanıcı nesneyi boş gönderdi");
                    return BadRequest("Model is empty");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model nesneyi doğru değil");
                    return BadRequest("Model is not valid");
                }

                var user = _wrapper.User.GetById(id);

                if (user == null)
                {
                    _logger.LogError("Kullanıcı bulunamadı");
                    return NotFound("User is not found");
                }

                _mapper.Map(updateDescriptionDto, user);

                _wrapper.User.UpdateDescription(user);
                _wrapper.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bir hata meydana geldi : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
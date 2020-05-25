using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.LoggerService;

namespace WebApi.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        private ILoggerManager _logger;
        private IMapper _mapper;

        public AddressController(IRepositoryWrapper wrapper, ILoggerManager logger, IMapper mapper)
        {
            _wrapper = wrapper;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet("{id}", Name = "AddressByUserId")]
        public IActionResult GetByUser(int id)
        {
            try
            {
                var address = _wrapper.Address.GetAddressByUser(id);
                if (address == null)
                {
                    _logger.LogError($"İstenilen adres bulunamadı");
                    return NotFound("Adres bulunamadı");
                }

                var addressResult = _mapper.Map<AddressDto>(address);
                _logger.LogInfo($"Kullanıcıya ait adresler döndürüldü");
                return Ok(addressResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Address controller da kullanıcının adresleri çekilirken hata meydana geldi : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        public IActionResult Add([FromBody] AddressCreateDto address)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Adres modeli doğrulanamadı");
                    return BadRequest("Model is not valid");
                }

                var userEntity = _mapper.Map<Address>(address);
                _wrapper.Address.CreateAddress(userEntity);
                _wrapper.Save();

                var createdAddress = _mapper.Map<AddressDto>(userEntity);

                _logger.LogInfo($"Address tablosuna 1 adet kayıt eklendi");
                return CreatedAtRoute("AddressByUserId", new { id = createdAddress.UserId }, createdAddress);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Address controllerda adres eklerken bir hata meydana geldi : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }


    }
}
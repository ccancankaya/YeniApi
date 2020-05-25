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
    [Route("api/kitchen")]
    [ApiController]
    public class KitchenController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _wrapper;
        private IMapper _mapper;

        public KitchenController(ILoggerManager logger, IRepositoryWrapper wrapper, IMapper mapper)
        {
            _logger = logger;
            _wrapper = wrapper;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddKitchen([FromBody] KitchenCreateDto kitchen)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid object");
                }

                var userEntity = _mapper.Map<Kitchen>(kitchen);
                _wrapper.Kitchen.CreateKitchen(userEntity);
                _wrapper.Save();
                var createdKitchen = _mapper.Map<KitchenDetailDto>(userEntity);
                _logger.LogInfo($"Kitchen tablosuna 1 adet kayıt eklendi");
                return CreatedAtRoute("KitchenById", new { id = createdKitchen.Id }, createdKitchen);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Kitchen controller addkitchen içinde hata oluştu : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet]
        public IActionResult GetAllKitchens([FromQuery] PageParameters pageParameters)
        {
            try
            {
                var kitchens = _wrapper.Kitchen.GetAllKitchens(pageParameters);
                if (kitchens == null)
                {
                    _logger.LogInfo($"Kitchen controller da getall methodunda mutfaklar bulunamadı hatası");
                    return NotFound("Kitchens not found");

                }

                var kitchenResult = _mapper.Map<List<KitchenDto>>(kitchens);
                _logger.LogInfo($"Bütün mutfaklar listelendi");
                return Ok(kitchenResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Kitchen controller getall methodunda hata meydana geldi {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        [HttpGet("{id}", Name = "KitchenById")]
        public IActionResult GetById(int id)
        {
            try
            {
                var kitchen = _wrapper.Kitchen.GetKitchenById(id);
                if (kitchen == null)
                {
                    _logger.LogInfo($"Kitchen controller istenilen ıd ye ait bir mutfak yok");
                    return NotFound("Kitchen not found");
                }

                var kitchenResult = _mapper.Map<KitchenDetailDto>(kitchen);
                _logger.LogInfo($"Verilen ıd ye göre mutfak veritabanından alındı");
                return Ok(kitchenResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Kitchen controller getbyıd methodunda hata meydana geldi {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetByKitchenByUser(int userId)
        {
            try
            {
                var kitchen = _wrapper.Kitchen.GetKitchenByUser(userId);

                if (kitchen==null)
                {
                    _logger.LogInfo($"İstenilen kullanıcıya ait mutfak bulunamadı");
                    return NotFound("Kitchen not found");
                }

                var kitchenResult = _mapper.Map<KitchenDetailDto>(kitchen);
                _logger.LogInfo($"Veritabanından istenilen kullanıcının mutfağı döndürüldü");
                return Ok(kitchenResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Kitchen controller getkitchenbyuser methodunda hata meydana geldi {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }










    }
}
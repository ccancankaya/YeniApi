using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.LoggerService;

namespace WebApi.Controllers
{
    [Route("api/food")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        private IMapper _mapper;
        private ILoggerManager _logger;

        public FoodController(IRepositoryWrapper wrapper, IMapper mapper, ILoggerManager logger)
        {
            _wrapper = wrapper;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PageParameters pageParameters)
        {
            try
            {
                var foods = _wrapper.Food.GetAllFoods(pageParameters);
                var foodResults = _mapper.Map<List<FoodDto>>(foods);
                _logger.LogInfo($"Food tablosundan tüm kayıtlar çekildi {foods.Count()} tane kayıt döndü.");
                return Ok(foodResults);
            }
            catch (Exception ex)
            {

                _logger.LogError($"FoodController icinde GetAll methodunda sorun var : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("latest")]
        public IActionResult GetLatest()
        {
            try
            {
                var foodId = _wrapper.Food.GetLatestFoodId();
                int id = foodId;
                //if (foodId)
                //{
                //    _logger.LogError("Yemek bulunamadı");
                //    return NotFound("Food is not found");
                //}

                //var foodResult = _mapper.Map<Food>(food);
                _logger.LogInfo("Yemek veritabanından çekildi");
                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bir hata oluştu : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("category/{id}", Name = "GetFoodByCategory")]
        public IActionResult GetByCategory(int id, [FromQuery] PageParameters pageParameters)
        {
            try
            {
                var foods = _wrapper.Food.GetFoodsByCategoryId(id, pageParameters);
                if (foods == null)
                {
                    _logger.LogInfo($"kategoriye göre yemek boş döndü");
                    return NotFound("There is no food on this category");
                }
                var foodResult = _mapper.Map<List<FoodDto>>(foods);
                _logger.LogInfo($"{id} numaralı kategoriye ait ürünler listelendi");
                return Ok(foodResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Food controller içinde kategoriye göre aramada sorun var : {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        [HttpGet("{id}", Name = "FoodById")]
        public IActionResult GetById(int id)
        {
            try
            {
                var food = _wrapper.Food.GetFoodById(id);
                if (food == null)
                {
                    return StatusCode(404, "Not found");
                }
                var foodResult = _mapper.Map<FoodDetailDto>(food);
                _logger.LogInfo($"Food tablosundan istenilen kayıt çekildi");
                return Ok(foodResult);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Food controller getbyId içinde hata oluştu : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("photos")]
        public IActionResult GetPhotosByFood(int id)
        {
            var photos = _wrapper.Photo.GetPhotosByFoodId(id);
            if (photos == null)
            {
                return StatusCode(404, "Not found");
            }
            _logger.LogInfo($"Yemegin fotograflari food controller icinde photo tablosundan cekildi");
            return Ok(photos);
        }

        [HttpPost]
        public IActionResult CreateFood([FromBody]FoodCreateDto food)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid object");
                }
                var userEntity = _mapper.Map<Food>(food);
                _wrapper.Food.CreateFood(userEntity);
                _wrapper.Save();
                var createdFood = _mapper.Map<FoodDto>(userEntity);
                _logger.LogInfo($"Food tablosuna 1 adet kayıt eklendi");
                return CreatedAtRoute("FoodById", new { id = createdFood.Id }, createdFood);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Food controller createFood içinde hata oluştu : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
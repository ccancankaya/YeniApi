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
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _wrapper;
        private IMapper _mapper;

        public CategoryController(ILoggerManager logger, IRepositoryWrapper wrapper, IMapper mapper)
        {
            _logger = logger;
            _wrapper = wrapper;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var categories = _wrapper.Category.GetAllCategories();
                var categoryResult = _mapper.Map<List<CategoryDto>>(categories);

                _logger.LogInfo($"Category controller dan tüm kategoriler listelendi");

                return Ok(categoryResult);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Category controller GetAll methodunda sorun var : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}", Name = "categoryById")]
        public IActionResult GetById(int id)
        {
            try
            {
                var category = _wrapper.Category.GetCategoryById(id);
                if (category == null)
                {
                    _logger.LogError($"Kategory bulunamadı");
                    return NotFound("Category couldnt find");
                }

                var categoryResult = _mapper.Map<CategoryDto>(category);

                _logger.LogInfo($"Id ye göre kategori çekildi");
                return Ok(categoryResult);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Category controller getById içinde sorun {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("kitchenCategory/{id}", Name = "categoryByKitchen")]
        public IActionResult GetByKitchen(int id)
        {
            try
            {
                var category = _wrapper.KitchenCategory.GetKitchenCategoriesByKitchenId(id);
                if (category == null)
                {
                    _logger.LogError($"Kategori bulunamadı");
                    return NotFound("Category couldnt find");
                }

                var categoryResult = _mapper.Map<KitchenCategoryDto>(category);

                _logger.LogInfo($"İstenilen mutfak için kategoriler çekildi");

                return Ok(categoryResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mutfak kategorileri controllerında sorun var: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        [HttpPost("kitchenCategory")]
        public IActionResult addKitchenCategory([FromBody] KitchenCategoryCreateDto kitchenCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"kitchencategory modeli hatalı");
                    return BadRequest("Invalid object");
                }

                var userEntity = _mapper.Map<KitchenCategory>(kitchenCategory);
                _wrapper.KitchenCategory.CreateKitchenCategory(userEntity);
                _wrapper.Save();
                var createdKitchenCategory = _mapper.Map<KitchenCategoryDto>(userEntity);
                _logger.LogInfo($"Mutkaf kategorileri eklendi");
                return CreatedAtRoute("kitchenCategoryById", new { id = createdKitchenCategory.Id }, createdKitchenCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mutfağa kategori eklenirken bir hata meydana geldi: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }





    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Extensions;
using WebApi.LoggerService;

namespace WebApi.Controllers
{
    [Route("api/food/{foodid}/photo")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        private IMapper _mapper;
        private ILoggerManager _logger;
        private IOptions<CloudinarySettings> _cloudinarySettings;

        private Cloudinary _cloudinary;

        public PhotoController(IRepositoryWrapper wrapper, IMapper mapper, ILoggerManager logger, IOptions<CloudinarySettings> cloudinarySettings)
        {
            _wrapper = wrapper;
            _mapper = mapper;
            _logger = logger;
            _cloudinarySettings = cloudinarySettings;
            Account account = new Account(_cloudinarySettings.Value.CloudName, _cloudinarySettings.Value.ApiKey, _cloudinarySettings.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        [HttpPost]
        public IActionResult AddPhotoForFood(int foodid, [FromForm]PhotoCreateDto photoCreateDto)
        {
            try
            {
                var food = _wrapper.Food.GetFoodById(foodid);
                if (food == null)
                {
                    return BadRequest("Couldnt find food");
                }

                //var currentUserId = 6;//int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                //var currentUserKitchenId = _wrapper.Kitchen.GetKitchenByUser(currentUserId).Id;

                //if (currentUserKitchenId != food.KitchenId)
                //{
                //    _logger.LogError($"Kullanıcının bu yemege erisimi yok");
                //    return Unauthorized();
                //}

                var file = photoCreateDto.File;

                var uploadResult = new ImageUploadResult();
                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(file.Name, stream),
                            Folder="food"
                        };

                        uploadResult = _cloudinary.Upload(uploadParams);
                    }
                }

                photoCreateDto.Url = uploadResult.Uri.ToString();
                photoCreateDto.PublicId = uploadResult.PublicId;

                var photo = _mapper.Map<Photo>(photoCreateDto);
                photo.Food = food;

                if (!food.Photos.Any(p => p.IsMain))
                {
                    photo.IsMain = true;
                }

                food.Photos.Add(photo);

                _wrapper.Save();

                var photoToReturn = _mapper.Map<PhotoDto>(photo);
                _logger.LogInfo($"Fotograf bilgileri veritabanına kaydedildi");
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Fotograf yüklenirken bir hata meydana geldi : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public IActionResult GetPhoto(int id)
        {
            try
            {
                var photoFromDb = _wrapper.Photo.GetPhotoById(id);
                var photo = _mapper.Map<PhotoDto>(photoFromDb);

                return Ok(photo);
            }
            catch (Exception ex)
            {

                _logger.LogError($"GetPhoto methodunda hata : {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }



    }
}
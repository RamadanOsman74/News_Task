using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Application.DTOs;
using News.Domain.Entities;
using News.Domain.Repositories;
using System.Text.Json;

namespace News_Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllNews()
        {
            var news = _unitOfWork.News.GetAll(query => query
                .Include(n => n.Image)
                .Include(n => n.Translations));
            return Ok(news);
        }

        [HttpGet("{id}")]
        public IActionResult GetNewsById(int id)
        {
            var news = _unitOfWork.News.Get(id, query => query
                .Include(n => n.Image)
                .Include(n => n.Translations));
            if (news == null)
                return NotFound();

            return Ok(news);
        }

        [HttpPost]
        public IActionResult AddNews([FromForm] NewsDTO newsDTO)
        {
            if (newsDTO == null)
                return BadRequest("News data cannot be null.");

            // Parse Translations JSON
            List<TranslationDTO> translations;
            try
            {
                // Handle case where TranslationsJson might be null or empty
                translations = string.IsNullOrEmpty(newsDTO.TranslationsJson)
                    ? new List<TranslationDTO>()
                    : JsonSerializer.Deserialize<List<TranslationDTO>>(newsDTO.TranslationsJson);
            }
            catch (Exception ex)
            {
                return BadRequest($"Invalid Translations JSON: {ex.Message}");
            }

            // Create the main news object
            var news = new New
            {
                Title = newsDTO.Title,
                Content = newsDTO.Content,
                CreatedDate = newsDTO.CreatedDate,
                IsFeatured = newsDTO.IsFeatured,
                Image = new List<Images>(),
                Translations = new List<NewTranslation>()
            };

            // Handle image uploads
            foreach (var imageFile in newsDTO.Image)
            {
                if (imageFile.Length > 0)
                {
                    var filePath = Path.Combine("Uploads", Guid.NewGuid().ToString() + "_" + imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    news.Image.Add(new Images { ImagePath = filePath });
                }
            }

            // Add translations
            foreach (var translation in translations)
            {
                if (!Enum.TryParse<Languages>(translation.Language, true, out var languageEnum))
                    return BadRequest($"Invalid language: {translation.Language}");

                news.Translations.Add(new NewTranslation
                {
                    Language = languageEnum,
                    Title = translation.Title,
                    Content = translation.Content
                });
            }

            // Save the news to the database
            _unitOfWork.News.Add(news);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetNewsById), new { id = news.NewId }, news);
        }



    }
}

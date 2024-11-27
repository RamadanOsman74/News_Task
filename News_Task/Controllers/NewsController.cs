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

            // Ensure the "Uploads" directory exists
            var rootPath = Directory.GetCurrentDirectory();
            var uploadPath = Path.Combine(rootPath, "Uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Handle image uploads
            foreach (var imageFile in newsDTO.Image)
            {
                if (imageFile.Length > 0)
                {
                    try
                    {
                        // Generate a unique file name
                        var fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        var filePath = Path.Combine(uploadPath, fileName);

                        // Save the file to the "Uploads" directory
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            imageFile.CopyTo(stream);
                        }

                        // Add the image path to the news object
                        news.Image.Add(new Images { ImagePath = filePath });
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error saving file {imageFile.FileName}: {ex.Message}");
                    }
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
            try
            {
                _unitOfWork.News.Add(news);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving news to the database: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetNewsById), new { id = news.NewId }, news);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNews(int id, [FromForm] NewsDTO newsDTO)
        {
            if (newsDTO == null)
                return BadRequest("News data cannot be null.");

            // Find the existing news record in the database
            var existingNews = _unitOfWork.News.Get(id, query => query
                .Include(n => n.Image)
                .Include(n => n.Translations));

            if (existingNews == null)
                return NotFound("News not found.");

            // Parse Translations JSON
            List<TranslationDTO> translations;
            try
            {
                translations = string.IsNullOrEmpty(newsDTO.TranslationsJson)
                    ? new List<TranslationDTO>()
                    : JsonSerializer.Deserialize<List<TranslationDTO>>(newsDTO.TranslationsJson);
            }
            catch (Exception ex)
            {
                return BadRequest($"Invalid Translations JSON: {ex.Message}");
            }

            // Update the main news object
            existingNews.Title = newsDTO.Title;
            existingNews.Content = newsDTO.Content;
            existingNews.CreatedDate = newsDTO.CreatedDate;
            existingNews.IsFeatured = newsDTO.IsFeatured;

            // Handle image updates (delete existing images and add new ones if necessary)
            if (newsDTO.Image != null && newsDTO.Image.Any())
            {


                // Clear current images list and add new ones
                existingNews.Image.Clear();

                var rootPath = Directory.GetCurrentDirectory();
                var uploadPath = Path.Combine(rootPath, "Uploads");

                foreach (var imageFile in newsDTO.Image)
                {
                    if (imageFile.Length > 0)
                    {
                        try
                        {
                            var fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                            var filePath = Path.Combine(uploadPath, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                imageFile.CopyTo(stream);
                            }

                            existingNews.Image.Add(new Images { ImagePath = filePath });
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(500, $"Error saving file {imageFile.FileName}: {ex.Message}");
                        }
                    }
                }
            }

            // Update translations
            foreach (var translation in translations)
            {
                if (!Enum.TryParse<Languages>(translation.Language, true, out var languageEnum))
                    return BadRequest($"Invalid language: {translation.Language}");

                // Find or create translation
                var existingTranslation = existingNews.Translations
                    .FirstOrDefault(t => t.Language == languageEnum);

                if (existingTranslation != null)
                {
                    existingTranslation.Title = translation.Title;
                    existingTranslation.Content = translation.Content;
                }
                else
                {
                    existingNews.Translations.Add(new NewTranslation
                    {
                        Language = languageEnum,
                        Title = translation.Title,
                        Content = translation.Content
                    });
                }
            }

            // Save the changes to the database
            try
            {
                _unitOfWork.News.Update(existingNews);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating news in the database: {ex.Message}");
            }

            return Ok(existingNews);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           
            var newsItem = _unitOfWork.News.Get(id);

            
            if (newsItem == null)
            {
                return NotFound(); 
            }

            try
            {
                _unitOfWork.News.Remove(newsItem);
                _unitOfWork.Complete(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting news item: {ex.Message}");
            }

            return NoContent();
        }



    }
}

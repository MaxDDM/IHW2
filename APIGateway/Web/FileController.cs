using APIGateway.Communication;
using APIGateway.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Web
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        public static MyHttpClient client;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            FileText file;

            try
            {
                file = await client.GetFile(id);
            } catch (HttpRequestException)
            {
                return Ok("Не удалось отправить запрос на получение файла");
            }

            if (file == null)
            {
                return NotFound("Нет файла с таким Id");
            }

            return Ok(file);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(FileText file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string uniqueness;
            try
            {
                uniqueness = await client.UniquenessCheck(file.Text);
            } catch (HttpRequestException)
            {
                return Ok("Не удалось отправить запрос проверки на плагиат");
            }
            if (uniqueness == "not_unique")
            {
                return Ok("Файл не является уникальным");
            }

            string id;
            try
            {
                id = await client.AddFile(file);
            } catch (HttpRequestException)
            {
                return Ok("Не удалось отправить запрос на добавление файла");
            }
            return Ok("Файл добавлен c id = " + id);
        }
    }
}

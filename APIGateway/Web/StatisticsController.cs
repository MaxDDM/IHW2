using APIGateway.Communication;
using APIGateway.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Web
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : Controller
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

            int symbolsCount = file.Text.Length;
            int wordsCount = file.Text.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
            int paragraphCount = file.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;

            return Ok("Количество символов: " + symbolsCount + ", количество слов: " +
                wordsCount + ", количество абзацев: " + paragraphCount);
        }
    }
}

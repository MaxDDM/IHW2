using FileAnalisysService.Communication;
using FileAnalisysService.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileAnalisysService.Web
{
    [ApiController]
    [Route("[controller]")]
    public class AnaliticsController : Controller
    {
        public static MyHttpClient client;

        [HttpGet]
        public async Task<IActionResult> GetAsync(string text)
        {
            List<FileText> files;
            try
            {
                files = await client.GetFiles();
            } catch(HttpRequestException)
            {
                return Ok("Не удалось отправить запрос на получение файлов");
            }

            for (int i = 0; i < files.Count; ++i)
            {
                if (text == files[i].Text)
                {
                    return Ok("not_unique");
                }
            }

            return Ok("unique");
        }
    }
}

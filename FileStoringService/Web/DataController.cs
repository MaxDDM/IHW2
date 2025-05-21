using FileStoringService.Infrastructure;
using FileStoringService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace FileStoringService.Web
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : Controller
    {
        public static FilesDBRepository repo;

        [HttpGet]
        public IActionResult Get()
        {

            List<FileText> files;

            try
            {
                files = repo.GetFiles();
            } catch (SocketException)
            {
                return Ok("Не удалось подключиться к базе данных");
            }

            if (files == null)
            {
                return NotFound("В системе нет файлов");
            }

            return Ok(files);
        }
    }
}

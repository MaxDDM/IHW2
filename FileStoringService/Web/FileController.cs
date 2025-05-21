using FileStoringService.Infrastructure;
using FileStoringService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace FileStoringService.Web
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        public static FilesDBRepository repo;

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            FileText file;
            try
            {
                file = repo.GetFile(id);
            } catch (SocketException)
            {
                return Ok("Не удалось подключиться к базе данных");
            }

            return Ok(file);
        }

        [HttpPost]
        public IActionResult Post(FileText file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                List<FileText> files = repo.GetFiles();
                file.Id = files.Count + 1;

                repo.AddFile(file);
            } catch(SocketException)
            {
                return Ok("Не удалось подключиться к базе данных");
            }
            return Ok(file.Id);
        }
    }
}

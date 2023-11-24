using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace RestAPI.Controllers;

[ApiController]
[Route("api/v1/health")]
public class Health : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        try
        {
            var stringConnection = "Server=localhost;Database=project;Uid=root;Pwd=12345678";

            using MySqlConnection connector = new(stringConnection);

            connector.Open();

            var command = connector.CreateCommand();

            command.CommandText = "SELECT COUNT(id) AS NumberOfProducts FROM teste;";

            var result = (long)command.ExecuteScalar();

            Console.WriteLine(result);

            connector.Close();

            return Ok("Api está no ar !");
        }
        catch (Exception )
        {
            return BadRequest("Erro de conexão !");
        }
    }
}
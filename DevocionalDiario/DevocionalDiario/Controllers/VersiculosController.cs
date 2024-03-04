using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DevocionalDiario.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class VersiculosController: ControllerBase
    {
        private readonly ILogger<VersiculosController> _logger;
        private const string StringBD = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MeuVersiculo;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public VersiculosController(ILogger<VersiculosController> logger) { _logger = logger; }

        [HttpGet(Name = "GetVersiculo")]
        public IEnumerable<Versiculos> GetVersiculo()
        {
            List<Versiculos> versiculos = new List<Versiculos>();

            using (SqlConnection connection = new SqlConnection(StringBD))
            {
                string query = "Select Id, Livro, Capitulo, Versiculo, Texto from Versiculos";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Versiculos pesquisa = new Versiculos
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Livro = reader["Livro"].ToString(),
                        Capitulo = Convert.ToInt32(reader["Capitulo"]),
                        Versiculo = Convert.ToInt32(reader["Versiculo"]),
                        Texto = reader["Texto"].ToString()
                    };

                    versiculos.Add(pesquisa);
                }

                reader.Close();
            }

            return versiculos;
        }

        [HttpGet("Random", Name = "GetRandomVersiculo")]
        public IActionResult GetRandomVersiculo()
        {
            using (SqlConnection connection = new SqlConnection(StringBD))
            {
                string query = "SELECT TOP 1 Id, Livro, Capitulo, Versiculo, Texto FROM Versiculos ORDER BY NEWID()";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Versiculos randomVersiculo = new Versiculos
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Livro = reader["Livro"].ToString(),
                        Capitulo = Convert.ToInt32(reader["Capitulo"]),
                        Versiculo = Convert.ToInt32(reader["Versiculo"]),
                        Texto = reader["Texto"].ToString()
                    };

                    reader.Close();
                    return Ok(randomVersiculo);
                }
                else
                {
                    reader.Close();
                    return NotFound();
                }
            }
        }
    }
}
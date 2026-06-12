using Microsoft.Data.SqlClient;

namespace NegocioWeb.Data;

public class Conexion
{
    private readonly string _connectionString;

    public Conexion(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConexionNegocio") ?? "";
    }

    public SqlConnection CrearConexion()
    {
        return new SqlConnection(_connectionString);
    }
}

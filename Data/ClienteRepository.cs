using Microsoft.Data.SqlClient;
using NegocioWeb.Models;

namespace NegocioWeb.Data;

public class ClienteRepository
{
    private readonly Conexion _conexion;

    public ClienteRepository(Conexion conexion)
    {
        _conexion = conexion;
    }

    public List<Cliente> Listar()
    {
        var clientes = new List<Cliente>();

        using var connection = _conexion.CrearConexion();
        connection.Open();

        var sql = "SELECT IdCliente, Nombre, Documento, Telefono, Email, Activo FROM Clientes WHERE Activo = 1";
        using var command = new SqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            clientes.Add(new Cliente
            {
                Id = Convert.ToInt32(reader["IdCliente"]),
                Nombre = reader["Nombre"] != DBNull.Value ? reader["Nombre"].ToString() ?? "" : "",
                Documento = reader["Documento"] != DBNull.Value ? reader["Documento"].ToString() ?? "" : "",
                Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() ?? "" : "",
                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() ?? "" : "",
                Activo = reader["Activo"] != DBNull.Value && Convert.ToBoolean(reader["Activo"])
            });
        }

        return clientes;
    }
}

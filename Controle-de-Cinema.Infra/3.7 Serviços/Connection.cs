using Controle_de_Cinema.Dominio.ModuloEmpresa;
using Controle_de_Cinema.Infra.Compartilhado;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Controle_de_Cinema.Infra.Servicos;

public class ConexaoBancoDeDados
{

    private static ConexaoBancoDeDados _instance;
    private static readonly object _lock = new object(); // entendo o uso, mas não entendo a nomenclatura...
    public SqlConnection Connection { get; set; }
    private ConexaoBancoDeDados()
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Max Pool Size=100;";

        string dbName = "ClienteDb";

        if (!DatabaseExists(connectionString, dbName))
        {
            CreateDatabase(connectionString, dbName);
        }

        Connection = new SqlConnection($"{connectionString};Initial Catalog={dbName};");
        Connection.Open();

        using (var context = new ClienteDbContext(new DbContextOptionsBuilder<ClienteDbContext>()
            .UseSqlServer(Connection)
            .Options))
        {
            context.ApplyMigrations();
        }
    }

    private void CreateDatabase(string connectionString, string databaseName)
    {
        string createDbCommand = $"CREATE DATABASE {databaseName}";

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(createDbCommand, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    private bool DatabaseExists(string connectionString, string databaseName)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand($"SELECT db_id('{databaseName}')", connection))
            {
                return (command.ExecuteScalar() != DBNull.Value);
            }
        }
    }

    public static ConexaoBancoDeDados Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ConexaoBancoDeDados();
                }
                return _instance;
            }
        }
    }
}

using Microsoft.Data.SqlClient;

namespace Controle_de_Cinema.Infra.Servicos;

public class ConexaoBancoDeDados
{

    private static ConexaoBancoDeDados _instance;
    private static readonly object _lock = new object(); // entendo o uso, mas não entendo a nomenclatura...
    public SqlConnection Connection { get; set; }

    private ConexaoBancoDeDados()
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SeuBancoDeDados;Integrated Security=True;";

        Connection = new SqlConnection(connectionString);

        Connection.Open();
    }

    public static ConexaoBancoDeDados instance
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

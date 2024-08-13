//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using Controle_de_Cinema.WebApp.Models;
//using Controle_de_Cinema.WebApp.Extensions;
//using Controle_de_Cinema.Infra.Compartilhado;
//using Controle_de_Cinema.Infra.ModuloEmpresa;
//using Controle_de_Cinema.Dominio.ModuloEmpresa;
//using Microsoft.EntityFrameworkCore;

//namespace Controle_de_Cinema.WebApp.Controllers;

//public class LoginController : Controller
//{
//    readonly ClienteDbContext _clienteDb;
//    readonly IRepositorioEmpresa _dbConnection;
//    private readonly DbContextOptions<ClienteDbContext> _dbContextOptions;

//    public LoginController(IRepositorioEmpresa dbConnection,
//        ClienteDbContext clienteDb,
//        DbContextOptions<ClienteDbContext> dbContextOptions)
//    {
//        _clienteDb = clienteDb;
//        _dbConnection = dbConnection;
//        _dbContextOptions = dbContextOptions;
//    }

//    public ViewResult login()
//    {
//        return View();
//    }

//    [HttpPost]
//    public ViewResult login(string Usuario, string Senha, LoginViewModel loginVM)
//    {
//        var options = new DbContextOptionsBuilder<ClienteDbContext>()
//    .UseSqlServer($"Server=(localdb)\\MSSQLLocalDB;Database={Usuario};Trusted_Connection=True;MultipleActiveResultSets=true")
//    .Options;

//        var db = new ClienteDbContext(options);
//        var repositorio = new RepositorioEmpresa(db);

//        var empresas = repositorio.SelecionarTodos();

//        var empresa = empresas.FirstOrDefault(e => e.Login == Usuario && e.Senha == Senha);

//        if (empresa != null)
//        {
//            loginVM.Saudacao = $"Olá, {empresa.NomeDaEmpresa}";

//            var dbContextOptions = new DbContextOptionsBuilder<CinemaDbContext>()
//                .UseSqlServer($"Server=(localdb)\\MSSQLLocalDB;Database={empresa.NomeDaEmpresa};Trusted_Connection=True;MultipleActiveResultSets=true")
//                .Options;

//            var mensagem = new MensagemViewModel
//            {
//                Mensagem = $"Seja bem-vindo {empresa.NomeDaEmpresa}!",
//                Controlador = "/Login",
//                Link = "/gerenciar"
//            };


//            return View("Logando", mensagem);
//        }
//        else
//        {
//            return View("Login", loginVM);
//        }
//    }

//    public ViewResult Registrar()
//    {
//        return View();
//    }

//    [HttpPost]
//    public IActionResult Registrar(RegistrarViewModel registrar)
//    {
//        var db = new ClienteDbContext(_dbContextOptions);
//        var repositorio = new RepositorioEmpresa(db);

//        var novaEmpresa = new Empresa(
//            registrar.Usuario,
//            registrar.Senha,
//            registrar.Email,
//            registrar.NomeEmpresa
//            );

//        CreateDatabase(novaEmpresa.Login);

//        repositorio.Cadastrar(novaEmpresa);


//        TempData.SerializarMensagemViewModel(new MensagemViewModel
//        {
//            Titulo = "Sucesso",
//            Mensagem = $"O registro ID [{novaEmpresa.Id}] foi inserido com sucesso!",
//            Controlador = "/login",
//            Link = "/login"
//        });


//        return RedirectToAction(nameof(login));
//    }

//    public IActionResult CreateDatabase(string Empresa)
//    {
//        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;";

//        string createDatabaseCommand = $"CREATE DATABASE {Empresa}";

//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            try
//            {
//                connection.Open();

//                using (SqlCommand command = new SqlCommand(createDatabaseCommand, connection))
//                {
//                    command.ExecuteNonQuery();


//                    using(var context = new CinemaDbContext())
//                    {
//                        context.Database.Migrate();
//                    }
//                }
//            }
//            catch (SqlException ex)
//            {
//                // Trata exceções relacionadas ao SQL
//                ViewBag.Message = $"Erro ao criar o banco de dados: {ex.Message}";
//                return View();
//            }
//        }

//        ViewBag.Message = "Banco de dados criado com sucesso!";
//        return View();
//    }





//public ViewResult contratar()
//    {
//        return View();
//    }
//    public ViewResult demitir()
//    {
//        return View();
//    }
//    public ViewResult gerenciar(LoginViewModel logado)
//    {

//        return View();
//    }

//    [HttpPost]
//    public ViewResult inserir(InserirFuncionarioViewModel novoFuncionarioVM)
//    {

//        return View();
//    }

//    public IActionResult Logout()
//    {
//        var db = new ClienteDbContext(_dbContextOptions);
//        db.Database.CloseConnection(); 

//        HttpContext.Session.Clear();
//        return RedirectToAction("Login");
//    }
//}

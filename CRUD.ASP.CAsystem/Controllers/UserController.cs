using CRUD.ASP.CAsystem.Data.Interface;
using CRUD.ASP.CAsystem.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Relational;
using Microsoft.AspNetCore.Http;

namespace CRUD.ASP.CAsystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _IUser;
        private readonly ITabla _ITabla;
        private readonly IListAsistencia _IListAsistencia;

        public UserController(IUser userInterface, ITabla TableInterface, IListAsistencia ListAsistenciaInterface)
        {
            _IUser = userInterface ?? throw new ArgumentNullException(nameof(userInterface));
            _ITabla = TableInterface ?? throw new ArgumentNullException(nameof(userInterface));
            _IListAsistencia = ListAsistenciaInterface ?? throw new ArgumentNullException(nameof(ListAsistenciaInterface));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult ErrorViewUser()
        {
            return View();
        }

        public IActionResult AñadirAsistencia()
        {
            return View();
        }

        public IActionResult InterfaceIndexUser()
        {
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
            {
                return RedirectToAction("Login"); 
            }

            var tablas = _ITabla.ObtenerTablas(idUsuario.Value); 
            return View(tablas); 
        }


        public IActionResult CrearTabla()
        {
            return View();
        }

        [HttpPost]
        // Acción para crear una tabla
        public IActionResult CrearTabla(Tabla tabla)
        {
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (ModelState.IsValid)
            {
                _ITabla.CrearTabla(tabla, idUsuario);
                return RedirectToAction("InterfaceIndexUser");
            }
            return View(tabla);
        }

        // Acción para eliminar una tabla
        public IActionResult EliminarTabla(int id)
        {
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (idUsuario == null)
            {
                return RedirectToAction("Login"); 
            }

            _ITabla.EliminarTabla(id, idUsuario);
            return RedirectToAction("InterfaceIndexUser");
        }


        /*Logica negocio Login*/
        [HttpPost]
        public IActionResult Login(string Usuario, string contraseña)
        {
            var VerificacionUsuario = _IUser.Login(Usuario, contraseña);

            if (VerificacionUsuario)
            {
                int idUsuario = _IUser.ObtenerIdUsuario(Usuario);
                HttpContext.Session.SetInt32("IdUsuario", idUsuario);

                return RedirectToAction("InterfaceIndexUser");
            }
            else
            {
                return View();
            }
        }

        /*Logica negocio Registro*/
        [HttpPost]
        public IActionResult Registro(string Usuario, string correo, string contraseña)
        {
            User usuario = new User()
            {
                Iduser = 0,
                Usuario = Usuario,
                correo = correo,
                contraseña = contraseña
            };

            _IUser.Insertar(usuario);
            return RedirectToAction("Login");
        }

        /*------------------------------------------------------------------------------------*/
        /*------------------------------------------------------------------------------------*/

        /*Listado de Asitencias*/

        public IActionResult ListadoAsistencia(int idTabla, string nombreTabla)
        {
            var asistencias = _IListAsistencia.ObtenerAsistencias(idTabla, nombreTabla);
            ViewBag.IdTabla = idTabla; 
            ViewBag.NombreTabla = nombreTabla;
            return View(asistencias);
        }        //okey

        [HttpGet]
        public IActionResult AñadirAsistencia(int idTabla, string nombreTabla)
        {
            var asistente = new Asistente();
            ViewBag.IdTabla = idTabla;
            ViewBag.NombreTabla = nombreTabla;
            return View(asistente);
        }       //okey

        [HttpPost]
        public IActionResult AñadirAsistencia(Asistente asistente, int idTabla, string nombreTabla)
        {
            if (ModelState.IsValid)
            {
                _IListAsistencia.CrearAsistencia(asistente, idTabla);
                return RedirectToAction("ListadoAsistencia", new { idTabla = idTabla, nombreTabla = nombreTabla });
            }
            ViewBag.IdTabla = idTabla;
            ViewBag.NombreTabla = nombreTabla;
            return View(asistente);
        }       //okey


        [HttpGet]
        public IActionResult EliminarAsistencia(int id)
        {
            _IListAsistencia.EliminarAsistencia(id);
            return RedirectToAction("InterfaceIndexUser");
        }

        public IActionResult ErrorViewAsistencia()
        {
            return View();
        }
    }
}

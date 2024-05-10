using Microsoft.AspNetCore.Mvc;
using SAHMV8.AccesoDatos.Repositorio.IRepositorio;
using SAHMV8.Modelos;

namespace SAHMV8.Areas.Admin.Controllers
{
    [Area("Admin")] //  muy importante esta palabra clave cuando trabajamos con areas.
    public class DepositoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public DepositoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult>Upsert(int? id)
        {
            Deposito deposito = new Deposito();
            if (id == null)
            {
                //nuevo deposito
                return View(deposito);
            }
            
                //actualizar el deposito
                deposito = await _unidadTrabajo.Deposito.Obtener(id.GetValueOrDefault());
            if (deposito== null)
            {
                return NotFound();
            }
            return View(deposito);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Deposito.ObtenerTodos();
            return Json(new { data = todos }); // el nombre data es el que usaremos en javascript para referencial al conjunto de datos que devuelve el metodo
        }
        #endregion
    }
}

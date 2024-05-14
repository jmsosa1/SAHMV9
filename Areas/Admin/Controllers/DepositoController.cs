using Microsoft.AspNetCore.Mvc;
using SAHMV8.AccesoDatos.Repositorio.IRepositorio;
using SAHMV8.Modelos;
using SAHMV8.Utilidades;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upsert(Deposito deposito)
        {
            if (ModelState.IsValid)
            {
                if (deposito.Iddeposito == 0)
                {
                    await _unidadTrabajo.Deposito.Agregar(deposito);
                    TempData[DS.Exitosa] = "Depósito creado exitosamente";
                }
                else
                {
                    _unidadTrabajo.Deposito.Actualizar(deposito);
                    TempData[DS.Exitosa] = "Depósito creado exitosamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar el deposito";
            return View(deposito);
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Deposito.ObtenerTodos();
            return Json(new { data = todos }); // el nombre data es el que usaremos en javascript para referencial al conjunto de datos que devuelve el metodo
        }

        [HttpPost]
        public async  Task<IActionResult>Delete (int id)
        {
            var dep = await _unidadTrabajo.Deposito.Obtener(id);
            if (dep.Iddeposito == null)
            {
                return Json(new {success  = false,message = "Error al borrar la bodega"});
            }
            _unidadTrabajo.Deposito.Remover(dep);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Bodega borrada exitosamente" });
        }
        #endregion
    }
}

using BL.Service;
using CalculosMVC.Models;
using DAL.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Diagnostics;

namespace CalculosMVC.Controllers
{
    public class CalculoController : Controller
    {
        private readonly ICalculo _calculoService;
        public CalculoController(ICalculo calculoService)
        {
            _calculoService = calculoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Registro registro)
        {
            if(ModelState.IsValid)
            {
                var nombre = registro.Usuario.TrimStart().TrimEnd().ToLowerInvariant();
                if(nombre == "juliana" || nombre == "pedro" || nombre == "juan" || nombre == "lina")
                {
                    try
                    {
                        var resultado = _calculoService.AddCalculo(registro.Usuario, registro.Limite);
                        ViewBag.Resultado = resultado;
                        return View();
                    }
                    catch(Exception ex) 
                    {
                        return RedirectToAction("Error", new {StatusCode = 400, Message = ex.Message, Path ="Index"});
                    }
                }
                else
                {
                    ViewData["Error"] = "El usuario no está autorizado";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Error", new { StatusCode = 400, Message = "El modelo no es válido", Path = "Index" });
            }
        }

        public IActionResult Delete(int id)
        {
            var calculo = _calculoService.GetCalculo(id);
            if (calculo == null)
            {
                return RedirectToAction("Error", new {StatusCode = 404, Message = "El cálculo no existe" , Path = "Delete"});
            }
            var fechaActual = DateTime.Now;
            var fechaAnterior = (DateTime)calculo.Fecha;
            int numDias = (int)fechaActual.Subtract(fechaAnterior).TotalDays;
            if (numDias >= 30)
            {
                return View(calculo);
            }
            else
            {
                TempData["Error"] = "No puedes borrar el registro porque tiene menos de 30 dias de creado";
                return RedirectToAction("GetRegistros");
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            var calculo = _calculoService.GetCalculo(id);
            if (calculo == null)
                return RedirectToAction("Error", new { StatusCode = 404, Message = "No existe el cálculo", Path = "Delete" });
            var fechaActual = DateTime.Now;
            var fechaAnterior = (DateTime)calculo.Fecha;
            int numDias = (int)fechaActual.Subtract(fechaAnterior).TotalDays;
            if(numDias >= 30)
            {
                try
                {
                    _calculoService.DeleteCalculo(id);
                    return RedirectToAction("GetRegistros");
                }
                catch(Exception ex)
                {
                    return RedirectToAction("Error", new { StatusCode = 400, Message = ex.Message, Path = "Delete" });
                }
            }
            else
            {
                TempData["Error"] = "No puedes borrar el registro porque tiene menos de 30 dias de creado";
                return RedirectToAction("GetRegistros");
            }
        }

        public IActionResult GetRegistros() 
        {
            var calculos = _calculoService.GetCalculos();
            if(calculos == null)
            {
                return RedirectToAction("Error", new { StatusCode = 404, Message = "No existen cálculos", Path = "GetRegistros" });
            }
            InformationViewModel viewModel = new InformationViewModel();
            viewModel.Calculos = calculos;
            return View(viewModel); 
        }

        [HttpPost]
        public IActionResult GetRegistros(Filtro filtro)
        {
            if (ModelState.IsValid)
            {
                if (filtro.RespuestaMax < filtro.RespuestaMin) 
                {
                    ViewBag.Message = "El valor 'Respuesta Máxima' no debe ser inferior o igual al valor de 'Respuesta Mínima'";
                    var viewModel2 = new InformationViewModel();
                    return View(viewModel2);
                }
                var calculos = _calculoService.GetCalculos(filtro.Usuario, filtro.RespuestaMax.GetValueOrDefault(), filtro.RespuestaMin.GetValueOrDefault());
                if (calculos == null)
                {
                    ViewBag.Message = "Los calculos no existen";
                }
                if (calculos.Count() < 1)
                {
                    ViewBag.Message = "No hay resultados con los criterios de busqueda";
                }    
                InformationViewModel viewModel = new InformationViewModel();
                viewModel.Filtro = filtro;
                viewModel.Calculos = calculos;
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Error", new {StatusCode = 400, Message = "El modelo no es válido", Path = "GetRegistros"});
            }
        }

        public IActionResult Error(int statusCode, string message, string path)
        {
            var errorViewModel = new ErrorViewModel(statusCode, message, path);
            return View(errorViewModel);
        }
    }
}

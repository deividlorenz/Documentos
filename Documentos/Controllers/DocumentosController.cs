using Documentos.DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Documentos.Controllers
{
    public class DocumentosController : Controller
    {



        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public JsonResult Add(string code, string title, string revision, DateTime plannedDate, decimal value)
        {
            var repository = new Repository();

            String[] newKey = new string[] { "id", "" };
            String[] newCampos = new string[] { "code", "title", "rev", "planned_date", "value" };
            String[] newValues = new string[] { code, title, revision, plannedDate.ToString("yyyy-MM-dd HH:mm:ss"), value.ToString() };
            repository.SaveDados(newCampos, newValues, newKey, "documento", "add");
            return Json(new { success = true });
        }


        [HttpPost]
        public JsonResult Edit(int id, string code, string title, string revision, DateTime plannedDate, decimal value)
        {
            var repository = new Repository();

            String[] newKey = new string[] { "id", id.ToString() };
            String[] newCampos = new string[] { "code", "title", "rev", "planned_date", "value" };
            String[] newValues = new string[] { code, title, revision, plannedDate.ToString("yyyy-MM-dd HH:mm:ss"), value.ToString() };
            repository.SaveDados(newCampos, newValues, newKey, "documento", "edit");
            return Json(new { success = true });

        }


        [HttpPost]
        public JsonResult Get(int id)
        {
            var repository = new Repository();          
                      

            return Json( new { success = true, documento = repository.Get(id) });

        }



        [HttpPost]
        public JsonResult Del(int id, string code, string title, string revision, DateTime plannedDate, decimal value)
        {
            var repository = new Repository();

            String[] newKey = new string[] { "id", id.ToString() };
            String[] newCampos = new string[] { "code", "title", "rev", "planned_date", "value" };
            String[] newValues = new string[] { code, title, revision, plannedDate.ToString("yyyy-MM-dd HH:mm:ss"), value.ToString() };
            repository.SaveDados(newCampos, newValues, newKey, "documento", "del");
            return Json(new { success = true });

        }



        [HttpPost]
        public JsonResult List()
        {
            var repository = new Repository();      
                       

            return Json(repository.LoadDados());


        }

    }
}
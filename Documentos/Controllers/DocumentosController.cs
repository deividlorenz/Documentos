using Documentos.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Documentos.Controllers
{
    public class DocumentosController : Controller
    {

        public class PropertiesData
        {
            public string CreationTime { get; set; }
            public string Extension { get; set; }
            public int Length { get; set; }
        }

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public JsonResult Add(string code, string title, string revision, DateTime plannedDate, decimal value, string FileName)
        {
            var repository = new Repository();

            String[] newKey = new string[] { "id", "" };
            String[] newCampos = new string[] { "code", "title", "rev", "planned_date", "value", "filename" };
            String[] newValues = new string[] { code, title, revision, plannedDate.ToString("yyyy-MM-dd HH:mm:ss"), value.ToString(), "-" };
            repository.SaveDados(newCampos, newValues, newKey, "documento", "add");
            return Json(new { success = true });
        }


        [HttpPost]
        public JsonResult Edit(int id, string code, string title, string revision, DateTime plannedDate, decimal value, string FileName)
        {
            var repository = new Repository();

            String[] newKey = new string[] { "id", id.ToString() };
            String[] newCampos = new string[] { "code", "title", "rev", "planned_date", "value", "filename" };
            String[] newValues = new string[] { code, title, revision, plannedDate.ToString("yyyy-MM-dd HH:mm:ss"), value.ToString(), FileName };
            repository.SaveDados(newCampos, newValues, newKey, "documento", "edit");
            return Json(new { success = true });

        }


        [HttpPost]
        public JsonResult Get(int id)
        {
            var repository = new Repository();
            return Json(new { success = true, documento = repository.Get(id) });

        }



        [HttpPost]
        public JsonResult Del(int id)
        {
            string uploadPath = @"C:\Users\Deivid\source\repos\Documentos\Documentos\Anexos\";

            var repository = new Repository();
            var Documento = repository.Get(id);


            try
            {
                String[] newKey = new string[] { "id", id.ToString() };
                String[] newCampos = new string[] { };
                String[] newValues = new string[] { };
                repository.SaveDados(newCampos, newValues, newKey, "documento", "del");

                foreach (var file in Directory.GetFiles(uploadPath))
                {
                    if (file.Contains(id.ToString()))

                    {
                        try
                        {
                            System.IO.File.Delete(file);
                        }
                        catch
                        {
                            return Json(new { success = false });
                        }
                    }
                }
            }
            catch
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }


        [HttpPost]
        public JsonResult List()
        {
            var repository = new Repository();
            return Json(repository.LoadDados());

        }

        [HttpPost]
        public JsonResult ImportFile()
        {
            var documentId = Request["documentId"];
            try
            {
                var repository = new Repository();
                var Documento = repository.Get(Convert.ToInt32(documentId));
                string uploadPath = @"C:\Users\Deivid\source\repos\Documentos\Documentos\Anexos\";


                foreach (var file in Directory.GetFiles(uploadPath))
                {
                    if (file.Contains(documentId))

                    {
                        try
                        {
                            System.IO.File.Delete(file);
                        }
                        catch
                        {
                            return Json(new { resp = "falhousub" });
                        }
                    }

                }

                foreach (string arquivo in Request.Files)
                {
                    try

                    {
                        HttpPostedFileBase file = Request.Files[arquivo] as HttpPostedFileBase;

                        if (file.ContentLength > 0)
                        {

                            string NomeOriginal = Path.GetFileName(file.FileName.ToString());
                            string NomeBanco = Path.GetFileNameWithoutExtension(file.FileName.ToString());
                            string ExtensaoArquivo = Path.GetExtension(file.FileName.ToString());
                            string NovoNome = documentId + ExtensaoArquivo;
                            string FullFile = uploadPath + NovoNome;

                            string Countpath = Path.Combine(uploadPath, NomeOriginal);

                            if (Countpath.Count() <= 255)
                            {
                                //salva o arquivo na pasta
                                file.SaveAs(Path.Combine(uploadPath, NovoNome));
                                //salva o nome original no banco

                                String[] newKey = new string[] { "id", documentId.ToString() };
                                String[] newCampos = new string[] { "filename" };
                                String[] newValues = new string[] { NomeBanco };
                                repository.SaveDados(newCampos, newValues, newKey, "documento", "edit");
                                return Json(new { resp = "sucesso" });
                            }
                            else
                            {
                                return Json(new { resp = "nomelongo" });
                            }
                        }

                        else
                        {
                            return Json(new { resp = "vazio" });
                        }
                    }
                    catch
                    {
                        return Json(new { resp = "falhou" });
                    }
                }
            }

            catch
            {
                return Json(new { resp = "falhou" });
            }

            return Json(new { resp = "falhou" });


        }


        [HttpPost]
        public JsonResult VerificaArquivo(int id)
        {

            string UploadPath = @"C:\Users\Deivid\source\repos\Documentos\Documentos\Anexos\";
            string FileId = id.ToString();
            bool FileExist = false;
            var repository = new Repository();
            var Documento = repository.Get(id);
            var DocumentoName = Documento.FileName;

            if (DocumentoName == "-")
            {
                return Json(new { success = false });
            }
            else
            {
                string DiskFile = FileId;
                string FullFile = UploadPath + DiskFile;

                foreach (var file in Directory.GetFiles(UploadPath))
                {

                    if (file.Contains(DiskFile))
                    {
                        FileExist = true;
                        return Json(new { success = true });
                    }
                }
            }

            if (!FileExist)
            {
                String[] newKey = new string[] { "id", id.ToString() };
                String[] newCampos = new string[] { "filename" };
                String[] newValues = new string[] { "-" };
                repository.SaveDados(newCampos, newValues, newKey, "documento", "edit");
                return Json(new { resp = "ausente" });
            }

            return Json(new { success = false });
        }


        [HttpPost]
        public JsonResult Properties(int id)
        {

            string UploadPath = @"C:\Users\Deivid\source\repos\Documentos\Documentos\Anexos\";
            string FileId = id.ToString();

            var repository = new Repository();
            var Documento = repository.Get(id);
            var DocumentoName = Documento.FileName;

            if (DocumentoName == "-")
            {
                return Json(new { success = false });
            }
            else
            {
                string DiskFile = FileId;
                string FullFile = UploadPath + DiskFile;

                foreach (var file in Directory.GetFiles(UploadPath))
                {

                    if (file.Contains(DiskFile))
                    {
                        FileInfo FileProperties = new FileInfo(file);

                        List<PropertiesData> Fprop = new List<PropertiesData>();
                        Fprop.Add(new PropertiesData()
                        {
                            CreationTime = FileProperties.CreationTime.ToString(),
                            Extension = FileProperties.Extension,
                            Length = Convert.ToInt32(FileProperties.Length)
                        });

                        return Json(new { success = true, propfiles = Fprop[0] });
                    }
                }
            }

            return Json(new { success = false });
        }


        [HttpGet]
        public ActionResult Download(int id)
        {

            string UploadPath = @"C:\Users\Deivid\source\repos\Documentos\Documentos\Anexos\";
            string FileId = id.ToString();
            var repository = new Repository();
            var Documento = repository.Get(id);
            string DiskFile = UploadPath + FileId;



            foreach (var file in Directory.GetFiles(UploadPath))
            {

                if (file.Contains(DiskFile))
                {

                    string ExtFile = Path.GetExtension(file);
                    string FullFile = UploadPath + FileId + ExtFile;
                    string FileType = MimeMapping.GetMimeMapping(FullFile);
                    var DocumentoName = Documento.FileName + ExtFile;
                    return File(FullFile, FileType, DocumentoName);

                }
            }

            return View();
        }
    }
}
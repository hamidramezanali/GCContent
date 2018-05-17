using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using WebApplication10.Models;
using WebApplication10.Models.Home;


namespace WebApplication10.Controllers
{
    public class HomeController : Controller
    {

        //using this to upload and download file to and from server
        private readonly IFileProvider fileProvider;

        public HomeController(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");




            var FN=Path.GetFileName(file.FileName);
            //  var path =   @"C:\Users\GeneMapperS1\GIT\GCContent\WebApplication10\wwwroot\Upload\Untitled document3.txt";
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\Upload",
                       FN);


            using (var stream = new FileStream(path
                , FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Index");
        }


       

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

         //This action retrives and lists all files in the uplad path (service configure)
        public IActionResult Files()
        {
            var model = new FilesViewModel();
            foreach (var item in this.fileProvider.GetDirectoryContents(""))
            {
                model.Files.Add(
                    new FileDetails { Name = item.Name, Path = item.PhysicalPath });
            }
            return View(model);
        }


        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/Download", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(path), Path.GetFileName(path));
        }


        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        public async Task<IActionResult> FileContent(string filename)
        {
            int counter = 0;
            string Seq, Qual;
            List<string> Sequences = new List<string>();
            List<string> Qualities = new List<string>();
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot/Upload", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            StreamReader sr = new StreamReader(memory);
            //string mem = sr.ReadToEnd();


            while (true)
            {
                sr.ReadLine();
                Seq = sr.ReadLine();
                sr.ReadLine();
                Qual = sr.ReadLine();
                if (Seq == null)
                    break;
                Sequences.Add(Seq);
                Qualities.Add(Qual);
                counter++;
            }

            string Seq1= Models.Home.MakeAnArray.countGC(Sequences);
            ViewBag.GCATN = Models.Home.MakeAnArray.GCATNcount;
            ViewBag.Message = Seq1;
            ViewBag.Count = Models.Home.MakeAnArray.count;
            ViewBag.AvgGCContent = Models.Home.MakeAnArray.AvgGCContent;
            return View(memory);
        }


        [HttpGet]
        public IActionResult MakeAnArray()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MakeAnArray(int min,int max,int nodes,int percise)
        {
            MakeAnArray ma = new MakeAnArray( min,  max,  nodes,  percise);
            ViewBag.Message = ma.GetResult();
            return View();
        }


    }

}

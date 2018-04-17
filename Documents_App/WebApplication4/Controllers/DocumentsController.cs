using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Entites;
using WebApplication4.Infrastructure;
using WebApplication4.Repository;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly DocumentsRepository dRepository;

        private string login;
        private string password;

        public DocumentsController(IConfiguration configuration)
        {
            login = AuthorizeData.Login;
            password = AuthorizeData.Password;
            dRepository = new DocumentsRepository(configuration, login, password);
        }

        public IActionResult Index()
        {
            return View(dRepository.FindAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DocumentViewModel dvm)
        {
            Documents docs = new Documents { DocumentName = dvm.DocumentName, DocumentIntroNumber = dvm.DocumentIntroNumber };

            if (ModelState.IsValid)
            {
                if (dvm.Contents != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(dvm.Contents.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)dvm.Contents.Length);
                    }
                    docs.Contents = imageData;
                }
                dRepository.Add(docs);
                return RedirectToAction("Index");
            }
            return View(docs);
        }

        [HttpGet]
        public FileResult GetFile(Guid? id)
        {
            var result = dRepository.Download(id.Value);
            string file_type = "application/txt";
            return File(result.Contents, file_type, result.DocumentName);
        }

        // GET: /Documents/Edit/1
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Documents obj = dRepository.SelectDocumentById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST: /Documents/Edit 
        [HttpPost]
        public IActionResult Edit(Documents obj, Guid? id)
        {
            if (ModelState.IsValid)
            {
                dRepository.Update(obj, obj.DocumentId = id.Value);
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}

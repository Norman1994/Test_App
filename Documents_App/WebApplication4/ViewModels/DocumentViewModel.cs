using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.ViewModels
{
    public class DocumentViewModel
    {
        public string DocumentName { get; set; }

        public IFormFile Contents { get; set; }

        public string DocumentIntroNumber { get; set; }
    }
}

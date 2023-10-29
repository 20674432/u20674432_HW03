using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework03.Models
{
    public class LibraryVM
    {

        public IEnumerable<students> mystudentsList { get; set; }
        public IEnumerable<types> mytypeList { get; set; }
        public IEnumerable<borrows> borrowsList { get; set; }
        public IEnumerable<books> booksList { get; set; }
        [NotMapped]
        public List<string> numOfBookstaken { get; set; }
        [AllowHtml]
        public string Content { get; set; }

        public string Extension { get; set; }

        public string FileName { get; set; }
    }
}
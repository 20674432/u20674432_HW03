using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Homework03.Models
{
    public class Students_Books
    {
        public IEnumerable<students> studentList { get; set; }
        public IEnumerable<books> booksList { get; set; }
        public IEnumerable<types> typesList { get; set; }
        public IEnumerable<authors> authorsList { get; set; }
        public IEnumerable<borrows> borrowsList { get; set;}
        [NotMapped]
        public List<string> StatusList { get; set; }
    }
}
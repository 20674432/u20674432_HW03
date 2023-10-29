using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework03.Models
{
    public class MaintainModelView
    {
        public IEnumerable<borrows> borrowsList { get; set; }
        public IEnumerable<books> booksList { get; set; }
        public IEnumerable<types> typesList { get; set; }
        public IEnumerable<authors> authorsList { get; set; }
        public IEnumerable<students> studentsList { get; set; }
    }
}
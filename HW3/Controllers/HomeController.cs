using HW3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HW3.Controllers
{
    public class HomeController : Controller
    {

        // Returns the home page
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        // Recives the file and radio button input
        [HttpPost]
        public ActionResult Index(string FileType, HttpPostedFileBase file)
        {                     
            
               
            // check if a file has been uploaded
            if (file != null)
            {
                string extension = Path.GetExtension(file.FileName);
                // check  the file type
                // If it is image and radio button sellected as of a image 
                if( FileType == "Image")
                {
                    // Save Image into the images folder
                    file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Media/Images"), Path.GetFileName(file.FileName)));
                    ViewBag.Message = "File uploaded successfully";
                } 
                else if( FileType == "Video")
                {
                    // Save video into the videos folder
                    file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Media/Videos"), Path.GetFileName(file.FileName)));
                    ViewBag.Message = "File uploaded successfully";
                }
                else if(FileType == "Document")
                {
                    // Save Document into the Documents folder
                    file.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/Media/Documents"), Path.GetFileName(file.FileName)));
                    ViewBag.Message = "File uploaded successfully";
                }
            }
            else
            {
                ViewBag.Message = "Plese Select a file";
             
            }
            // after successfully uploading redirect the user
            //return RedirectToAction("actionname", "controller name");
            return View();
        }

        public ActionResult About()
        {
            return View();
        }



    }
}
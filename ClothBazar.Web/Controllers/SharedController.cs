using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class SharedController : Controller
    {
       public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();

            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
           
            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                file.SaveAs(path);
                result.Data = new { Success = true, ImageURL = string.Format("/Content/images/{0}",fileName) };

                //var newImage = new Image() { Name = fileName };
                //if (ImagesService.Instance.SaveNewImage(newImage))
                //{
                //    result.Data = new { success = true, Image = fileName, File = newImage.ID, ImageURL = string.Format("{0}{1}", Variables.ImageFolderPath, fileName) };
                //}
                //else
                //{
                //    result.Data = new { success = false, message = new HttpStatusCodeResult(500) };
                //}
            }
            catch(Exception ex)
            {
                result.Data = new { success = false, message = ex.Message };
            }
            return result;
        }
        public JsonResult UploadImage1()
        {
            JsonResult result = new JsonResult();

            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                file.SaveAs(path);
                result.Data = new { Success = true, ImageURL1 = string.Format("/Content/images/{0}", fileName) };

                //var newImage = new Image() { Name = fileName };
                //if (ImagesService.Instance.SaveNewImage(newImage))
                //{
                //    result.Data = new { success = true, Image = fileName, File = newImage.ID, ImageURL = string.Format("{0}{1}", Variables.ImageFolderPath, fileName) };
                //}
                //else
                //{
                //    result.Data = new { success = false, message = new HttpStatusCodeResult(500) };
                //}
            }
            catch (Exception ex)
            {
                result.Data = new { success = false, message = ex.Message };
            }
            return result;
        }
        public JsonResult UploadImage2()
        {
            JsonResult result = new JsonResult();

            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                file.SaveAs(path);
                result.Data = new { Success = true, ImageURL2 = string.Format("/Content/images/{0}", fileName) };

                //var newImage = new Image() { Name = fileName };
                //if (ImagesService.Instance.SaveNewImage(newImage))
                //{
                //    result.Data = new { success = true, Image = fileName, File = newImage.ID, ImageURL = string.Format("{0}{1}", Variables.ImageFolderPath, fileName) };
                //}
                //else
                //{
                //    result.Data = new { success = false, message = new HttpStatusCodeResult(500) };
                //}
            }
            catch (Exception ex)
            {
                result.Data = new { success = false, message = ex.Message };
            }
            return result;
        }
    }
}
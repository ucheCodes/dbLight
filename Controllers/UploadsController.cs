using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using FilesUploadApi.Models;

namespace FilesUploadApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadsController : ControllerBase
    {
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult("This is my custom files upload api");
        }

        [HttpPost]
        public JsonResult SaveFile()
        {
            string photoPath = Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"),("Files"));
            FilesInfo info = new FilesInfo();
            try
            {
                List<FilesInfo> fileList = new List<FilesInfo>();
                var files = Request.Form.Files;
                if (files.Count == 1)
                {
                    var extension = Path.GetExtension(files[0].FileName);
                    var filename = Guid.NewGuid().ToString()+extension;
                    var physicalPath = photoPath + "/" + filename;
                    using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    var fileByte = System.IO.File.ReadAllBytes(physicalPath);
                    if (extension == ".jpg" || extension == ".jpeg")
                    {
                        info.ByteConverterString = "data:image/jpeg;base64,";
                    }
                    else if (extension == ".mp4" || extension == ".3gp")
                    {
                        info.ByteConverterString = "data:video/mp4;base64,";
                    }
                    else if (extension == ".mp3")
                    {
                        info.ByteConverterString = "data:audio/wav;base64,";
                    }
                    else if (extension == ".png")
                    {
                        info.ByteConverterString = "data:image/png;base64,";
                    }
                    info.FileName = filename;
                    info.FileExtension = extension;
                    info.FileCount = files.Count;
                    info.ByteString = fileByte;
            		return new JsonResult(info);
                }
                else if (files.Count > 1)
                {
                    foreach (var file in files)
                    {
                        info = new FilesInfo();
                        var extension = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid().ToString()+extension;
                        var physicalPath = photoPath + "/" + filename;
                        using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        var fileByte = System.IO.File.ReadAllBytes(physicalPath);
                        if (extension == ".jpg" || extension == ".jpeg")
                        {
                            info.ByteConverterString = "data:image/jpeg;base64,";
                        }
                        else if (extension == ".mp4" || extension == ".3gp")
                        {
                            info.ByteConverterString = "data:video/mp4;base64,";
                        }
                        else if (extension == ".mp3")
                        {
                            info.ByteConverterString = "data:audio/wav;base64,";
                        }
                        else if (extension == ".png")
                        {
                            info.ByteConverterString = "data:image/png;base64,";
                        }
                        info.FileName = filename;
                        info.FileExtension = extension;
                        info.FileCount = files.Count;
                        info.ByteString = fileByte;
                        fileList.Add(info);
                    }
                    return new JsonResult(fileList);
                }
                else
                {
                    return new JsonResult("failed");
                }
            }
            catch (System.Exception e)
            {
                
               return new JsonResult (e.Message);
            }
        }
        
        // [HttpPost]
        // public JsonResult SaveFile2()
        // {
        //     string photoPath = Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"),("Photos"));
        //     try
        //     {
        //         List<byte[]> fileList = new List<byte[]>();
        //         FilesInfo info = new FilesInfo();
        //         var files = Request.Form.Files;
        //         if (files.Count == 1)
        //         {
        //             var extension = Path.GetExtension(files[0].FileName);
        //             var filename = Guid.NewGuid().ToString()+extension;
        //             //var physicalPath = _env.ContentRootPath + "/Photos/" + filename;
        //             var physicalPath = photoPath + "/" + filename;
        //             using (var fileStream = new FileStream(physicalPath, FileMode.Create))
        //             {
        //                 files[0].CopyTo(fileStream);
        //             }
        //    		    var fileByte = System.IO.File.ReadAllBytes(physicalPath);
        //             info.FileName = filename;
        //             info.FileExtension = extension;
        //             info.FileCount = files.Count;
        //             info.ByteString = fileByte;
        //             info.ByteConverterString = "";
        //     		return new JsonResult(info);
        //         }
        //         else if (files.Count > 1)
        //         {
        //             foreach (var file in files)
        //             {
        //                 var extension = Path.GetExtension(file.FileName);
        //                 var filename = Guid.NewGuid().ToString()+extension;
        //                 //var physicalPath = _env.ContentRootPath + "/Photos/" + filename;
        //                 var physicalPath = photoPath + "/" + filename;
        //                 using (var fileStream = new FileStream(physicalPath, FileMode.Create))
        //                 {
        //                     file.CopyTo(fileStream);
        //                     var fileByte = System.IO.File.ReadAllBytes(physicalPath);
        //                     fileList.Add(fileByte);
        //                 }
        //                 // info.FileName = filename;
        //                 // info.FileExtension = extension;
        //                 // info.FileCount = files.Count;
        //                 // info.ByteString = fileByte;
        //                 // info.ByteConverterString = "";
        //     		    // return new JsonResult(info);
        //             }
        //             return new JsonResult(fileList);
        //         }
        //         else
        //         {
        //             return new JsonResult("failed");
        //         }
        //     }
        //     catch (System.Exception)
        //     {
                
        //        return new JsonResult (photoPath);
        //     }
        // }
    }
}

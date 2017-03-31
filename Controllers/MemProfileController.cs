using oresa.API.Models;
using oresa.API.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace oresa.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[RoutePrefix("myapi/MemberProfile")]
    public class MemProfileController : ApiController
    {
        MemProfile memprofileService = new MemProfile();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        //[Route("{id:guid}/{details}")]
        [HttpGet]
        public DataTable Get(Guid id)
        {
            return memprofileService.GetMemberShipData(id);
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> Post()
        {

            // const string StoragePath = "~/UploadedImage";
            ImgModel _img = new ImgModel();
            bool res = false;
            Dictionary<string, List<string>> myData = new Dictionary<string, List<string>>();
            Dictionary<string, string> Mydata = new Dictionary<string, string>();
            string StoragePath = HttpContext.Current.Server.MapPath("~/UploadedImage/");
            //string sp=HttpContext.Current.Server.MapPath(@"D:\Inetpub\vhosts\liveodia.co.in\devenv.liveodia.co.in\UploadedImage\");
            if (Request.Content.IsMimeMultipartContent())
            {
                try
                {
                    string fname = "";
                    List<string> Imglist = new List<string>();
                    var streamProvider = new MultipartFormDataStreamProvider(StoragePath);
                    await Request.Content.ReadAsMultipartAsync(streamProvider);
                    MultipartFileData fdata = null;
                    foreach (MultipartFileData fileData in streamProvider.FileData)
                    {

                        if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                        {
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                        }
                        string fileName = fileData.Headers.ContentDisposition.FileName;

                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                            fname = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + "_" + fileName;
                            //_img.ImageName = fname;
                            Imglist.Add(fname);
                        }

                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fname = Path.GetFileName(fileName);
                            //_img.ImageName = fname;
                            Imglist.Add(fname);
                        }
                        string fpath = Path.Combine(StoragePath, fname);
                        File.Copy(fileData.LocalFileName, Path.Combine(StoragePath, fname));
                        //compressFile(fpath);

                        //                        File.Copy(fileData.LocalFileName, Path.Combine(StoragePath, fname));
                        //File.Copy(fileData.LocalFileName, Path.Combine(sp, fname));
                        //                      compressFile(string fname);
                    }
                    myData.Add("img", Imglist);
                    foreach (var key in streamProvider.FormData.AllKeys)
                    {
                        foreach (var val in streamProvider.FormData.GetValues(key))
                        {
                            Mydata.Add(key, val);
                        }
                    }

                    if (streamProvider.FileData.Count == 0)
                    {
                        Mydata.Add("img", "Default");
                    }
                    if (!myData.ContainsKey("todaydate"))
                        Mydata.Add("todaydate", DateTime.Now.ToString("dd-MM-yyyy"));

                    if (Mydata["ptype"] == "Completed")
                    {
                        res = memprofileService.SaveMemProfileData(myData, Mydata);
                    }
                    else
                        res = memprofileService.SaveUpcomingProject(myData, Mydata);

                }
                catch (Exception e)
                {
                    Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted");
            }
        }

        //private void compressFile(string fname)
        //{

        //    byte[] photoBytes = File.ReadAllBytes(fname);
        //    int quality = 70;
        //    ImageProcessor.Imaging.Formats.ISupportedImageFormat format = new ImageProcessor.Imaging.Formats.JpegFormat();
        //    Size size = new Size(800, 600);
        //    using (MemoryStream inStream = new MemoryStream(photoBytes))
        //    {
        //        using (MemoryStream outStream = new MemoryStream())
        //        {
        //            using (ImageFactory imageFactory = new ImageFactory())
        //            {
        //                // Load, resize, set the format and quality and save an image.
        //                imageFactory.Load(inStream)
        //                            //.Resize(size)
        //                            .Format(format)
        //                            .Quality(quality)
        //                            .Resolution(100, 100)
        //                            .Save(outStream);
        //                FileStream file = new FileStream(fname, FileMode.Create, FileAccess.Write);
        //                outStream.WriteTo(file);
        //                file.Close();
        //                outStream.Close();
        //            }

        //            // Do something with the stream.
        //        }
        //    }
        //}

        private bool deleteFromDrive()
        {
            bool res = false;
            int count = 0;
            int fileCopied = 0;
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/UploadedImage/");
                string targetPath = HttpContext.Current.Server.MapPath("~/backupFiles/");
                DirectoryInfo di = new DirectoryInfo(path);
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }
                foreach (var srcPath in Directory.GetFiles(path))
                {
                    //Copy the file from sourcepath and place into mentioned target path, 
                    //Overwrite the file if same file is exist in target path
                    File.Copy(srcPath, srcPath.Replace(path, targetPath), true);
                    fileCopied++;
                }
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                    count++;
                }
                if (count >= 0 && fileCopied >= 0)
                    res = true;
            }
            catch (Exception ex)
            {
                res = false;
            }
            return res;
        }
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Whistleblower.Custom
{
    public class FileHandler : Controller
    {

        public FileResult DownloadFile(int id)
        {
            if (id > 0)
            {
                using (var db = new DB.DBEntity())
                {
                    DB.File file = db.File.First(f => f.FileID == id);
                    byte[] imageBytes = Convert.FromBase64String(file.Base64);
                    string ext = file.Extension.Substring(file.Extension.IndexOf("/") + 1);
                    return File(imageBytes, file.Extension, file.FileID.ToString() + "." + ext.Trim());
                }
            }
            return null;
        }
        public FileResult DownloadZip(int id)
        {
            using (var db = new DB.DBEntity())
            {
                List<DB.File> files = db.File.Where(f => f.WhistleID == id).ToList();
                using (ZipFile zip = new ZipFile())
                {
                    foreach (DB.File f in files)
                    {
                        string ext = f.Extension.Substring(f.Extension.IndexOf("/") + 1);
                        zip.AddEntry(f.FileID.ToString() + "." + f.Extension.Substring(f.Extension.IndexOf("/") + 1).Trim(), Convert.FromBase64String(f.Base64));
                    }
                    using (MemoryStream output = new MemoryStream())
                    {
                        zip.Save(output);
                        return File(output.ToArray(), "application/zip", id.ToString() + ".zip");
                    }
                }
            }
        }

    }
}
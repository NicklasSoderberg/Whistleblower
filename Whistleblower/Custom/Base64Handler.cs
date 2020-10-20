using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Whistleblower.Custom
{
    public class Base64Handler
    {

        public static string FileToBase64(HttpPostedFileBase file)
        {
            using (Image image = Image.FromStream(file.InputStream))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    return Convert.ToBase64String(imageBytes);
                }
            }
        }

        public static Image Base64ToImage(string Base64)
        {
            byte[] bytes = Convert.FromBase64String(Base64);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            return image;
        }
    }
}
using System;
using System.Drawing;
using System.IO;

namespace CTCrawler.Common.Helper
{
    public class CommonHelper
    {
        public static string ImageToBase64(string imgPath, bool exception = true)
        {
            if (!File.Exists(imgPath))
            {
                if (exception)
                    throw new FileNotFoundException(imgPath);

                return string.Empty;
            }

            try
            {
                using (Image image = Image.FromFile(imgPath))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        return Convert.ToBase64String(imageBytes);
                    }
                }
            }
            catch
            {
                if (exception)
                    throw;

                return string.Empty;
            }
        }
    }
}

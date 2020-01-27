using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Tools
{
    public class UploadPDF
    {
        public async Task<string>PDFUpload(IFormFile file, string destination1)
        {
            if (file != null)
            {
                string filename = "";
                var rnd = new Random();
                int rand = rnd.Next(10, 99999999);
                if (file.Length > 0 && file.Length < 20971520)
                {
                    string extension = Path.GetExtension(file.FileName);
                    filename = Guid.NewGuid().ToString() + rand.ToString() + extension;
                    if (extension == ".pdf" || extension == ".doc" || extension == ".docx")
                    {
                        string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), destination1));
                        using (var fileStream = new FileStream(
                                Path.Combine(filePath, filename),
                                FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                    }
                    else
                    {
                        filename = "File must be .pdf extension and Maximum Size is 5MB";
                    }
                }
                return filename;
            }
            else
            {
                string fileName = "";
                return fileName;
            }
        }
    }
}

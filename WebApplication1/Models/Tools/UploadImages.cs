using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LazZiya.ImageResize;
using System.Drawing;

namespace WebApplication1.Models
{
    public class UploadImages
    {
        public async Task<string>ImagesUpload(IFormFile file, string directory, string directory2)
        {
            try {
                if(file != null)
                {
                    string filename = "";
                    var rnd = new Random();
                    int rand = rnd.Next(10, 99999999);
                    if (file.Length > 0 && file.Length < 20971520)
                    {
                        string extension = Path.GetExtension(file.FileName);
                        filename = Guid.NewGuid().ToString() + rand.ToString() + extension;
                        if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".pjpeg" || extension == ".gif" || extension == "tiff")
                        {
                            string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), directory));

                            using(var fileStream = new FileStream(
                                Path.Combine(filePath, filename),
                                FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                            var newImage = Image.FromFile(directory2+filename);
                            var scaleImage = ImageResize.Scale(newImage, 1280, 1280);
                            scaleImage.SaveAs("" + directory2 + "\\1" + filename);
                            newImage.Dispose();
                            scaleImage.Dispose();
                            var oldImages = Path.Combine(Directory.GetCurrentDirectory(), directory, filename);
                            filename = "1" + filename;
                            System.IO.File.Delete(oldImages);
                        }
                        else
                        {
                            filename = "File must be either .jpg, .jpeg, .png and Maximum Size is 4MB";
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
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}

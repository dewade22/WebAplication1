using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Tools
{
    public class PDFDelete
    {
        public string Delete(string fileName, string directory)
        {
            string hasil = "";
            var path = Path.Combine(Directory.GetCurrentDirectory(), directory, fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                hasil = "Success";
            }
            else
            {
                hasil = "Picture Not Found";
            }
            return hasil;
        }
    }
}

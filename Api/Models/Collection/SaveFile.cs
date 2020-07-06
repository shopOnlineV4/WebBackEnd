using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Collection
{
    public class SaveFile
    {
        public IWebHostEnvironment _HostEnvironment;

        public SaveFile(IWebHostEnvironment HostEnvironment)
        {
            _HostEnvironment = HostEnvironment;
        }

        public  string SaveB64File(string data )
        {
            string fileName = DateTime.Now.Ticks + ".png";
            string pathBase = _HostEnvironment.WebRootPath + "\\productImages\\";
            if (!Directory.Exists(pathBase + "\\productImages\\"))
                Directory.CreateDirectory(pathBase + "\\productImages\\");
            byte[] contents = Convert.FromBase64String(data);
            MemoryStream ms = new MemoryStream(contents, 0, contents.Length);
            File.WriteAllBytes(pathBase + fileName, contents);
            

            return fileName;
        }

        public  void DeteFile(string fileImageName)
        {
            try
            {
                string pathBase = _HostEnvironment.WebRootPath + "\\productImages\\";
                if (File.Exists(Path.Combine(pathBase, fileImageName)))
                    File.Delete(Path.Combine(pathBase, fileImageName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pri.Drinks.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Drinks.Core.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> StoreFileAsync<T>(IFormFile formFile, string subFolder)
        {
            //create a unique filename
            var filename = $"{Guid.NewGuid()}_{formFile.FileName}";
            //set up the path to wwwfolder and images
            var pathToFolder = Path.Combine($"{_webHostEnvironment.WebRootPath}",
                subFolder,typeof(T).Name);
            //check if path exists if not creat it
            if(!Directory.Exists(pathToFolder))
            {
                Directory.CreateDirectory(pathToFolder);
            }
            //set up path to file
            var filePath = Path.Combine(pathToFolder,filename);
            //store file
            using(FileStream fileStream = new(filePath,FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }
            //return filename
            return filename;
        }
    }
}

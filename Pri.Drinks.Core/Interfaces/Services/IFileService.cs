using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Drinks.Core.Interfaces.Services
{
    public interface IFileService
    {
        Task<string> StoreFileAsync<T>(IFormFile formFile,string subFolder);
    }
}

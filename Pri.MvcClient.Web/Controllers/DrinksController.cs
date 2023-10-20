using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pri.MvcClient.Web.Models;
using Pri.MvcClient.Web.ViewModels;
using System.Net.Http.Headers;
using System.Text;

namespace Pri.MvcClient.Web.Controllers
{
    public class DrinksController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public DrinksController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _baseUrl = $"{_configuration.GetSection("ApiUrl:BaseUrl").Value}/Drinks";
        }
        public async Task<IActionResult> Index()
        {
            var url = new Uri(_baseUrl);
            var result = await _httpClient.GetAsync(url);
            if(!result.IsSuccessStatusCode)
            {
                return BadRequest("Error");
            }
            var content = await result.Content.ReadAsStringAsync();
            var drinksIndexViewModel = JsonConvert.DeserializeObject
                <DrinksIndexViewModel>(content);
            return View(drinksIndexViewModel);
        }
        public async Task<IActionResult> Get(int id)
        {
            var url = new Uri($"{_baseUrl}/{id}");
            var result = await _httpClient.GetAsync(url);
            if(!result.IsSuccessStatusCode)
            {
                return NotFound();
            }
            //read the content to serialized string
            var content = await result.Content.ReadAsStringAsync();
            //deserialize json string to object of type DrinksGetViewModel
            var drinksGetViewModel =
                JsonConvert.DeserializeObject<DrinksGetViewModel>(content);
            return View(drinksGetViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //get the categories
            var categoryUrl = new Uri($"{_baseUrl}/categories");
            var result = await _httpClient.GetAsync(categoryUrl);
            var content = await result.Content.ReadAsStringAsync();
            var categories = JsonConvert
                .DeserializeObject<BaseItemsViewModel>(content);
            //get the properties
            var propertyUrl = new Uri($"{_baseUrl}/properties");
            result = await _httpClient.GetAsync(propertyUrl);
            content = await result.Content.ReadAsStringAsync();
            var properties = JsonConvert.DeserializeObject<BaseItemsViewModel>(content);
            //populate form data properties and categories
            //create the model
            var drinksAddViewModel = new DrinksAddViewModel
            {
                Properties = properties.Items.Select(i =>
                new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                Categories = categories.Items.Select(i =>
                new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            //pass to the view
            return View(drinksAddViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(DrinksAddViewModel drinksAddViewModel)
        {
            //get the categories
            var categoryUrl = new Uri($"{_baseUrl}/categories");
            var result = await _httpClient.GetAsync(categoryUrl);
            var content = await result.Content.ReadAsStringAsync();
            var categories = JsonConvert
                .DeserializeObject<BaseItemsViewModel>(content);
            //get the properties
            var propertyUrl = new Uri($"{_baseUrl}/properties");
            result = await _httpClient.GetAsync(propertyUrl);
            content = await result.Content.ReadAsStringAsync();
            var properties = JsonConvert.DeserializeObject<BaseItemsViewModel>(content);
            //modelstate error
            if (!ModelState.IsValid)
            {
                drinksAddViewModel.Properties = properties.Items.Select(i =>
                new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                drinksAddViewModel.Categories = categories.Items.Select(i =>
                new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(drinksAddViewModel);
            }
            //create the multipartformdata and add the Viewmodel properties
            MultipartFormDataContent multipartFormDataContent = new()
            {
                { new StringContent(drinksAddViewModel.Name,Encoding.UTF8), "Name" },
                { new StringContent(drinksAddViewModel.CategoryId.ToString(),Encoding.UTF8), "CategoryId" },
                { new StringContent(drinksAddViewModel.AlcoholPercentage.ToString(), Encoding.UTF8), "AlcoholPercentage" },
            };
            //add the int ienumerable as PropertyIds[]
            foreach (var property in drinksAddViewModel.PropertyIds)
            {
                multipartFormDataContent.Add(new StringContent(property.ToString(), Encoding.UTF8), "PropertyIds[]");
            }
            //Add file StreamContent to multiForm Content
            var filecontent = new StreamContent(drinksAddViewModel.Image.OpenReadStream());
            //set the content type
            filecontent.Headers.ContentType = new MediaTypeHeaderValue(drinksAddViewModel.Image.ContentType);
            //set the contentdisposition
            filecontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "Image",//this is the property name in the dto
                FileName = drinksAddViewModel.Image.FileName,
                FileNameStar = drinksAddViewModel.Image.FileName
            };
            //add to multipartformdata
            multipartFormDataContent.Add(filecontent);
            //send to api
            var postResult = await _httpClient.PostAsync(_baseUrl, multipartFormDataContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (postResult.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            //api error
            ModelState.AddModelError("", "Something went wrong, please try again later...");
            drinksAddViewModel.Properties = properties.Items.Select(i =>
            new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            drinksAddViewModel.Categories = categories.Items.Select(i =>
            new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(drinksAddViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //build the url using _baseUrl and Id
            //call the DeleteAsync() method.
            Uri deleteUrl = new Uri($"{_baseUrl}/{id}");
            var result = await _httpClient.DeleteAsync(deleteUrl);
            if(result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}

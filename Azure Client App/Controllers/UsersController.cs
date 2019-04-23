using Azure_Client_App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Azure_Client_App.Controllers
{
    public class UsersController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "http://stayaddicted2.azurewebsites.net";
        public async Task<ActionResult> Index()
        {
            List<Users> UsersInfo = new List<Users>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Users/GetAllUsers");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var UsersResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    UsersInfo = JsonConvert.DeserializeObject<List<Users>>(UsersResponse);

                }
                //returning the employee list to view  
                return View(UsersInfo);
            }
        }
    }
}
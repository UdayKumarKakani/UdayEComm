using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace UdayEComm.Pages;

public class OrderModel : PageModel
{
    private readonly ILogger<OrderModel> _logger;

    public OrderModel(ILogger<OrderModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
      public async void OnPostWeather()
        {
            var ip = Request.Form["ip"];
            ViewData["ip"] = ip;
            var client = new HttpClient();
            var address = $"https://{ip}/WeatherForecast";
            try
            {
                var resp = client.GetAsync(address).Result;
                if (resp.IsSuccessStatusCode)
                {
                    var jsonString = await resp.Content.ReadAsStringAsync();
                    ViewData["weather"] = JsonConvert.DeserializeObject<List<OrderData>>(jsonString);
                }

            }
            catch (Exception ex)
            {
                ViewData["error"] = "Error getting Orders data: " + ex.Message;
            }
        }
}
public class OrderData
{
    public DateTime Date { get; set; }

    public string? ProductName { get; set; }

    public string? ProductDescription { get; set; }

    public int  Count { get; set; }
     public int  Cost { get; set; }
}
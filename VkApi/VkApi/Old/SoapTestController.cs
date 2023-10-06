using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N11ServiceReference;

namespace VkApi.Controllers;

[NonController]
[Route("api/[controller]")]
[ApiController]
public class SoapTestController : ControllerBase
{
    public SoapTestController() { }

    [HttpGet]
    public void Test()
    {
        CityServicePortClient cityServicePort = new CityServicePortClient();

        GetCitiesRequest request = new();
        request.auth = new Authentication
        {
            appKey = "test",
            appSecret = "test"
        };
        GetCitiesResponse response = cityServicePort.GetCities(request);
        GetCitiesResponse getCitiesResponse = new CityServicePortClient().GetCities(new GetCitiesRequest { auth = new Authentication { appKey = "test", appSecret = "test" } });
    }


}

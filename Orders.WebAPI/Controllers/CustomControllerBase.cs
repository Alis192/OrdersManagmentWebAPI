using Microsoft.AspNetCore.Mvc;

namespace Orders.WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {

    }
}

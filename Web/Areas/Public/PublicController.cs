using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Public
{
    [Authorize]
    [Area("Public")]                       // Area name
    [Route("api/[area]/[controller]")]       // API route includes area
    [ApiController]
    public class PublicController : ControllerBase
    {
        
    }
}

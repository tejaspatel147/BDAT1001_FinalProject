using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiProject.Controllers
{
    [Authorize]
    public class BaseAuthenticatedController : Controller
    {
    }
}

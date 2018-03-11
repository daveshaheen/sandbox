using Microsoft.AspNetCore.Mvc;

namespace App.Controllers {
    /// <summary>
    /// BaseContoller
    /// <para>An abstract base class for controllers to inherit from.</para>
    /// </summary>
    /// <remarks>Inherits from <see cref="Controller" /></remarks>
    [Route("api/{version}")]
    public abstract class BaseController : Controller { }
}

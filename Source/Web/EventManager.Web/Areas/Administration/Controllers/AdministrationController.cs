namespace EventManager.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using EventManager.Common;
    using EventManager.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
    }
}

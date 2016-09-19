namespace EventManager.Web.Controllers
{
    using System.Web.Mvc;

    using EventManager.Services.Data;
    using EventManager.Web.Infrastructure.Mapping;
    using EventManager.Web.ViewModels.Home;
    using Data;

    public class JokesController : BaseController
    {
        private readonly IJokesService jokes;

        public JokesController(
            IJokesService jokes)
        {
            this.jokes = jokes;
        }

        public ActionResult ById(string id)
        {
            var joke = this.jokes.GetById(id);
            var viewModel = this.Mapper.Map<JokeViewModel>(joke);
            return this.View(viewModel);
        }
    }
}

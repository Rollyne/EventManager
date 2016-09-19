namespace EventManager.Web.ViewModels.Home
{
    using EventManager.Data.Models;
    using EventManager.Web.Infrastructure.Mapping;

    public class JokeCategoryViewModel : IMapFrom<JokeCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

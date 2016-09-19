namespace EventManager.Services.Data
{
    using System.Linq;

    using EventManager.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<JokeCategory> GetAll();

        JokeCategory EnsureCategory(string name);
    }
}

namespace EventManager.Services.Data
{
    using System.Linq;

    using EventManager.Data.Models;

    public interface IJokesService
    {
        IQueryable<Joke> GetRandomJokes(int count);

        Joke GetById(string id);
    }
}

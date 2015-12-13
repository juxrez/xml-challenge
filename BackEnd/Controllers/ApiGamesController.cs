using System.Collections.Generic;
using System.Web.Http;
//using System.Web.Http.cors;
using BackEnd.Infrastructure;
using BackEnd.Models;

namespace BackEnd.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GamesController : ApiController
    {
        private IRepository<BoardGame> repo;

        public GamesController()
        {
            repo = new Repository();
        }

        public GamesController(Repository repository)
        {
            repo = repository;
        }

        // GET: api/Games
        public IEnumerable<BoardGame> Get()
        {
            return repo.List;
        }

        // GET: api/Games/5
        public BoardGame Get(int id)
        {
            BoardGame game = repo.FindById(id);
            return game;
        }

        // POST: api/Games
        public void Post(BoardGame game)
        {
            repo.Add(game);
            //return new { Success = true };
        }

        // PUT: api/Games/5
        public void Put(int Id, BoardGame game)
        {
            repo.Update(Id, game);
        }

        // DELETE: api/Games/5
        public void Delete(int id)
        {
            repo.Delete(id);
        }


    }
}

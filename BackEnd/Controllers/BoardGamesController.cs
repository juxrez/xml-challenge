using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BackEnd.Infrastructure;
using BackEnd.Models;
namespace BackEnd.Controllers
{
    public class BoardGamesController : ApiController
    {
        private IRepository<BoardGame> repo;

        public BoardGamesController()
        {
            repo = new Repository();
        }

        public BoardGamesController(Repository repository)
        {
            repo = repository;
        }
        // GET: api/BoardGames
        public IEnumerable<BoardGame> Get()
        {
            return repo.List;
        }

        // GET: api/BoardGames/5
        public BoardGame Get(int id)
        {
            BoardGame game = repo.FindById(id);
            return game;
        }

        // POST: api/BoardGames
        public void Post(BoardGame game)
        {
            repo.Add(game);
        }

        // PUT: api/BoardGames/5
        public void Put(BoardGame game)//public void Put(int id, BoardGame game)
        {
            repo.Update(game.Id, game);
        }

        // DELETE: api/BoardGames/5
        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}

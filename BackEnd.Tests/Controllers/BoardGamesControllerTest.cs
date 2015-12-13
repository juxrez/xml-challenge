using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BackEnd.Controllers;
using BackEnd.Models;
using System.IO;
using BackEnd.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Tests.Controllers
{
    [TestClass]
    public class BoardGamesControllerTest
    {
        
        private string repoPath = Path.Combine(Environment.CurrentDirectory, @"..\..\App_Data\BoardGames.xml");
        private Repository repo;

        public BoardGamesControllerTest()
        {        
            repo = new Repository(repoPath);
        }
        [TestMethod]
        public void GetAllGames_ShouldGetAllBoardGames()
        {
            BoardGamesController controller = new BoardGamesController(repo);
            IEnumerable<BoardGame> response = controller.Get();
            
            Assert.IsNotNull(response);
            Assert.AreNotEqual(0, response.Count());
        }
        [TestMethod]
        public void GetAGame_ShouldGetASingleGame()
        {
            BoardGamesController controller = new BoardGamesController(repo);
            BoardGame game = controller.Get(1);
            
            BoardGame expectedGame = new BoardGame() {Id = 1, Name = "Chest", Description= "Move your peons", MaxPlayers = 4 };
            Assert.IsNotNull(game);
            Assert.AreEqual(expectedGame.Name, game.Name);

        }
        [TestMethod]
        public void PostAGame_ShouldAddABoardGame()
        {
            BoardGamesController controller = new BoardGamesController(repo);
            BoardGame newGame = new BoardGame()
            {
                Id = 2,
                Name = "Uno",
                Description = "Who put down all cards on his hand, wins",
                MaxPlayers = 6
            };
            controller.Post(newGame);
            BoardGame uno = controller.Get(2);
            Assert.IsNotNull(uno);
            Assert.AreEqual(newGame.Name, uno.Name);
        }
        [TestMethod]
        public void PutAGame_ShouldPutAGame()
        {
            BoardGamesController controller = new BoardGamesController(repo);
            BoardGame boardGame = controller.Get(1);
            boardGame.MaxPlayers = 2;

            controller.Put(boardGame);

            BoardGame _game = controller.Get(1);
            Assert.IsNotNull(_game);
            Assert.AreEqual(boardGame.MaxPlayers, boardGame.MaxPlayers);

        }
        [TestMethod]
        public void DeteleteAGame_ShouldDeleteAGame()
        {
            BoardGamesController controller = new BoardGamesController(repo);
            controller.Delete(2);
            BoardGame game = controller.Get(2);
            Assert.IsNull(game);
        }
        
    }
}

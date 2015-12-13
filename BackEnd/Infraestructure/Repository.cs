using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Web;
using BackEnd.Models;
namespace BackEnd.Infrastructure
{
	public class Repository : IRepository<BoardGame>
	{
		private XDocument xmlRepo;
		private List<BoardGame> allGames;
		private string repoPath;

		public Repository(string xmlPath)
		{
			repoPath = xmlPath;
			allGames = new List<BoardGame>();
			xmlRepo = XDocument.Load(xmlPath);
            UpdateGamesList();
		}

		public Repository()
		{
			repoPath = HttpContext.Current.Server.MapPath("~/App_Data/Games.xml");
			allGames = new List<BoardGame>();
			xmlRepo = XDocument.Load(repoPath);
            UpdateGamesList();
		}

		public IEnumerable<BoardGame> List
		{
			get
			{
				return allGames;
			}
		}

		public void Add(BoardGame elm)
		{
            if (allGames != null)
            {
                var bg = allGames.Last();
                elm.Id = bg.Id + 1;
            }
            else
                elm.Id = 1;
            XElement game = new XElement("Game");
			game.Add(
				new XElement("ID", elm.Id),
				new XElement("Name", elm.Name),
				new XElement("Description", elm.Description),
				new XElement("MaxPlayers", elm.MaxPlayers)
			);
			xmlRepo.Element("BoardGames").Add(game);
			xmlRepo.Save(repoPath);
            UpdateGamesList();
		}

		public void Delete(int id)
		{
			try
			{
				xmlRepo.Descendants("Game").Where(ent => ent.Element("ID").Value == id.ToString()).Remove();
				xmlRepo.Save(repoPath);
			}
			catch (Exception ex)
            {
               string error = ex.Message;
            }
            UpdateGamesList();
		}

		public BoardGame FindById(int id)
		{
			return allGames.Find(u => u.Id == id);
		}

		public void Update(int id, BoardGame element)
		{
			XElement el = xmlRepo.Descendants("Game").Where(ent => ent.Element("ID").Value == id.ToString())
				.FirstOrDefault();
			el.SetElementValue("Name", element.Name);
			el.SetElementValue("Description", element.Description);
			el.SetElementValue("MaxPlayers", element.MaxPlayers);
			xmlRepo.Save(repoPath);
            UpdateGamesList();
		}

		private void UpdateGamesList()
		{
			allGames.Clear();
			var game = from elm in xmlRepo.Descendants("Game")
					   select new BoardGame()
					   {
						   Id = Convert.ToInt32(elm.Element("ID").Value),
						   Name = elm.Element("Name").Value,
						   Description = elm.Element("Description").Value,
						   MaxPlayers = Convert.ToInt32(elm.Element("MaxPlayers").Value)
					   };

			allGames.AddRange(game.ToList());
		}

        //public int GetLastId()
        //{
        //    BoardGame bg = allGames.LastOrDefault();
        //    return bg != null ? bg.Id : 0;
        //}
    }
}
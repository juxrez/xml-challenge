using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using BackEnd.Models;
namespace FrontEnd.Controllers
{
    public class BoardGamesController : Controller
    {

        // GET: BoardGamesw
        public async Task<ActionResult> Index()
        {     
            using (var get = new HttpClient())
            {
                get.BaseAddress = new Uri("http://localhost:58187/");
                get.DefaultRequestHeaders.Accept.Clear();
                get.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await get.GetAsync("api/Games");
                if (response.IsSuccessStatusCode)
                {
                    try {
                        IEnumerable<BoardGame> _bg = await response.Content.ReadAsAsync<IEnumerable<BoardGame>>();
                        response.EnsureSuccessStatusCode();           
                        return View(_bg);
                    }
                    catch(HttpRequestException e)
                    {
                        return View("Error");
                    }
                }
            }
                return View();
        }

        // GET: BoardGames/Details/5
        public async Task<ActionResult> Details(int id)
        {
            using (var get = new HttpClient())
            {
                get.BaseAddress = new Uri("http://localhost:58187/");
                get.DefaultRequestHeaders.Accept.Clear();
                get.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await get.GetAsync("api/Games/" + id);
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        BoardGame _bg = await response.Content.ReadAsAsync<BoardGame>();
                        return View(_bg);
                    }
                    catch
                    {
                        return View("Error");
                    }
                }
            }
                    return View();
        }

        // GET: BoardGames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BoardGames/Create
        [HttpPost]
        public async Task<ActionResult> Create(BoardGame postBoardGame)
        {
            
            using (var post = new HttpClient())
            {
                post.BaseAddress = new Uri("http://localhost:58187/");
                post.DefaultRequestHeaders.Accept.Clear();
                post.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    // TODO: Add insert logic here
                    // HttpResponseMessage responseLastId = await post.GetAsync("api/Games");
                    var _bg = new BoardGame()
                    {
                        Id = 1,
                        Name = postBoardGame.Name,
                        Description = postBoardGame.Description,
                        MaxPlayers = postBoardGame.MaxPlayers
                    };
                    HttpResponseMessage response = await post.PostAsJsonAsync("api/Games", _bg);
                    if(response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: BoardGames/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var edit = new HttpClient())
            {
                edit.BaseAddress = new Uri("http://localhost:58187/");
                edit.DefaultRequestHeaders.Accept.Clear();
                edit.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await edit.GetAsync("api/Games/" + id);
                if(response.IsSuccessStatusCode)
                {
                    try
                    {
                        BoardGame bg = await response.Content.ReadAsAsync<BoardGame>();
                        return View(bg);
                    }
                    catch
                    {
                        return View("Error");
                    }
                }
            }
                return View();
        }

        // POST: BoardGames/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, BoardGame postBoardGame)
        {
            using (var edit = new HttpClient())
            {
                edit.BaseAddress = new Uri("http://localhost:58187/");
                edit.DefaultRequestHeaders.Clear();
                edit.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await edit.PutAsJsonAsync("api/Games/" + id, postBoardGame);
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: BoardGames/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            using (var get = new HttpClient())
            {
                get.BaseAddress = new Uri("http://localhost:58187/");
                get.DefaultRequestHeaders.Accept.Clear();
                get.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await get.GetAsync("api/Games/" + id);
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        BoardGame _bg = await response.Content.ReadAsAsync<BoardGame>();
                        response.EnsureSuccessStatusCode();
                        if(response.IsSuccessStatusCode)
                            return View(_bg);
                    }
                    catch (HttpRequestException e)
                    {
                        return View("Error");
                    }

                }

                }
                return View();
        }

        // POST: BoardGames/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, BoardGame postBoardGame)
        {
            using (var delete = new HttpClient())
            {
                delete.BaseAddress = new Uri("http://localhost:58187/");
                delete.DefaultRequestHeaders.Accept.Clear();
                delete.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    // TODO: Add insert logic here
                    HttpResponseMessage response = await delete.DeleteAsync("api/Games/" + postBoardGame.Id);          
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
    }
}

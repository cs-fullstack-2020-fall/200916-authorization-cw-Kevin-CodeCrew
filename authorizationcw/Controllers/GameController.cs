using System.Linq;
using authorizationcw.Models;
using authorizationcw.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Descriptionizationcw.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }
        // View All Bands
        public IActionResult Index()
        {
            return View(_context);
        }
        // View GameModel Details
        public IActionResult GameDetails(int GameModelID)
        {      
            // find GameModel in db by id
            GameModel matchingGameModel = _context.Games.FirstOrDefault(GameModel => GameModel.id == GameModelID);
            // if GameModel is found
            if(matchingGameModel != null)
            {
                return View(matchingGameModel);
            } else 
            // if GameModel is not found
            {
                return Content("No GameModel found");
            }
        }
        // Add GameModel to DB
        [HttpPost]
        public IActionResult CreateGame(GameModel newGameModel)
        {
            // if data passed meets model validation
            if(ModelState.IsValid)
            {
                // add to db
                _context.Games.Add(newGameModel);
                _context.SaveChanges();
                return RedirectToAction("Index");  
            } else 
            {
                return View("CreateForm", newGameModel);
            }
        } 
        // Update GameModel in DB
        [HttpPost]
        public IActionResult UpdateGame(GameModel updateGameModel)
        {
            // find GameModel by id
            GameModel matchingGameModel = _context.Games.FirstOrDefault(GameModel => GameModel.id == updateGameModel.id);
            if(matchingGameModel != null)
            {   
                // if data passed meets model validation
                if(ModelState.IsValid)
                {
                    // update found GameModel with data passed
                    matchingGameModel.Title = updateGameModel.Title;
                    matchingGameModel.Description = updateGameModel.Description;
                    matchingGameModel.Publisher = updateGameModel.Publisher;
                    matchingGameModel.Rating = updateGameModel.Rating;
                    _context.SaveChanges();
                    return RedirectToAction("Index");  
                } else 
                // render form again populated with invalid data
                {
                    return View("UpdateForm", updateGameModel);
                }
            } else 
            {
                return Content("No GameModel found");
            }
        } 
        // Delete GameModel from DB
        public IActionResult DeleteGame(int GameModelID)
        {
            // find GameModel in db by id
            GameModel matchingGameModel = _context.Games.FirstOrDefault(GameModel => GameModel.id == GameModelID);
            // if GameModel is found
            if(matchingGameModel != null)
            {
                // remove from db
                _context.Remove(matchingGameModel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            } else 
            {
                return Content("No GameModel found");
            }
        } 
        // Display form to add GameModel - Must be logged in to access
        [Authorize]
        public IActionResult CreateForm()
        {
            return View();
        } 
        // Display form to update GameModel - Must be logged in to access
        [Authorize]
        public IActionResult UpdateForm(int GameModelID)
        {
            // find GameModel in db by id
            GameModel matchingGameModel = _context.Games.FirstOrDefault(GameModel => GameModel.id == GameModelID);
            // if GameModel is found
            if(matchingGameModel != null)
            {
                return View(matchingGameModel);
            } else 
            {
                return Content("No GameModel found");
            }
        } 
        // Display page to delete GameModel - Must be logged in to access
        [Authorize]
        public IActionResult DeleteConf(int GameModelID)
        {            
            // find GameModel by id
            GameModel matchingGameModel = _context.Games.FirstOrDefault(GameModel => GameModel.id == GameModelID);
            // if GameModel is found
            if(matchingGameModel != null)
            {
                return View(matchingGameModel);
            } else 
            {
                return Content("No GameModel found");
            }
        }
    }
}
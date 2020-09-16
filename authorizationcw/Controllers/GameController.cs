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
        
        public IActionResult Index()
        {
            return View(_context);
        }
        
        // View Game Details
        public IActionResult GameDetails(int GameModelID)
        {      
            GameModel matchingGameModel = _context.Games.FirstOrDefault(GameModel => GameModel.id == GameModelID);
            if(matchingGameModel != null)
            {
                return View(matchingGameModel);
            } 
            else 
            {
                return Content("No GameModel found");
            }
        }

        // Add Game
        [HttpPost]
        public IActionResult CreateGame(GameModel newGameModel)
        {
            if(ModelState.IsValid)
            {
                _context.Games.Add(newGameModel);
                _context.SaveChanges();
                return RedirectToAction("Index");  
            } 
            else 
            {
                return View("CreateForm", newGameModel);
            }
        } 
        
        // Update Game
        [HttpPost]
        public IActionResult UpdateGame(GameModel updateGameModel)
        {
            GameModel matchingGameModel = _context.Games.FirstOrDefault(GameModel => GameModel.id == updateGameModel.id);
            if(matchingGameModel != null)
            {   
                if(ModelState.IsValid)
                {
                    matchingGameModel.Title = updateGameModel.Title;
                    matchingGameModel.Description = updateGameModel.Description;
                    matchingGameModel.Publisher = updateGameModel.Publisher;
                    matchingGameModel.Rating = updateGameModel.Rating;
                    _context.SaveChanges();
                    return RedirectToAction("Index");  
                } 
                else 
                {
                    return View("UpdateForm", updateGameModel);
                }
            } else 
            {
                return Content("No GameModel found");
            }
        } 
        // Delete Game
        public IActionResult DeleteGame(int GameModelID)
        {
            GameModel matchingGameModel = _context.Games
                .FirstOrDefault(GameModel => GameModel.id == GameModelID);
                
            if(matchingGameModel != null)
            {
                _context.Remove(matchingGameModel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            } 
            else 
            {
                return Content("No Game found");
            }
        } 
        // Display add game form
        [Authorize]
        public IActionResult CreateForm()
        {
            return View();
        } 
        
        // Display update form
        [Authorize]
        public IActionResult UpdateForm(int GameModelID)
        {
            GameModel matchingGameModel = _context.Games.FirstOrDefault(GameModel => GameModel.id == GameModelID);
            if(matchingGameModel != null)
            {
                return View(matchingGameModel);
            } 
            else 
            {
                return Content("No GameModel found");
            }
        } 
        
        // Display page to delete Game
        [Authorize]
        public IActionResult DeleteConf(int GameModelID)
        {            
            // find GameModel by id
            GameModel matchingGameModel = _context.Games.FirstOrDefault(GameModel => GameModel.id == GameModelID);
            // if GameModel is found
            if(matchingGameModel != null)
            {
                return View(matchingGameModel);
            } 
            else 
            {
                return Content($"No Game found for game id {GameModelID}");
            }
        }
    }
}
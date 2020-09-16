using System.ComponentModel.DataAnnotations;

namespace authorizationcw.Models
{
    public class GameModel
    {
        [Key]
        public int id {get;set;}
        
        [Required(ErrorMessage = "An Game Title must be provided")]
        [Display(Name = "Game Title")]
        [StringLength(100, ErrorMessage = "A Game's title must be under 100 characters")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "An Game Description must be provided")]
        [Display(Name = "Game Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "An Game Publisher must be provided")]
        [Display(Name = "Game Publisher")]
        public string Publisher { get; set; }

        [Range(1, 5, ErrorMessage = "A game rating must be between 1 and 5")]
        [Display(Name = "Game Rating")]
        public int Rating { get; set; }
    }
}
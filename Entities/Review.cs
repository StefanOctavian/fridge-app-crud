using System.ComponentModel.DataAnnotations;

namespace Crud.Entities
{
    public class Review : BaseEntity
    {
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public required string Comment { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public required int Rating { get; set; }
    }
}
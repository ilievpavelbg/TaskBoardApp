using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace TaskBoardApp.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(DataConstants.User.MaxUserFirstName)]
        public string FirstName { get; init; } = null!;

        [Required]
        [MaxLength(DataConstants.User.MaxUserLastName)]
        public string LastName { get; init; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaTickets.Domain.DTOs
{
    public class AddUserToRoleDto
    {
        [Required]
        [Display(Name = "User")]
        public string SelectedUserId { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string SelectedRoleName { get; set; }

        public SelectList Users { get; set; }
        public SelectList Roles { get; set; }
    }
}

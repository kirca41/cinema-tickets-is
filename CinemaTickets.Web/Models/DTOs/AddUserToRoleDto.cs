using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CinemaTickets.Web.Models.DTOs
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

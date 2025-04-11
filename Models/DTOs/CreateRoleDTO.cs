using System.ComponentModel.DataAnnotations;

namespace PlooCinema.WebApi.Models.DTOs
{
    public class CreateRoleDTO
    {
        [Required]
        [MaxLength(256)]
        public required string RoleName { get; set; }
    }
}

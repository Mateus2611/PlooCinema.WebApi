using System.Text.Json.Serialization;

namespace PlooCinema.WebApi.Models.DTOs
{
    public class UpdateRoleDTO (string roleName)
    {
        public required string RoleName { get; set; } = roleName;
    }
}

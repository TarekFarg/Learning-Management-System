using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace LearningManagementSystem.Dtos
{
    public class RigesterDto
    {
        [MaxLength (100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(255)]
        public string Username { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
    }
}

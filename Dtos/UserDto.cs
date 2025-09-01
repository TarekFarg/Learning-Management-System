using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystem.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Status { get; set; }

        public bool Succeeded { get; set; }
    }
}

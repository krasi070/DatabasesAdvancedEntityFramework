namespace _08.CreateUser.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Range(1, 2147483647)]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"$(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*()_+<>?])[A-Za-z\d!@#$%^&*()_+<>?]{8,}^")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"$([\w\.\-]+)@([\w\-]+)((\.(\w)+)+)^")]
        public string Email { get; set; }

        [MaxLength(1024 * 1024)]
        public byte[] ProfilePicture { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }
    }
}
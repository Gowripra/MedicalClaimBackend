using System.ComponentModel.DataAnnotations;

namespace Medical_Claim.Models
{
    public class PolicyHolder
    {
        [Key]
        public int PolicyId { get; set; }
        [Required]
        public string PolicyHolderName { get; set; } = String.Empty;
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; } = String.Empty;
        [Required]
        public DateTime DateofBirth { get; set; }
        [Required]
        public string Email { get; set; } = String.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [Required]
        public string PolicyType { get; set; } = String.Empty;
    }
}

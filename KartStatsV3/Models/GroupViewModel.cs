using System.ComponentModel.DataAnnotations;

namespace KartStatsV3.Models
{
    public class GroupViewModel
    {
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Het naam veld mag niet leeg zijn")]
        [StringLength(25, ErrorMessage = "De naam mag niet langer zijn dan 25 karakters")]
        public string Name { get; set; }
        public int AdminUserId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace KartStatsV3.Models
{
    public class GroupViewModel
    {
        public int GroupId { get; set; }
        [Required]
        public string Name { get; set; }
        public int AdminUserId { get; set; }
    }
}

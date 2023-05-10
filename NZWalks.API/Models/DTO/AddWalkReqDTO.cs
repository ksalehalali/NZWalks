using NZWalks.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkReqDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name has to be a maximum of 50 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Description has to be a maximum of 200 characters")]
        public string Description { get; set; }
        [Required]
       
        public double LengthInKm { get; set; }
    
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }


    }
}

using System.ComponentModel.DataAnnotations;

namespace ServiceA.ContentApI.DTOs.Requests
{
    public class CreateEmailRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [Required]
        [StringLength(50)]
        public string Tone { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

    }
}

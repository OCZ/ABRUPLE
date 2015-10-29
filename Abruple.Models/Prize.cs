namespace Abruple.Models
{
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Prize
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ContestId { get; set; }
        public virtual Contest  Contest { get; set; }

    }
}

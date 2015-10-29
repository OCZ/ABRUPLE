namespace Abruple.Models
{
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Notification
    {
        // ID
        [Key]
        public int Id { get; set; }

        // CONTENT
        [Required]
        public string Content { get; set; }

        // RECIPIENT
        [Required]
        public string RecipientId { get; set; }
        public User Recipient { get; set; }

        //THE TYPE - IF IS SENT/REPORTED/ TO ADMIN FOR INAPPROPIATE ENTRY => HAS TO BE TYPE REPORT
        public NotificationType Type { get; set; }
    }
}

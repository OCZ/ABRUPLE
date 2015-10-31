namespace Abruple.App.Models.ViewModels
{
    using Abruple.Models.Enums;

    public class NotificationViewModel
    {
        // ID
        public int Id { get; set; }

        // CONTENT
        public string Content { get; set; }

        // RECIPIENT
        public string RecipientId { get; set; }
        public string Recipient { get; set; }

        //THE TYPE - IF IS SENT/REPORTED/ TO ADMIN FOR INAPPROPIATE ENTRY => HAS TO BE TYPE REPORT
        public NotificationType Type { get; set; }
    }
}
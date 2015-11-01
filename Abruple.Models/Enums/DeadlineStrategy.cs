namespace Abruple.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum DeadlineStrategy
    {
        [Display(Name="When the deadline is over")]
        ByTime,
        [Display(Name = "When contest entries reach the maximum")]
        ByParticipants
    }
}

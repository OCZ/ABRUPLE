namespace Abruple.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum EntryType
    {
        [Display(Name = "Everyone")]
        Open,
        [Display(Name = "Choose certain users")]
        Close
    }
}

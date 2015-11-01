namespace Abruple.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum RewardStrategy
    {
        [Display(Name = "Just one")]
        Single,
        [Display(Name = "TOP N participants")]
        MultipleWinners
    }
}

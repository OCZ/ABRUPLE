namespace Abruple.App.Models.BindingModels.ContestEntry
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class NewContestEntryBindingModel
    {
        [Required]
        [Display(Name = "Entry Title")]
        [StringLength(50, ErrorMessage = "{0} must be betweet {2} and {1} characters long.", MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image File")]
        public HttpPostedFileBase ImageInputFile { get; set; }

        [Required]
        public int ContestId { get; set; }
    }
}
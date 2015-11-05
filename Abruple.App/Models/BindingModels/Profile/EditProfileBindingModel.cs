using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Abruple.Models.Enums;

namespace Abruple.App.Models.BindingModels.Profile
{
    public class EditProfileBindingModel
    {
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "About me")]
        public string AboutMe { get; set; }
    }
}
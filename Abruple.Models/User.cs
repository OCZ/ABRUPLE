namespace Abruple.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Contest> _contestsCreated;

        public User()
        {
            this._contestsCreated = new HashSet<Contest>();
        }

        public virtual ICollection<Contest> ContestsCreated
        {
            get { return this._contestsCreated; }
            set { this._contestsCreated = value; }
        }



        // GENERATE IDENTITY
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            return userIdentity;
        }




    }
}

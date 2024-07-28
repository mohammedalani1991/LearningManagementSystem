using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            SystemGroupUsers = new HashSet<SystemGroupUser>();
            UserProfileTranslations = new HashSet<UserProfileTranslation>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public int? ContactId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? LastLogin { get; set; }
        public string ProfilePhoto { get; set; }
        public int? PreferedLanguageId { get; set; }
        public DateTime? StartWorkDate { get; set; }
        public int? JobId { get; set; }
        public int? EmployeeColorId { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual ICollection<SystemGroupUser> SystemGroupUsers { get; set; }
        public virtual ICollection<UserProfileTranslation> UserProfileTranslations { get; set; }
    }
}

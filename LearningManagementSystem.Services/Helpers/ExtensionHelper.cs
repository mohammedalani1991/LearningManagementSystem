using DataEntity.Models.EfModels;
using LearningManagementSystem.Core.SystemEnums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningManagementSystem.Services.Helpers
{
    public static class ExtensionHelper
    {
        public static string GetFullNameByEmail(this string email, int? languageId = 0)
        {
            using var db = new LearningManagementSystemContext();
            var contact = db.UserProfiles.Include(x => x.Contact.ContactTranslations)
                .FirstOrDefault(x =>
                    x.Username == email && x.Status != (int)GeneralEnums.StatusEnum.Deleted)
                ?.Contact;
            if (contact == null)
            {
                return email;
            }
            if (languageId == CultureHelper.GetDefaultLanguageId())
            {
                return contact.FullName;
            }
            var trans = contact.ContactTranslations.FirstOrDefault(r => r.LanguageId == languageId);
            if (trans != null)
                contact.FullName = trans.FullName;
            return contact.FullName;
        }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace DataEntity.Models.EfModels
{
    public partial class GroupUpdater
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
        public bool IsUpdater { get; set; }
        public bool IsOwner { get; set; }

        public virtual SystemGroup Group { get; set; }
    }
}

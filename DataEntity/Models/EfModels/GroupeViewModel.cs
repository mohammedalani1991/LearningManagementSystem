using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntity.Models.EfModels
{
    public class GroupeViewModel
    {
        public int Id { get; set; }
        public List<int> UpdaterIds { get; set; }
        public int GroupId { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
        public bool IsUpdater { get; set; }
        public bool IsOwner { get; set; }
    }
}

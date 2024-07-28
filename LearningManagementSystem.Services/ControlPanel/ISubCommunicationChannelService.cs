using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;


namespace LearningManagementSystem.Services.ControlPanel
{
 public interface ISubCommunicationChannelService
    {
        IPagedList<SubCommunicationChannel> GetSubCommunicationChannels(int communicationChannelId, int languageId, string searchText, int? page, int pagination);
        void AddSubCommunicationChannel(SubCommunicationChannelViewModel subCommunicationChannelViewModel);
        List<SubCommunicationChannelViewModel> GetSubCommunicationChannels(int communicationChannelId, int languageId, int? parentId);
        SubCommunicationChannel GetSubCommunicationChannelById(int id);
        SubCommunicationChannelViewModel GetSubCommunicationChannelById(int id, int languageId);
        void EditSubCommunicationChannel(SubCommunicationChannelViewModel subCommunicationChannelViewModel, SubCommunicationChannel subCommunicationChannel);
        void DeleteSubCommunicationChannel(SubCommunicationChannel subCommunicationChannel);
    }
}

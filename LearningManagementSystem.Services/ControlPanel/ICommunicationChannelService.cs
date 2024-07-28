using DataEntity.Models.EfModels;
using DataEntity.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using X.PagedList;

namespace LearningManagementSystem.Services.ControlPanel
{
  public interface ICommunicationChannelService
    {
        IPagedList<CommunicationChannel> GetCommunicationChannels(string searchText, int? page, int languageId, int pagination);
        void AddCommunicationChannel(CommunicationChannelViewModel communicationChannelViewModel);
        CommunicationChannel GetCommunicationChannelById(int id);
        CommunicationChannelViewModel GetCommunicationChannelById(int id, int languageId);
        void EditCommunicationChannel(CommunicationChannelViewModel CommunicationChannelViewModel, CommunicationChannel CommunicationChannel);
        void DeleteCommunicationChannel(CommunicationChannel master);

    }
}

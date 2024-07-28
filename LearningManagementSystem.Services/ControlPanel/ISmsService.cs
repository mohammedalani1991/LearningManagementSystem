
using DataEntity.Models.ViewModels;
using System.Threading.Tasks;

namespace LearningManagementSystem.Services.ControlPanel
{
    public interface ISmsService
    {
        bool SendSms(MessageViewModel message);
        Task<bool> SendEmail(MessageViewModel messageViewModel);
        //Task SendEmailUsingSendGrid(MessageViewModel message);
    }
}

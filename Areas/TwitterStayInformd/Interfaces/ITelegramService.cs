using System.Threading.Tasks;

namespace HomeAutomation.Areas.TwitterStayInformd.Interfaces
{
    public interface ITelegramService
    {
         Task<object> SendMessage(string bodId, string chatId, string text);
    }
}
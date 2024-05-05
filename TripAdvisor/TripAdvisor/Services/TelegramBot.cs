

using TripAdvisor.Models;

namespace TripAdvisor.Services
{
    public class TelegramBot
    {
        public string apikey { get; set; }
        public string chatid { get; set; }
        public string boturl { get; set; }
        public TelegramBot(string apikey, string chatid)
        {
            this.apikey = apikey;
            this.chatid = chatid;
            boturl = "https://api.telegram.org/bot";
        }

        public async void SendMessageAsync(string message)
        {
            HttpClient cliente = new HttpClient();
            Uri uri = new Uri("https://api.telegram.org/bot7137023147:AAGza7NQA0T9PkO3kEUnFoyI9JjJNzv-vys/sendMessage?chat_id=-4173595237&text="+message);
            var response = await cliente.GetAsync(uri);
        }
        public async void NewUser(RegistrationUser user)
        {
            HttpClient cliente = new HttpClient();
            string message = "Nuovo Utente registrato!" + " Nome:" + user.name + " Classe:" + user.classid;
            Uri uri = new Uri(boturl + apikey +"/sendMessage?chat_id="+chatid+"&text=" + message);
            var response = await cliente.GetAsync(uri);
        }
        public async void NewUser(RegistrationUser user)
        {
            HttpClient cliente = new HttpClient();
            string message = "Nuovo Utente registrato!" + " Nome:" + user.name + " Classe:" + user.classid;
            Uri uri = new Uri(boturl + apikey + "/sendMessage?chat_id=" + chatid + "&text=" + message);
            var response = await cliente.GetAsync(uri);
        }

    }
}

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorBattles.Client.Services
{
    public class BananaService : IBananaService
    {
        private readonly HttpClient _http;

        public event Action OnChange;

        public BananaService(HttpClient http)
        {
            _http = http;
        }

        public int Bananas { get; set; } = 0;

        public void EatBananas(int amount)
        {
            Bananas -= amount;
            BananasChanged();
        }

        private void BananasChanged() => OnChange.Invoke();

        public async Task AddBananas(int amount)
        {
            var result = await _http.PutAsJsonAsync<int>("api/user/addbananas", amount);
            Bananas = await result.Content.ReadFromJsonAsync<int>();

            BananasChanged();
        }

        public async Task GetBananas()
        {
            Bananas = await _http.GetFromJsonAsync<int>("api/user/getbananas");
            BananasChanged();
        }
    }
}
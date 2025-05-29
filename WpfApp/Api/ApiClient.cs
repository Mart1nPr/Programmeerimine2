using System.Net.Http;
using System.Net.Http.Json;

namespace WpfApp.Api
{
    class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task<List<User>> List()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("Users");
        }

        public async Task Save(User user)
        {
            if (user.Id == 0)
                await _httpClient.PostAsJsonAsync("Users", user);
            else
                await _httpClient.PutAsJsonAsync($"Users/{user.Id}", user);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"Users/{id}");
        }
    }
}

using System.Net.Http;
using System.Net.Http.Json;

namespace WpfApp.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task<Result<List<User>>> List()
        {
            try
            {
                var users = await _httpClient.GetFromJsonAsync<List<User>>("Users");
                return new Result<List<User>> { Value = users };
            }
            catch (Exception ex)
            {
                return new Result<List<User>> { Error = ex.Message };
            }
        }

        public async Task<Result> Save(User user)
        {
            try
            {
                HttpResponseMessage response;

                if (user.Id == 0)
                    response = await _httpClient.PostAsJsonAsync("Users", user);
                else
                    response = await _httpClient.PutAsJsonAsync($"Users/{user.Id}", user);

                if (!response.IsSuccessStatusCode)
                    return new Result { Error = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}" };

                return new Result();
            }
            catch (Exception ex)
            {
                return new Result { Error = ex.Message };
            }
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Users/{id}");

                if (!response.IsSuccessStatusCode)
                    return new Result { Error = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}" };

                return new Result();
            }
            catch (Exception ex)
            {
                return new Result { Error = ex.Message };
            }
        }
    }
}

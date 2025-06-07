using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KooliProjekt.PublicAPI.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7136/api/")
            };
        }

        public async Task<Result<List<User>>> List()
        {
            try
            {
                var users = await _httpClient.GetFromJsonAsync<List<User>>("Users");
                return Result<List<User>>.Success(users);
            }
            catch (Exception ex)
            {
                return Result<List<User>>.Failure(ex.Message);
            }
        }

        public async Task<Result<User>> Get(int id)
        {
            try
            {
                var user = await _httpClient.GetFromJsonAsync<User>($"Users/{id}");

                if (user == null)
                    return Result<User>.Failure("User not found");

                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                return Result<User>.Failure(ex.Message);
            }
        }

        public async Task<Result> Save(User user)
        {
            HttpResponseMessage response;

            try
            {
                if (user.Id == 0)
                    response = await _httpClient.PostAsJsonAsync("Users", user);
                else
                    response = await _httpClient.PutAsJsonAsync($"Users/{user.Id}", user);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResult = await response.Content.ReadFromJsonAsync<Result>();
                    return errorResult ?? Result.Failure($"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Users/{id}");

                if (!response.IsSuccessStatusCode)
                    return Result.Failure($"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}");

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }
    }
}

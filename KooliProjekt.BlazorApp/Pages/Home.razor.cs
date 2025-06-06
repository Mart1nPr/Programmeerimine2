using KooliProjekt.PublicAPI.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KooliProjekt.BlazorApp.Pages
{
    public partial class Home
    {
        [Inject]
        protected IApiClient ApiClient { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        protected NavigationManager Navigation { get; set; }

        private List<User> users;

        protected override async Task OnInitializedAsync()
        {
            var result = await ApiClient.List();

            users = result.HasError ? new List<User>() : result.Value;
        }

        protected async Task Delete(int id)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?");
            if (!confirmed) return;

            await ApiClient.Delete(id);
            var result = await ApiClient.List();
            if (!result.HasError)
            {
                users = result.Value;
            }
        }

        protected void NavigateToEdit(int id)
        {
            Navigation.NavigateTo($"/users/edit/{id}");
        }

        protected void NavigateToAdd()
        {
            Navigation.NavigateTo("/users/add");
        }
    }
}

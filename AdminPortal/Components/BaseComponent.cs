using AdminPortal.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using Shared.Models.AutoWrapperResponses;

namespace AdminPortal.Components;

public abstract class BaseComponent : ComponentBase
{
    [Inject] protected HttpService HttpService { get; set; }
    [Inject] protected IDialogService DialogService { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }
    [Inject] protected NavigationManager NavigationManager { get; set; }

    protected async Task<T> ExecuteHttpService<T>(Func<HttpService, Task<HttpResponseMessage?>> service)
    {
        var response = await service.Invoke(HttpService);

        if (response is not null)
        {
            if (response.IsSuccessStatusCode)
                return GetValueFromResponse<T>(await response.Content.ReadAsStringAsync());

            Snackbar.Add(await response.Content.ReadAsStringAsync(), Severity.Error);
        }


        Type? responseType = typeof(T);
        if (responseType == typeof(string))
            return (T)Convert.ChangeType("", typeof(T));
        if (responseType == typeof(bool?))
            return (T)Convert.ChangeType(false, typeof(T));
        if (responseType == typeof(int?))
            return (T)Convert.ChangeType(0, typeof(T));
        if (responseType == typeof(Guid?))
            return (T)Convert.ChangeType(Guid.Empty, typeof(T));
        return Activator.CreateInstance<T>();
    }

    private T GetValueFromResponse<T>(string responseContent)
    {


        var responseModel = JsonConvert.DeserializeObject<AutoWrapperResponseModel>(responseContent);
        if (responseModel is not null)
        {
            if (responseModel is { IsError: false, IsValidationError: false })
            {
                var result = JsonConvert.DeserializeObject<T>(responseModel.Result);
                if(result is not null) return result;
            }
        }
            
        Type? responseType = typeof(T);

        if (responseType == typeof(string) || 
            responseType == typeof(bool) ||
            responseType == typeof(int) ||
            responseType == typeof(Guid))
            return (T)Convert.ChangeType(responseContent, typeof(T));
            
        return Activator.CreateInstance<T>();
    }
    
    protected void ToastSuccess(string message) => Snackbar.Add(message, Severity.Success);
    protected void ToastWarning(string message) => Snackbar.Add(message, Severity.Warning);
}
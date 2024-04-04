namespace AppUI.Services.Interfaces
{
    internal abstract class HttpClientBase : IHttpClientService
    {
        public abstract Task<T> GetAsync<T>(string uri);
    }
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string uri);
    }

}

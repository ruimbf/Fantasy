using System.Net;

namespace Fantasy.Frontend.Repositories;

public class HttpResponseWrapper<T>
{
    public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
    {
        Response = response;
        Error = error;
        HttpResponseMessage = httpResponseMessage;
    }

    public T? Response { get; }
    public bool Error { get; }
    public HttpResponseMessage HttpResponseMessage { get; }

    public async Task<string?> GetErrorMessageAsync()
    {
        if (!Error)
        {
            return null;
        }

        var statusCode = HttpResponseMessage.StatusCode;
        if (statusCode == HttpStatusCode.NotFound)
        {
            return "Recurso não encontrado!";
        }
        if (statusCode == HttpStatusCode.BadRequest)
        {
            return await HttpResponseMessage.Content.ReadAsStringAsync();
        }
        if (statusCode == HttpStatusCode.Unauthorized)
        {
            return "É necessário estar autenticado para executar esta operação!";
        }
        if (statusCode == HttpStatusCode.Forbidden)
        {
            return "Não tem permissão para executar esta operação!";
        }

        return "Erro inesperado!";
    }
}
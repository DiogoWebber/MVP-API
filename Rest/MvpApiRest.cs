using System.Dynamic;
using System.Text.Json;
using mvpAPI.Dtos;
using mvpAPI.Interfaces;
using mvpAPI.Model;

namespace mvpAPI.Rest
{
    public class MvpApiRest : IMvpApi
    {
        public async Task<ResponseGenerico<List<CepimModel>>> BuscarCepimPorCnpj(string cnpj)
        {
            var apiKey = "7c137febe8f79a03ffe7a437026f0e05"; 
            var requestUri =
                $"https://api.portaldatransparencia.gov.br/api-de-dados/cepim?cnpjSancionado={cnpj}&pagina=1";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Add("chave-api-dados", apiKey);

            var response = new ResponseGenerico<List<CepimModel>>();

            using (var client = new HttpClient())
            {
                try
                {
                    var responseBrasilApi = await client.SendAsync(request);
                    var contentResp = await responseBrasilApi.Content.ReadAsStringAsync();


                    if (responseBrasilApi.IsSuccessStatusCode)
                    {
                        try
                        {
                            // Desserializa a resposta como uma lista de CepimModel
                            var objResponse = JsonSerializer.Deserialize<List<CepimModel>>(contentResp);

                            response.CodigoHttp = responseBrasilApi.StatusCode;
                            response.DadosRetorno = objResponse;
                        }
                        catch (JsonException jsonEx)
                        {
                            Console.WriteLine("JSON Deserialization error: " + jsonEx.Message);
                            dynamic erroRetorno = new ExpandoObject();
                            erroRetorno.message = "Erro ao desserializar a resposta.";
                            response.ErroRetorno = erroRetorno;
                        }
                    }
                    else
                    {
                        dynamic erroRetorno = new ExpandoObject();
                        erroRetorno.message = "Erro na API: " + contentResp;
                        response.CodigoHttp = responseBrasilApi.StatusCode;
                        response.ErroRetorno = erroRetorno;
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    Console.WriteLine("HTTP Request error: " + httpEx.Message);
                    response.CodigoHttp = System.Net.HttpStatusCode.ServiceUnavailable;
                    dynamic erroRetorno = new ExpandoObject();
                    erroRetorno.message = "Erro na solicitação HTTP.";
                    response.ErroRetorno = erroRetorno;
                }
            }

            return response;
        }
       public async Task<ResponseGenerico<List<PepsModel>>> BuscarPepsPorCpf(string cpf)
{
    var apiKey = "7c137febe8f79a03ffe7a437026f0e05";
    var requestUri = $"https://api.portaldatransparencia.gov.br/api-de-dados/peps?cpf={cpf}&pagina=1";

    var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
    request.Headers.Add("chave-api-dados", apiKey);

    var response = new ResponseGenerico<List<PepsModel>>();

    using (var client = new HttpClient())
    {
        try
        {
            var responseBrasilApi = await client.SendAsync(request);
            var contentResp = await responseBrasilApi.Content.ReadAsStringAsync();

            if (responseBrasilApi.IsSuccessStatusCode)
            {
                try
                {
                    var objResponse = JsonSerializer.Deserialize<List<PepsModel>>(contentResp);

                    response.CodigoHttp = responseBrasilApi.StatusCode;
                    response.DadosRetorno = objResponse;
                }
                catch (JsonException jsonEx)
                {
                    Console.WriteLine("JSON Deserialization error: " + jsonEx.Message);
                    dynamic erroRetorno = new ExpandoObject();
                    erroRetorno.message = "Erro ao desserializar a resposta.";
                    response.ErroRetorno = erroRetorno;
                }
            }
            else
            {
                dynamic erroRetorno = new ExpandoObject();
                erroRetorno.message = "Erro na API: " + contentResp;
                response.CodigoHttp = responseBrasilApi.StatusCode;
                response.ErroRetorno = erroRetorno;
            }
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine("HTTP Request error: " + httpEx.Message);
            response.CodigoHttp = System.Net.HttpStatusCode.ServiceUnavailable;
            dynamic erroRetorno = new ExpandoObject();
            erroRetorno.message = "Erro na solicitação HTTP.";
            response.ErroRetorno = erroRetorno;
        }
    }

    return response;
}


    }
}


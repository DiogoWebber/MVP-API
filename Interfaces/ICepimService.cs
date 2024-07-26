using IntegraBrasilApi.Dtos;

namespace IntegraBrasilApi.Interfaces;

public interface ICepimService
{
    Task<ResponseGenerico<List<CepimResponse>>> BuscarCepim(string cnpj);
}
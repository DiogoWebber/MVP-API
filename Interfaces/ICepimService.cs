using mvpAPI.Dtos;

namespace mvpAPI.Interfaces;

public interface ICepimService
{
    Task<ResponseGenerico<List<CepimResponse>>> BuscarCepim(string cnpj);
}
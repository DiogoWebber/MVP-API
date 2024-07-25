using IntegraBrasilApi.Dtos;
using IntegraBrasilApi.Model;

namespace IntegraBrasilApi.Interfaces{

public interface IBrasilApi
{
    Task<ResponseGenerico<List<CepimModel>>> BuscarCepimPorCnpj(string cnpj);

    
}}
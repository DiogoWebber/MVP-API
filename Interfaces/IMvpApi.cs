using mvpAPI.Dtos;
using mvpAPI.Model;

namespace mvpAPI.Interfaces{

public interface IMvpApi
{
    Task<ResponseGenerico<List<CepimModel>>> BuscarCepimPorCnpj(string cnpj);
    Task<ResponseGenerico<List<PepsModel>>> BuscarPepsPorCpf(string cpf);

    
}}
using AutoMapper;
using mvpAPI.Dtos;
using mvpAPI.Interfaces;

namespace mvpAPI.Services;

public class CepimService : ICepimService
{
    private readonly IMapper _mapper;
    private readonly IMvpApi _brasilApi;
    
    public CepimService(IMapper mapper, IMvpApi brasilApi)
    {
        _mapper = mapper;
        _brasilApi = brasilApi;
    }
    public async Task<ResponseGenerico<List<CepimResponse>>> BuscarCepim(string cnpj)
    {
        var cepim = await _brasilApi.BuscarCepimPorCnpj(cnpj);
        return _mapper.Map<ResponseGenerico<List<CepimResponse>>>(cepim);
    }
}
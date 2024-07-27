using AutoMapper;
using mvpAPI.Dtos;
using mvpAPI.Interfaces;

namespace mvpAPI.Services;

public class PepsService :IPepsService
{
    private readonly IMapper _mapper;
    private readonly IMvpApi _brasilApi;
    
    public PepsService(IMapper mapper, IMvpApi brasilApi)
    {
        _mapper = mapper;
        _brasilApi = brasilApi;
    }
    public async Task<ResponseGenerico<List<PepsResponse>>> BuscarPeps(string cpf)
    {
        var peps = await _brasilApi.BuscarPepsPorCpf(cpf);
        return _mapper.Map<ResponseGenerico<List<PepsResponse>>>(peps);
    }
}
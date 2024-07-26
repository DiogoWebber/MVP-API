using AutoMapper;
using IntegraBrasilApi.Dtos;
using IntegraBrasilApi.Interfaces;

namespace IntegraBrasilApi.Services;

public class PepsService :IPepsService
{
    private readonly IMapper _mapper;
    private readonly IBrasilApi _brasilApi;
    
    public PepsService(IMapper mapper, IBrasilApi brasilApi)
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
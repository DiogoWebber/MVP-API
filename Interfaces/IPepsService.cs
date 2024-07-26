using IntegraBrasilApi.Dtos;

namespace IntegraBrasilApi.Interfaces;

public interface IPepsService
{
    Task<ResponseGenerico<List<PepsResponse>>> BuscarPeps(string cpf);

}
using mvpAPI.Dtos;

namespace mvpAPI.Interfaces;

public interface IPepsService
{
    Task<ResponseGenerico<List<PepsResponse>>> BuscarPeps(string cpf);

}
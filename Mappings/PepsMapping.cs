using AutoMapper;
using IntegraBrasilApi.Dtos;
using IntegraBrasilApi.Model;


namespace IntegraBrasilApi.Mappings;

public class PepsMapping : Profile
{
    public PepsMapping(){
        CreateMap<PepsModel, PepsResponse>();
        
        
        CreateMap<ResponseGenerico<List<PepsModel>>, ResponseGenerico<List<PepsResponse>>>()
            .ForMember(dest => dest.DadosRetorno, opt => opt.MapFrom(src => src.DadosRetorno));
    }
}

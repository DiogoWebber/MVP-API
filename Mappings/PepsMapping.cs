using AutoMapper;
using mvpAPI.Dtos;
using mvpAPI.Model;


namespace mvpAPI.Mappings;

public class PepsMapping : Profile
{
    public PepsMapping(){
        CreateMap<PepsModel, PepsResponse>();
        
        
        CreateMap<ResponseGenerico<List<PepsModel>>, ResponseGenerico<List<PepsResponse>>>()
            .ForMember(dest => dest.DadosRetorno, opt => opt.MapFrom(src => src.DadosRetorno));
    }
}

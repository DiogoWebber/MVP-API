using AutoMapper;
using IntegraBrasilApi.Dto;
using IntegraBrasilApi.Dtos;
using IntegraBrasilApi.Model;

namespace IntegraBrasilApi.Mappings;

public class CepimMapping : Profile
{
    public CepimMapping(){
        CreateMap<CepimModel, CepimResponse>();
        CreateMap<OrgaoSuperior, OrgaoSuperiorResponse>();
        CreateMap<OrgaoMaximo, OrgaoMaximoResponse>();
        CreateMap<PessoaJuridica, PessoaJuridicaResponse>();
        CreateMap<Convenio, ConvenioResponse>();
        
        CreateMap<ResponseGenerico<List<CepimModel>>, ResponseGenerico<List<CepimResponse>>>()
            .ForMember(dest => dest.DadosRetorno, opt => opt.MapFrom(src => src.DadosRetorno));
    }
}
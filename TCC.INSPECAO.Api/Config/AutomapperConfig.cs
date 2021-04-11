using System.Linq;
using AutoMapper;
using TCC.INSPECAO.Domain.Entity;
using TCC.INSPECAO.Domain.Models.Response;
using System.Collections.Generic;
using TCC.INSPECAO.Domain.Models.Response.Relatorio;
using TCC.INSPECAO.Domain.Models.Response.Claim;

namespace TCC.INSPECAO.Api.Config
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Inspecao, InspecaoResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DataHoraInicio, opt => opt.MapFrom(src => src.DataHoraInicio))
                .ForMember(dest => dest.Observacao, opt => opt.MapFrom(src => src.Observacao))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.InspecaoStatus.Nome));

            CreateMap<Usuario, UsuarioResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Estabelecimento, opt => opt.MapFrom(src => src.Estabelecimento.Nome))
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo))
                .ForMember(dest => dest.Claims, opt => opt.MapFrom(src => src.UsuarioClaims.Select(x => x.Claim).ToList()));

            CreateMap<List<UsuarioClaims>, UsuarioResponse>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Select(x => x.Usuario.Email).FirstOrDefault()))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Select(x => x.Usuario.Nome).FirstOrDefault()))
                .ForMember(dest => dest.Estabelecimento, opt => opt.MapFrom(src => src.Select(x => x.Usuario.Estabelecimento.Nome).FirstOrDefault()))
                .ForMember(dest => dest.Claims, opt => opt.MapFrom(src => src.Select(x => x.Claim).ToList()));

            CreateMap<Estabelecimento, EstabelecimentoResponse>().ReverseMap();

            CreateMap<SistemaItem, ItensSistemaResponse>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.NumeroOrdem, opt => opt.MapFrom(src => src.NumeroOrdem))
                .ForMember(dest => dest.IdUnidadeMedida, opt => opt.MapFrom(src => src.UnidadeMedida.Id))
                .ForMember(dest => dest.NomeUnidadeMedida, opt => opt.MapFrom(src => src.UnidadeMedida.Nome))
                .ForMember(dest => dest.TipoDado, opt => opt.MapFrom(src => src.UnidadeMedida.TipoDado))
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo));

            CreateMap<Sistema, SistemaResponse>().ReverseMap();

            CreateMap<UnidadeMedida, UnidadeMedidaResponse>().ReverseMap();

            CreateMap<Sistema, SistemaDetalheResponse>()
                .ForMember(dest => dest.ItensSistema, opt => opt.MapFrom(src => src.SistemaItens.ToList().OrderBy(x => x.NumeroOrdem)));

            CreateMap<Inspecao, RelatorioInspecaoResponse>()
                .ForMember(dest => dest.IdInspecao, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario.Nome));

            CreateMap<Claims, ClaimResponse>().ReverseMap();

        }
    }
}
﻿using AutoMapper;
using SM.Application.DTOs;
using SM.Domaiin.Entities;

namespace SM.Application.Mapping
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            // Mapeamento de Cliente
            CreateMap<ClienteCreateDto, Cliente>()
                .ForMember(dest => dest.EnderecoComplemento, opt => opt.MapFrom(src => src.EnderecoComplementoCreateDto));

            CreateMap<Cliente, ClienteDto>()
            .ForMember(dest => dest.EnderecoComplementoDto, opt => opt.MapFrom(src => src.EnderecoComplemento))
            .ForMember(dest => dest.ServicosDto, opt => opt.MapFrom(src => src.Servicos))
            .ForMember(dest => dest.ServicosDto, opt => opt.Ignore());


            CreateMap<ClienteSemEnderecoDto, Cliente>();
            CreateMap<EnderecoComplementoDto, EnderecoComplemento>();

            CreateMap<EnderecoComplemento, EnderecoComplementoDto>()
                 .ForMember(dest => dest.EnderecoDto, opt => opt.MapFrom(src => src.Endereco))
                 .ForMember(dest => dest.ClienteDto, opt => opt.Ignore())
                 .ForMember(dest => dest.TecnicoDto, opt => opt.Ignore());

            // Mapeamento de EnderecoComplemento
            CreateMap<EnderecoComplementoCreateDto, EnderecoComplemento>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco))
                .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.Complemento));

            // Mapeamento de Endereco
            CreateMap<Endereco, EnderecoDto>();

            // Mapeamento de Tecnico
            CreateMap<TecnicoCreateDto, Tecnico>()
                .ForMember(dest => dest.EnderecoComplemento, opt => opt.MapFrom(src => src.EnderecoComplementoCreateDto));

            CreateMap<Tecnico, TecnicoDto>()
                .ForMember(dest => dest.EnderecoComplementoDto, opt => opt.MapFrom(src => src.EnderecoComplemento));

            CreateMap<TecnicoDto, Tecnico>();

            // Mapeamento de Servico
            CreateMap<Servicos, ServicosDto>()
               .ForMember(dest => dest.Tecnicos, opt => opt.MapFrom(src => src.servicoTecnicos))
               .ForMember(dest => dest.ClienteDto, opt => opt.MapFrom(src => src.Cliente))
               .ReverseMap();

            // ServicoTecnico mapping
            CreateMap<ServicoTecnico, ServicoTecnicoDto>()
                .ForMember(dest => dest.TecnicoDto, opt => opt.MapFrom(src => src.Tecnico))
                .ReverseMap()
                .ForPath(src => src.Servico, opt => opt.Ignore());

        }
    }
}
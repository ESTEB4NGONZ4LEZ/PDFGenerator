
using API.Dto.Cliente;
using API.Dto.Compra;
using API.Dto.Producto;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Cliente, ClientexCompraDto>().ReverseMap();

            CreateMap<Compra, CompraDto>().ReverseMap();

            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Producto, ProductoxCompraDto>().ReverseMap();
        }
    }
}
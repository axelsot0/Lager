using AutoMapper;
using Entidades.Dtos.Entity;
using Entidades.Entity;

namespace Servicio.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Producto 
            CreateMap<Producto, ProductDTO>()
                .ReverseMap();


            #endregion


        }
    }
}

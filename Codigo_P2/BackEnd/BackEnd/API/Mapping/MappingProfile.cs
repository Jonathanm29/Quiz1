using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using data = DAL.DO.Objects;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // Representa el casting del objeto a el similar 
            // Desde DO hacia DataModels 
            // Domain to Resource

            CreateMap<data.Orders, DataModels.Orders>().ReverseMap();

            CreateMap<data.Customers, DataModels.Customers>().ReverseMap();

            CreateMap<data.Stores, DataModels.Stores>().ReverseMap();

            CreateMap<data.Staffs, DataModels.Staffs>().ReverseMap();

            CreateMap<data.Stocks, DataModels.Stocks>().ReverseMap();
        }
    }
}

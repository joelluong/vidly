using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.App_Start
{
    using AutoMapper;

    using Vidly.Dtos;
    using Vidly.Models;

    /*
     * We need to create a mapping profile which determines how objects of different types can be mapped to each other
     */
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // DTO to Domain
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<MovieDto, Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            
            // Domain to DTO
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Genre, GenreDto>();
            /*
             * when we call this create map method
             * Auto Mapper uses reflection to scan these types it finds their properties and maps them based on their name
             *
             * -> Convention based mapping tool
             */
        }
    }
}
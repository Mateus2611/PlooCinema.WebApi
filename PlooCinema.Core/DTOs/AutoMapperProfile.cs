using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlooCinema.Core.Models;

namespace PlooCinema.Core.DTOs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GenreDTO, Genre>()
                .ReverseMap();
            
            CreateMap<CreateMovieDTO, Movie>()
                .ReverseMap();

            CreateMap<UpdateMovieDTO, Movie>()
                .ReverseMap();

            CreateMap<RoomDTO, Room>()
                .ReverseMap();

            CreateMap<SessionDTO, Session>()
                .ReverseMap();
        }
    }
}
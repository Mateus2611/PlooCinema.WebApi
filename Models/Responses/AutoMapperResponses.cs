using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PlooCinema.WebApi.Model;

namespace PlooCinema.WebApi.Models.Responses
{
    public class AutoMapperResponses : Profile
    {
        public AutoMapperResponses()
        {
            CreateMap<GenreResponse, Genre>()
                .ReverseMap();
            
            CreateMap<GetMovieResponse, Movie>()
                .ReverseMap();
            
            CreateMap<RoomResponse, Room>()
                .ReverseMap();
        }
    }
}
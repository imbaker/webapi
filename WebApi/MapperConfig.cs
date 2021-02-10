using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;
using WebApi.Models.Dto;
using WebApi.Models.Mappers;

namespace WebApi
{
    public static class MapperConfig
    {
        public static IMapper Mapper { get; set; }

        public static void Configure()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PolicyProfile>());
            Mapper = config.CreateMapper();
        }
    }
}
using AutoMapper;
using DataAccesLayer.Models;
using Medical_Claim.DataAccess;
using Medical_Claim.Models;
using System;

namespace DataAccesLayer
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Claims, ClaimsDTO>().ReverseMap();
            CreateMap<Claims, ClaimsCreateDTO>().ReverseMap();
            CreateMap<PolicyHolder,PolicyHolder1DTO>().ReverseMap();
        }

    }
}
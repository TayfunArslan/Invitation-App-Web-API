using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Invitation_App_Web_API.Data.Entities;
using Invitation_App_Web_API.Models.ViewModels;

namespace Invitation_App_Web_API.Util
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<Organization, OrganizationViewModel>();
            CreateMap<OrganizationViewModel, Organization>();
            CreateMap<Guest, GuestViewModel>();
            CreateMap<GuestViewModel, Guest>();
        }
    }
}

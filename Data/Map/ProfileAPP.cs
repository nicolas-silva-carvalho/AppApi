using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAPP.Models;
using AutoMapper;

namespace ApiAPP.Data.Map
{
    public class ProfileAPP : Profile
    {
        public ProfileAPP()
        {
            CreateMap<UsuarioLogin, Usuario>();
        }
    }
}
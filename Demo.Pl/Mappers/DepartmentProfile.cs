﻿using AutoMapper;
using Demo.DAL.Models;
using Demo.Pl.Models;

namespace Demo.Pl.Mappers
{
    public class DepartmentProfile :Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department,DepartmentViewModel>().ReverseMap();
        }
    }
}

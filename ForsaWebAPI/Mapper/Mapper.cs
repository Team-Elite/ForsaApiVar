using AutoMapper;
using ForsaWebAPI.Models;
using ForsaWebAPI.Perrsistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI
{
    public  class Mapping: Profile
    {
      ///  private ForsaEntities _forsaEntities { get; set; }

        public void Initialize()
        {
            Mapper.Initialize(cnfg=>cnfg.CreateMap<USP_ValidateUser_Result, UserModel>());
            Mapper.Initialize(cnfg => cnfg.CreateMap<USP_GetMaturityList_Result, MaturityModel>());
        }

    }
}
﻿using Motocomplex.DTOs.BrandDTOs;
using Motocomplex.DTOs.ModelDTOs;

namespace Motocomplex.DTOs.BrandModelDTOs
{
    public class BrandModelDisplayDto
    {
        public BrandDisplayDto Brand { get; set; }
        public List<ModelDetailsDto> Models { get; set; }
    }
}
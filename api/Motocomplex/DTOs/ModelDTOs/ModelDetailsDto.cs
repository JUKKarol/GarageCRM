﻿namespace Motocomplex.DTOs.ModelDTOs
{
    public class ModelDetailsDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid brandId { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
    }
}
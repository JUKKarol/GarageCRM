﻿namespace Motocomplex.DTOs.CarDTOs
{
    public class CarCreateDto
    {
        public int Engine { get; set; }
        public string RegistrationNumber { get; set; }
        public string Vin { get; set; }
        public int yearOfProduction { get; set; }
        public Guid ModelId { get; set; }
    }
}

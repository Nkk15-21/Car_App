using CarApp.Core.Domain;
using CarApp.Core.Dto;
using CarApp.Core.ServiceInterface;
using CarApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CarApp.ApplicationServices.Services
{
    public class CarService : ICarService
    {
        private readonly CarAppDbContext _context;

        public CarService(CarAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarDto>> GetAllAsync()
        {
            return await _context.Cars
                .Select(car => new CarDto
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    Year = car.Year,
                    Color = car.Color,
                    Mileage = car.Mileage,
                    CreatedAt = car.CreatedAt,
                    ModifiedAt = car.ModifiedAt
                })
                .ToListAsync();
        }

        public async Task<CarDto?> GetByIdAsync(Guid id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (car == null)
                return null;

            return new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                Mileage = car.Mileage,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };
        }

        public async Task<CarDto> CreateAsync(CarDto dto)
        {
            var car = new Car
            {
                Id = Guid.NewGuid(),
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                Color = dto.Color,
                Mileage = dto.Mileage,
                CreatedAt = DateTime.UtcNow
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            dto.Id = car.Id;
            dto.CreatedAt = car.CreatedAt;

            return dto;
        }

        public async Task<CarDto> UpdateAsync(CarDto dto)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (car == null)
                throw new Exception("Car not found");

            car.Brand = dto.Brand;
            car.Model = dto.Model;
            car.Year = dto.Year;
            car.Color = dto.Color;
            car.Mileage = dto.Mileage;
            car.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            dto.ModifiedAt = car.ModifiedAt;
            return dto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (car == null)
                return false;

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

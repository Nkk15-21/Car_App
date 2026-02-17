using Car_App.ApplicationServices.Services;
using Car_App.Core.Dto;
using Car_App.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Car_App.CarTest
{
    public class CarServiceTests
    {
        private CarService GetService()
        {
            var options = new DbContextOptionsBuilder<Car_AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new Car_AppDbContext(options);
            return new CarService(context);
        }

        [Fact]
        public async Task CreateAsync_AddsCar()
        {
            var service = GetService();

            var car = new CarDto
            {
                Brand = "BMW",
                Model = "X5",
                Year = 2020,
                Color = "Black",
                Mileage = 50000
            };

            var result = await service.CreateAsync(car);

            Assert.NotNull(result.Id);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsCars()
        {
            var service = GetService();
            await service.CreateAsync(new CarDto { Brand = "Audi" });

            var cars = await service.GetAllAsync();

            Assert.Single(cars);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesCar()
        {
            var service = GetService();
            var car = await service.CreateAsync(new CarDto { Brand = "Toyota" });

            car.Brand = "Honda";
            await service.UpdateAsync(car);

            var updated = await service.GetByIdAsync(car.Id!.Value);

            Assert.Equal("Honda", updated!.Brand);
        }

        [Fact]
        public async Task DeleteAsync_RemovesCar()
        {
            var service = GetService();
            var car = await service.CreateAsync(new CarDto { Brand = "Mazda" });

            var result = await service.DeleteAsync(car.Id!.Value);

            Assert.True(result);
        }
    }
}

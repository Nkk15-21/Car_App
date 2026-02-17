using Car_App.Core.Dto;

namespace Car_App.Core.ServiceInterface
{
    public interface ICarService
    {
        Task<CarDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<CarDto>> GetAllAsync();
        Task<CarDto> CreateAsync(CarDto dto);
        Task<CarDto> UpdateAsync(CarDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

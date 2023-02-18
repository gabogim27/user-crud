namespace UserCrud.Domain.Services
{
    using UserCrud.Domain.Common;
    using UserCrud.Domain.DTOs;
    
    public interface IUserService
    {
        Task<UserDto> AddUser(UserDto userDto);

        Task<UserDto> UpdateUser(UserDto userDto);

        Task<UserDto> DeleteUser(int userId);

        Task<UserDto> GetUser(int userId);

        Task<IEnumerable<UserDto>> GetUsers(Pagination pagination);
    }
}

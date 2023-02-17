namespace UserCrud.Application.Services
{
    using UserCrud.Domain.Common;
    using UserCrud.Domain.DTOs;
    using UserCrud.Domain.Services;
    
    public class UserService : IUserService
    {
        public Task<UserDto> AddUser(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetUsers(Pagination<UserDto> pagination)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> UpdateUser(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}

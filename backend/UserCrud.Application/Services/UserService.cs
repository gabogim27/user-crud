namespace UserCrud.Application.Services
{
    using AutoMapper;
    using UserCrud.Domain.Common;
    using UserCrud.Domain.DTOs;
    using UserCrud.Domain.Entities;
    using UserCrud.Domain.Exceptions;
    using UserCrud.Domain.Repositories;
    using UserCrud.Domain.Services;

    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> AddUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _userRepository.AddAsync(user);
            
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> DeleteUser(int userId)
        {
            var existsUser = await _userRepository.ExistAsync(x => x.Id == userId);
            if (!existsUser)
            {
                throw new NotFoundException("The user does not exist.");
            }

            var user = await _userRepository.GetByIdAsync(userId);
            await _userRepository.DeleteAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUser(int userId)
        {
            var existsUser = await _userRepository.ExistAsync(x => x.Id == userId);
            if (!existsUser)
            {
                throw new NotFoundException("The user does not exist.");
            }

            var user = await _userRepository.GetByIdAsync(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetUsers(Pagination pagination)
        {
            var users = await _userRepository.GetAllAsync(pagination.PageIndex, pagination.PageSize);
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return usersDto;
        }

        public async Task<UserDto> UpdateUser(UserDto userDto)
        {
            var existsUser = await _userRepository.ExistAsync(x => x.Id == userDto.Id);
            if (!existsUser)
            {
                throw new NotFoundException("The user does not exist.");
            }

            var user = _mapper.Map<User>(userDto);
            await _userRepository.UpdateAsync(user);
            return _mapper.Map<UserDto>(user);
        }
    }
}

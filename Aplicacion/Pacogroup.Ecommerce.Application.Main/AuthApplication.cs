using AutoMapper;
using Pacogroup.Ecommerce.Application.DTO;
using Pacogroup.Ecommerce.Application.Interfaces;
using Pacogroup.Ecommerce.Domain.Entity;
using Pacogroup.Ecommerce.Domain.Interfaces;
using Pacogroup.Ecommerce.Transversal.Common;
using Pacogroup.Ecommerce.Transversal.Logging;

namespace Pacogroup.Ecommerce.Application.Main
{
    public class AuthApplication : IAuthApplication
    {
        private readonly IUsersDomain _usersDomain;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IAppLogger<AuthApplication> _logger;

        public AuthApplication(IUsersDomain usersDomain, IJwtService jwtService, IMapper mapper, IAppLogger<AuthApplication> logger)
        {
            _usersDomain = usersDomain;
            _jwtService = jwtService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<TokenDTO>> SignInAsync(SingInDTO signInDto)
        {
            var response = new Response<TokenDTO>();

            try
            {
                var user = await _usersDomain.GetByEmailAsync(signInDto.Email);
                if (user == null)
                {
                    response.IsSucces = false;
                    response.Message = "Email no existe o no se encuentra registrado";
                    _logger.LogError("Failed to validate email. Error: {Message}", response.Message);
                    return response;
                }

                var isValidPassword = await _usersDomain.CheckPasswordAsync(user, signInDto.Password);
                if (!isValidPassword)
                {
                    response.IsSucces = false;
                    response.Message = "Credenciales inválidas";
                    _logger.LogError("Failed to validate login. Error: {Message}", response.Message);
                    return response;
                }

                var token = _jwtService.GenerateToken(user);
                response.Data = new TokenDTO
                {
                    AccessToken = token,
                    ExpiresIn = 3600
                };

                response.IsSucces = true;
                response.Message = "Autenticación exitosa";
            }
            catch (Exception e)
            {
                response.IsSucces = false;
                response.Message = e.Message;
                _logger.LogError("Failed to execute login. Error: {Message}", response.Message);
            }

            return response;
        }

        public async Task<Response<bool>> SignUpAsync(SingUpDTO signUpDto)
        {
            var response = new Response<bool>();
            try
            {
                var existingUser = await _usersDomain.GetByEmailAsync(signUpDto.Email);
                if (existingUser != null)
                {
                    response.IsSucces = false;
                    response.Message = "El usuario ya existe";
                    _logger.LogError("Failed to register user. Error: {Message}", response.Message);
                    return response;
                }

                var user = _mapper.Map<User>(signUpDto);
                response.Data = await _usersDomain.CreateUserAsync(user, signUpDto.Password);

                if (response.Data)
                {
                    response.IsSucces = false;
                    response.IsSucces = true;
                    response.Message = "Usuario creado exitosamente";
                }
            }
            catch (Exception e)
            {
                response.IsSucces = false;
                response.Message = e.Message;
                _logger.LogError("Failed to execute register. Error: {Message}", response.Message);
            }

            return response;
        }
    }
}
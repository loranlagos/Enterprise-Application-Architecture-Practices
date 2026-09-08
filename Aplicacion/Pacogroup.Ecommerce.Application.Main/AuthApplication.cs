using AutoMapper;
using Pacogroup.Ecommerce.Application.DTO;
using Pacogroup.Ecommerce.Application.Interfaces;
using Pacogroup.Ecommerce.Application.Validator;
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
        private readonly SingInDTOValidator _singInValidator;
        private readonly SingUpDTOValidator _singUpValidator;

        public AuthApplication(IUsersDomain usersDomain, IJwtService jwtService, IMapper mapper, IAppLogger<AuthApplication> logger, SingInDTOValidator singInValidator, SingUpDTOValidator singUpValidator)
        {
            _usersDomain = usersDomain;
            _jwtService = jwtService;
            _mapper = mapper;
            _logger = logger;
            _singInValidator = singInValidator;
            _singUpValidator = singUpValidator;
        }

        public async Task<Response<TokenDTO>> SignInAsync(SingInDTO signInDto)
        {
            var response = new Response<TokenDTO>();
            var validator = await _singInValidator.ValidateAsync(signInDto);

            try
            {
                if (!validator.IsValid)
                {
                    response.IsSucces = false;
                    response.Message = "Autenticación fallida por uno o más errores";
                    response.Errors = validator.Errors;

                    var errors = string.Join(
                        " | ",
                        validator.Errors.Select(e => e.ErrorMessage)
                    );

                    _logger.LogError(
                        "Failed for bad data in parameters. Errors: {Errors}",
                        errors
                    );

                    return response;
                }

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
            var validator = await _singUpValidator.ValidateAsync(signUpDto);

            try
            {
                if (!validator.IsValid)
                {
                    response.IsSucces = false;
                    response.Message = "Registro fallido por uno o más errores";
                    response.Errors = validator.Errors;

                    var errors = string.Join(
                        " | ",
                        validator.Errors.Select(e => e.ErrorMessage)
                    );

                    _logger.LogError(
                        "Failed for bad data in parameters. Errors: {Errors}",
                        errors
                    );

                    return response;
                }

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
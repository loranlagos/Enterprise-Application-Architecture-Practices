using Pacogroup.Ecommerce.Application.DTO;
using Pacogroup.Ecommerce.Transversal.Common;

namespace Pacogroup.Ecommerce.Application.Interfaces
{
    public interface IAuthApplication
    {
        Task<Response<bool>> SignUpAsync(SingUpDTO signUpDto);
        Task<Response<TokenDTO>> SignInAsync(SingInDTO signInDto);
    }
}
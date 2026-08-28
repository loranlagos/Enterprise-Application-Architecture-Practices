namespace Pacogroup.Ecommerce.Application.DTO
{
    public sealed record SingInDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
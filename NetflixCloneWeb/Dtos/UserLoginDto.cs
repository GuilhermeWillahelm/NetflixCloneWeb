using System.ComponentModel.DataAnnotations;

namespace NetflixCloneWeb.Dtos
{
    public class UserLoginDto
    {
        public int Id { get; set; }
        [Display(Name = "Usuário")]
        public string UserName { get; set; } = string.Empty;
        [Display(Name = "Senha")]
        public string Password { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}

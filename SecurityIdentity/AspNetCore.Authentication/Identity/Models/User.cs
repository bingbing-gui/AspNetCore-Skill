using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class User
    {
        [Required]
        [DisplayName("用户名")]
        public string Name { get; set; } = null!;
        [Required]
        [DisplayName("邮箱")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "电子邮件格式不正确")]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        public string Password { get; set; } = null!;
    }
    public class UpdateUserDTO
    {
        [Required]
        [DisplayName("编号")]
        public string Id { get; set; } = null!;

        [Required]
        [DisplayName("用户名")]
        public string Name { get; set; } = null!;

        [Required]
        [DisplayName("邮箱")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        public string Password { get; set; } = null!;
    }
}

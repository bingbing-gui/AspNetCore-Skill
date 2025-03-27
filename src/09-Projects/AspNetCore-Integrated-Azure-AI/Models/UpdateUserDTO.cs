﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AspNetCore.Integrated.Azure.AI.Models
{
    public class UpdateUserDTO
    {
        [Required]
        [DisplayName("编号")]
        public int Id { get; set; }
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

        [DisplayName("年龄")]
        public int Age
        {
            get; set;
        }
        [DisplayName("国家")]
        public Country Country
        {
            get;
            set;
        }
        [DisplayName("薪水")]
        [Required]
        public string Salary
        {
            get; set;
        } = null!;
    }
}

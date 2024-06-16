using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Identity.Models
{
    public class AppUser : IdentityUser
    {
        public int Age
        {
            get; set;
        }
        public Country Country
        {
            get;
            set;
        }
        [Required]
        public string Salary
        {
            get; set;
        } = null!;
    }
    public enum Country
    {
        USA,
        UK,
        France,
        Germany,
        Russia
    }
}
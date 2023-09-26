using System.ComponentModel;

namespace AspNetCore.ModelValidation.Models
{
    public class JobApplication
    {
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("出生日期")]
        public DateTime DOB { get; set; }
        [DisplayName("性别")]
        public string Sex { get; set; }
        [DisplayName("工作经验")]
        public string Experience { get; set; }
        [DisplayName("条款")]
        public bool TermsAccepted { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class UserMetadata
    {
        [Key]
        public int UserID { get; set; }

        [Display(Name = "نقش کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int RoleID { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "کد فعال سازی")]
        public string ActiveCode { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public System.DateTime RegisterDate { get; set; }

        [Display(Name = "فعال / غیر فعال")]
        public bool IsActive { get; set; }
    }
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
    }
}

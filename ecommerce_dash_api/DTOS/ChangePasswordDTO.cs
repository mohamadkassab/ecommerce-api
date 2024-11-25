using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class ChangePasswordDTO
    {
        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 6, ErrorMessage = "Old Password must be between 6 and 255 characters.")]
        public string OldPassword { get; set; } = null!;

        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 6, ErrorMessage = "New Password must be between 6 and 255 characters.")]
        public string NewPassword { get; set; } = null!;

        [Required(AllowEmptyStrings = false), StringLength(255, MinimumLength = 6, ErrorMessage = "Confirm New Password must be between 6 and 255 characters.")]
        public string ConfirmNewPassword { get; set; } = null!;
    }
}

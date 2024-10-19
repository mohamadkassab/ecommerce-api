using System.ComponentModel.DataAnnotations;

namespace ecommerce_dash_api.DTOS
{
    public class UserRolesUpdateDTO
    {
        public int UserId { get; set; }

        public  List<int> RoleIds { get; set; }
    }
}

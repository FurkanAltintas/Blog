using System.Collections.Generic;

namespace Blog.Entities.Dtos
{
    public class UserRoleAssingDto
    {
        public UserRoleAssingDto()
        {
            RoleAssignDtos = new List<RoleAssignDto>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public IList<RoleAssignDto> RoleAssignDtos { get; set; }
    }
}
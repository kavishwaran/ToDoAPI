using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Shared.Enums;

namespace Utils.Shared.DTO
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
      //  public string UserName { get; set; }
       // public string Password { get; set; }
        public string Email { get; set; }
        //public int Gender { get; set; }
        public GenderEnum GenderEn { get; set; }
    }
}

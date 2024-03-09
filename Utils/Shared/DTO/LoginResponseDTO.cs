using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Shared.DTO
{
    public class LoginResponseDTO
    {
        public AdministrationUserDTO User { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}

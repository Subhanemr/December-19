using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Dtos.Account
{
    public record RegisterDto(string UserName, string Name, string Surname, string Email, string Password, string ConfirmPassword);
}

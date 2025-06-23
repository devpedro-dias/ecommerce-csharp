using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Shared.DTOs.Response.User;

public class UserResponse
{
    public string? Email { get; set; }
    public bool EmailIsConfirmed { get; set; }
}

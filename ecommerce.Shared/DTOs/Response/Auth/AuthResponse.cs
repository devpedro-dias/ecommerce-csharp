using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Shared.DTOs.Response.NovaPasta;
    public class AuthResponse
    {
        public bool Sucesso { get; set; }
        public string[] Erros { get; set; }
    }

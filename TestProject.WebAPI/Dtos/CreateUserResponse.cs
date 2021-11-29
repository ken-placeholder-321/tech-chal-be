﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.WebAPI.Dtos
{
    public class CreateUserResponse
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

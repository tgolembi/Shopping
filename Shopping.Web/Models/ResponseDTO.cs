﻿namespace Shopping.Web.Models
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}

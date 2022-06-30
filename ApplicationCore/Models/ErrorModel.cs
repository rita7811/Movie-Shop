﻿using System;
namespace ApplicationCore.Models
{
	public class ErrorModel
	{
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime ExceptionDateTime { get; set; }
        public string ExceptionType { get; set; }
        public string Path { get; set; }
        public string HttpRequestType { get; set; }
        public string User { get; set; }
    }
}


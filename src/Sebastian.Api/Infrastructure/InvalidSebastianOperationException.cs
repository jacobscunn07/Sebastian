using System;

namespace Sebastian.Api.Infrastructure
{
    public class InvalidSebastianOperationException : InvalidOperationException
    {
        public InvalidSebastianOperationException(string message) : base(message)
        {
        }
    }
}
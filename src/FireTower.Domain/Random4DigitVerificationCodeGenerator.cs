using System;

namespace FireTower.Domain
{
    public class Random4DigitVerificationCodeGenerator : IVerificationCodeGenerator
    {
        public string Generate()
        {
            return new Random().Next(1000, 9999).ToString();
        }
    }
}
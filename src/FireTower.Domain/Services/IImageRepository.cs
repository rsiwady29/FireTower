using System;

namespace FireTower.Domain.Services
{
    public interface IImageRepository
    {
        Uri Save(string base64ImageString);
    }
}
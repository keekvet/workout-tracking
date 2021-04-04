using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.Models;

namespace WorkoutTracking.Application.Services.Interfaces
{
    public interface IEncryptionService
    {
        Task<HashedPassword> EncryptAsync(byte[] data);
        Task<HashedPassword> EncryptAsync(byte[] data, byte[] salt);
        Task<bool> PasswordEqualsHashAsync(string password, byte[] hash, byte[] salt);
    }
}

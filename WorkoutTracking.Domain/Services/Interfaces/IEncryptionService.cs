using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Domain.Models;

namespace WorkoutTracking.Domain.Services.Interfaces
{
    public interface IEncryptionService
    {
        Task<HashedPassword> EncryptAsync(byte[] data);
        Task<HashedPassword> EncryptAsync(byte[] data, byte[] salt);
    }
}

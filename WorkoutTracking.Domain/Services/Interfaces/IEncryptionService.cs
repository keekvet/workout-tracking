using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Domain.Dto;

namespace WorkoutTracking.Domain.Services.Interfaces
{
    interface IEncryptionService
    {
        Task<PasswordDto> EncryptAsync(byte[] data);
    }
}

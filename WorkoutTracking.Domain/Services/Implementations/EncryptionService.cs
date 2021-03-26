using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Domain.Dto;
using WorkoutTracking.Domain.Services.Interfaces;

namespace WorkoutTracking.Domain.Services.Implementations
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] pepper;
        public Task<PasswordDto> EncryptAsync(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}

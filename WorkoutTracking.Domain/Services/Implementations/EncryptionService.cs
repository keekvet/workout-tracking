using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Domain.ConfigurationTemplates;
using WorkoutTracking.Domain.Models;
using WorkoutTracking.Domain.Services.Interfaces;

namespace WorkoutTracking.Domain.Services.Implementations
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] pepper;
        private readonly int iterations;
        private readonly int hashSize;
        private readonly int saltSize;

        public EncryptionService(IOptions<EncryptionConfiguration> configuraion)
        {
            pepper = Encoding.UTF8.GetBytes(configuraion.Value.Pepper);
            iterations = configuraion.Value.Iterations;
            hashSize = configuraion.Value.HashSize;
            saltSize = configuraion.Value.SaltSize;
        }

        public async Task<HashedPassword> EncryptAsync(byte[] data, byte[] salt)
        {
            return await Task.Run(() =>
            {
                byte[] pepperedData = data.Zip(pepper, (d, s) => (byte)(d + s)).ToArray();

                Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(pepperedData, salt, iterations);
                return new HashedPassword { Hash = pbkdf2.GetBytes(hashSize), Salt = salt };
            });
        }

        public async Task<HashedPassword> EncryptAsync(byte[] data)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[saltSize];
            provider.GetBytes(salt);

            return await EncryptAsync(data, salt);
        }
    }
}

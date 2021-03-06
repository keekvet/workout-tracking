using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracking.Application.ConfigurationTemplates;
using WorkoutTracking.Application.Models;
using WorkoutTracking.Application.Services.Interfaces;

namespace WorkoutTracking.Application.Services.Implementations
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
            if (data is null || salt is null)
                return null;

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

        public async Task<bool> PasswordEqualsHashAsync(string password, byte[] hash, byte[] salt)
        {
            HashedPassword hashedPassword = await EncryptAsync(
                   Encoding.UTF8.GetBytes(password), salt);

            if (Enumerable.SequenceEqual(hash, hashedPassword.Hash))
                return true;

            return false;
        }
    }
}

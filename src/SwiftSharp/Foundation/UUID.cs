using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace SwiftSharp.Foundation
{
    public struct UUID
    {
        private readonly byte[] bytes; // 16 bytes

        #region Constructors

        public UUID()
        {
            bytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            // Set version to 4 (random)
            bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
            // Set variant to RFC 4122
            bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
        }

        public UUID(byte[] bytes)
        {
            if (bytes.Length != 16)
                throw new ArgumentException("UUID must be 16 bytes long.");
            this.bytes = new byte[16];
            System.Array.Copy(bytes, this.bytes, 16);
        }

        #endregion

        #region Utility Methods

        public override string ToString()
        {
            return BitConverter.ToString(bytes, 0, 4).Replace("-", "") + "-" +
                   BitConverter.ToString(bytes, 4, 2).Replace("-", "") + "-" +
                   BitConverter.ToString(bytes, 6, 2).Replace("-", "") + "-" +
                   BitConverter.ToString(bytes, 8, 2).Replace("-", "") + "-" +
                   BitConverter.ToString(bytes, 10, 6).Replace("-", "");
        }

        public override bool Equals(object? obj)
        {
            return obj is UUID other && Equals(other);
        }

        public bool Equals(UUID other)
        {
            for (int i = 0; i < 16; i++)
            {
                if (bytes[i] != other.bytes[i])
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var b in bytes)
                hash = hash * 31 + b;
            return hash;
        }

        public static bool operator ==(UUID a, UUID b) => a.Equals(b);
        public static bool operator !=(UUID a, UUID b) => !a.Equals(b);

        #endregion
    }
}
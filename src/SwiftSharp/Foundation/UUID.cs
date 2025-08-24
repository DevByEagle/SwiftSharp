using System;
using System.Data.Common;
using System.Security.Cryptography;

namespace SwiftSharp.Foundation
{
    public readonly struct UUID : IEquatable<UUID>
    {
        private readonly byte[] bytes;

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

        #endregion

        #region Properties

        public static bool operator ==(UUID lhs, UUID rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            else if (lhs.bytes.Length != rhs.bytes.Length)
                return false;

            for (int i = 0; i < lhs.bytes.Length; i++)
                if (lhs.bytes[i] != rhs.bytes[i])
                    return false;

            return true;
        }

        public static bool operator !=(UUID lhs, UUID rhs) => !(lhs == rhs);

        #endregion

        #region Utility Methods

        public override bool Equals(object? obj) => obj is UUID other && Equals(other);

        public bool Equals(UUID other)
        {
            if (bytes.Length != other.bytes.Length)
                return false;

            for (int i = 0; i < bytes.Length; i++)
                if (bytes[i] != other.bytes[i])
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (var b in bytes)
                hash = hash * 31 + b;
            return hash;
        }

        public override string ToString()
        {
            return BitConverter.ToString(bytes, 0, 4).Replace("-", "") + "-" +
                   BitConverter.ToString(bytes, 4, 2).Replace("-", "") + "-" +
                   BitConverter.ToString(bytes, 6, 2).Replace("-", "") + "-" +
                   BitConverter.ToString(bytes, 8, 2).Replace("-", "") + "-" +
                   BitConverter.ToString(bytes, 10, 6).Replace("-", "");
        }

        #endregion
    }
}
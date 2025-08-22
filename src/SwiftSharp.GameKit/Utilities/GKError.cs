using System;

namespace SwiftSharp.GameKit
{
    public struct GKError
    {
        public enum Code : int
        {
            NotSupported,
            Unknown,
            Cancelled,
            CommunicationsFailure,
            InvalidPlayer,
            InvalidParameter,
            ConnectionTimeout,
            InvalidCredentials,
            NotAuthenticated,
        }
    }
}
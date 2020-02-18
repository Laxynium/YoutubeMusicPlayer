using System;

namespace YoutubeMusicPlayer.Framework.Exceptions
{
    public abstract class AppException : Exception
    {
        public abstract string Code { get; }
        protected AppException(string message) : base(message)
        {
        }
    }
}
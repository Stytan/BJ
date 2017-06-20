using System;

namespace BJ
{
    public class FException : ApplicationException
    {
        public FException(string message) : base(message)
        {
        }
        public ConsoleKey Key { get; private set; }
        public FException(ConsoleKey Key)
        {
            this.Key = Key;
        }
    }
}

using System;
namespace ApplicationCore.Exceptions
{
	public class ConflictException: Exception
	{
        // create a constructor (ctor)
        public ConflictException(string message): base(message)
        {

        }

	}
}


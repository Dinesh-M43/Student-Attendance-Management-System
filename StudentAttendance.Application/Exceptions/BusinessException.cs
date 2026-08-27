using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAttendance.Application.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message)
        {
        }
    }
}

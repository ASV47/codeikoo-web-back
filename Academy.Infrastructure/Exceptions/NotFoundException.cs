using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Infrastructure.Exceptions
{
	public abstract class NotFoundException(string Message) : Exception(Message)
	{
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuprum
{
	internal static class Classes
	{
		internal enum LogLevel
		{
			Info,
			Alert,
			Warning,
			Error,
		}
		internal enum LogSource
		{
			Client,
			Cuprum,
			Node,
		}
	}
}

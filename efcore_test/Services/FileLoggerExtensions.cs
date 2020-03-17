using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IO;

namespace efcore_test.Services
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory loggerFactory, string filename)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), filename);
            loggerFactory.AddProvider(new FileLoggerProvider(path));
            return loggerFactory;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace Library.Utils
{
    public static class FileReader
    {
        public static IEnumerable<string> TextFileToList(string url, out int r)
        {
            try
            {
                var output = new List<string>(File.ReadAllLines(url));
                r = 1;
                return output;
            }
            catch (Exception)
            {
                r = -1;
                return null;
            }
        }
    }
}
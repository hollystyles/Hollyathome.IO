using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hollyathome.IO
{
    /// <summary>
    /// Used to read a comma delimited text file or stream.
    /// </summary>
    public class CsvReader : StreamReader
    {
        #region Constructors

        public CsvReader(Stream stream)
            : base(stream)
        {
        }

        public CsvReader(Stream stream, Encoding encoding)
            : base(stream, encoding)
        {
        }

        public CsvReader(string filePath)
            : base(filePath)
        {
        }

        public CsvReader(string path, Encoding encoding)
            : base(path, encoding)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Reads the current line in the stream and returns an 
        /// array of the delimited values
        /// </summary>
        /// <returns></returns>
        public string[] ReadRow()
        {
            List<string> row = new List<string>();
            bool inQuotedString = false;
            bool escapeNext = false;
            string val = string.Empty;
            string line = base.ReadLine();

            foreach (char c in line)
            {
                if (c == '\\' && !escapeNext)
                {
                    escapeNext = true;
                }
                else if (c == '"' && !escapeNext)
                {
                    inQuotedString = !inQuotedString;
                }
                else if (c == ',' && !inQuotedString)
                {
                    row.Add(val);
                    val = string.Empty;
                }
                else
                {
                    val += c;
                    escapeNext = false;
                }
            }
            
            row.Add(val);
            
            return row.ToArray();
        }

        #endregion

    }
}

/**************************************************
 * Author : hollystyles@fastmail.co.uk
 * Date : 26/05/2013
 * 
 * Unit tests for the CsvReader.
 * 
 * ************************************************/
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hollyathome.IO;

namespace UnitTests
{
    [TestClass]
    public class CsvReaderTests
    {
        [TestMethod]
        public void ReadHeaders()
        {
            string[] expectedValues = new string[]{"ID","Value1","Value2","Value3"};
            
            ReadAndAssert("test1.csv", 0, expectedValues);            
        }

        [TestMethod]
        public void ReadSecondRowWithBackslash()
        {
            string[] expectedValues = new string[] { "1", "String0 with a \\ backslash", "1", "1.0" };

            ReadAndAssert("test1.csv", 1, expectedValues);
        }

        [TestMethod]
        public void ReadThirdRowWithEmbeddedComa()
        {
            string[] expectedValues = new string[] { "2", "String1, with comma", "2", "2.0" };

            ReadAndAssert("test1.csv", 2, expectedValues);
        }

        [TestMethod]
        public void ReadFourthRowWithEscapedQuotes()
        {
            string[] expectedValues = new string[] { "3", "String2 \"quoted\" with escape sequence", "3", "3.0" };

            ReadAndAssert("test1.csv", 3, expectedValues);
        }

        private void ReadAndAssert(string path, int lineIndex, string[] expectedValues)
        {
            using (var t = new CsvReader(path, System.Text.Encoding.Default))
            {
                while (lineIndex > 0)
                {
                    t.ReadLine();
                    lineIndex--;
                }

                string[] actualValues = t.ReadRow();

                for (int i = 0; i < 4; i++)
                {
                    Assert.AreEqual(expectedValues[i], actualValues[i]);
                }
            }
        }
    }
}

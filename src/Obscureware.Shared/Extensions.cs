// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Obscureware Solutions">
// MIT License
//
// Copyright(c) 2016 Sebastian Gruchacz
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>
// <summary>
//   Defines some Extension methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Obscureware.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Some extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The minimum fitting area for string splitting.
        /// </summary>
        private const uint MIN_FIT_AREA = 3;

        /// <summary>
        /// Info-units suffixes
        /// </summary>
        private static readonly string[] Sufixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        
        /// <summary>
        /// Converts natural number (indexing, staring from 1) into Excel-like column numbering format (i.e. A, B, ... Z, AB, AC, ... ZY, ZZ)
        /// </summary>
        /// <param name="value">Number to be converted.</param>
        /// <returns>Excel-like column number</returns>
        /// <remarks>Taken from http://stackoverflow.com/questions/837155/fastest-function-to-generate-excel-column-letters-in-c-sharp </remarks>
        public static string ToAlphaEnum(this uint @value)
        {
            string columnString = string.Empty;
            decimal columnNumber = @value;
            while (columnNumber > 0)
            {
                decimal currentLetterNumber = (columnNumber - 1) % 26;
                char currentLetter = (char)(currentLetterNumber + 65);
                columnString = currentLetter + columnString;
                columnNumber = (columnNumber - (currentLetterNumber + 1)) / 26;
            }

            return columnString;
        }

        /// <summary>
        /// Converts Excel-like column numbering format into corresponding natural integer
        /// </summary>
        /// <param name="value">String to be "parsed" (converted)</param>
        /// <returns></returns>
        /// <remarks>Taken from http://stackoverflow.com/questions/837155/fastest-function-to-generate-excel-column-letters-in-c-sharp </remarks>
        public static int FromAlphaEnum(this string @value)
        {
            int retVal = 0;
            string col = @value.ToUpper();
            for (int charIndex = col.Length - 1; charIndex >= 0; charIndex--)
            {
                char colPiece = col[charIndex];
                int colNum = colPiece - 64;
                retVal = retVal + (colNum * (int)Math.Pow(26, col.Length - (charIndex + 1)));
            }
            return retVal;
        }

        /// <summary>
        /// Converts number of bytes into user friendly format of KB, MB, GB etc.
        /// </summary>
        /// <param name="byteCount">Count of bytes</param>
        /// <param name="culture">Optional culture for number formatting</param>
        /// <returns></returns>
        /// <remarks>Based on http://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net </remarks>
        public static string ToFriendlyXBytesText(this long byteCount, CultureInfo culture = null)
        {
            if (byteCount == 0)
            {
                return $"{0} {Sufixes[0]}";
            }

            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));

            if (place >= Sufixes.Length)
            {
                throw new ArgumentException(
                    $"Unexpectedly large number, objects larger than 1023{Sufixes[Sufixes.Length - 1]} are not expected to exists in the entire universe!",
                    nameof(byteCount));
            }
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);

            return $"{(Math.Sign(byteCount) * num).ToString(culture ?? CultureInfo.InvariantCulture),1} {Sufixes[place]}";
        }

        /// <summary>
        /// Splits text into lines that fit into designated column width
        /// </summary>
        /// <param name="text">Text to split</param>
        /// <param name="columnWidth">Area width to fit the string</param>
        /// <returns>Text splinted into matching pieces.</returns>
        /// <remarks>Used this imperfect solution for now: http://stackoverflow.com/a/1678162
        /// This will not work properly for long words.
        /// This is not able to properly break the words in the middle to optimize space...
        /// TODO: use Humanizer library perhaps?
        /// </remarks>
        public static IEnumerable<string> SplitTextToFit(this string text, uint columnWidth)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (columnWidth < MIN_FIT_AREA)
            {
                throw new ArgumentException($"This is just nonsense - area to fit text into must be at least {MIN_FIT_AREA} characters wide.", nameof(columnWidth));
            }

            int offset = 0;
            while (offset < text.Length)
            {
                int index = text.LastIndexOf(" ", Math.Min(text.Length, offset + (int)columnWidth), StringComparison.Ordinal); // TODO: use CultureInfo!
                string line = text.Substring(offset, (index - offset <= 0 ? text.Length : index) - offset);
                offset += line.Length + 1;
                yield return line;
            }
        }
    }
}
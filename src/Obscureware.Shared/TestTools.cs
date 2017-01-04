// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestTools.cs" company="Obscureware Solutions">
// MIT License
//
// Copyright(c) 2016-2017 Sebastian Gruchacz
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
//   Defines the TestTools class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Obscureware.Shared
{
    using System;

    public static class TestTools
    {
        private static readonly Random rnd = new Random();

        public static float GetRandomFloat()
        {
            return (float)rnd.NextDouble(); // TODO: add float type (Real, NegativeReal, PositiveReal) and range selection
        }

        public static float GetRandomFloat(int multiplier)
        {
            return GetRandomFloat() * multiplier;
        }

        /// <summary>
        /// Builds string of required length concatenating random characters from given string.
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuildRandomStringFrom(this string sourceString, uint length)
        {
            char[] array = new char[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = sourceString[rnd.Next(0, sourceString.Length)];
            }

            return new string(array);
        }

        public static string BuildRandomStringFrom(this string sourceString, int minLength, int maxLength)
        {
            int length = rnd.Next(minLength, maxLength + 1);

            char[] array = new char[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = sourceString[rnd.Next(0, sourceString.Length)];
            }

            return new string(array);
        }

        public static string Numeric => @"0123456789";

        public static string UpperAlpha => @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string LowerAlpha => @"abcdefghijklmnopqrstuvwxyz";

        public static string UpperAlphanumeric => UpperAlpha + Numeric;

        public static string LowerAlphanumeric => LowerAlpha + Numeric;

        public static string MixedAlphanumeric => UpperAlphanumeric + LowerAlphanumeric;

        public static string AlphanumericIdentifier => UpperAlphanumeric + LowerAlphanumeric + @"_____"; // increased probability ;-)

        public static string AlphaSentence => LowerAlpha + @" .:! ,? ;"; // increased probability ;-)
    }
}
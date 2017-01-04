// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExcelIdentifierConverterTests.cs" company="Obscureware Solutions">
// MIT License
//
// Copyright(c) 2017 Sebastian Gruchacz
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
//   Defines ToAlphaEnum & FromAlphaEnum functions tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Obscureware.Shared.Tests
{
    using Shouldly;
    using Xunit;

    public class ExcelIdentifierConverterTests
    {
        [Theory]
        [InlineData(1u, "A")]
        [InlineData(2u, "B")]
        [InlineData(26u, "Z")]
        public void BasicNumberToTextConversionShallWork(uint num, string expected)
        {
            num.ToAlphaEnum().ShouldBe(expected);
        }

        [Theory]
        [InlineData(27u, "AA")]
        [InlineData(28u, "AB")]
        [InlineData(702u, "ZZ")]
        public void AdnvancedNumberToTextConversionShallWork(uint num, string expected)
        {
            num.ToAlphaEnum().ShouldBe(expected);
        }

        [Theory]
        [InlineData("A", 1u)]
        [InlineData("B", 2u)]
        [InlineData("Z", 26u)]
        public void BasicTextToNumberConversionShallWork(string identifier, uint expected)
        {
            identifier.FromAlphaEnum().ShouldBe(expected);
        }

        [Theory]
        [InlineData("AA", 27u)]
        [InlineData("AB", 28u)]
        [InlineData("ZZ", 702u)]
        public void AdnvancedTextToNumberConversionShallWork(string identifier, uint expected)
        {
            identifier.FromAlphaEnum().ShouldBe(expected);
        }
    }
}

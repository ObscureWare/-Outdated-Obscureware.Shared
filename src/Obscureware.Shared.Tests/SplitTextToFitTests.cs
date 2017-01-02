// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SplitTextToFitTests.cs" company="Obscureware Solutions">
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
//   Defines SplitTextToFit function tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Obscureware.Shared.Tests
{
    using System;

    using Shared;

    using Shouldly;

    using Xunit;

    public class SplitTextToFitTests
    {
        [Fact]
        public void SplitOfEmptyStringShallYieldEmptyString()
        {
            string.Empty.SplitTextToFit(5).ShouldNotBeEmpty();
            string.Empty.SplitTextToFit(5).ShouldBe(new string[] { String.Empty });
        }

        [Fact]
        public void NullStringShallThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Extensions.SplitTextToFit(null, 5));
        }

        [Fact]
        public void WhenAreaIsTooSmallItShallThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => Extensions.SplitTextToFit("abc", 2));
        }
    }
}

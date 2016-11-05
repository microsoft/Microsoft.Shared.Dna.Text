//-----------------------------------------------------------------------------
// <copyright file="StringSegmentTests.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation. All rights reserved.
//     Licensed under the MIT license. See license file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------------

namespace Microsoft.Shared.Dna.Text.Test
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for the <see cref="StringSegment"/> struct.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class StringSegmentTests
    {
        /// <summary>
        /// Constructor sets empty string.
        /// </summary>
        [TestMethod]
        public void StringSegment_Constructor_Sets_Empty_String()
        {
            string expected = string.Empty;
            StringSegment target = new StringSegment(expected);
            Assert.AreSame(expected, target.String);
            Assert.AreEqual(0, target.Offset);
            Assert.AreEqual(expected.Length, target.Count);
        }

        /// <summary>
        /// Constructor sets empty string and offset.
        /// </summary>
        [TestMethod]
        public void StringSegment_Constructor_Sets_Empty_String_And_Offset()
        {
            string expected = string.Empty;
            StringSegment target = new StringSegment(expected, 0);
            Assert.AreSame(expected, target.String);
            Assert.AreEqual(0, target.Offset);
            Assert.AreEqual(expected.Length, target.Count);
        }

        /// <summary>
        /// Constructor sets empty string offset and count.
        /// </summary>
        [TestMethod]
        public void StringSegment_Constructor_Sets_Empty_String_Offset_And_Count()
        {
            string expected = string.Empty;
            StringSegment target = new StringSegment(expected, 0, 0);
            Assert.AreSame(expected, target.String);
            Assert.AreEqual(0, target.Offset);
            Assert.AreEqual(expected.Length, target.Count);
        }

        /// <summary>
        /// Constructor sets string.
        /// </summary>
        [TestMethod]
        public void StringSegment_Constructor_Sets_String()
        {
            string expected = "Test";
            StringSegment target = new StringSegment(expected);
            Assert.AreSame(expected, target.String);
            Assert.AreEqual(0, target.Offset);
            Assert.AreEqual(expected.Length, target.Count);
        }

        /// <summary>
        /// Constructor sets string and offset.
        /// </summary>
        [TestMethod]
        public void StringSegment_Constructor_Sets_String_And_Offset()
        {
            string expected = "Test";
            StringSegment target = new StringSegment(expected, 1);
            Assert.AreSame(expected, target.String);
            Assert.AreEqual(1, target.Offset);
            Assert.AreEqual(expected.Length - 1, target.Count);
        }

        /// <summary>
        /// Constructor sets string offset and count.
        /// </summary>
        [TestMethod]
        public void StringSegment_Constructor_Sets_String_Offset_And_Count()
        {
            string expected = "Test";
            StringSegment target = new StringSegment(expected, 1, 2);
            Assert.AreSame(expected, target.String);
            Assert.AreEqual(1, target.Offset);
            Assert.AreEqual(2, target.Count);
        }

        /// <summary>
        /// Constructor rejects null string.
        /// </summary>
        [TestMethod]
        public void StringSegment_Constructor_Rejects_Null_String()
        {
            try
            {
                StringSegment target = new StringSegment(null, 0);
                Assert.IsNull(target);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(typeof(ArgumentNullException), ex.GetType());
            }

            try
            {
                StringSegment target = new StringSegment(null, 0, 0);
                Assert.IsNull(target);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(typeof(ArgumentNullException), ex.GetType());
            }
        }

        /// <summary>
        /// Constructor rejects out of range offset.
        /// </summary>
        [TestMethod]
        public void StringSegment_Constructor_Rejects_Out_Of_Range_Offset()
        {
            string expected = "Test";
            try
            {
                StringSegment target = new StringSegment(expected, -1);
                Assert.IsNull(target);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }

            try
            {
                StringSegment target = new StringSegment(expected, expected.Length);
                Assert.IsNull(target);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }

            try
            {
                StringSegment target = new StringSegment(expected, -1, 0);
                Assert.IsNull(target);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }

            try
            {
                StringSegment target = new StringSegment(expected, expected.Length, 0);
                Assert.IsNull(target);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }
        }

        /// <summary>
        /// Constructor rejects out of range count.
        /// </summary>
        [TestMethod]
        public void StringSegment_Constructor_Rejects_Out_Of_Range_Count()
        {
            string expected = "Test";
            try
            {
                StringSegment target = new StringSegment(expected, 0, -1);
                Assert.IsNull(target);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }

            try
            {
                StringSegment target = new StringSegment(expected, 1, expected.Length);
                Assert.IsNull(target);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }
        }

        /// <summary>
        /// ToString returns substring.
        /// </summary>
        [TestMethod]
        public void StringSegment_ToString_Returns_Substring()
        {
            string expected = "Test";
            StringSegment target = new StringSegment(expected, 1, 2);
            Assert.AreEqual("es", target.ToString());
        }
    }
}

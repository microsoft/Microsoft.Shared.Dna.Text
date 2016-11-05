//-----------------------------------------------------------------------------
// <copyright file="FixedStringBuilderTests.cs" company="Microsoft">
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
    /// Tests for the <see cref="FixedStringBuilder"/> class.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FixedStringBuilderTests
    {
        /// <summary>
        /// Constructor rejects negative capacity.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_Constructor_Rejects_Negative_Capacity()
        {
            try
            {
                FixedStringBuilder target = new FixedStringBuilder(-1);
                Assert.IsNull(target);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }
        }

        /// <summary>
        /// Clear resets builder.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_Clear_Resets_Builder()
        {
            FixedStringBuilder target = new FixedStringBuilder(1);
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.AreEqual("X", target.ToString());
            target.Clear();
            Assert.AreEqual(string.Empty, target.ToString());
        }

        /// <summary>
        /// Last returns last character.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_Last_Returns_Last_Character()
        {
            FixedStringBuilder target = new FixedStringBuilder(2);
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.AreEqual('X', target.Last);
        }

        /// <summary>
        /// Last returns null character when empty.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_Last_Returns_Null_Character_When_Empty()
        {
            FixedStringBuilder target = new FixedStringBuilder(1);
            Assert.AreEqual('\0', target.Last);
        }

        /// <summary>
        /// Length returns current length.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_Length_Returns_Current_Length()
        {
            FixedStringBuilder target = new FixedStringBuilder(2);
            Assert.AreEqual(0, target.Length);
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.AreEqual(1, target.Length);
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.AreEqual(2, target.Length);
        }

        /// <summary>
        /// TryAppend does not exceed capacity with character.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryAppend_Does_Not_Exceed_Capacity_With_Character()
        {
            FixedStringBuilder target = new FixedStringBuilder(1);
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.IsFalse(target.TryAppend('X', 0));
            Assert.AreEqual("X", target.ToString());
        }

        /// <summary>
        /// TryAppend does not exceed capacity with character and reserve.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryAppend_Does_Not_Exceed_Capacity_With_Character_And_Reserve()
        {
            FixedStringBuilder target = new FixedStringBuilder(2);
            Assert.IsTrue(target.TryAppend('X', 1));
            Assert.IsFalse(target.TryAppend('X', 1));
            Assert.AreEqual("X", target.ToString());
        }

        /// <summary>
        /// TryAppend rolls back when capacity is reached with character and reserve.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryAppend_Rolls_Back_When_Capacity_Is_Reached_With_Character_And_Reserve()
        {
            FixedStringBuilder target = new FixedStringBuilder(2);
            Assert.IsTrue(target.TryAppend('X', 1, 0));
            Assert.IsFalse(target.TryAppend('X', 1, 0));
            Assert.AreEqual(string.Empty, target.ToString());
        }

        /// <summary>
        /// TryAppend keeps rollback when capacity is reached with character and reserve.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryAppend_Keeps_Rollback_When_Capacity_Is_Reached_With_Character_And_Reserve()
        {
            FixedStringBuilder target = new FixedStringBuilder(2);
            Assert.IsTrue(target.TryAppend('X', 1));
            int actual = 0;
            Assert.IsFalse(target.TryAppend('X', 1, out actual));
            Assert.AreEqual(1, actual);
            Assert.AreEqual("X", target.ToString());
        }

        /// <summary>
        /// TryAppend sets rollback with character and reserve.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryAppend_Sets_Rollback_With_Character_And_Reserve()
        {
            FixedStringBuilder target = new FixedStringBuilder(3);
            Assert.IsTrue(target.TryAppend('X', 0));
            int actual = 0;
            Assert.IsTrue(target.TryAppend('X', 1, out actual));
            Assert.IsFalse(target.TryAppend('X', 1, actual));
            Assert.AreEqual(1, actual);
            Assert.AreEqual("X", target.ToString());
        }

        /// <summary>
        /// TryAppend does not exceed capacity with string.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryAppend_Does_Not_Exceed_Capacity_With_String()
        {
            FixedStringBuilder target = new FixedStringBuilder(3);
            Assert.IsTrue(target.TryAppend("XX", 0));
            Assert.IsFalse(target.TryAppend("XX", 0));
            Assert.AreEqual("XX", target.ToString());
        }

        /// <summary>
        /// TryAppend does not exceed capacity with string and reserve.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryAppend_Does_Not_Exceed_Capacity_With_String_And_Reserve()
        {
            FixedStringBuilder target = new FixedStringBuilder(4);
            Assert.IsTrue(target.TryAppend("XX", 1));
            Assert.IsFalse(target.TryAppend("XX", 1));
            Assert.AreEqual("XX", target.ToString());
        }

        /// <summary>
        /// TryAppend does nothing with null strings.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryAppend_Does_Nothing_With_Null_Strings()
        {
            FixedStringBuilder target = new FixedStringBuilder(0);
            Assert.IsTrue(target.TryAppend(null, 0));
            Assert.AreEqual(0, target.Length);
            Assert.IsTrue(target.TryAppend(null, 0, 0));
            Assert.AreEqual(0, target.Length);
        }

        /// <summary>
        /// TryAppend rolls back when capacity is reached with string and reserve.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryAppend_Rolls_Back_When_Capacity_Is_Reached_With_String_And_Reserve()
        {
            FixedStringBuilder target = new FixedStringBuilder(4);
            Assert.IsTrue(target.TryAppend("XX", 1, 0));
            Assert.IsFalse(target.TryAppend("XX", 1, 0));
            Assert.AreEqual(string.Empty, target.ToString());
        }

        /// <summary>
        /// TryAppend rejects negative reserve.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryAppend_Rejects_Negative_Reserve()
        {
            FixedStringBuilder target = new FixedStringBuilder(2);
            try
            {
                target.TryAppend('X', -1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }

            try
            {
                target.TryAppend("XX", -1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }

            try
            {
                target.TryAppend('X', -1, 0);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }

            try
            {
                target.TryAppend("XX", -1, 0);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }
        }

        /// <summary>
        /// TryAppend rejects negative rollback.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryAppend_Rejects_Negative_Rollback()
        {
            FixedStringBuilder target = new FixedStringBuilder(2);
            try
            {
                target.TryAppend('X', 0, -1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }

            try
            {
                target.TryAppend("XX", 0, -1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }
        }

        /// <summary>
        /// TryExpand can always expand capacity.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryExpand_Can_Always_Expand_Capacity()
        {
            FixedStringBuilder target = new FixedStringBuilder(1);
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.IsTrue(target.TryExpand(2));
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.AreEqual("XX", target.ToString());
        }

        /// <summary>
        /// TryExpand does nothing if capacity is already larger.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryExpand_Does_Nothing_If_Capacity_Is_Already_Larger()
        {
            FixedStringBuilder target = new FixedStringBuilder(3);
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.IsFalse(target.TryExpand(2));
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.AreEqual("XXX", target.ToString());
        }

        /// <summary>
        /// TryExpand rejects negative capacity.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryExpand_Rejects_Negative_Capacity()
        {
            FixedStringBuilder target = new FixedStringBuilder(0);
            try
            {
                target.TryExpand(-1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }
        }

        /// <summary>
        /// TryResize can always expand capacity.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryResize_Can_Always_Expand_Capacity()
        {
            FixedStringBuilder target = new FixedStringBuilder(1);
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.IsTrue(target.TryResize(2, 0));
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.AreEqual("XX", target.ToString());
        }

        /// <summary>
        /// TryResize only reduces capacity if there is room.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryResize_Only_Reduces_Capacity_If_There_Is_Room()
        {
            FixedStringBuilder target = new FixedStringBuilder(3);
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.IsTrue(target.TryAppend('X', 0));
            Assert.IsFalse(target.TryResize(1, 0));
            Assert.IsTrue(target.TryResize(2, 0));
            Assert.AreEqual("XX", target.ToString());
        }

        /// <summary>
        /// TryResize rejects capacity smaller than reserve.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryResize_Rejects_Capacity_Smaller_Than_Reserve()
        {
            FixedStringBuilder target = new FixedStringBuilder(1);
            try
            {
                target.TryResize(1, 2);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }
        }

        /// <summary>
        /// TryResize rejects negative reserve.
        /// </summary>
        [TestMethod]
        public void FixedStringBuilder_TryResize_Rejects_Negative_Reserve()
        {
            FixedStringBuilder target = new FixedStringBuilder(1);
            try
            {
                target.TryResize(0, -1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            }
        }
    }
}

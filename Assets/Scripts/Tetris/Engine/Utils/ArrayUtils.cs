using System.Linq;

namespace Selftris.Tetris.Engine.Utils
{
    /// <summary>
    /// Array utilities class.
    /// </summary>
    public static class ArrayUtils
    {
        /// <summary>
        /// Insert into the middle of an array.
        /// </summary>
        /// <param name="originalArr">The original array.</param>
        /// <param name="at">The index to insert at.</param>
        /// <param name="content">The object to be inserted.</param>
        /// <returns>The modified array.</returns>
        /// <example>
        /// <code>
        /// int[] intArr = new int[] { 0, 1, 2, 3 };
        /// intArr = InsertAt(intArr, 2, 6);
        /// // intArr is { 0, 1, 6, 2, 3 }
        /// </code>
        /// </example>
        public static T[] InsertAt<T>(T[] originalArr, int at, T content)
        {
            T[] newArr = new T[originalArr.Length + 1];
            int off = 0;
            for (int i = 0; i < newArr.Length; i++)
            {
                if (i == at || (i - off > originalArr.Length - 1))
                {
                    off = 1;
                    newArr[i] = content;
                    continue;
                }
                newArr[i] = originalArr[i - off];
            }
            return newArr;
        }

        /// <summary>
        /// Remove an object from an array.
        /// </summary>
        /// <param name="originalArr">The original array.</param>
        /// <param name="content">The object to be removed.</param>
        /// <returns>The modified array.</returns>
        /// <example>
        /// <code>
        /// int[] intArr = new int[] { 0, 1, 2, 3 };
        /// intArr = RemoveFrom(intArr, 2);
        /// // intArr is { 0, 1, 3 }
        /// </code>
        /// </example>
        public static T[] RemoveFrom<T>(T[] originalArr, T content)
        {
            return originalArr.Where(val => !val.Equals(content)).ToArray();
        }
    }
}

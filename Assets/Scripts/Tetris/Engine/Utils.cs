using System;
using System.Linq;

namespace Selftris.Tetris.Engine
{
    public static class Utils
    {
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

        public static T[] RemoveFrom<T>(T[] originalArr, T content)
        {
            return originalArr.Where(val => !val.Equals(content)).ToArray();
        }
    }
}

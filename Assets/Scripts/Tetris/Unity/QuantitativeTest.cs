using UnityEngine;
using UnityEngine.Assertions;
using Selftris.Tetris.Engine;

namespace Selftris.Tetris.Unity
{
    public class QuantitativeTest : MonoBehaviour
    {
        private void Start()
        {
            TestInsertAt();
            TestRemoveFrom();
        }

        public void TestInsertAt()
        {
            int[] array = new int[] { 1, 2, 3 };
            Assert.IsTrue(ArrayEqual(Utils.InsertAt(array, -1, 0), new int[] { 1, 2, 3, 0 }));
            Assert.IsTrue(ArrayEqual(Utils.InsertAt(array, 0, 0), new int[] { 0, 1, 2, 3 }));
            Assert.IsTrue(ArrayEqual(Utils.InsertAt(array, 2, 0), new int[] { 1, 2, 0, 3, }));
        }

        public void TestRemoveFrom()
        {
            int[] array = new int[] { 1, 2, 3, 4 };
            Assert.IsTrue(ArrayEqual(Utils.RemoveFrom(array, 4), new int[] { 1, 2, 3 }));
            Assert.IsTrue(ArrayEqual(Utils.RemoveFrom(array, 1), new int[] { 2, 3, 4 }));
            Assert.IsTrue(ArrayEqual(Utils.RemoveFrom(array, 2), new int[] { 1, 3, 4, }));
        }

        public bool ArrayEqual<T>(T[] a, T[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (!a[i].Equals(b[i])) { return false; }
            }
            return true;
        }
    }
}

using UnityEngine;

namespace WOM.SheetData
{
    public class SheetData : ScriptableObject
    {
        public int dataId;
        public T CopyInstance<T>()
        {
            return (T)this.MemberwiseClone();
        }
    }

   
}


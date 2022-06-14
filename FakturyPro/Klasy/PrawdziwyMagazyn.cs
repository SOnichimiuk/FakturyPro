using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FakturyPro.Klasy
{
    public class PrawdziwyMagazyn : Magazyn
    {
        private static PrawdziwyMagazyn instance;

        private List<TowarMagazyn> listaTowarow;

        private PrawdziwyMagazyn()
        {
            listaTowarow = new List<TowarMagazyn>();
        }

        public static PrawdziwyMagazyn Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PrawdziwyMagazyn();
                }
                return instance;
            }
        }

        public void Serializuj(Stream stream, IFormatter formatter)
        {
            foreach (TowarMagazyn tm in listaTowarow)
            {
                formatter.Serialize(stream, tm);
            }
        }

        public void Deserializuj(Stream stream, IFormatter formatter)
        {
            TowarMagazyn tm;
            while (stream.Position < stream.Length)
            {
                tm = formatter.Deserialize(stream) as TowarMagazyn;
                listaTowarow.Add(tm);
            }
        }

        public void Add(TowarMagazyn item)
        {
            listaTowarow.Add(item);
        }

        public void Clear()
        {
            listaTowarow.Clear();
        }

        public bool Contains(TowarMagazyn item)
        {
            return listaTowarow.Contains(item);
        }

        public void CopyTo(TowarMagazyn[] array, int arrayIndex)
        {
            listaTowarow.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return listaTowarow.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TowarMagazyn item)
        {
            return listaTowarow.Remove(item);
        }

        public IEnumerator<TowarMagazyn> GetEnumerator()
        {
            return listaTowarow.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}

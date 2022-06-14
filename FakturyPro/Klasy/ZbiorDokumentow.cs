using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakturyPro.Klasy
{
    [Serializable]
    class ZbiorDokumentow<T> : ICollection<T> where T: Dokument
    {
        List<T> lista = new List<T>();

        static ZbiorDokumentow<Faktura> faktury = null;
        static ZbiorDokumentow<Zamowienie> zamowienia = null;
        private ZbiorDokumentow() { }

        public static ZbiorDokumentow<Faktura> FakturyInstance
        {
            get
            {
                if (faktury == null)
                {
                    faktury = new ZbiorDokumentow<Faktura>();
                }
                return faktury;
            }
        }

        public static ZbiorDokumentow<Zamowienie> ZamowieniaInstance
        {
            get
            {
                if (zamowienia == null)
                {
                    zamowienia = new ZbiorDokumentow<Zamowienie>();
                }
                return zamowienia;
            }
        }


        public void Add(T item)
        {
            lista.Add(item);
        }

        public void Clear()
        {
            lista.Clear();
        }

        public bool Contains(T item)
        {
            return lista.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lista.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return lista.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return lista.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Filtrator(lista);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return lista.GetEnumerator();
        }
    }
}

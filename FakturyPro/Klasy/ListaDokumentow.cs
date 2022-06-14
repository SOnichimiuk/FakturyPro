using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakturyPro.Klasy
{
    public abstract class ListaDokumentow<T> : ICollection<T> where T : Dokument
    {
        private List<T> lista;

        private int version = 0;

        private string klient = string.Empty;
        private string nrDokumentu = string.Empty;

        public ListaDokumentow()
        {
            lista = new List<T>();
        }

        private int Version
        {
            get { return version; }
            set { version = value % 10000; }
        }

        public string Klient
        {
            get { return klient; }
            set
            {
                klient = (value == null) ? string.Empty : value;
                Version++;
            }
        }

        public string NrDokumentu
        {
            get { return nrDokumentu; }
            set
            {
                nrDokumentu = (value == null) ? string.Empty : value;
                Version++;
            }
        }

        public void Add(T item)
        {
            Version++;
            lista.Add(item);
        }

        public void Clear()
        {
            Version++;
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
            Version++;
            return lista.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Filtrator<T>(this);
            //return lista.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new Filtrator<T>(this);
            //return lista.GetEnumerator();
        }

        private class Filtrator<R> : IEnumerator<R> where R : Dokument
        {
            int index = -1;
            ListaDokumentow<R> dokumenty;
            int versionSnapshot;
            R current;

            public Filtrator(ListaDokumentow<R> dokumenty)
            {
                this.dokumenty = dokumenty;
                versionSnapshot = dokumenty.Version;
            }

            public R Current
            {
                get
                {
                    if (current == null)
                    {
                        throw new InvalidOperationException();
                    }
                    return current;
                }
            }

            public void Dispose()
            {
                //throw new NotImplementedException();
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (versionSnapshot != dokumenty.version)
                {
                    // Invalidate Enumerator
                    throw new InvalidOperationException();
                }

                index++;
                R dokument;
                while (index < dokumenty.lista.Count)
                {
                    dokument = dokumenty.lista[index];

                    if (dokument != null && dokument.Klient.Nazwa.ToLower().Contains(dokumenty.klient) && dokument.NrDokumentu.ToLower().Contains(dokumenty.nrDokumentu))
                    {
                        current = dokument;
                        return true;
                    }

                    index++;
                }
                // No more elements
                current = null; // Must be undefined
                return false;
            }

            public void Reset()
            {
                if (versionSnapshot != dokumenty.version)
                {
                    // Invalidate Enumerator
                    throw new InvalidOperationException();
                }

                current = null;
                index = -1;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FakturyPro.Klasy
{
    public class LoggerMagazyn : Magazyn
    {
        private Magazyn magazyn;

        public LoggerMagazyn(Magazyn magazyn)
        {
            if (magazyn == null)
            {
                throw new ArgumentNullException();
            }
            this.magazyn = magazyn;
        }

        public void Add(TowarMagazyn item)
        {
            // TODO: Loguj np. System.out.println("Dodaje " + item.Nazwa);
            StreamWriter w = File.AppendText("logMagazynu.txt");
            w.Write("\r\n");
            w.WriteLine("{0} {1} {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), "Dodaje " + item.Nazwa);
            w.Close();
            magazyn.Add(item);
        }

        public void Clear()
        {
            // TODO: Loguj
            StreamWriter w = File.AppendText("logMagazynu.txt");
            w.Write("\r\n");
            w.WriteLine("{0} {1} {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), "Clear");
            w.Close();
            magazyn.Clear();
        }

        public bool Contains(TowarMagazyn item)
        {
            return magazyn.Contains(item);
        }

        public void CopyTo(TowarMagazyn[] array, int arrayIndex)
        {
            magazyn.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return magazyn.Count; }
        }

        public bool IsReadOnly
        {
            get { return magazyn.IsReadOnly; }
        }

        public bool Remove(TowarMagazyn item)
        {
            // TODO: Loguj
            StreamWriter w = File.AppendText("logMagazynu.txt");
            w.Write("\r\n");
            w.WriteLine("{0} {1} {2}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), "Usuwam " + item.Nazwa);
            w.Close();
            return magazyn.Remove(item);
        }

        public IEnumerator<TowarMagazyn> GetEnumerator()
        {
            return magazyn.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return magazyn.GetEnumerator();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace FakturyPro.Klasy
{
    public class ListaZamowien : ListaDokumentow<Zamowienie>
    {
        private static ListaZamowien instance;
        
        private ListaZamowien() { }

        public static ListaZamowien Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ListaZamowien();
                }
                return instance;
            }
        }

        public void Serializuj(Stream stream, IFormatter formatter)
        {
            foreach (Zamowienie zm in this)
            {
                formatter.Serialize(stream, zm);
            }
        }

        public void Deserializuj(Stream stream, IFormatter formatter)
        {
            Zamowienie zm;
            while (stream.Position < stream.Length)
            {
                zm = formatter.Deserialize(stream) as Zamowienie;
                this.Add(zm);
            }
        }

    }
}

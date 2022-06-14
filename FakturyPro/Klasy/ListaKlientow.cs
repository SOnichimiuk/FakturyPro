using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace FakturyPro.Klasy
{
    public class ListaKlientow : List<Kontrahent>
    {
        private static ListaKlientow instance;

        private ListaKlientow() { }

        public static ListaKlientow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ListaKlientow();
                }
                return instance;
            }
        }

        public void Serializuj(Stream stream, IFormatter formatter)
        {
            foreach (Kontrahent k in this)
            {
                formatter.Serialize(stream, k);
            }
        }

        public void Deserializuj(Stream stream, IFormatter formatter)
        {
            Kontrahent k;
            while (stream.Position < stream.Length)
            {
                k = formatter.Deserialize(stream) as Kontrahent;
                this.Add(k);
            }
        }
        
    }
}

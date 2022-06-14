using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace FakturyPro.Klasy
{
    public class ListaFaktur : ListaDokumentow<Faktura>
    {
        private static ListaFaktur instance;

        private ListaFaktur() { }

        public static ListaFaktur Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ListaFaktur();
                }
                return instance;
            }
        }

        public void Serializuj(Stream stream, IFormatter formatter)
        {
            foreach (Faktura fv in this)
            {
                formatter.Serialize(stream, fv);
            }
        }

        public void Deserializuj(Stream stream, IFormatter formatter)
        {
            Faktura fv;
            try
            {
                while (true)
                {
                    fv = formatter.Deserialize(stream) as Faktura;
                    if (fv != null)
                    {
                        this.Add(fv);
                    }
                    
                }
            }
            catch (SerializationException ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            //while (stream.Position < stream.Length)
            //{
            //    fv = formatter.Deserialize(stream) as Faktura;
            //    this.Add(fv);
            //}
        }

    }
}

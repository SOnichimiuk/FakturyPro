using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FakturyPro
{
    public class StorageCommands
    {
        private static RoutedUICommand addToList;
        private static RoutedUICommand removeFromList;

        static StorageCommands()
        {
            addToList = new RoutedUICommand("Dodaj zaznaczone do listy", "AddToList", typeof(StorageCommands));
            //addToList.InputGestures.Add(new KeyGesture(Key.OemPeriod, ModifierKeys.Control));
            addToList.InputGestures.Add(new KeyGesture(Key.Right, ModifierKeys.Control));

            removeFromList = new RoutedUICommand("Usuń zaznaczone z listy", "RemoveFromList", typeof(StorageCommands));
            //removeFromList.InputGestures.Add(new KeyGesture(Key.OemComma, ModifierKeys.Control));
            removeFromList.InputGestures.Add(new KeyGesture(Key.Left, ModifierKeys.Control));
        }

        public static RoutedUICommand AddToList
        {
            get { return addToList; }
        }

        public static RoutedUICommand RemoveFromList
        {
            get { return removeFromList; }
        }
    }

    public class ListCommands
    {
        private static RoutedUICommand add;
        private static RoutedUICommand remove;
        private static RoutedUICommand edit;
        private static RoutedUICommand clearSearch;

        static ListCommands()
        {
            add = new RoutedUICommand("Dodaj", "Add", typeof(ListCommands));
            add.InputGestures.Add(new KeyGesture(Key.Insert));

            remove = new RoutedUICommand("Usuń", "Remove", typeof(ListCommands));
            remove.InputGestures.Add(new KeyGesture(Key.Delete));

            edit = new RoutedUICommand("Edytuj", "Edit", typeof(ListCommands));
            edit.InputGestures.Add(new KeyGesture(Key.Enter));

            clearSearch = new RoutedUICommand("Wyczyść pola wyszukiwania", "ClearSearch", typeof(ListCommands));
            clearSearch.InputGestures.Add(new KeyGesture(Key.Escape));
        }

        public static RoutedUICommand Add
        {
            get { return add; }
        }

        public static RoutedUICommand Remove
        {
            get { return remove; }
        }

        public static RoutedUICommand Edit
        {
            get { return edit; }
        }

        public static RoutedUICommand ClearSearch
        {
            get { return clearSearch; }
        }
    }

    public class DocumentCommands
    {
        private static RoutedUICommand faktura;
        private static RoutedUICommand zamowienie;
        private static RoutedUICommand przyjecie;
        private static RoutedUICommand ksieguj;
        private static RoutedUICommand print;
        private static RoutedUICommand saveAsPDF;

        static DocumentCommands()
        {
            faktura = new RoutedUICommand("+ Faktura", "Faktura", typeof(DocumentCommands));
            faktura.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));

            zamowienie = new RoutedUICommand("+ Zamówienie", "Zamowienie", typeof(DocumentCommands));
            zamowienie.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));

            przyjecie = new RoutedUICommand("+ Przyjęcie", "Przyjecie", typeof(DocumentCommands));
            przyjecie.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));

            ksieguj = new RoutedUICommand("c Księguj", "Ksieguj", typeof(DocumentCommands));
            ksieguj.InputGestures.Add(new KeyGesture(Key.K, ModifierKeys.Control));

            print = new RoutedUICommand("Drukuj", "Print", typeof(DocumentCommands));
            print.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));

            saveAsPDF = new RoutedUICommand("Zapisz jako PDF", "SaveAsPDF", typeof(DocumentCommands));
            saveAsPDF.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
        }

        public static RoutedUICommand Faktura
        {
            get { return faktura; }
        }

        public static RoutedUICommand Zamowienie
        {
            get { return zamowienie; }
        }

        public static RoutedUICommand Przyjecie
        {
            get { return przyjecie; }
        }

        public static RoutedUICommand Ksieguj
        {
            get { return ksieguj; }
        }

        public static RoutedUICommand SaveAsPDF
        {
            get { return saveAsPDF; }
        }

        public static RoutedUICommand Print
        {
            get { return print; }
        }
    }

    public class AppCommands
    {
        private static RoutedUICommand save;
        private static RoutedUICommand cancel;
        private static RoutedUICommand exit;

        static AppCommands()
        {
            save = new RoutedUICommand("Zapisz", "Save", typeof(AppCommands));
            //save.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Control));

            cancel = new RoutedUICommand("Anuluj", "Cancel", typeof(AppCommands));
            //cancel.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));

            exit = new RoutedUICommand("Zakończ", "Exit", typeof(AppCommands));
            exit.InputGestures.Add(new KeyGesture(Key.F4, ModifierKeys.Alt));
        }

        public static RoutedUICommand Save
        {
            get { return save; }
        }

        public static RoutedUICommand Cancel
        {
            get { return cancel; }
        }

        public static RoutedUICommand Exit
        {
            get { return exit; }
        }
    }
}

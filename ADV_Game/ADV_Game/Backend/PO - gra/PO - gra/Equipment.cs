using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POProjekt
{

    public class Equipment
    {
        LinkedList<Item> bagpack = new LinkedList<Item>();
        public decimal bagpack_weight;

        public LinkedList<Item> Bagpack { get => bagpack; set => bagpack = value; }
        public decimal Bagpack_weight { get => bagpack_weight; set => bagpack_weight = value; }

        public Equipment()
        {
            bagpack_weight = 0;
        }

        public void AddItem(Item p)
        {
            bagpack.AddFirst(p);
            bagpack_weight += p.Weight;
        }

        public void RemoveItem(Item p)
        {
            bagpack_weight -= p.Weight;
            bagpack.Remove(p);

        }

        //Ta funkcja będzie korzystała z takich poleceń:
        //Przedmiot szukanyPrzedmiot = ekwipunek.znajdz_przedmiot(p => p.Nazwa == "Miecz"); gdzie "Miecz" można zastąpić inputIntegerem wprowadzonym przez gracza
        // ew stringiem, któremu będzie przyporządkowany numer. 

        public Item FindItem(Func<Item, bool> condition)
        {
            return bagpack.FirstOrDefault(condition);
        }



        public void BackpackContains()
        {
            Console.Clear();
            if (bagpack.Count == 0)
            {
                Console.WriteLine("Twój plecak jest pusty!");
                return;
            }
            List<Item> sortedList = new List<Item>(bagpack);
            sortedList.Sort();

            bagpack = new LinkedList<Item>(sortedList);

            int i = 1;
            foreach (Item x in bagpack)
            {
                Console.WriteLine($"{i}.{x.type} [{x.Weight}kg]\n{x.description}");
                i++;
            }
        }

        public Item RemoveItemAt(int index)
        {
            if (index >= 0 && index < bagpack.Count)
            {
                Item removedItem = bagpack.ElementAt(index);
                bagpack_weight -= removedItem.Weight;
                // Zamień LinkedList na List, usuń element i przekonwertuj z powrotem na LinkedList 
                List<Item> itemList = new List<Item>(bagpack);
                itemList.RemoveAt(index);
                bagpack = new LinkedList<Item>(itemList);
                return removedItem;
            }
            else
            {
                Console.WriteLine("Nieprawidłowy indeks. Nie można usunąć przedmiotu.");
                return null;
            }
        }


    }
}

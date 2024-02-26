using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POProjekt
{

    public abstract class Localization
    {
        public uint amount_of_items;
        public Item item;
        public string localization_name;
        public string localization_description;

        public uint Amount_of_items { get => amount_of_items; set => amount_of_items = value; }
        public Item Item { get => item; set => item = value; }
        public string Localization_name { get => localization_name; set => localization_name = value; }
        public string Localization_description { get => localization_description; set => localization_description = value; }

        public Localization()
        {
            this.amount_of_items = 1;
            this.item = null;
            this.localization_name = string.Empty;
            this.localization_description = string.Empty;
        }

        public virtual void LookAround()
        {
            Console.WriteLine(Localization_description);
        }

        public virtual void TakeItem(Item item, Equipment p)
        {
            p.AddItem(item);
            this.amount_of_items--;
        }

    }

    public class StartingLocation : Localization
    {
        public StartingLocation() : base()
        {
            this.localization_name = "Początek";
            this.localization_description = "Spokojna, zielona polana";
        }


    }

    public class Valley : Localization
    {
        public Valley() : base()
        {
            this.localization_name = "Dolina Magów";
            this.localization_description = "Wszędzie widzisz marumurowe wieże, które sięgają aż po sam nieboskłon.\n" +
                "Zewsząd słyszysz zażarte dyskuje oraz dźwięki rzucanych zaklęć.";
            //Generowanie losowego przedmiotu do znalezienia w lokalizacji 
            Random r = new Random();
            int rInt = r.Next(0, 3);
            this.item = new Item((ItemType)rInt);
        }

    }

    public class Port : Localization
    {
        public Port() : base()
        {
            this.localization_name = "Port";
            this.localization_description = "Czujesz unoszącą się morską bryzę.\n" +
                "Z oddali słyszysz śpiewane szanty oraz rozmaite wiązanki przekleństw.";
            Random r = new Random();
            int rInt = r.Next(2, 4);
            this.item = new Item((ItemType)rInt);
        }

    }

    public class Graveyard : Localization
    {
        public Graveyard() : base()
        {
            this.localization_name = "Cmentarzysko";
            this.localization_description = "Ciemny, ponury cmentarz.\n" +
                "Nienaturalna cisza tego miejsca przyprawia cię o gęsią skórkę..";
            Random r = new Random();
            int rInt = r.Next(4, 6);
            this.item = new Item((ItemType)rInt);
        }

    }



    public class Gates : Localization
    {
        public Gates() : base()
        {
            this.localization_name = "Wrota";
            this.localization_description = "Przed tobą stoją masywne zamknięte wrota\n";
        }

        public bool OpenTheGates(Player p)
        {
            if (p.Key_count < 3)
            {
                Console.WriteLine("Nie jesteś wstanie otworzyć wrót.\nWróć kiedy zdobędziesz wszystkie trzy klucze.");
                return true;
            }
            else { return false; }
        }
    }
}

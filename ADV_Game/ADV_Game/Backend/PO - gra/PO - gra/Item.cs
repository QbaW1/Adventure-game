using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POProjekt
{
    public class Item : IComparable<Item>
    {
        public ItemType type;
        public decimal weight;
        public string description;
       

        public decimal Weight { get => weight; set => weight = value; }
        public string Description { get => description; set => description = value; }
        public ItemType Type { get => type; set => type = value; }

        public Item() 
        { 
            this.weight = 0;
            this.description = null;
        }

        public Item(ItemType rodzaj) : this()
        {
            switch (rodzaj) 
            {
                case ItemType.Sword:
                    this.type = ItemType.Sword;
                    this.weight = 15.5M;
                    this.description = "Wygląda na stary.\nMimo to wciąż możesz nim kogoś nieźle poobijać.\n";
                    break;
                case ItemType.Armor:
                    this.type = ItemType.Armor;
                    this.weight = 45.0M;
                    this.description = "Wytrzymała, a do tego elegancka.\nNic tylko w niej wyruszyć na przygodę!\n";
                    break;
                case ItemType.HealthPotion:
                    this.type = ItemType.HealthPotion;
                    this.weight = 5.0M;
                    this.description = "Niezbędnik każdego poszukiwacza przygód.\n";
                    break;
                case ItemType.Dagger:
                    this.type = ItemType.Dagger;
                    this.weight = 10.0M;
                    this.description = "Lekki, a do tego zabójczy.\n";
                    break;
                case ItemType.MagicScepter:
                    this.type = ItemType.MagicScepter;
                    this.weight = 30.0M;
                    this.description = "Niezbędnik każdego szanującego się praktyka magii.\n";
                    break;
                case ItemType.HealthStone:
                    this.type = ItemType.HealthStone;
                    this.weight = 22.0M;
                    this.description = "Bijący ciepłem, kawałek czerwonego kamienia.\nSprawia, że czujesz się silniejszy.";
                    break;
                case ItemType.Key:
                    this.type = ItemType.Key;
                    this.description = "Stary, mosiężny klucz, potrzebny do otworzenia bramy.\n";
                    break;

            }
        }

        public string zobacz_przedmiot()
        {
            return this.description;
        }

        public int CompareTo(Item? other)
        {
            if (other is null)
                return 1;
            return this.Type.CompareTo(other.Type);
        }
    }

    public enum ItemType //W zależności od rodzaju oręża (i klasy) gracz powinien dostawać różne bonusy 
    {
        MagicScepter,
        Dagger,
        HealthPotion,
        Sword,
        HealthStone,
        Armor,
        Key
    }

}

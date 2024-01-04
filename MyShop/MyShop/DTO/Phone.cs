using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class Phone:INotifyPropertyChanged, ICloneable
    {

        public int phoneID { get; set; }
        public string phoneName { get; set; }
        public string imagePath { get; set; }
        public double screenSize { get; set; }
        public int phoneRam {  get; set; }
        public decimal price { get; set; }
        public string manufacturer { get; set; }
        public int batteryCapacity { get; set; }
        public Category category { get; set; }
        public int phoneRom {  get; set; }
        //public int promotionID { get; set; }
        public Promotion? promotion { get; set; }
        public Decimal? promotionPrice { get; set; }
        public int quantity { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }


        public void copy(Phone other)
        {
            this.phoneID = other.phoneID;
            this.phoneName = other.phoneName;
            this.imagePath = other.imagePath;
            this.screenSize = other.screenSize;
            this.phoneRam = other.phoneRam;
            this.price = other.price;
            this.manufacturer = other.manufacturer;
            this.batteryCapacity = other.batteryCapacity;
            this.category = other.category;
            this.phoneRom = other.phoneRom;
            //this.promotionID = other.promotionID;
            this.promotion = other.promotion;
            this.promotionPrice = other.promotionPrice;
            this.quantity = other.quantity;
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class Order : INotifyPropertyChanged
    {

        public int orderID { get; set; }
        public string cusName { get; set; }
        public string cusAddress { get; set; }
        public DateTime orderDate { get; set; }
        public Decimal? priceTotal { get; set; }
        public Decimal? profitTotal { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class Purchase : INotifyPropertyChanged
    {

        public int purchaseID { get; set; }
        public int orderID {  get; set; }
        //public int phoneID { get; set; }
        public int quantity { get; set; }
        public Phone phone { get; set; }
        public Decimal totalPrice { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

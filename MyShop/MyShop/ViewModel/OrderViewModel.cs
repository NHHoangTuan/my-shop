using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    internal class OrderViewModel : INotifyPropertyChanged
    {

        public BindingList<Order> orderList { get; set; } = new BindingList<Order>();
        public List<Order> selectedOrderList { get; set; } = new List<Order>();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

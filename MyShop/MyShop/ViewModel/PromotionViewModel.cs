using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    internal class PromotionViewModel : INotifyPropertyChanged
    {

        public BindingList<Promotion> promotions { get; set; } = new BindingList<Promotion>();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

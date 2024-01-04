using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    internal class PhoneViewModel : INotifyPropertyChanged
    {

        public BindingList<Phone> phoneList {  get; set; } = new BindingList<Phone>();
        public List<Phone> selectedPhoneList { get; set; } = new List<Phone>();


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

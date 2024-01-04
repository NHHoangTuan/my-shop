using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ViewModel
{
    internal class CategoryViewModel : INotifyPropertyChanged
    {
        public BindingList<Category> catList {  get; set; } = new BindingList<Category>();
        public List<Category> selectedCatList { get; set; } = new List<Category>();


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

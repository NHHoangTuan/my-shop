using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class Category: INotifyPropertyChanged, ICloneable
    {
        public int catID { get; set; }
        public string catName { get; set; }
        public string description { get; set; }
        public string logoPath { get; set; }
        public BindingList<Phone>? phoneList { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

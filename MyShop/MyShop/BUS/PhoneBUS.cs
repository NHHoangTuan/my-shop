using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Config;
using Aspose.Cells;

namespace MyShop.BUS
{
    internal class PhoneBUS
    {

        private PhoneDAO _phoneDAO;

        public PhoneBUS()
        {
            _phoneDAO = new PhoneDAO();
        }

        public BindingList<Phone> getAllPhone()
        {
            return _phoneDAO.getAllPhone();
        }


        public string uploadImage(FileInfo selectedImage, int id, string key)
        {
            _phoneDAO.updateImage(id, key);

            var folder = AppDomain.CurrentDomain.BaseDirectory;

            var filePath = $"{folder}/Assets/Images/Data/{key}.png";
            var relativePath = $"Assets/Images/Data/{key}.png";

            File.Copy(selectedImage.FullName, filePath);

            return relativePath;
        }

        public int saveProduct(Phone phone)
        {
            int id = _phoneDAO.insertPhone(phone);

            return id;
        }

        public void deletePhone(Phone phone)
        {
            _phoneDAO.deletePhone(phone);
        }

        public void updatePhone(Phone phone)
        {
            _phoneDAO.updatePhone(phone);
        }

        // Compare method for sorting phones by name
        public int ComparePhonesByNameInCreasing(Phone x, Phone y)
        {

            return string.Compare(x.phoneName, y.phoneName, StringComparison.OrdinalIgnoreCase);

        }

        public int ComparePhonesByNameDeCreasing(Phone x, Phone y)
        {

            return string.Compare(y.phoneName, x.phoneName, StringComparison.OrdinalIgnoreCase);

        }

        public int ComparePhonesByPriceIncreasing(Phone x, Phone y)
        {
            if (x == null && y == null)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;

            return Nullable.Compare(x.promotionPrice, y.promotionPrice);
        }

        public int ComparePhonesByPriceDecreasing(Phone x, Phone y)
        {
            // Đảo ngược kết quả so sánh để có thứ tự giảm dần
            return ComparePhonesByPriceIncreasing(y, x);
        }

        // Merge Sort implementation
        public List<Phone> MergeSort(List<Phone> list, Comparison<Phone> comparison)
        {
            if (list.Count <= 1)
                return list;

            int middle = list.Count / 2;
            List<Phone> left = new List<Phone>(list.GetRange(0, middle));
            List<Phone> right = new List<Phone>(list.GetRange(middle, list.Count - middle));

            left = MergeSort(left, comparison);
            right = MergeSort(right, comparison);

            return Merge(left, right, comparison);
        }

        private List<Phone> Merge(List<Phone> left, List<Phone> right, Comparison<Phone> comparison)
        {
            List<Phone> result = new List<Phone>(left.Count + right.Count);
            int leftIndex = 0, rightIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                if (comparison(left[leftIndex], right[rightIndex]) <= 0)
                {
                    result.Add(left[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    result.Add(right[rightIndex]);
                    rightIndex++;
                }
            }

            while (leftIndex < left.Count)
            {
                result.Add(left[leftIndex]);
                leftIndex++;
            }

            while (rightIndex < right.Count)
            {
                result.Add(right[rightIndex]);
                rightIndex++;
            }

            return result;
        }

        public int getRowPerPage()
        {
            int _rowsPerPage;
            string rowsPerPageString = AppConfig.GetValue(AppConfig.NumberProductPerPage);
            if (int.TryParse(rowsPerPageString, out int parsedRowsPerPage))
            {
                _rowsPerPage = parsedRowsPerPage;
            }
            else
            {
                // Giá trị mặc định nếu không thể chuyển đổi sang int
                _rowsPerPage = 5;
            }

            return _rowsPerPage;
        }

        public List<Phone> getPhoneListByCategory(int categoryId)
        {
            return _phoneDAO.getPhoneListByCategory(categoryId);
        }


        public List<Phone> getPhoneListFromExcel(string fileName)
        {
            var workbook = new Workbook(fileName);
            var tabPhones = workbook.Worksheets;
            List<Phone> list = new List<Phone>();
            List<Category> categoryList = new List<Category>();
            List<int> catIDList = new List<int>();

            var _catBUS = new CategoryBUS();
            categoryList = _catBUS.getCategoryListFromExcel(fileName);
            if (categoryList.Count == 0)
            {
                categoryList = _catBUS.getAllCategory().ToList();
            }

            for (int i = 1; i < tabPhones.Count; i++)
            {
                
                

                var column = 'B';
                var row = 3;
                var cell = tabPhones[i].Cells[$"{column}{row}"];

                while (cell.Value != null)
                {
                    string phoneName = cell.StringValue;
                    int phoneRam = tabPhones[i].Cells[$"C{row}"].IntValue;
                    int phoneRom = tabPhones[i].Cells[$"D{row}"].IntValue;
                    double screenSize = tabPhones[i].Cells[$"E{row}"].DoubleValue;
                    int price = tabPhones[i].Cells[$"F{row}"].IntValue;
                    string imagePath = tabPhones[i].Cells[$"G{row}"].StringValue;
                    int batteryCapacity = tabPhones[i].Cells[$"H{row}"].IntValue;
                    int quantity = tabPhones[i].Cells[$"I{row}"].IntValue;
                    string manufacturer = tabPhones[i].Cells[$"J{row}"].StringValue;
                    var category = categoryList[i - 1];
                    FileInfo fileInfo = new FileInfo(imagePath);

                    var newPhone = new Phone()
                    {
                        phoneName = phoneName,
                        phoneRam = phoneRam,
                        phoneRom = phoneRom,
                        screenSize = screenSize,
                        price = price,
                        
                        batteryCapacity = batteryCapacity,
                        quantity = quantity,
                        manufacturer = manufacturer,
                        category = category,
                    };

                    int id = _phoneDAO.insertPhone(newPhone);
                    string key = Guid.NewGuid().ToString();
                    newPhone.imagePath = uploadImage(fileInfo,id,key);

                    list.Add(newPhone);
                    row++;
                    cell = tabPhones[i].Cells[$"{column}{row}"];
                }
            }
            return list;
        }

    }
}

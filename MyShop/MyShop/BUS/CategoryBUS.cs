using Aspose.Cells;
using Microsoft.Win32;
using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    internal class CategoryBUS
    {

        private CategoryDAO _catDAO;

        public CategoryBUS() { 
            _catDAO = new CategoryDAO();
        }

        public BindingList<Category> getAllCategory()
        {
            return _catDAO.getAllCategory();
        }


        public string uploadImage(FileInfo selectedImage, int id, string key)
        {
            _catDAO.updateImage(id, key);

            var folder = AppDomain.CurrentDomain.BaseDirectory;

            var filePath = $"{folder}/Assets/Images/Data/{key}.png";
            var relativePath = $"Assets/Images/Data/{key}.png";

            File.Copy(selectedImage.FullName, filePath);

            return relativePath;
        }

        public int saveCategory(Category category)
        {
            int id = _catDAO.insertCategory(category);

            return id;
        }

        public void deleteCategory(Category category)
        {
            _catDAO.deleteCategory(category);
        }

        public void updateCategory(Category category)
        {
            _catDAO.updateCategory(category);
        }


        public List<Category> getCategoryListFromExcel(string fileName)
        {
            var workbook = new Workbook(fileName);
            var tabCategory = workbook.Worksheets[0];
            List<Category> list = new List<Category>();

            var column = 'B';
            var row = 3;
            var cell = tabCategory.Cells[$"{column}{row}"];

            while (cell.Value != null)
            {
                string catName = cell.StringValue;
                string desciption = tabCategory.Cells[$"C{row}"].StringValue;
                string logoPath = tabCategory.Cells[$"D{row}"].StringValue;

                FileInfo fileInfo = new FileInfo(logoPath);

                var newCat = new Category()
                {
                    catName = catName,
                    description = desciption,
                    
                };

                //var catID = _catDAO.insertCategory(newCat);

                int id = _catDAO.insertCategory(newCat);
                string key = Guid.NewGuid().ToString();
                newCat.logoPath = uploadImage(fileInfo, id, key);
                newCat.catID = id;
                //catIDList.Add(catID);
                list.Add(newCat);
                row++;
                cell = tabCategory.Cells[$"{column}{row}"];
            }

            return list;
            
        }

    }
}

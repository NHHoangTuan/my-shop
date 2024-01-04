using Microsoft.Data.SqlClient;
using MyShop.DTO;
using MyShop.SQLHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DAO
{
    internal class CategoryDAO: SQLData
    {

        public BindingList<Category> getAllCategory()
        {
            var sqlquery = "select * from category";
            var command = new SqlCommand(sqlquery, Connection);
            var reader = command.ExecuteReader();

            BindingList<Category> list = new BindingList<Category>();
            while (reader.Read())
            {
                var ID = (int)reader["CatID"];
                var categoryName = (string)reader["CatName"];
                var logoPath = (string)reader["CatLogo"];
                var description = (string)reader["CatDescription"];
              


                Category category = new Category()
                {
                    catID = ID,
                    catName = categoryName,
                    logoPath = logoPath,
                    description = description

                };

                list.Add(category);
            }
            reader.Close();
            return list;
        }


        public int insertCategory(Category category)
        {
            // insert to SQL
            var sqlquery = "insert into category (CatName, CatDescription) " +
                "values (@categoryName, @description)";
            var command = new SqlCommand(sqlquery, Connection);

            command.Parameters.AddWithValue("@categoryName", category.catName);
            command.Parameters.AddWithValue("@description", category.description);
           
            command.ExecuteNonQuery();

            // select SQL
            int id = -1;
            string sql1 = "SELECT TOP 1 CatID FROM category ORDER BY CatID DESC ";

            var command1 = new SqlCommand(sql1, Connection);

            var reader = command1.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader["CatID"];
            }

            reader.Close();

            return id;
        }


        public void updateImage(int id, string key)
        {
            // update SQL
            string sql = $"""
                update category 
                set CatLogo = 'Assets/Images/Data/{key}.png'
                where CatID = {id}
                """;

            var command = new SqlCommand(sql, Connection);

            command.ExecuteNonQuery();
        }

        public void deleteCategory(Category category)
        {
            var query = "delete from category where CatID = @catID";
            var command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@catID", category.catID);
            command.ExecuteNonQuery();
        }

        public void updateCategory(Category category)
        {
            var query = "update category set CatName = @catName, CatDescription = @description" +
                "where CatID = @catID";
            var command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@catName", category.catName);
            command.Parameters.AddWithValue("@description", category.description);
           
            command.Parameters.AddWithValue("@catID", category.catID);

            command.ExecuteNonQuery();

        }

        public Category getCategoryByID(int id)
        {
            var sql1 = "select CatName, CatLogo, CatDescription from category where CatID = @catID";
            var command1 = new SqlCommand(sql1, Connection);
            command1.Parameters.AddWithValue("@catID", id);
            command1.ExecuteNonQuery();
            var reader1 = command1.ExecuteReader();
            var category = new Category();
            while (reader1.Read())
            {
                category.catID = id;
                category.catName = (string)reader1["CatName"];
                category.description = (string)reader1["CatDescription"];
                category.logoPath = (string)reader1["CatLogo"];
            }

            return category;
        }

    }
}

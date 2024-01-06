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
    internal class PhoneDAO:SQLData
    {

        public BindingList<Phone> getAllPhone()
        {
            var sqlquery = "select * from product";
            var command = new SqlCommand(sqlquery, Connection);
            var reader = command.ExecuteReader();

            BindingList<Phone> list = new BindingList<Phone>();
            CategoryDAO _catDAO = new CategoryDAO();
            PromotionDAO _promoDAO = new PromotionDAO();
            while (reader.Read())
            {
                var ID = (int)reader["ProID"];
                var phoneName = (string)reader["ProName"];
                var imagePath = (string)reader["ImagePath"];
                var manufacturer = (string)reader["Manufacturer"];
                var price = (int)reader["Price"];
                var phoneRam = (int)reader["Ram"];
                var phoneRom = (int)reader["Rom"];
                var screenSize = (double)reader["ScreenSize"];
                var batteryCapacity = (int)reader["BatteryCapacity"];
                var categoryID = (int)reader["CatID"];
                var quantity = (int)reader["Quantity"];
                var promoID = reader["PromoID"] == DBNull.Value ? null : (int?)reader["PromoID"];
                var promoPrice = reader["PromotionPrice"] == DBNull.Value ? (int)reader["Price"] : (decimal?)reader["PromotionPrice"];


                Phone phone = new Phone()
                {
                    phoneID = ID,
                    phoneName = phoneName,
                    imagePath = imagePath,
                    manufacturer = manufacturer,
                    price = price,
                    phoneRam = phoneRam,
                    phoneRom = phoneRom,
                    screenSize = screenSize,
                    batteryCapacity = batteryCapacity,
                    category = _catDAO.getCategoryByID(categoryID),
                    quantity = quantity,
                    promotionPrice = promoPrice,
                    promotion = _promoDAO.getPromotionByID(promoID)

                };

                list.Add(phone);
            }
            reader.Close();
            return list;
        }


        public int insertPhone(Phone phone)
        {
            // insert to SQL
            var sqlquery = "insert into product (ProName, Ram, Rom, ScreenSize, BatteryCapacity, CatID, Quantity, Manufacturer, Price, PromotionPrice) " +
                "values (@phoneName, @phoneRam, @phoneRom, @screenSize, @batteryCapacity, @categoryID, @quantity, @manufacturer, @price, @promoPrice)";
            var command = new SqlCommand(sqlquery, Connection);

            command.Parameters.AddWithValue("@phoneName", phone.phoneName);
            command.Parameters.AddWithValue("@phoneRam", phone.phoneRam);
            command.Parameters.AddWithValue("@phoneRom", phone.phoneRom);
            command.Parameters.AddWithValue("@screenSize", phone.screenSize);
            command.Parameters.AddWithValue("@batteryCapacity", phone.batteryCapacity);
            command.Parameters.AddWithValue("@categoryID", phone.category.catID);
            command.Parameters.AddWithValue("@quantity", phone.quantity);
            command.Parameters.AddWithValue("@manufacturer", phone.manufacturer);
            command.Parameters.AddWithValue("@price", phone.price);
            command.Parameters.AddWithValue("@promoPrice", phone.promotionPrice == null ? phone.price: phone.promotion);

            command.ExecuteNonQuery();

            // select SQL
            int id = -1;
            string sql1 = "SELECT TOP 1 ProID FROM product ORDER BY ProID DESC ";

            var command1 = new SqlCommand(sql1, Connection);

            var reader = command1.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader["ProID"];
            }

            reader.Close();

            return id;
        }


        public void updateImage(int id, string key)
        {
            // update SQL
            string sql = $"""
                update product 
                set ImagePath = 'Assets/Images/Data/{key}.png'
                where ProID = {id}
                """;

            var command = new SqlCommand(sql, Connection);

            command.ExecuteNonQuery();
        }

        public void deletePhone(Phone phone)
        {
            var query = "delete from product where ProID = @ID";
            var command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@ID", phone.phoneID);
            command.ExecuteNonQuery();
        }

        public void updatePhone(Phone phone)
        {
            var query = "update product set ProName = @phoneName, Ram = @phoneRam, Rom = @phoneRom, ScreenSize = @screenSize" +
                ", BatteryCapacity = @batteryCapacity, CatID = @categoryID, Quantity = @quantity, Manufacturer = @manufacturer, Price = @price, " +
                "PromoID = @promoID, PromotionPrice = @promoPrice " +
                "where ProID = @ID";
            var command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@phoneName", phone.phoneName);
            command.Parameters.AddWithValue("@phoneRam", phone.phoneRam);
            command.Parameters.AddWithValue("@phoneRom", phone.phoneRom);
            command.Parameters.AddWithValue("@screenSize", phone.screenSize);
            command.Parameters.AddWithValue("@batteryCapacity", phone.batteryCapacity);
            command.Parameters.AddWithValue("@categoryID", phone.category.catID);
            command.Parameters.AddWithValue("@quantity", phone.quantity);
            command.Parameters.AddWithValue("@manufacturer", phone.manufacturer);
            command.Parameters.AddWithValue("@price", phone.price);
            command.Parameters.AddWithValue("@ID", phone.phoneID);
            if (phone.promotion.promotionID == null)
            {
                phone.promotionPrice = phone.price;
            }
            command.Parameters.AddWithValue("@promoID", phone.promotion.promotionID == null ? DBNull.Value : phone.promotion.promotionID);
            command.Parameters.AddWithValue("@promoPrice", phone.promotionPrice == null ? phone.price : phone.promotionPrice);

            command.ExecuteNonQuery();

        }

        public List<Phone> getPhoneListByCategory(int categoryID)
        {
            var sqlquery = "select * from product where CatID = @catID";
            var command = new SqlCommand(sqlquery, Connection);

            command.Parameters.AddWithValue("@catID", categoryID);

            var reader = command.ExecuteReader();

            List<Phone> list = new List<Phone>();
            CategoryDAO _catDAO = new CategoryDAO();
            PromotionDAO _promoDAO = new PromotionDAO();
            while (reader.Read())
            {
                var ID = (int)reader["ProID"];
                var phoneName = (string)reader["ProName"];
                var imagePath = (string)reader["ImagePath"];
                var manufacturer = (string)reader["Manufacturer"];
                var price = (int)reader["Price"];
                var phoneRam = (int)reader["Ram"];
                var phoneRom = (int)reader["Rom"];
                var screenSize = (double)reader["ScreenSize"];
                var batteryCapacity = (int)reader["BatteryCapacity"];
                //var categoryID = (int)reader["CatID"];
                var quantity = (int)reader["Quantity"];
                var promoPrice = reader["PromotionPrice"] == DBNull.Value ? price : (decimal?)reader["PromotionPrice"];
                var promoID = reader["PromoID"] == DBNull.Value ? null : (int?)reader["PromoID"];


                Phone phone = new Phone()
                {
                    phoneID = ID,
                    phoneName = phoneName,
                    imagePath = imagePath,
                    manufacturer = manufacturer,
                    price = price,
                    phoneRam = phoneRam,
                    phoneRom = phoneRom,
                    screenSize = screenSize,
                    batteryCapacity = batteryCapacity,
                    category = _catDAO.getCategoryByID(categoryID),
                    quantity = quantity,
                    promotion = _promoDAO.getPromotionByID(promoID),
                    promotionPrice = promoPrice

                };

                list.Add(phone);
            }
            reader.Close();
            return list;
        }


        public Phone getPhoneByID(int id)
        {
            var sql1 = "select * from product where ProID = @proID";
            var command1 = new SqlCommand(sql1, Connection);
            command1.Parameters.AddWithValue("@proID", id);
            command1.ExecuteNonQuery();
            var reader1 = command1.ExecuteReader();
            var phone = new Phone();

            while (reader1.Read())
            {
                var ID = (int)reader1["ProID"];
                var phoneName = (string)reader1["ProName"];
                var imagePath = (string)reader1["ImagePath"];
                var manufacturer = (string)reader1["Manufacturer"];
                var price = (int)reader1["Price"];
                var phoneRam = (int)reader1["Ram"];
                var phoneRom = (int)reader1["Rom"];
                var screenSize = (double)reader1["ScreenSize"];
                var batteryCapacity = (int)reader1["BatteryCapacity"];
                var categoryID = (int)reader1["CatID"];
                var quantity = (int)reader1["Quantity"];
                var promoPrice = reader1["PromotionPrice"] == DBNull.Value ? price : (decimal?)reader1["PromotionPrice"];
                var promoID = reader1["PromoID"] == DBNull.Value ? null : (int?)reader1["PromoID"];

                var _catDAO = new CategoryDAO();
                var _promoDAO = new PromotionDAO();


                phone.phoneID = id;
                phone.phoneName = phoneName;
                phone.imagePath = imagePath;
                phone.manufacturer = manufacturer;
                phone.price = price;
                phone.phoneRam = phoneRam;
                phone.phoneRom = phoneRom;
                phone.screenSize = screenSize;
                phone.batteryCapacity = batteryCapacity;
                phone.category = _catDAO.getCategoryByID(categoryID);
                phone.quantity = quantity;
                phone.promotion = _promoDAO.getPromotionByID(promoID);
                phone.promotionPrice = promoPrice;
            }
            


            return phone;
        }

    }
}

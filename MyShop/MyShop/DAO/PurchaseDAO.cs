using Microsoft.Data.SqlClient;
using MyShop.BUS;
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
    internal class PurchaseDAO: SQLData
    {


        public DateTime getDateOrder(Purchase purchase)
        {
            DateTime result = new DateTime();
            var sqlquery = "select OrderDate from shop_order where OrderID = @orderID";
            var command = new SqlCommand(sqlquery, Connection);

            command.Parameters.AddWithValue("@orderID", purchase.orderID);
            var reader =  command.ExecuteReader();

            while (reader.Read())
            {
                var orderDate = (DateTime)reader["OrderDate"];
                result = orderDate;
            }

            return result;
        } 



        public BindingList<Purchase> getAllPurchase()
        {
            var sqlquery = "select * from purchase";
            var command = new SqlCommand(sqlquery, Connection);

            var reader = command.ExecuteReader();

            BindingList<Purchase> list = new BindingList<Purchase>();

            while (reader.Read())
            {
                var ID = (int)reader["PurchaseID"];
                var proID = (int)reader["ProID"];
                var orderID = (int)reader["OrderID"];
                var quantity = (int)reader["Quantity"];
                var priceTotal = (Decimal)reader["TotalPrice"];


                var _phoneDAO = new PhoneDAO();

                Purchase purchase = new Purchase()
                {
                    purchaseID = ID,
                    quantity = quantity,
                    phone = _phoneDAO.getPhoneByID(proID),
                    orderID = orderID,
                    totalPrice = priceTotal,

                };

                if (purchase.totalPrice != purchase.quantity * purchase.phone.promotionPrice)
                {
                    purchase.totalPrice = purchase.quantity * (decimal)purchase.phone.promotionPrice;
                }

                list.Add(purchase);
            }
            reader.Close();
            return list;
        }




        public BindingList<Purchase> getAllPurchaseByOrderID(int orderID)
        {
            var sqlquery = "select * from purchase where OrderID = @orderID";
            var command = new SqlCommand(sqlquery, Connection);

            command.Parameters.AddWithValue("@orderID", orderID);

            var reader = command.ExecuteReader();

            BindingList<Purchase> list = new BindingList<Purchase>();
            
            while (reader.Read())
            {
                var ID = (int)reader["PurchaseID"];
                var proID = (int)reader["ProID"];
                var quantity = (int)reader["Quantity"];
                var priceTotal = (Decimal)reader["TotalPrice"];
                

                var _phoneDAO = new PhoneDAO();

                Purchase purchase = new Purchase()
                {
                    purchaseID = ID,
                    quantity = quantity,
                    phone = _phoneDAO.getPhoneByID(proID),
                    orderID = orderID,
                    totalPrice = priceTotal,
                    
                };

                if (purchase.totalPrice != purchase.quantity*purchase.phone.promotionPrice)
                {
                    purchase.totalPrice = purchase.quantity * (decimal)purchase.phone.promotionPrice;
                }

                list.Add(purchase);
            }
            reader.Close();
            return list;
        }


        public int insertPurchase(Purchase purchase)
        {
            // insert to SQL
            var sqlquery = "insert into purchase (OrderID, ProID, Quantity, TotalPrice) " +
                "values (@orderID, @proID, @quantity, @totalPrice)";
            var command = new SqlCommand(sqlquery, Connection);

            command.Parameters.AddWithValue("@orderID", purchase.orderID);
            command.Parameters.AddWithValue("@proID", purchase.phone.phoneID);
            command.Parameters.AddWithValue("@quantity", purchase.quantity);
            command.Parameters.AddWithValue("@totalPrice", purchase.totalPrice);
            


            command.ExecuteNonQuery();



            // Xóa Quantity ở sản phẩm ProID

            PhoneDAO _phoneDAO = new PhoneDAO();

            var phone = _phoneDAO.getPhoneByID(purchase.phone.phoneID);

            phone.quantity = phone.quantity - purchase.quantity;

            _phoneDAO.updatePhone(phone);

            // select SQL
            int id = -1;
            string sql1 = "SELECT TOP 1 PurchaseID FROM purchase ORDER BY PurchaseID DESC ";

            var command1 = new SqlCommand(sql1, Connection);

            var reader = command1.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader["PurchaseID"];
            }

            reader.Close();

            return id;
        }




        public void deletePurchase(Purchase purchase)
        {
            var query = "delete from purchase where PurchaseID = @ID";
            var command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@ID", purchase.purchaseID);
            command.ExecuteNonQuery();
        }

        public void updatePurchase(Purchase purchase)
        {
            var query = "update purchase set OrderID = @orderID, ProID = @proID, Quantity = @quantity, TotalPrice = @totalPrice "  +
                
                "where PurchaseID = @ID";
            var command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@orderID", purchase.orderID);
            command.Parameters.AddWithValue("@proID", purchase.phone.phoneID);
            command.Parameters.AddWithValue("@quantity", purchase.quantity);
            command.Parameters.AddWithValue("@totalPrice", purchase.totalPrice);
            command.Parameters.AddWithValue("@ID", purchase.purchaseID);


            command.ExecuteNonQuery();

        }


        public void deletePurchaseByOrderID(int orderID)
        {
            var query = "delete from purchase where OrderID = @ID";
            var command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@ID", orderID);
            command.ExecuteNonQuery();
        }

    }
}

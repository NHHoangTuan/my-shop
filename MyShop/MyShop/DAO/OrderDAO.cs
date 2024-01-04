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
    internal class OrderDAO:SQLData

    {

        public BindingList<Order> getAllOrder()
        {
            var sqlquery = "select * from shop_order";
            var command = new SqlCommand(sqlquery, Connection);
            var reader = command.ExecuteReader();

            BindingList<Order> list = new BindingList<Order>();
            CategoryDAO _catDAO = new CategoryDAO();
            while (reader.Read())
            {
                var ID = (int)reader["OrderID"];
                var cusName = (string)reader["CusName"];
                var cusAddress = (string)reader["CusAddress"];
                var orderDate = (DateTime)reader["OrderDate"];
                var priceTotal = (Decimal)reader["PriceTotal"];
                var profitTotal = (Decimal)reader["ProfitTotal"];
                


                Order order = new Order()
                {
                    orderID = ID,
                    cusName = cusName,
                    cusAddress = cusAddress,
                    orderDate = orderDate,
                    priceTotal = priceTotal,
                    profitTotal = profitTotal,

                };

                list.Add(order);
            }
            reader.Close();
            return list;
        }


        public int insertOrder(Order order)
        {
            // insert to SQL
            var sqlquery = "insert into shop_order (CusName, CusAddress, OrderDate, PriceTotal, ProfitTotal) " +
                "values (@cusName, @cusAddress, @orderDate, @priceTotal, @profitTotal)";
            var command = new SqlCommand(sqlquery, Connection);

            command.Parameters.AddWithValue("@cusName", order.cusName);
            command.Parameters.AddWithValue("@cusAddress", order.cusAddress);
            command.Parameters.AddWithValue("@orderDate", order.orderDate);
            command.Parameters.AddWithValue("@priceTotal", order.priceTotal);
            command.Parameters.AddWithValue("@profitTotal", order.profitTotal);
            

            command.ExecuteNonQuery();

            // select SQL
            int id = -1;
            string sql1 = "SELECT TOP 1 OrderID FROM shop_order ORDER BY OrderID DESC ";

            var command1 = new SqlCommand(sql1, Connection);

            var reader = command1.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader["OrderID"];
            }

            reader.Close();

            return id;
        }


        

        public void deleteOrder(Order order)
        {
            var query = "delete from shop_order where OrderID = @ID";
            var command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@ID", order.orderID);
            command.ExecuteNonQuery();
        }

        public void updateOrder(Order order)
        {
            var query = "update shop_order set CusName = @cusName, CusAddress = @cusAddress, OrderDate = @orderDate" +
                ", PriceTotal = @priceTotal, ProfitTotal = @profitTotal " +
                "where OrderID = @ID";
            var command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@cusName", order.cusName);
            command.Parameters.AddWithValue("@cusAddress", order.cusAddress);
            command.Parameters.AddWithValue("@orderDate", order.orderDate);
            command.Parameters.AddWithValue("@priceTotal", order.priceTotal);
            command.Parameters.AddWithValue("@profitTotal", order.profitTotal);
            command.Parameters.AddWithValue("@ID", order.orderID);
            

            command.ExecuteNonQuery();

        }


    }

}

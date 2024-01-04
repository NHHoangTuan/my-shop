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
    internal class PromotionDAO:SQLData
    {

        public BindingList<Promotion> getAllPromotion()
        {
            var sqlquery = "select * from promotion";
            var command = new SqlCommand(sqlquery, Connection);
            var reader = command.ExecuteReader();

            BindingList<Promotion> list = new BindingList<Promotion>();
            while (reader.Read())
            {
                var ID = (int)reader["PromoID"];
                var promoCode = (string)reader["PromoCode"];
                var disPercent = (int)reader["DiscountPercent"];
               



                Promotion promotion = new Promotion()
                {
                    promotionID = ID,
                    promotionCode = promoCode,
                    discountPercent = disPercent

                };

                list.Add(promotion);
            }
            reader.Close();
            return list;
        }


        public int insertPromotion(Promotion promotion)
        {
            // insert to SQL
            var sqlquery = "insert into promotion (PromoCode, DiscountPercent) " +
                "values (@promoCode, @disPercent)";
            var command = new SqlCommand(sqlquery, Connection);

            command.Parameters.AddWithValue("@promoCode", promotion.promotionCode);
            command.Parameters.AddWithValue("@disPercent", promotion.discountPercent);

            command.ExecuteNonQuery();

            // select SQL
            int id = -1;
            string sql1 = "SELECT TOP 1 PromoID FROM promotion ORDER BY PromoID DESC ";

            var command1 = new SqlCommand(sql1, Connection);

            var reader = command1.ExecuteReader();
            while (reader.Read())
            {
                id = (int)reader["PromoID"];
            }

            reader.Close();

            return id;
        }



        public void deletePromotion(Promotion promotion)
        {
            var query = "delete from promotion where PromoID = @promoID";
            var command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@promoID", promotion.promotionID);
            command.ExecuteNonQuery();
        }

        public void updatePromotion(Promotion promotion)
        {
            var query = "update promotion set PromoCode = @promoCode, DiscountPercent = @disPercent" +
                "where PromoID = @promoID";
            var command = new SqlCommand(query, Connection);

            command.Parameters.AddWithValue("@promoID", promotion.promotionID);
            command.Parameters.AddWithValue("@promoCode", promotion.promotionCode);

            command.Parameters.AddWithValue("@disPercent", promotion.discountPercent);

            command.ExecuteNonQuery();

        }

        public Promotion getPromotionByID(int? id)
        {
            if (id == null) return new Promotion()
            {
                promotionID = null,
                discountPercent = 0,
                promotionCode = "No Apply"
            };

            var sql1 = "select PromoCode, DiscountPercent from promotion where PromoID = @promoID";
            var command1 = new SqlCommand(sql1, Connection);
            command1.Parameters.AddWithValue("@promoID", id);
            command1.ExecuteNonQuery();
            var reader1 = command1.ExecuteReader();
            var promotion = new Promotion();
            while (reader1.Read())
            {
                promotion.promotionID = id;
                promotion.promotionCode = (string)reader1["PromoCode"];
                promotion.discountPercent = (int)reader1["DiscountPercent"];
            
            }

            return promotion;
        }

    }
}

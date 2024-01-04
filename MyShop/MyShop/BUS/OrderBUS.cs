using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    internal class OrderBUS
    {

        private OrderDAO _orderDAO;
        private PurchaseDAO _purchaseDAO;

        public OrderBUS()
        {
            _orderDAO = new OrderDAO();
            _purchaseDAO = new PurchaseDAO();
        }

        public BindingList<Order> getAllOrder()
        {
            return _orderDAO.getAllOrder();
        }

        // tính lợi nhuận trên một sản phẩm
        public decimal calProductProfit(decimal productPrice)
        {
            float profit = 1.07f;

            decimal result = productPrice * (decimal)profit;

            return result;
        }


        public int saveOrder(Order order)
        {
            int id = _orderDAO.insertOrder(order);

            return id;
        }

        public void deleteOrder(Order order)
        {
            _orderDAO.deleteOrder(order);
        }

        public void updateOrder(Order order)
        {
            _orderDAO.updateOrder(order);
        }


        public BindingList<Purchase> getPurchaseListByOrderID(int orderID)
        {
            return _purchaseDAO.getAllPurchaseByOrderID(orderID);
        }
    }
}

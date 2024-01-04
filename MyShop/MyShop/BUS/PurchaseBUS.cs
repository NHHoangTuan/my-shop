using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    internal class PurchaseBUS
    {

        private PurchaseDAO _purchaseDAO;

        public PurchaseBUS()
        {
            _purchaseDAO = new PurchaseDAO();
        }

        public BindingList<Purchase> getAllPurchase()
        {
            return _purchaseDAO.getAllPurchase();
        }

        public BindingList<Purchase> getAllPurchaseByOrderID(int orderID)
        {
            return _purchaseDAO.getAllPurchaseByOrderID(orderID);
        }

       
        public DateTime getOrderDate(Purchase purchase)
        {
            return _purchaseDAO.getDateOrder(purchase);
        }

        public int savePurchase(Purchase purchase)
        {
            int id = _purchaseDAO.insertPurchase(purchase);

            return id;
        }

        public void deletePurchase(Purchase purchase)
        {
            _purchaseDAO.deletePurchase(purchase);
        }

        public void updatePurchase(Purchase purchase)
        {
            _purchaseDAO.updatePurchase(purchase);
        }

        public void deletePurchaseByOrderID(int orderID)
        {
            _purchaseDAO.deletePurchaseByOrderID(orderID);
        }
    }
}

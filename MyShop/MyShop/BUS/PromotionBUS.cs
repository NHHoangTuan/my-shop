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
    internal class PromotionBUS
    {

        PromotionDAO _promotionDAO;

        public PromotionBUS()
        {
            _promotionDAO = new PromotionDAO();
        }

        public BindingList<Promotion> getAllPromotion()
        {
            return _promotionDAO.getAllPromotion();
        }

        public int savePromotion(Promotion promotion)
        {
            return _promotionDAO.insertPromotion(promotion);
        }

        public void updatePromotion(Promotion promotion)
        {
            _promotionDAO.updatePromotion(promotion);
        }

        public void deletePromotion(Promotion promotion)
        {
            _promotionDAO.deletePromotion(promotion);
        }
    }
}

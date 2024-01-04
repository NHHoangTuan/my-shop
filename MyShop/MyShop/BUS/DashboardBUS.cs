using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    class DashboardBUS
    {

        PhoneBUS _phoneBUS;
        OrderBUS _orderBUS;

        public DashboardBUS() {

            _phoneBUS = new PhoneBUS();
            _orderBUS = new OrderBUS();

        }

        public int getTotalPhone()
        {
            int totalPhone = 0;

            List<Phone> phoneList = new List<Phone>();
            phoneList = _phoneBUS.getAllPhone().ToList();
            foreach (var item in phoneList)
            {
                totalPhone += item.quantity;
            }

            return totalPhone;
        }

        public decimal getTotalRevenue()
        {
            decimal totalRevenue = 0;

            List<Order> orderList = _orderBUS.getAllOrder().ToList();

            foreach(var item in orderList)
            {
                totalRevenue += (decimal)item.priceTotal;
            }

            return totalRevenue;
        }

        public int getTotalOrderInMonth(int year, int month)
        {
            int totalOrder = 0;

            List<Order> orderList = new List<Order>();
            orderList = _orderBUS.getAllOrder().ToList();

            foreach (var item in orderList)
            {
                if (item.orderDate.Year == year && item.orderDate.Month == month)
                {
                    totalOrder++;
                }
            }

            return totalOrder;
        }

        public int getTotalOrderInWeek(int year, int month, int day)
        {

            DateTime currentDate = new DateTime(year, month, day);

            // Ngay bat dau tuan
            //DateTime startOfWeek = currentDate.AddDays((int)currentDate.DayOfWeek - (int)DayOfWeek.Monday);
            DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
            // Lấy ngày kết thúc của tuần
            DateTime endOfWeek = startOfWeek.AddDays(6);

            int totalOrder = 0;

            List<Order> orderList = new List<Order>();
            orderList = _orderBUS.getAllOrder().ToList();

            foreach (var item in orderList)
            {
                if (item.orderDate.Date >= startOfWeek && item.orderDate.Date <= endOfWeek)
                {
                    totalOrder++;
                }
            }

            return totalOrder;
        }


        public List<Phone> top5OutOfStock()
        {
            // danh sach top 5 ban chay trong nam
            List<Phone> phones = new List<Phone>();
            phones = _phoneBUS.getAllPhone().ToList();
            var sortedPhones = phones.OrderBy(p => p.quantity);

            // Lấy 5 phần tử đầu tiên
            List<Phone> result = sortedPhones.Take(5).ToList();

            return result;

        }

    }
}

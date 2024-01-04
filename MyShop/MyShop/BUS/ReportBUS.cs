using Microsoft.VisualBasic;
using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    class ReportBUS
    {
        private List<Order> _orders;
        private OrderBUS _orderBUS;
        private OrderDAO _orderDAO;
        private PhoneBUS _phoneBUS;
        private PurchaseBUS _purchaseBUS;

        public ReportBUS()
        {
            var orderDAO = new OrderDAO();
            var orderBUS = new OrderBUS();
            _orderBUS = orderBUS;
            _orderDAO = orderDAO;
            _phoneBUS = new PhoneBUS();
            _orders = new List<Order>();
            _purchaseBUS = new PurchaseBUS();
        }

        // Đã có dữ liệu rồi phân tích dữ liệu nào  :) 

        private List<Order> getData()
        {
            var ob =  _orderDAO.getAllOrder();

            return ob.ToList();
        }

        public List<Decimal> groupPriceTotalByMonth(int year)
        {
            _orders = getData();
            List<Decimal> result = new List<Decimal>();

            for (int month = 1; month <= 12; month++)
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.orderDate.Month == month && order.orderDate.Year == year)
                    {
                        // này nguy hiểm :))) nhưng kệ 
                        prices.Add((decimal)order.priceTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public List<int> groupQuantityOfProductByMonth(Phone product, int year)
        {
            _orders = getData();
            List<int> result = new List<int>();

            for (int month = 1; month <= 12; month++)
            {
                int quantity = 0;
                foreach (var order in _orders)
                {
                    if (order.orderDate.Month == month
                        && order.orderDate.Year == year)
                    {
                        var purchases = _orderBUS.getPurchaseListByOrderID(order.orderID).ToList();
                        foreach (var purchase in purchases)
                        {
                            if (purchase.phone.phoneID == product.phoneID)
                            {
                                quantity += purchase.quantity;
                            }
                        }
                    }
                }

                result.Add(quantity);
            }

            return result;
        }

        public List<Decimal> groupProfitTotalByMonth(int year)
        {
            _orders = getData();
            List<Decimal> result = new List<Decimal>();

            for (int month = 1; month <= 12; month++)
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.orderDate.Month == month && order.orderDate.Year == year)
                    {
                        prices.Add((decimal)order.profitTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }


        public List<Decimal> groupPriceTotalByYear(int startYear, int endYear)
        {
            _orders = getData();
            List<Decimal> result = new List<Decimal>();

            for (int year = startYear; year <= endYear; year++)
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.orderDate.Year == year)
                    {
                        // này nguy hiểm :))) nhưng kệ 
                        prices.Add((decimal)order.priceTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public List<Decimal> groupProfitTotalByYear(int startYear, int endYear)
        {
            _orders = getData();
            List<Decimal> result = new List<Decimal>();

            for (int year = startYear; year <= endYear; year++)
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.orderDate.Year == year)
                    {
                        // này nguy hiểm :))) nhưng kệ 
                        prices.Add((decimal)order.profitTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }


        public List<int> groupQuantityOfProductByYear(Phone product, int startYear, int endYear)
        {
            _orders = getData();
            List<int> result = new List<int>();

            for (int year = startYear; year <= endYear; year++)
            {
                int quantity = 0;
                foreach (var order in _orders)
                {
                    if (order.orderDate.Year == year)
                    {
                        var purchases = _orderBUS.getPurchaseListByOrderID(order.orderID);
                        foreach (var purchase in purchases)
                        {
                            if (purchase.phone.phoneID == product.phoneID)
                            {
                                quantity += purchase.quantity;
                            }
                        }
                    }
                }

                result.Add(quantity);
            }

            return result;
        }


        public List<decimal> groupProfitTotalByWeek(int month, int year)
        {
            _orders = getData();
            List<decimal> result = new List<decimal>();
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime lastDayOfMonth = new DateTime(year, month, daysInMonth);
            int weekCount = (int)Math.Ceiling((double)daysInMonth / 7);

            for (int week = 1; week <= 5; week++) // loop over 5 weeks only
            {
                decimal totalPrice = 0;
                DateTime startDate = firstDayOfMonth.AddDays((week - 1) * 7);
                DateTime endDate = startDate.AddDays(6);

                foreach (var order in _orders)
                {
                    if (order.orderDate >= startDate && order.orderDate <= endDate)
                    {
                        totalPrice += (decimal)order.profitTotal;
                    }
                }

                result.Add(totalPrice);

                if (weekCount < 5 && week == weekCount) // if there are less than 5 weeks in the month
                {
                    for (int i = week + 1; i <= 5; i++) // add 0 to the remaining weeks
                    {
                        result.Add(0);
                    }
                }
            }

            return result;
        }

        public List<int> groupQuantityOfProductByWeek(Phone product, int year, int month)
        {
            _orders = getData();
            List<int> result = new List<int>();
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime lastDayOfMonth = new DateTime(year, month, daysInMonth);
            int weekCount = (int)Math.Ceiling((double)daysInMonth / 7);

            for (int week = 1; week <= 5; week++) // loop over 5 weeks only
            {
                int quantity = 0;
                DateTime startDate = firstDayOfMonth.AddDays((week - 1) * 7);
                DateTime endDate = startDate.AddDays(6);

                foreach (var order in _orders)
                {
                    if (order.orderDate >= startDate && order.orderDate <= endDate)
                    {
                        var purchases = _orderBUS.getPurchaseListByOrderID(order.orderID);
                        foreach (var purchase in purchases)
                        {
                            if (purchase.phone.phoneID == product.phoneID)
                            {
                                quantity += purchase.quantity;
                            }
                        }
                    }
                }

                result.Add(quantity);

                if (weekCount < 5 && week == weekCount) // if there are less than 5 weeks in the month
                {
                    for (int i = week + 1; i <= 5; i++) // add 0 to the remaining weeks
                    {
                        result.Add(0);
                    }
                }
            }

            return result;
        }

        public List<decimal> groupPriceTotalByWeek(int month, int year)
        {
            _orders = getData();
            List<decimal> result = new List<decimal>();
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime lastDayOfMonth = new DateTime(year, month, daysInMonth);
            int weekCount = (int)Math.Ceiling((double)daysInMonth / 7);

            for (int week = 1; week <= 5; week++) // loop over 5 weeks only
            {
                decimal totalPrice = 0;
                DateTime startDate = firstDayOfMonth.AddDays((week - 1) * 7);
                DateTime endDate = startDate.AddDays(6);

                foreach (var order in _orders)
                {
                    if (order.orderDate >= startDate && order.orderDate <= endDate)
                    {
                        totalPrice += (decimal)order.priceTotal;
                    }
                }

                result.Add(totalPrice);

                if (weekCount < 5 && week == weekCount) // if there are less than 5 weeks in the month
                {
                    for (int i = week + 1; i <= 5; i++) // add 0 to the remaining weeks
                    {
                        result.Add(0);
                    }
                }
            }

            return result;
        }



        public List<Decimal> groupPriceTotalByDate(DateTime startDate, DateTime endDate)
        {
            _orders = getData();
            List<Decimal> result = new List<Decimal>();

            foreach (DateTime day in EachDay(startDate, endDate))
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.orderDate.Date == day)
                    {
                        // này nguy hiểm :))) nhưng kệ 
                        prices.Add((decimal)order.priceTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public List<Decimal> groupProfitTotalByDate(DateTime startDate, DateTime endDate)
        {
            _orders = getData();
            List<Decimal> result = new List<Decimal>();

            foreach (DateTime day in EachDay(startDate, endDate))
            {
                List<decimal> prices = new List<decimal>();
                foreach (var order in _orders)
                {
                    if (order.orderDate.Date == day)
                    {
                        // này nguy hiểm :))) nhưng kệ 
                        prices.Add((decimal)order.profitTotal);
                    }
                }
                var totalPrice = prices.Sum();

                result.Add(totalPrice);
            }

            return result;
        }

        public List<int> groupQuantityOfProductByDate(Phone product, DateTime startDate, DateTime endDate)
        {
            _orders = getData();
            List<int> result = new List<int>();

            foreach (DateTime day in EachDay(startDate, endDate))
            {
                int quantity = 0;
                foreach (var order in _orders)
                {
                    if (order.orderDate.Date == day)
                    {
                        var purchases = _orderBUS.getPurchaseListByOrderID(order.orderID);
                        foreach (var purchase in purchases)
                        {
                            if (purchase.phone.phoneID == product.phoneID)
                            {
                                quantity += purchase.quantity;
                            }
                        }
                    }
                }
                result.Add(quantity);
            }

            return result;
        }

        public List<String> EachDayConverter(DateTime startDate, DateTime endDate)
        {
            List<string> result = new List<string>();

            foreach (DateTime day in EachDay(startDate, endDate))
            {
                result.Add(day.Date.ToString());
            }

            return result;
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public Tuple<List<Phone>, List<int>> top5SalesByYear(int year)
        {
            // danh sach top 5 ban chay trong nam
            List<Phone> resultPhones = new List<Phone>();
            // danh sach so luong ban trong nam cua all phone
            List<int> sales = new List<int>();

            List<Phone> _listPhone = new List<Phone>();
            _listPhone = _phoneBUS.getAllPhone().ToList();
            List<Purchase> _listPurchase = new List<Purchase>();
            _listPurchase = _purchaseBUS.getAllPurchase().ToList();
            int saleTotal = 0;
            foreach (var phone in _listPhone)
            {
                saleTotal = 0;
                foreach (var purchase in _listPurchase)
                {
                    if (phone.phoneID == purchase.phone.phoneID && _purchaseBUS.getOrderDate(purchase).Year == year)
                    {
                        saleTotal += purchase.quantity;
                    }
                }
                sales.Add(saleTotal);
            }

            var indexNumbers = sales.Select((value, index) => new {value=value, index=index});

            var sortNumbers = indexNumbers.OrderByDescending(x => x.value);

            var indexResult = sortNumbers.Take(5).Select(x=>x.index).ToList();

            List<int> resultQuantity = new List<int>();
            resultQuantity = sortNumbers.Take(5).Select(y=>y.value).ToList();

            for (int i = 0; i < indexResult.Count; i++)
            {
                resultPhones.Add(_listPhone[indexResult[i]]);
            }

            return new Tuple<List<Phone>, List<int>>(resultPhones, resultQuantity);

        }



        public Tuple<List<Phone>, List<int>> top5SalesByMonth(int year, int month)
        {
            
            List<Phone> resultPhones = new List<Phone>();
            
            List<int> sales = new List<int>();

            List<Phone> _listPhone = new List<Phone>();
            _listPhone = _phoneBUS.getAllPhone().ToList();
            List<Purchase> _listPurchase = new List<Purchase>();
            _listPurchase = _purchaseBUS.getAllPurchase().ToList();
            int saleTotal = 0;
            foreach (var phone in _listPhone)
            {
                saleTotal = 0;
                foreach (var purchase in _listPurchase)
                {
                    if (phone.phoneID == purchase.phone.phoneID && _purchaseBUS.getOrderDate(purchase).Year == year
                        && _purchaseBUS.getOrderDate(purchase).Month == month)
                    {
                        saleTotal += purchase.quantity;
                    }
                }
                sales.Add(saleTotal);
            }

            var indexNumbers = sales.Select((value, index) => new { value = value, index = index });

            var sortNumbers = indexNumbers.OrderByDescending(x => x.value);

            var indexResult = sortNumbers.Take(5).Select(x => x.index).ToList();

            List<int> resultQuantity = new List<int>();
            resultQuantity = sortNumbers.Take(5).Select(y => y.value).ToList();

            for (int i = 0; i < indexResult.Count; i++)
            {
                resultPhones.Add(_listPhone[indexResult[i]]);
            }

            return new Tuple<List<Phone>, List<int>>(resultPhones, resultQuantity);

        }


        public Tuple<List<Phone>, List<int>> top5SalesByWeek(int year, int month, int day)
        {
            DateTime currentDate = new DateTime(year, month, day);

            // Ngay bat dau tuan
            //DateTime startOfWeek = currentDate.AddDays((int)currentDate.DayOfWeek - (int)DayOfWeek.Monday);
            DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
            // Lấy ngày kết thúc của tuần
            DateTime endOfWeek = startOfWeek.AddDays(6);

            List<Phone> resultPhones = new List<Phone>();

            List<int> sales = new List<int>();

            List<Phone> _listPhone = new List<Phone>();
            _listPhone = _phoneBUS.getAllPhone().ToList();
            List<Purchase> _listPurchase = new List<Purchase>();
            _listPurchase = _purchaseBUS.getAllPurchase().ToList();
            int saleTotal = 0;
            foreach (var phone in _listPhone)
            {
                saleTotal = 0;
                foreach (var purchase in _listPurchase)
                {
                    if (phone.phoneID == purchase.phone.phoneID && _purchaseBUS.getOrderDate(purchase).Date >= startOfWeek.Date
                        && _purchaseBUS.getOrderDate(purchase).Date <= endOfWeek.Date)
                    {
                        saleTotal += purchase.quantity;
                    }
                }
                sales.Add(saleTotal);
            }

            var indexNumbers = sales.Select((value, index) => new { value = value, index = index });

            var sortNumbers = indexNumbers.OrderByDescending(x => x.value);

            var indexResult = sortNumbers.Take(5).Select(x => x.index).ToList();

            List<int> resultQuantity = new List<int>();
            resultQuantity = sortNumbers.Take(5).Select(y => y.value).ToList();

            for (int i = 0; i < indexResult.Count; i++)
            {
                resultPhones.Add(_listPhone[indexResult[i]]);
            }

            return new Tuple<List<Phone>, List<int>>(resultPhones, resultQuantity);

        }



    }
}

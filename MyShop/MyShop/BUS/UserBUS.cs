using MyShop.DAO;
using MyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.BUS
{
    class UserBUS
    {



        private UserDAO _userDAO;

        public UserBUS()
        {
            _userDAO = new UserDAO();
        }

        public User getOne(string username, string password)
        {
            return _userDAO.GetOne(username, password);
        }
        public bool createUser(User user)
        {
            return _userDAO.CreateUser(user);
        }
    }
}

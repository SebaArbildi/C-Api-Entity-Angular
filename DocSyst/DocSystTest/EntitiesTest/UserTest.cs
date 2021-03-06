﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocSystEntities.User;

namespace DocSystTest.EntitiesTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateUser_WithoutParameters_Ok()
        {
            User user = new User();

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void CreateUser_WithParameters_Ok()
        {
            User user = new User("Name", "LastName", "UserName", "Password", "Mail", true);

            Assert.IsNotNull(user);
        }
    }
}

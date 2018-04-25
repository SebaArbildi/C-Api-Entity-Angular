﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DocSystBusinessLogicInterface.UserBusinessLogicInterface;
using DocSystWebApi.Controllers;
using DocSystEntities.User;
using DocSystDataAccessInterface.UserDataAccessInterface;
using DocSystDataAccess.UserDataAccessImplementation;
using DocSystBusinessLogicImplementation.UserBusinessLogicImplementation;
using System.Web.Http;
using DocSystWebApi.Models.UserModel;
using System;
using System.Collections.Generic;

namespace DocSystTest.ApiTest
{
    [TestClass]
    public class UserApiTest
    {
        private UserModel userModel;
        private User user;
        private Mock<IUserBusinessLogic> mockUserBusinessLogic;
        private UserController userController;

        [TestCleanup]
        public void CleanDataBase()
        {
            Utils.DeleteBd();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            user = Utils.CreateUserForTest();
            userModel = UserModel.ToModel(user);
            mockUserBusinessLogic = new Mock<IUserBusinessLogic>();
            userController = new UserController(mockUserBusinessLogic.Object);
        }

        [TestMethod]
        public void CreateUserController_WithParameters_Ok()
        {
            Assert.IsNotNull(userController);
        }

        [TestMethod]
        public void GetUser_UserExists_Ok()
        {
            mockUserBusinessLogic.Setup(b1 => b1.GetUser(user.Username)).Returns(user);
            IHttpActionResult statusObtained = userController.Get(user.Username);
        }

        [TestMethod]
        public void GetUser_BadRequest_Exception()
        {
            mockUserBusinessLogic.Setup(b1 => b1.GetUser(user.Username)).Throws(new Exception());
            IHttpActionResult statusObtained = userController.Get(user.Username);
        }

        [TestMethod]
        public void AddUser_ExpectedParameters_Ok()
        {
            mockUserBusinessLogic.Setup(b1 => b1.AddUser(user));
            IHttpActionResult statusObtained = userController.Post(userModel);
        }

        [TestMethod]
        public void AddUser_BadRequest_Exception()
        {
            mockUserBusinessLogic.Setup(b1 => b1.AddUser(user)).Throws(new Exception());
            IHttpActionResult statusObtained = userController.Post(userModel);
        }

        [TestMethod]
        public void GetUsers_ExpectedParameters_Ok()
        {
            IList<User> users = new List<User>();
            users.Add(user);
            mockUserBusinessLogic.Setup(b1 => b1.GetUsers()).Returns(users);
            IHttpActionResult statusObtained = userController.Get();
        }

        [TestMethod]
        public void GetUsers_BadRequest_Exception()
        {
            IList<UserModel> usersModel = new List<UserModel>();
            usersModel.Add(userModel);
            mockUserBusinessLogic.Setup(b1 => b1.GetUsers()).Throws(new Exception());
            IHttpActionResult statusObtained = userController.Get();
        }

        [TestMethod]
        public void ModifyUser_ExpectedParameters_Ok()
        {
            mockUserBusinessLogic.Setup(b1 => b1.ModifyUser(user));
            IHttpActionResult statusObtained = userController.Put(userModel);
        }

        [TestMethod]
        public void ModifyUser_BadRequest_Exception()
        {
            IList<UserModel> usersModel = new List<UserModel>();
            usersModel.Add(userModel);
            mockUserBusinessLogic.Setup(b1 => b1.ModifyUser(user)).Throws(new Exception());
            IHttpActionResult statusObtained = userController.Put(userModel);
        }

        [TestMethod]
        public void DeleteUser_ExpectedParameters_Ok()
        {
            mockUserBusinessLogic.Setup(b1 => b1.DeleteUser(user.Username));
            IHttpActionResult statusObtained = userController.Delete(user.Username);
        }

        [TestMethod]
        public void DeleteUser_BadRequest_Exception()
        {
            mockUserBusinessLogic.Setup(b1 => b1.DeleteUser(user.Username)).Throws(new Exception());
            IHttpActionResult statusObtained = userController.Delete(user.Username);
        }

        [TestMethod]
        public void IntegrationTest_ExpectedParameters_Ok()
        {
            UserBusinessLogic userBL = new UserBusinessLogic(new UserDataAccess());
            UserController userC = new UserController(userBL);
            UserModel user2 = UserModel.ToModel(Utils.CreateUserForTest());
            userC.Post(userModel);
            userC.Post(user2);
            user2.Name = "modified";
            userC.Put(user2);
            userC.Delete(userModel.Username);
            IHttpActionResult statusObtained = userC.Get();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using DocSystBusinessLogicImplementation.DocumentStructureLogicImplementation;
using DocSystBusinessLogicInterface.DocumentStructureLogicInterface;
using DocSystDataAccessImplementation.DocumentStructureDataAccessImplementation;
using DocSystDataAccessInterface.DocumentStructureDataAccessInterface;
using DocSystEntities.DocumentStructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DocSystTest.BusinessLogicTest
{
    [TestClass]
    public class DocumentBusinessLogicTest
    {
        Mock<IDocumentDataAccess> mockDocumentDataAccess;
        IDocumentBusinessLogic documentBusinessLogic;
        Document document;
        Paragraph paragraph;

        [TestCleanup]
        public void CleanDataBase()
        {
            Utils.DeleteBd();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            mockDocumentDataAccess = new Mock<IDocumentDataAccess>();
            documentBusinessLogic = new DocumentBusinessLogic(mockDocumentDataAccess.Object);
            document = Utils.CreateDocumentForTest();
            paragraph = Utils.CreateParagraphForTest();
        }

        [TestMethod]
        public void CreateDocumentBL_WithoutParameters_Ok()
        {
            IDocumentBusinessLogic documentBL = new DocumentBusinessLogic();

            Assert.IsNotNull(documentBL);
        }

        [TestMethod]
        public void CreateDocumentBL_WithParameters_Ok()
        {
            IDocumentDataAccess documentDataAccess = new DocumentDataAccess();

            IDocumentBusinessLogic documentBL = new DocumentBusinessLogic(documentDataAccess);

            Assert.IsNotNull(documentBL);
        }

        [TestMethod]
        public void AddDocument_ExpectedParameters_Ok()
        {
            documentBusinessLogic.AddDocument(document);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateWaitObjectException))]
        public void AddDocument_DocumentAlreadyExists_DuplicateException()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(true);

            documentBusinessLogic.AddDocument(document);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddDocument_DataAccessThrowException_ExceptionCatched()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Add(document)).Throws(new Exception());

            documentBusinessLogic.AddDocument(document);
        }

        [TestMethod]
        public void DeleteDocument_ExpectedParameters_Ok()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(true);
            mockDocumentDataAccess.Setup(b1 => b1.Delete(document.Id)).Verifiable();

            documentBusinessLogic.DeleteDocument(document.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteDocument_DocumentNotExists_DuplicateException()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(false);

            documentBusinessLogic.DeleteDocument(document.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeleteDocument_DataAccessThrowException_ExceptionCatched()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(true);
            mockDocumentDataAccess.Setup(b1 => b1.Delete(document.Id)).Throws(new Exception());

            documentBusinessLogic.DeleteDocument(document.Id);
        }

        [TestMethod]
        public void ModifyDocument_ExpectedParameters_Ok()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(true);
            mockDocumentDataAccess.Setup(b1 => b1.Modify(document)).Verifiable();
            mockDocumentDataAccess.Setup(b1 => b1.Get(document.Id)).Returns(
                new Document()
                {
                    Id = document.Id,
                    DocumentParts = new List<Body>() { paragraph }
                });

            document.SetDocumentPart(MarginAlign.PARAGRAPH, paragraph);
            documentBusinessLogic.ModifyDocument(document);

            Assert.AreEqual(document, documentBusinessLogic.GetDocument(document.Id));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ModifyDocument_DocumentNotExists_DuplicateException()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(false);

            documentBusinessLogic.ModifyDocument(document);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ModifyDocument_DataAccessThrowException_ExceptionCatched()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(true);
            mockDocumentDataAccess.Setup(b1 => b1.Modify(document)).Throws(new Exception());

            documentBusinessLogic.ModifyDocument(document);
        }

        [TestMethod]
        public void GetDocuments_ExpectedParameters_Ok()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Get()).Returns(new List<Document>
            {
                document
            });

            documentBusinessLogic.GetDocuments();
        }

        [TestMethod]
        public void GetSetDocumentPart_ExpectedParameters_Ok()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(true);
            mockDocumentDataAccess.Setup(b1 => b1.Modify(document)).Verifiable();
            mockDocumentDataAccess.Setup(b1 => b1.Get(document.Id)).Returns(
                new Document()
                {
                    Id = document.Id,
                    DocumentParts = new List<Body>() { paragraph }
                });

            document.SetDocumentPart(MarginAlign.PARAGRAPH, paragraph);

            Assert.AreEqual(paragraph, 
                documentBusinessLogic.GetDocumentPart(document.Id,MarginAlign.PARAGRAPH));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDocumentPart_DocumentNoExist_ExceptionCatched()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(false);

            documentBusinessLogic.GetDocumentPart(document.Id, MarginAlign.PARAGRAPH);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetDocumentPart_DataAccessThrowException_ExceptionCatched()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(true);
            mockDocumentDataAccess.Setup(b1 => b1.Get(document.Id)).Throws(new Exception());

            documentBusinessLogic.GetDocumentPart(document.Id, MarginAlign.PARAGRAPH);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetDocumentPart_DocumentNoExist_ExceptionCatched()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(false);

            documentBusinessLogic.SetDocumentPart(document.Id, MarginAlign.PARAGRAPH, paragraph);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SetDocumentPart_DataAccessThrowException_ExceptionCatched()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(true);
            mockDocumentDataAccess.Setup(b1 => b1.Get(document.Id)).Throws(new Exception());

            documentBusinessLogic.SetDocumentPart(document.Id, MarginAlign.PARAGRAPH, paragraph);
        }

        [TestMethod]
        public void ExistDocumentPart_ExpectedParameters_True()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(true);
            mockDocumentDataAccess.Setup(b1 => b1.Get(document.Id)).Returns(
                new Document()
                {
                    Id = document.Id,
                    DocumentParts = new List<Body>() { paragraph }
                });


            Assert.IsTrue(documentBusinessLogic.ExistDocumentPart(document.Id, MarginAlign.PARAGRAPH));
        }

        [TestMethod]
        public void ExistDocumentPart_ExpectedParameters_False()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(true);
            mockDocumentDataAccess.Setup(b1 => b1.Get(document.Id)).Returns(
                new Document()
                {
                    Id = document.Id,
                    DocumentParts = new List<Body>()
                });

            Assert.IsFalse(documentBusinessLogic.ExistDocumentPart(document.Id, MarginAlign.PARAGRAPH));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExistDocumentPart_NotExistDocument_ExceptionCatched()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(false);

            documentBusinessLogic.ExistDocumentPart(document.Id, MarginAlign.PARAGRAPH);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ExistDocumentPart_ThrowsException_ExceptionCatched()
        {
            mockDocumentDataAccess.Setup(b1 => b1.Exists(document.Id)).Returns(true);
            mockDocumentDataAccess.Setup(b1 => b1.Get(document.Id)).Throws(new Exception());

            documentBusinessLogic.ExistDocumentPart(document.Id, MarginAlign.PARAGRAPH);
        }

        [TestMethod]
        public void IntegrationTest_ExpectedParameters_Ok()
        {
            DocumentDataAccess documentDA = new DocumentDataAccess();
            DocumentBusinessLogic documentBL = new DocumentBusinessLogic(documentDA);
            Document document1 = Utils.CreateDocumentForTest();
            Document document2 = Utils.CreateDocumentForTest();
            documentBL.AddDocument(document1);
            documentBL.AddDocument(document2);

            document2.SetDocumentPart(MarginAlign.PARAGRAPH, paragraph);
            documentBL.ModifyDocument(document2);

            documentBL.DeleteDocument(document1.Id);

            Document document2Obtained = documentBL.GetDocument(document2.Id);
            IList<Document> documentsObtained = documentBL.GetDocuments();

            Assert.IsTrue(!documentsObtained.Contains(document1) && documentsObtained.Contains(document2Obtained));
        }
    }
}
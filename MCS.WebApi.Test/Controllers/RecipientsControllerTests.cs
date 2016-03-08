using System;
using NUnit.Framework;
using AspNetIdentity_WebApi.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetIdentity_WebApi.Controllers;
using AspNetIdentity_WebApi.Data.Repository.IRepository;
using Moq;
using System.Web.Http;
using System.Web.Http.Results;

namespace MCS.WebApi.Test.Controllers
{
    [TestFixture]
    public class RecipientsControllerTests
    {
        private List<Recipient> _listOfRecipient = new List<Recipient>();

        private Mock<IRecipientRepository> _mockIRecipientRepository;
        //private Mock<ICommandBus> commandBus;

        [SetUp]
        public void SetUp()
        {
            _mockIRecipientRepository = new Mock<IRecipientRepository>();
            _listOfRecipient = GetAllRecipient();
        }


        [Test]
        public void RecipientsControllerTest_GetAllRecipients_AllRecipients_are_All_and_Equivalent()
        {

            // Arrange

            _mockIRecipientRepository.Setup(r => r.GetAll()).Returns(_listOfRecipient.AsQueryable());

            //Act
            var recipientController = new RecipientsController(_mockIRecipientRepository.Object);

            var result = recipientController.GetAllRecipients();

            //Assert
            Assert.AreEqual(2, result.Count());
            CollectionAssert.AreEquivalent(_listOfRecipient,result.ToList());
        }

        [Test]
        public void RecipientsControllerTest_GetRecipient_ById_AllRecipients()
        {
            var guid = new Guid("3331BAC3-EEDB-E511-8E9D-001DBA8AE923");

            // Arrange
            _mockIRecipientRepository.Setup(r => r.Get(guid))
                .Returns(new Recipient
                {
                    Id_Recipient = new Guid("3331BAC3-EEDB-E511-8E9D-001DBA8AE923"),
                    FirstName = "Pietro",
                    SecondName = "De Michele",
                    Email = "P.Michele@gmail.com",
                    MobileNumber = "3281121212",
                    Active_Flg = true,
                    HomePhoneNumber = null,
                    OfficePhoneNumber = null,
                    Birthday = Convert.ToDateTime("1978-01-25"),
                    User_Id_Created = new Guid("BA7764E3-5AF4-4C7A-864B-5BA3C371CA1E"),
                    Date_Created = Convert.ToDateTime("2016-01-25 19:30:02.117"),
                    Gender = 0
                });

            //Act
            var controller = new RecipientsController(_mockIRecipientRepository.Object);

            var actionResult = controller.GetRecipientById(guid);
            var contentResult = actionResult as OkNegotiatedContentResult<Recipient>;


            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(guid, contentResult.Content.Id_Recipient);
        }

        [Test]
        public void GetReturnsNotFound()
        {
            var guid = new Guid("3331BAC3-EEDB-E511-8E9D-001DBA8AE913");

            // Arrange

            //Act
            var controller = new RecipientsController(_mockIRecipientRepository.Object);

            var actionResult = controller.GetRecipientById(guid);
            //var contentResult = actionResult as OkNegotiatedContentResult<Recipient>;
            

            // Assert
            Assert.Equals(actionResult, typeof(NotFoundResult));
        }

        [Test]
        public void DeleteReturnsOk()
        {
            var guid = new Guid("3331BAC3-EEDB-E511-8E9D-001DBA8AE923");
            // Arrange

            var controller = new RecipientsController(_mockIRecipientRepository.Object);

            // Act
            Task<IHttpActionResult> actionResult = controller.DeleteRecipient(guid);

            // Assert
            Assert.Equals(actionResult, typeof(OkResult));
        }



        #region private
        private List<Recipient> GetAllRecipient()
        {
            return new List<Recipient>
            {
                new Recipient
                {
                    Id_Recipient = new Guid("668905C8-EDDB-E511-8E9D-001DBA8AE923"),
                    FirstName = "Michele",
                    SecondName = "De Lazzaro",
                    Email = "s.Lazzaro@gmail.com",
                    MobileNumber = "3281121333",
                    Active_Flg = true,
                    HomePhoneNumber = null,
                    OfficePhoneNumber = null,
                    Birthday = Convert.ToDateTime("1983-03-25"),
                    User_Id_Created = new Guid("BA7764E3-5AF4-4C7A-864B-5BA3C371CA1E"),
                    Date_Created = Convert.ToDateTime("2016-02-25 19:30:02.117"),
                    Gender = 0
                },
                new Recipient
                {
                    Id_Recipient = new Guid("3331BAC3-EEDB-E511-8E9D-001DBA8AE923"),
                    FirstName = "Pietro",
                    SecondName = "De Michele",
                    Email = "P.Michele@gmail.com",
                    MobileNumber = "3281121212",
                    Active_Flg = true,
                    HomePhoneNumber = null,
                    OfficePhoneNumber = null,
                    Birthday = Convert.ToDateTime("1978-01-25"),
                    User_Id_Created = new Guid("BA7764E3-5AF4-4C7A-864B-5BA3C371CA1E"),
                    Date_Created = Convert.ToDateTime("2016-01-25 19:30:02.117"),
                    Gender = 0
                }
            };




            //Id_Recipient FirstName   SecondName MobileNumber    Email Address City PostalCode  Province Country Birthday OfficePhoneNumber   HomePhoneNumber User_Id_Created Date_Created User_Id_Modify  Date_Modify Active_Flg  Gender
            //668905C8 - EDDB - E511 - 8E9D - 001DBA8AE923 Michele De Lazzaro  3281121333  s.Lazzaro @gmail.com via Vittorio Emanuele I 9   Roma    00137   RM Italy   NULL NULL    NULL BA7764E3-5AF4 - 4C7A - 864B - 5BA3C371CA1E    2016 - 02 - 25 19:30:02.117 NULL NULL    1   0
        }
        #endregion
    }
}
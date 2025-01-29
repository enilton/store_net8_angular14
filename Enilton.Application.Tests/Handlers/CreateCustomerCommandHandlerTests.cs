using Enilton.Application.Commands.Customer;
using Enilton.Application.Handlers.Customer;
using Enilton.Domain.Entities;
using Enilton.Domain.Interfaces;
using Moq;

namespace Enilton.Application.Tests.Handlers
{
    public class CreateCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly CreateCustomerCommandHandler _handler;

        public CreateCustomerCommandHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Create_Customer_And_Return_Result()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                Name = "John Doe",
                Email = "johndoe@email.com",
                Phone = "1234567890"
            };

            _customerRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Customer>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Email, result.Email);
            _customerRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Customer>()), Times.Once);
        }
    }
}

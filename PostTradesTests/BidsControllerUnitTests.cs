using Moq;
using Microsoft.AspNetCore.Mvc;
using PostTrades.Domain;
using PostTrades.Controllers;
using PostTrades.Repositories;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Bogus;
using Microsoft.AspNetCore.Http;

namespace PostTradesTests
{
    public class BidsControllerUnitTests
    {
        private readonly Mock<IBidRepository> _mockRepository;
        private readonly Mock<ILogger<BidsController>> _mockLogger;
        private readonly BidsController _controller;
        private readonly Faker<Bid> _bidFaker;

        public BidsControllerUnitTests()
        {
            _mockRepository = new Mock<IBidRepository>();
            _mockLogger = new Mock<ILogger<BidsController>>();
            _controller = new BidsController(_mockRepository.Object, _mockLogger.Object);
            _bidFaker = new Faker<Bid>()
                .RuleFor(b => b.BidId, f => f.Random.Int(1, 1000))
                .RuleFor(b => b.Account, f => f.Random.Word())
                .RuleFor(b => b.BidType, f => f.Random.Word())
                .RuleFor(b => b.Benchmark, f => f.Random.Word())
                .RuleFor(b => b.Commentary, f => f.Random.Words(3))
                .RuleFor(b => b.BidSecurity, f => f.Random.Word())
                .RuleFor(b => b.BidStatus, f => f.Random.Word())
                .RuleFor(b => b.Trader, f => f.Random.Word())
                .RuleFor(b => b.Book, f => f.Random.Word())
                .RuleFor(b => b.CreationName, f => f.Random.Word())
                .RuleFor(b => b.RevisionName, f => f.Random.Word())
                .RuleFor(b => b.DealName, f => f.Random.Word())
                .RuleFor(b => b.DealType, f => f.Random.Word())
                .RuleFor(b => b.SourceListId, f => f.Random.Word())
                .RuleFor(b => b.Side, f => f.Random.Word());
        }

        [Fact]
        public async Task GetAllBids_ReturnsListOfBids_WhenBidsExist()
        {
            // Arrange
            var expectedBids = _bidFaker.Generate(3);
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedBids);

            // Act
            var result = await _controller.GetAllBids();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>();
            okResult.Subject.Value.Should().BeEquivalentTo(expectedBids);
        }

        [Fact]
        public async Task GetAllBids_ReturnsNotFound_WhenNoBidsExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync((List<Bid>)null);

            // Act
            var result = await _controller.GetAllBids();

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetBidById_ReturnsBid_WhenBidExists()
        {
            // Arrange
            var expectedBid = _bidFaker.Generate();

            _mockRepository.Setup(repo => repo.GetByIdAsync(expectedBid.BidId)).ReturnsAsync(expectedBid);

            // Act
            var result = await _controller.GetBidById(expectedBid.BidId);

            // Assert
            //var okResult = result.Result.Should().BeOfType<OkObjectResult>();
            //okResult.Subject.Should().BeEquivalentTo(expectedBid, options => options
            //    .Including(b => b.BidId)
            //    .Including(b => b.Account)
            //    .Including(b => b.BidType));
            var okResult = result.Result.Should().BeEquivalentTo(expectedBid, options => options.ExcludingMissingMembers());

        }

        [Fact]
        public async Task GetBidById_ReturnsNotFound_WhenBidDoesNotExist()
        {
            // Arrange
            var bidId = 123;
            _mockRepository.Setup(repo => repo.GetByIdAsync(bidId)).ReturnsAsync((Bid)null);

            // Act
            var result = await _controller.GetBidById(bidId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CreateBid_ReturnsCreatedAtAction_WhenValidBidIsProvided()
        {
            // Arrange
            var bid = _bidFaker.Generate();

            // Act
            var result = await _controller.CreateBid(bid);

            // Assert
            var createdAtActionResult = result.Result.Should().BeOfType<CreatedAtActionResult>();
            createdAtActionResult.Subject.ActionName.Should().Be(nameof(_controller.GetBidById));
            createdAtActionResult.Subject.RouteValues["id"].Should().Be(bid.BidId);
            createdAtActionResult.Subject.Value.Should().BeEquivalentTo(bid);
        }

        [Fact]
        public async Task CreateBid_ReturnsBadRequest_WhenBidIsNull()
        {
            // Act
            var result = await _controller.CreateBid(null);

            // Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdateBid_ReturnsOk_WhenBidIsUpdated()
        {
            // Arrange
            var bid = _bidFaker.Generate();

            // Act
            var result = await _controller.UpdateBid(bid.BidId, bid);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task DeleteBidWithValidId()
        {
            // Arrange
            int bidId = 4;
            var mockRepository = new Mock<IBidRepository>();
            mockRepository.Setup(repo => repo.DeleteAsync(bidId)).ReturnsAsync(4);
            var mockLogger = new Mock<ILogger<BidsController>>();
            var controller = new BidsController(mockRepository.Object, mockLogger.Object);

            // Act
            var result = await controller.DeleteBid(bidId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBid_WithInvalidId()
        {
            // Arrange
            int bidId = 1;
            var mockRepository = new Mock<IBidRepository>();
            mockRepository.Setup(repo => repo.DeleteAsync(bidId)).ThrowsAsync(new KeyNotFoundException());
            var mockLogger = new Mock<ILogger<BidsController>>();
            var controller = new BidsController(mockRepository.Object, mockLogger.Object);

            // Act
            var result = await controller.DeleteBid(bidId);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
        }
    }
}

using KooliProjekt.Data;
using KooliProjekt.Services;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class PictureServiceTests : ServiceTestBase
    {
        private readonly IPictureService _pictureService;

        public PictureServiceTests()
        {
            _pictureService = new PictureService(DbContext);
        }

        [Fact]
        public async Task Create_ShouldAddPicture()
        {
            // Arrange
            var picture = new Picture
            {
                Name = "Test Picture",
                Context = "Test Context",
                ImageLink = "http://example.com/test-image.jpg"
            };

            // Act
            await _pictureService.Create(picture);

            // Assert
            var result = await _pictureService.Get(picture.Id);
            Assert.NotNull(result);
            Assert.Equal(picture.Name, result.Name);
            Assert.Equal(picture.Context, result.Context);
            Assert.Equal(picture.ImageLink, result.ImageLink);
        }

        [Fact]
        public async Task List_ShouldReturnPagedPictures()
        {
            // Arrange
            var picture1 = new Picture
            {
                Name = "Picture 1",
                Context = "Context 1",
                ImageLink = "http://example.com/image1.jpg"
            };
            var picture2 = new Picture
            {
                Name = "Picture 2",
                Context = "Context 2",
                ImageLink = "http://example.com/image2.jpg"
            };
            await _pictureService.Create(picture1);
            await _pictureService.Create(picture2);

            var search = new PicturesSearch { Keyword = "Picture" };

            // Act
            var result = await _pictureService.List(1, 10, search);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Results.Count);
        }

        [Fact]
        public async Task Get_ShouldReturnPictureById()
        {
            // Arrange
            var picture = new Picture
            {
                Name = "Test Picture",
                Context = "Test Context",
                ImageLink = "http://example.com/test-image.jpg"
            };

            await _pictureService.Create(picture);

            // Act
            var result = await _pictureService.Get(picture.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(picture.Id, result.Id);
            Assert.Equal("Test Picture", result.Name);
            Assert.Equal("Test Context", result.Context);
            Assert.Equal("http://example.com/test-image.jpg", result.ImageLink);
        }

        [Fact]
        public async Task Save_ShouldUpdatePicture()
        {
            // Arrange
            var picture = new Picture
            {
                Name = "Initial Name",
                Context = "Initial Context",
                ImageLink = "http://example.com/initial-image.jpg"
            };

            await _pictureService.Create(picture);

            // Act
            picture.Name = "Updated Name";
            picture.Context = "Updated Context";
            picture.ImageLink = "http://example.com/updated-image.jpg";
            await _pictureService.Save(picture);

            // Assert
            var updatedPicture = await _pictureService.Get(picture.Id);
            Assert.NotNull(updatedPicture);
            Assert.Equal("Updated Name", updatedPicture.Name);
            Assert.Equal("Updated Context", updatedPicture.Context);
            Assert.Equal("http://example.com/updated-image.jpg", updatedPicture.ImageLink);
        }

        [Fact]
        public async Task Delete_ShouldRemovePicture()
        {
            // Arrange
            var picture = new Picture
            {
                Name = "Test Picture",
                Context = "Test Context",
                ImageLink = "http://example.com/test-image.jpg"
            };

            await _pictureService.Create(picture);

            // Act
            await _pictureService.Delete(picture.Id);

            // Assert
            var result = await _pictureService.Get(picture.Id);
            Assert.Null(result);
        }
    }
}

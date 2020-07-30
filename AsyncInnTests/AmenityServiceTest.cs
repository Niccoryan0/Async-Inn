using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AsyncInnTests
{
    public class AmenityServiceTest : DatabaseTestBase
    {
        private IAmenity BuildRepository()
        {
            return new AmenityRepository(_db);
        }
        // TODO: Figure out how to test controllers
        [Fact]
        public async Task CanSaveAndGetAmenity()
        {
            var amenity = new AmenityDTO
            {
                Name = "Room1",
            };

            var repository = BuildRepository();

            var saved = await repository.Create(amenity);

            Assert.NotNull(saved);
            Assert.NotEqual(0, saved.ID);
            Assert.Equal(saved.ID, amenity.ID);
            Assert.Equal(saved.Name, amenity.Name);
        }
    }
}

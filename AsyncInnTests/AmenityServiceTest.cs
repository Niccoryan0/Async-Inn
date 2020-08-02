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
                Name = "Amenity1",
            };

            var repository = BuildRepository();

            var saved = await repository.Create(amenity);

            Assert.NotNull(saved);
            Assert.NotEqual(0, saved.ID);
            Assert.Equal(saved.ID, amenity.ID);
            Assert.Equal(saved.Name, amenity.Name);
        }

        [Fact]
        public async Task CanGetSpecificAmenity()
        {
            var amenity = new AmenityDTO
            {
                Name = "Amenity1",
            };
            var amenity2 = new AmenityDTO
            {
                Name = "Amenity2",
            }; 
            var amenity3 = new AmenityDTO
            {
                Name = "Amenity3",
            };
            var repository = BuildRepository();

            await repository.Create(amenity);
            var saved2 = await repository.Create(amenity2);
            await repository.Create(amenity3);

            // Amenity ID is 5 due to 3 previously seeded Amenities from DbContext
            var result = await repository.GetAmenity(5);

            Assert.Equal(saved2.Name, result.Name);
        }

        [Fact]
        public async Task CanGetAllAmenities()
        {
            var repository = BuildRepository();

            var saved = await repository.GetAmenities();

            Assert.Equal(3, saved.Count);
            Assert.Equal("A/C", saved[0].Name);
        }

        [Fact]
        public async Task CanDeleteAmenity()
        {
            var repository = BuildRepository();

            await repository.Delete(1);

            var saved = await repository.GetAmenities();

            Assert.Equal(2, saved.Count);
            Assert.Equal("Flatscreen TV", saved[0].Name);
        }

        [Fact]
        public async Task CanUpdateAmenity()
        {
            var amenityUpdate = new AmenityDTO
            {
                ID = 1,
                Name = "AmenityUpdate",
            };

            var repository = BuildRepository();
            await repository.Update(amenityUpdate);
            // Amenity ID is 5 due to 3 previously seeded Amenities from DbContext
            var result = await repository.GetAmenity(1);
            Assert.Equal(amenityUpdate.Name, result.Name);
        }
    }
}

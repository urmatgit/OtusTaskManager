using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence;
using UserService.DataAccess.Persistence.Repositories;
using UserService.DataAccess.Persistence.Repositories.Entities;

namespace UserService.DataAccess.xUnitTests
{
    public class ProjectRepositoryTests:IDisposable
    {
        private readonly DbContextOptions<TaskboardDbContext> _dbContextOptions;
        private readonly TaskboardDbContext _context;
        private readonly IProjectRepository _repository;
        public ProjectRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<TaskboardDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

            _context = new TaskboardDbContext(_dbContextOptions);
            _repository = new ProjectRepository(_context);
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        /// <summary>
        /// test find by id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetById_ReturnsEntity_WhenEntityExists()
        {
            // Arrange
            var Id = Guid.NewGuid();
            var expectedEntity = new Project { Id =Id, Name = "Test" };
            await _repository.AddAsync(expectedEntity);
            await _repository.SaveChangesAsync();

            // Act
            var result = await _repository.GetAsync(Id, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(expectedEntity.Id);
            result.Name.Should().Be(expectedEntity.Name);
        }
        /// <summary>
        /// test not exist
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetById_ReturnsNull_WhenEntityDoesNotExist()
        {
            // Act
            var result = await _repository.GetAsync(Guid.NewGuid(), CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
        /// <summary>
        /// test add
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Add_InsertsEntity_WhenEntityIsValid()
        {
            // Arrange
            var newEntity = new Project {Id=Guid.NewGuid(),  Name = "New Entity" };

            // Act
            var result = await _repository.AddAsync(newEntity);
            await _repository.SaveChangesAsync();

            // Assert
            var entityInDb = await _repository.GetAsync(result.Id);
            entityInDb.Should().NotBeNull();
            entityInDb.Name.Should().Be(newEntity.Name);
        }
        /// <summary>
        /// update
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Update_ModifiesEntity_WhenEntityExists()
        {
            // Arrange
            var id=Guid.NewGuid();
            var originalEntity = new Project { Id = id, Name = "Original" };
            await _repository.AddAsync(originalEntity);
            await _repository.SaveChangesAsync();

            var updatedEntity = new Project { Id =id, Name = "Updated" };
            originalEntity.Name = "Updated";
            // Act
            _repository.Update(originalEntity);
            await _context.SaveChangesAsync();

            // Assert
            var entityInDb = await _context.Projects.FindAsync(id);
            entityInDb.Name.Should().Be("Updated");
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_RemovesEntity_WhenEntityExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entityToDelete = new Project { Id = id, Name = "To Delete" };
            await _repository.AddAsync(entityToDelete);
            await _repository.SaveChangesAsync();

            // Act
            _repository.Delete(entityToDelete);
            await _repository.SaveChangesAsync();

            // Assert
            var entityInDb = await _repository.GetAsync(id);
            entityInDb.Should().BeNull();
        }
        [Fact]
        public async Task GetAll_GetAllProjectByPage_ProjectListWithPage()
        {
            // Arrange
            for (int i = 0; i < 10; i++)
            {
                var id = Guid.NewGuid();
                var entityToDelete = new Project { Id = id, Name = $"Project {i}" };
                await _repository.AddAsync(entityToDelete);
            }
            await _repository.SaveChangesAsync();
            // Act
            var listProject = await _repository.GetAllAsync(1, 3);





            // Assert

            listProject.CurrentPage.Should().Be(1);
            listProject.TotalCount.Should().BeGreaterThan(1);
            listProject.HasNextPage.Should().BeTrue();
            listProject.HasPreviousPage.Should().BeFalse();
            listProject.PageSize.Should().Be(3);
        }

    }
}

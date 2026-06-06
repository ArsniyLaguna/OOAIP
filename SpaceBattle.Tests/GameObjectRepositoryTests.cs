using System;
using System.Linq;
using Moq;
using SpaceBattle.Lib;
using Xunit;

namespace SpaceBattle.Tests
{
    public class GameObjectRepositoryTests
    {
        [Fact]
        public void Add_ValidGameObject_ShouldBeAddedToRepository()
        {
            var repository = new GameObjectRepository();
            var gameObjectMock = new Mock<IGameObject>();
            gameObjectMock.SetupGet(g => g.Id).Returns(1);
            
            repository.Add(gameObjectMock.Object);
            
            var result = repository.Get(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(gameObjectMock.Object, result);
        }

        [Fact]
        public void Add_NullGameObject_ShouldThrowArgumentNullException()
        {
            var repository = new GameObjectRepository();
            Assert.Throws<ArgumentNullException>(() => repository.Add(null!));
        }

        [Fact]
        public void Get_NonExistingId_ShouldReturnNull()
        {
            var repository = new GameObjectRepository();
            var result = repository.Get(999);
            Assert.Null(result);
        }

        [Fact]
        public void Remove_ExistingId_ShouldRemoveObject()
        {
            var repository = new GameObjectRepository();
            var gameObjectMock = new Mock<IGameObject>();
            gameObjectMock.SetupGet(g => g.Id).Returns(5);
            
            repository.Add(gameObjectMock.Object);
            repository.Remove(5);
            
            var result = repository.Get(5);
            Assert.Null(result);
        }

        [Fact]
        public void GetAll_EmptyRepository_ShouldReturnEmptyList()
        {
            var repository = new GameObjectRepository();
            var result = repository.GetAll().ToList();
            Assert.Empty(result);
        }

        [Fact]
        public void Add_UpdateExistingObject_ShouldOverwriteOldObject()
        {
            var repository = new GameObjectRepository();
            var obj1Mock = new Mock<IGameObject>();
            var obj2Mock = new Mock<IGameObject>();
            
            obj1Mock.SetupGet(g => g.Id).Returns(1);
            obj2Mock.SetupGet(g => g.Id).Returns(1);
            
            repository.Add(obj1Mock.Object);
            repository.Add(obj2Mock.Object);
            
            var result = repository.Get(1);
            Assert.Equal(obj2Mock.Object, result);
        }
    }
}

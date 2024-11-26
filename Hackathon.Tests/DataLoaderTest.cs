using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Hackathon.Tests
{
    public class DataLoaderTests
    {
        [Fact]
        public void LoadEmployees_ValidFile_ReturnsCorrectList()
        {
            // Arrange
            string testFilePath = "test_valid.csv";
            File.WriteAllLines(testFilePath, new[]
            {
                "Id;Name",
                "1;Alice",
                "2;Bob",
                "3;Charlie"
            });

            // Act
            var result = DataLoader.LoadEmployees(testFilePath, (id, name) => new Junior(id, name));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("Alice", result[0].Name);
            Assert.Equal(3, result[2].Id);
            Assert.Equal("Charlie", result[2].Name);

            // Cleanup
            File.Delete(testFilePath);
        }

        [Fact]
        public void LoadEmployees_FileDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            string nonExistentFile = "nonexistent.csv";

            // Act
            var result = DataLoader.LoadEmployees(nonExistentFile, (id, name) => new Junior(id, name));

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void LoadEmployees_InvalidData_SkipsInvalidLines()
        {
            // Arrange
            string testFilePath = "test_invalid.csv";
            File.WriteAllLines(testFilePath, new[]
            {
                "Id;Name",
                "1;Alice",
                "invalid;data",
                "2;Bob",
                ";;",
                "3;Charlie"
            });

            // Act
            var result = DataLoader.LoadEmployees(testFilePath, (id, name) => new Junior(id, name));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("Alice", result[0].Name);
            Assert.Equal(2, result[1].Id);
            Assert.Equal("Bob", result[1].Name);
            Assert.Equal(3, result[2].Id);
            Assert.Equal("Charlie", result[2].Name);

            // Cleanup
            File.Delete(testFilePath);
        }

        [Fact]
        public void LoadEmployees_EmptyFile_ReturnsEmptyList()
        {
            // Arrange
            string testFilePath = "empty.csv";
            File.WriteAllText(testFilePath, "Id;Name\n");

            // Act
            var result = DataLoader.LoadEmployees(testFilePath, (id, name) => new Junior(id, name));

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

            // Cleanup
            File.Delete(testFilePath);
        }
    }
}

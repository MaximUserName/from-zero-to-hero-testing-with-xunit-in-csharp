using LifecycleFeatures.Core.FileSystem;

namespace LifecycleFeatures.Core.Tests.ConstructorAndDisposable;

public class FileManagerTests
{

    [Fact]
    public void CreateFile_ValidName_CreatesFile()
    {
        // Arrange
        var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDirectory);
        var fileManager = new FileManager(tempDirectory);
        
        // Act
        fileManager.CreateFile("test.txt", "Hello World");

        // Assert
        var filePath = Path.Combine(tempDirectory, "test.txt");
        Assert.True(File.Exists(filePath));
        Assert.Equal("Hello World", File.ReadAllText(filePath));
        
        Directory.Delete(tempDirectory, recursive: true);
    }

    [Fact]
    public void ReadFile_ExistingFile_ReturnsContent()
    {
        // Arrange
        var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDirectory);
        var fileManager = new FileManager(tempDirectory);
        fileManager.CreateFile("test.txt", "Updated Content");

        // Act
        var content = fileManager.ReadFile("test.txt");

        // Assert
        Assert.Equal("Updated Content", content);
        
        Directory.Delete(tempDirectory, recursive: true);
    }
}

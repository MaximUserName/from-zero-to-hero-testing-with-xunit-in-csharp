using TestExecutionControl.Core.ParallelExecution;

namespace TestExecutionControl.Core.Tests.ParallelExecution;

[Collection("Ordered File Tests")]
public class OrderedTests
{
    private readonly FileManager _fileManager = new();
    
    [Fact]
    public void Test1_CreateFile_SetsUpForNextTests()
    {
        var fileName = "test1-file.txt";
        
        _fileManager.CreateFile(fileName, "Test 1 content");
        
        Assert.True(_fileManager.FileExists(fileName));
        
        // Cleanup
        _fileManager.DeleteFile(fileName);
    }

    [Fact]
    public void Test2_ProcessFile_ModifiesContent()
    {
        var fileName = "test2-file.txt";
        
        _fileManager.CreateFile(fileName, "Initial content");
        _fileManager.CreateFile(fileName, "Modified content"); // Overwrite
        
        var content = _fileManager.ReadFile(fileName);
        Assert.Equal("Modified content", content);
        
        // Cleanup
        _fileManager.DeleteFile(fileName);
    }

    [Fact]
    public void Test3_VerifyOperations_ChecksResults()
    {
        var fileName = "test3-file.txt";
        
        _fileManager.CreateFile(fileName, "Verification content");
        
        Assert.True(_fileManager.FileExists(fileName));
        var content = _fileManager.ReadFile(fileName);
        Assert.Equal("Verification content", content);
        
        // Cleanup
        _fileManager.DeleteFile(fileName);
    }
}
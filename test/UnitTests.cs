using api.Controllers;
using api.Infrastructure;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace test;

public class UnitTests
{
    [Fact]
    public void Adding_And_Retrieving_Synonym()
    {
        var dbContext = new ApplicationDbContext();
        var synonymController = new SynonymController(dbContext);

        var word = "hi";
        var synonym = "hello";
        synonymController.AddSynonym(word, synonym);
        
        var result = synonymController.GetSynonym(word);

        var okResult = result as OkObjectResult;
        var resultList = okResult!.Value as List<string>;

        Assert.NotNull(resultList);
        Assert.Contains(synonym, resultList);
        Assert.Single(resultList);
    }

    [Fact]
    public void Adding_And_Retrieving_Word()
    {
        var dbContext = new ApplicationDbContext();
        var synonymController = new SynonymController(dbContext);

        var word = "hi";
        var synonym = "hello";
        synonymController.AddSynonym(word, synonym);
        
        var result = synonymController.GetSynonym(synonym);

        var okResult = result as OkObjectResult;
        var resultList = okResult!.Value as List<string>;

        Assert.NotNull(resultList);
        Assert.Contains(word, resultList);
        Assert.Single(resultList);
    }

    [Fact]
    public void Adding_Nested_And_Retrieving_All_Synonyms()
    {
        var dbContext = new ApplicationDbContext();
        var synonymController = new SynonymController(dbContext);

        var wordOne = "hi";
        var synonymOne = "hello";
        var synonymTwo = "howdy";
        synonymController.AddSynonym(wordOne, synonymOne);
        synonymController.AddSynonym(synonymOne, synonymTwo);
        
        var result = synonymController.GetSynonym(wordOne);

        var okResult = result as OkObjectResult;
        var resultList = okResult!.Value as List<string>;

        Assert.NotNull(resultList);
        Assert.Contains(synonymOne, resultList);
        Assert.Contains(synonymTwo, resultList);
        Assert.Equal(2, resultList.Count);
    }

    [Fact]
    public void Adding_Nested_And_Retrieving_All_Synonyms_From_Nested_Word()
    {
        var dbContext = new ApplicationDbContext();
        var synonymController = new SynonymController(dbContext);

        var wordOne = "hi";
        var synonymOne = "hello";
        var synonymTwo = "howdy";
        synonymController.AddSynonym(wordOne, synonymOne);
        synonymController.AddSynonym(synonymOne, synonymTwo);
        
        var result = synonymController.GetSynonym(synonymTwo);

        var okResult = result as OkObjectResult;
        var resultList = okResult!.Value as List<string>;

        Assert.NotNull(resultList);
        Assert.Contains(wordOne, resultList);
        Assert.Contains(synonymOne, resultList);
        Assert.Equal(2, resultList.Count);
    }

    [Fact]
    public void Missing_Word()
    {
        var dbContext = new ApplicationDbContext();
        var synonymController = new SynonymController(dbContext);

        var word = "hi";
        
        var result = synonymController.GetSynonym(word);

        Assert.IsType<NotFoundResult>(result);
    }
}

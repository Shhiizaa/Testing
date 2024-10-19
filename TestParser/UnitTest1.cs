using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace Parser.Tests
{
  public class Tests
  {
    [SetUp]
    public void Setup()
    {
      // Method intentionally left empty.
    }

    //[TestCase("2+3", "good")]
    //[TestCase("5*6", "good")]
    //[TestCase("7-4", "good")]
    //[TestCase("9/3", "good")]
    //[TestCase("-1", "good")]
    //[TestCase("-(1)", "good")]
    //[TestCase("-(-1)", "good")]
    //[TestCase("-(+1)", "good")]
    //[TestCase("+(+1)", "good")]
    //[TestCase("++1", "bad")]
    //[TestCase("/1", "bad")]
    //[TestCase("1+", "bad")]
    //[TestCase("1+-", "bad")]
    //[TestCase("2/*1+-", "bad")]
    //[TestCase("*8", "bad")]
    //[TestCase("5/", "bad")]
    //[TestCase("1", "good")]
    //[TestCase("1+(2-3)", "good")]
    //[TestCase("(1+2)-3", "good")]
    //[TestCase("((1+2)-3", "bad")]
    //[TestCase("(1+2)-3)", "bad")]
    //[TestCase("(2*(2/3))", "good")]
    //[TestCase("(1+2)-(2*3)", "good")]
    //[TestCase("(1+(2-3*(2+3)))-(2*(4-5)*3)", "good")]
    //[TestCase("1+(2-3*(2+3))-(2*(4-5)*3)", "good")]
    //[TestCase("((((1+(2-3*(2+3))-(2*(4-5)*3)))))", "good")]
    //[TestCase("((((1+(2-3*(2+3))-(2*(4-5)*3))", "bad")]
    //[TestCase("(((())))", "bad")]
    //[TestCase("(1)", "good")]
    //[TestCase("(((((((1)))))))", "good")]
    //public void ParseExpr_ValidInput_ReturnsExpectedResult(string input, string expectedResult, bool isClosed = true, int startInd = 0)
    //{
    //  string result = Parser.ParseExpr(input, ref isClosed, ref startInd);
    //  Assert.That(result, Is.EqualTo(expectedResult));
    //}


    [TestCase("2/*1+-")]
    [TestCase("*8")]
    [TestCase("5/")]
    [TestCase("((1+2)-3")]
    [TestCase("((((1+(2-3*(2+3))-(2*(4-5)*3))")]
    [TestCase("1 2")]
    [TestCase("12 131 41242 ")]
    [TestCase("1+-2")]
    [TestCase("1 + - 2")]
    [TestCase("1 + tyhgjg - 2")]
    [TestCase("1 + 2 / / /")]
    [TestCase(
      """ 
        1 + ( - 1 ) 
        1+(-1)
      """)]
    [TestCase(
      """ 
        1 + ( - 1 ) 
        /
        /
        /

        /jhgjg
        1+(-1)
      """)]
    [TestCase(
      """ 
        1 + ( - 1 ) 
        /
        /
        1+(-1)
      """)]
    public static void ParseExpr_TestBad(string input)
    {
      bool result = ParserV2.Parse(input);
      Assert.That(result, Is.EqualTo(false));
    }

    [TestCase("(2*(2/3))")]
    [TestCase("(1+2)-(2*3)")]
    [TestCase("1")]
    [TestCase(" 1 ")]
    [TestCase(" 1 + 2 ")]
    [TestCase("1213141242")]
    [TestCase(" -1 ")]
    [TestCase("-(2*(2/3))")]
    [TestCase("1+(-1)")]
    [TestCase(" 1 + ( - 1 ) ")]
    [TestCase(
      """ 
        1 +( - 1 ) * 
        1+(-1)
      """)]
    [TestCase(
      """ 
        23 * ( 3 -
        1+(-1) )
      """)]
    [TestCase(
      """ 
        23 *    ( 3 -
        1+(-1) )
      """)]
    [TestCase("1+\t(-1)")]
    [TestCase(
      """ 
        23 *    ( 3 -
        1+(-1) ) // Я устав
        // Я ухожу
        +1
      """)]
    [TestCase(
      """ 
        23 *    ( 3 - //
        1+(-1) ) // Я устав
      """)]
    [TestCase(
      """ 
       23 *    ( 3 - // FHJASFHAI
      // FHJASFHAI
           // FHJASFHAI


                    
              
      // FHJASFHAI // FHJASFHAI
        1+(-1) ) // Я устав
      """)]

    //[TestCase("-1", "good")]
    public void ParseExpr_TestGood(string input)
    {
      bool result = ParserV2.Parse(input);
      Assert.That(result, Is.EqualTo(true));
    }
  }
}
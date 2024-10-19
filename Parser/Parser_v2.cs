using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace Parser
{
  public static class ParserV2
  {
    private static readonly List<char> opers = new List<char> { '+', '-', '*', '/' };
    private static readonly List<char> emptySymbols = new List<char> { ' ', '\t', '\n', '\r', '\f' };
    private static readonly List<char> unaryOpers = new List<char> { '+', '-' };
    private static int curIndex;
    private static string expr;

    public static bool Parse(string expression)
    {
      expr = expression;
      bool result = false;
      curIndex = 0;

      try
      {
        result = ParseExpr();
        if (!result)
        {
          return false;
        }
        else if (curIndex < expr.Length)
        {
          return false;
        }
        else return result;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public static bool ParseExpr()
    {
      ParseUnary();
      if (!ParseOperand())
      {
        return false;
      }
      while (ParseBinary())
      {
        if (!ParseOperand())
        {
          throw new ApplicationException("Error: " + curIndex);
        }
      }

      return true;
    }

    public static bool ParseOperand()
    {
      if (ParseNum())
      {
        return true;
      }
      else if (ParseChar('('))
      {
        ParseExpr();
        if (ParseChar(')'))
        {
          return true;
        }
        else return false;
      }
      else return false;
    }

    public static bool ParseNum()
    {
      Skip();
      var prevInd = curIndex;
      while (curIndex < expr.Length && char.IsDigit(expr[curIndex]))
      {
        curIndex++;
      }
      
      return curIndex > prevInd;
    }

    public static bool ParseChar(char symbol) 
    {
      Skip();

      if (curIndex < expr.Length && expr[curIndex].Equals(symbol))
      {
        curIndex++;
        return true;
      }
      else return false;
    }

    public static bool ParseBinary() 
    {
      Skip();
      if (curIndex < expr.Length && opers.Contains(expr[curIndex]))
      {
        curIndex++;
        return true;
      }
      else return false;
    }

    private static void Skip()
    {
      bool isPrevSlash = false;
      while (curIndex < expr.Length && emptySymbols.Contains(expr[curIndex]))
      {
        curIndex++;
      }
      if (curIndex < expr.Length - 1 
        && expr[curIndex] == '/'
        && expr[curIndex + 1] == '/')
      {
        curIndex += 2;
        while (curIndex < expr.Length && expr[curIndex] != '\n')
        {
          curIndex++;
        }
        if (curIndex < expr.Length)
        {
          curIndex++;
          Skip();
        }
      }
    }

    public static void ParseUnary() 
    {
      Skip();
      if (curIndex < expr.Length && unaryOpers.Contains(expr[curIndex]))
      {
        curIndex++;
      }
    }
  }
}

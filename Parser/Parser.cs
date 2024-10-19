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
  public static class Parser
  {
    private static readonly List<char> opers = new List<char> { '+', '-', '*', '/' };


    public static bool ParseExpr(string expr)
    {
      if (string.IsNullOrEmpty(expr))
      {
        return false;
      }

      bool isClosed = true;
      return ParseExpr(expr, 0, ref isClosed, out _);
    }

    private static bool ParseExpr(string expr, int startIndex, ref bool isClosed, out int curIndex)
    {
      bool res = false;
      bool prevWasOperator = true;
      curIndex = startIndex;

      while (curIndex < expr.Length)
      {
        char cur = expr[curIndex];
        
        if (char.IsDigit(cur))
        {
          res = true;
          prevWasOperator = false;
        }
        else if (opers.Contains(cur))
        {
          if (prevWasOperator || curIndex == expr.Length - 1)
          {
            return false;
          }
          prevWasOperator = true;
        }
        else if (cur == '(')
        {
          bool curIsClosed = false;
          res = ParseExpr(expr, curIndex + 1, ref curIsClosed, out curIndex);

          if (!curIsClosed)
          {
            return false;
          }
          prevWasOperator = false;
        }
        else if (cur == ')')
        {
          if (isClosed)
          {
            return false;
          }
          isClosed = true;
          return !prevWasOperator;
        }
        else
        {
          return false;
        }

        curIndex++;
      }

      isClosed = false;
      return res && !prevWasOperator;
    }
  }
}

using System;

namespace BlogSolution.Types.Exceptions
{
    public class BlogSolutionException : Exception
    {
        public string Code { get; }
        public ApplicationStatusCode ApplicationStatusCode { get; set; }

        public BlogSolutionException()
        {
        }

        public BlogSolutionException(string code)
        {
            Code = code;
        }
        public BlogSolutionException(string message, ApplicationStatusCode applicationStatusCode)
        {
            ApplicationStatusCode = applicationStatusCode;
        }


        public BlogSolutionException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public BlogSolutionException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public BlogSolutionException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public BlogSolutionException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}

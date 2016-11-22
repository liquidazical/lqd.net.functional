using System;
using System.Collections.Generic;

namespace lqd.net.functional {

    /// <summary>
    /// Base class for the errors in Result
    /// </summary>
    public abstract class ResultError { }

    /// <summary>
    ///   ResultError that contains a collaction of result errors
    /// </summary>
    public abstract class RecursiveResultError : ResultError {

        public IEnumerable<ResultError> errors { get; private set; }

        public RecursiveResultError
                ( IEnumerable<ResultError> errors ) {

            if ( errors == null ) throw new ArgumentException( "errors" );

            this.errors = errors;
        }
    }    
}
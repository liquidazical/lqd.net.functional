using System;
using System.Collections.Generic;
using System.Linq;


namespace lqd.net.functional {


    /// <summary>
    /// Result monad that can either be a success or an error.  If it is
    /// an error then there will be a list of one or more errors describing what the
    /// problem is.
    /// </summary>
    public class Result<P> {

        // Create a success result with argument as the value.  There is a 
        // check on the Succees result constructor to make sure that the
        // value actually has one, if this is null then an exception will
        // be thrown.
        public static Result<P> Success
                                 ( P value ) {

            return new SuccessR( value );
        }


        // Create an error result with the supplied errors.  There is a
        // check on the Error result constructor to make sure that the errors
        // have been supplied.
        public static Result<P> Error
                                 ( IEnumerable<ResultError> errors ) {

            return new ErrorR( errors );
        }


        public static Result<P> Error
                                 ( ResultError error ) {

            return new ErrorR ( new [] { error } );
        }


        // Calls the transformation function if it is a success otherwise
        // converts the errors to the new error type.
        public Result<Q> Bind<Q>
                          ( Func<P, Result<Q>> f ) {

            return Match (
                success: f,
                error: errors => Result<Q>.Error ( errors )
            );
        }

        // Apply the function to the value ( if we have one) if it returns 
        // false return an error result.  If it is already a error then just
        // return the error.
        public Result<P> Verify
                          ( Func<P, bool> f
                          , Func<ResultError> e ) {

            return Bind( val => f( val ) ? this : Error( e() ) );
        }



        // Calls and returns the result of the appropriate func
        // based on the result status.
        public Q Match<Q>
                  ( Func<P, Q> success
                  , Func<IEnumerable<ResultError>, Q> error ) {

            if ( this is SuccessR ) {
                return success( (this as SuccessR).value );
            }

            if (this is ErrorR) {
                return error( (this as ErrorR).errors );
            }
            throw new Exception( "Unexpected case" );
        }

        // call the apprpriate action base on the result status.
        public void Match
                     ( Action<P> success
                     , Action<IEnumerable<ResultError>> error ) {

            if ( success == null ) throw new ArgumentNullException( nameof( success ) );
            if ( error == null ) throw new ArgumentNullException( nameof( error ) );


            Match (
               success: value => { success ( value ); return new object(); }
              ,error: errors => { error( errors ); return new object(); }
            );

        }


        private class SuccessR
                       : Result<P> {

            public P value { get; private set; }

            public SuccessR
                    ( P value ) {

                if ( value == null ) throw new ArgumentNullException ( nameof( value ) );

                this.value = value;
            }

        }

        private class ErrorR
                       : Result<P> {

            public IEnumerable<ResultError> errors { get; private set; }

            public ErrorR
                    ( IEnumerable<ResultError> errors ) {

                if ( errors == null ) throw new Exception( "Errors was null!" );
                if ( errors.Contains( null )) throw new Exception( "Contains a null error!" );

                this.errors = errors;
            }

        }


        private Result () { } // Prevent any inheritance or direct instanciation of this class

    }
}

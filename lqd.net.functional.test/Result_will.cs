using System;
using Xunit;

namespace lqd.net.functional.test {

    public class Result_will {

        // done: allow you to create a result as a success
        // done: throw a argument null exception if you supply a null when trying to create a success result
        // done: allow you to create a result as an error with an error
        // done: allow you to create a result with multiple errors
        // done: allow you to match against the result and specify a function to perform if was a success
        // done: allow you to match against the result and specify a function to perform if it was an error
        // done: allow you to specify a continuation if it is a sucess
        // done: allow you to specify a continuation base on verification of the succcess value


        [Fact]
        public void allow_you_to_create_a_result_with_a_value() {

            var a = new object();
            var r = Result<object>.Success( a );


            Assert.True( r is Result<object> );
        }
        
        [Fact]
        public void throw_a_argument_null_exception_if_you_supply_a_null_when_trying_to_create_a_success_result() {

            var act = (Action)( () => Result<object>.Success( null ) );


            Assert.Throws<ArgumentNullException>( act  );
        }

        [Fact]
        public void allow_you_to_create_a_result_as_an_error_with_an_error() {

            var error = new TestResultError();
            var r = Result<object>.Error( error );


            Assert.True( r is Result<object> );
        }

        [Fact]
        public void allow_you_to_create_a_result_with_multiple_errors() {

            var errors = new [] { new TestResultError() };
            var r = Result<object>.Error( errors );


            Assert.True( r is Result<object> );
        }

        [Fact]
        public void allow_you_to_match_against_the_result_and_specify_a_function_to_perform_if_was_a_success() {

            var a = new object();
            var r = Result<object>.Success( a );

            var b = r.Match(
                success: o => o
               ,error: errs => new object()
            );
       

            Assert.Equal( a, b );
        }

        [Fact]
        public void allow_you_to_match_against_the_result_and_specify_a_function_to_perform_if_it_was_an_error() {
            var a = new TestResultError();
            var r = Result<object>.Error( a );

            var b = r.Match(
                success: o => new object()
               ,error: errs => a 
            );
       

            Assert.Equal( a, b );
        }

        [Fact]
        public void allow_you_to_specify_a_continuation_if_it_is_a_sucess() {
            var ra = Result<object>.Success( new object() );
            var rb = Result<object>.Success( new object() );


            Assert.Equal( rb, ra.Bind( a => rb ) );

        }

        [Fact]
        public void allow_you_to_specify_a_continuation_base_on_verification_of_the_succcess_value () {

            var ra = Result<object>.Success( new object() );
            var rb = Result<object>.Success( new object() );


            Assert.Equal( ra, ra.Verify( a => true, () => new TestResultError() ) );


        }

        public class TestResultError : ResultError { }

    }

}

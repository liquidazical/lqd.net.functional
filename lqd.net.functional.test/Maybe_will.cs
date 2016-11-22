using System;
using Xunit;

namespace lqd.net.functional.test {

    public class Maybe_will {

        // done: allow you to create a maybe with a value
        // done: throw a argument null exception if you supply a null when trying to create a maybe with a value
        // done: allow you to create a maybe without a value
        // done: allow you to match against the maybe and specify a function to perform if it has a value
        // done: allow you to match against the maybe and specify a function to perform if it has no value
        // done: allow you to check if there is a value
        // done: allow you to specify an action to take if there is a value
        // done: allow you to specify an action to take if there is no value
        // done: allow you to specify an action if there is a value
        // done: allow you to specify an action if there is not a value


        
        [Fact]
        public void allow_you_to_create_a_maybe_with_a_value() {

            var a = new object();
            var m = Maybe<object>.just( a );


            Assert.True( m is Maybe<object> );
        }
        
        
        [Fact]
        public void throw_a_argument_null_exception_if_you_supply_a_null_when_trying_to_create_a_maybe_with_a_value() {

            var act = (Action)( () => Maybe<object>.just( null ) );


            Assert.Throws<ArgumentNullException>( act );
        }


        [Fact]
        public void allow_you_to_create_a_maybe_without_a_value() {

            var m = Maybe<object>.nothing();

            Assert.True( m is Maybe<object> );
        }


        [Fact]
        public void allow_you_to_match_against_the_maybe_and_specify_a_function_to_perform_if_it_has_a_value () {

            var a = new object();
            var m = Maybe<object>.just( a );

            var b = m.Match(
                just: o => o
               ,nothing: () => new object()
            );
       

            Assert.Equal( a, b );
        }

        [Fact]
        public void allow_you_to_match_against_the_maybe_and_specify_a_function_to_perform_if_it_has_no_value() {

            var a = new object();
            var m = Maybe<object>.nothing();

            var b = m.Match(
                 just: o => { throw new Exception( "This case should never execute" ); }
                ,nothing: () => a
            );

            Assert.Equal( a, b );
        }

        [Fact]
        public void allow_you_to_check_if_there_is_a_value() {

            var m1 = Maybe<object>.just( new object() );
            var m2 = Maybe<object>.nothing();


            Assert.True( m1.HasValue() );
            Assert.False( m2.HasValue() );
        }

        [Fact]
        public void allow_you_to_specify_an_action_to_take_if_there_is_a_value() {

            var a = new object();
            var b = new Object();

            var m = Maybe<object>.just( a );

            m.Match(
               just: n => { b = n;  }
              ,nothing : () => { throw new ArgumentNullException( "This case should never be called" ); }
            );


            Assert.Equal( a, b );
        }

        [Fact]
        public void allow_you_to_specify_an_action_to_take_if_there_is_no_value() {

            var a = new object();
            var b = new Object();

            var m = Maybe<object>.nothing( );

            m.Match(
               just: n => { throw new ArgumentNullException( "This case should never be called" ); }
              ,nothing : () => { b = a; }
            );


            Assert.Equal( a, b );
        }


        [Fact]
        public void allow_you_to_specify_an_action_if_there_is_a_value() {

            var ma = Maybe<object>.just( new object() );
            var mb = Maybe<object>.just( new object() );


            Assert.Equal( mb, ma.Bind( a => mb ) );

        }

        [Fact]
        public void allow_you_to_specify_an_action_if_there_is_not_a_value() {

            var ma = Maybe<object>.nothing();
            var mb = Maybe<object>.just( new object() );


            Assert.Equal( mb, ma.IfNothing( () => mb ) );
        }

    }
}

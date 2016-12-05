using System;
using Xunit;

namespace lqd.net.functional.test {

    public class Either_will {

        // done: allow you to create a left either
        // done: throw a argument null exception if you supply a null when trying to create a left either 
        // done: allow you to create a right either
        // done: throw a argument null exception if you supply a null when trying to create a right either 
        // done: allow you to match against the left 
        // done: allow you to match against the right 
        // done: allow you to specify an action if it is a left either
        // done: map the right through on a left action if it is a right 
        // done: allow you to specify an action if it is a right either
        // done: map the left through on an right action if it is a left
        // done: provide a lifted left
        // done: provide a lifted right
        // done: allow you to match against the left with an action
        // done: allow you to match against the right with an action


        [Fact]
        public void allow_you_to_create_a_left_either() {

            var a = new object();
            var either = Either<object,object>.Left( a );

            Assert.True( either is Either<object,object> );
        }

        [Fact]
        public void throw_an_argument_null_exception_if_you_supply_a_null_when_trying_to_create_a_left_either() {

            var act = (Action)( () => Either<object,object>.Left( null ) );

            Assert.Throws<ArgumentNullException>( act );

        }

        [Fact]
        public void allow_you_to_create_a_right_either() {

            var b = new object();
            var either = Either<object,object>.Right( b );


            Assert.True(  either is Either<object,object> );
        }

        [Fact]
        public void throw_a_argument_null_exception_if_you_supply_a_null_when_trying_to_create_a_right_either() {

            var act = (Action)( () => Either<object,object>.Right( null ) );


            Assert.Throws<ArgumentNullException>( act );
        }

        [Fact]
        public void allow_you_to_match_against_the_left() {

            var a = new object();
            var either = Either<object,object>.Left( a );

            var b = either.Match(
               left: o => o
              ,right: o => { throw new Exception( "Unexpected case" ); } 
            );

            Assert.Equal( a, b );

        }

        [Fact]
        public void allow_you_to_match_against_the_right() {

            var a = new object();
            var either = Either<object,object>.Right( a );

            var b = either.Match(
               left: o => { throw new Exception( "Unexpected case" ); }
              ,right: o => o
            );

            Assert.Equal( a, b );
        } 

        [Fact]
        public void allow_you_to_specify_an_action_if_it_is_a_left_either() {

            var a = new object();
            var b = Either<Left,Right>
                      .Left( new Left() )
                      .IfLeft( l => Either<object,Right>.Left( a ))
                      .Match(
                         left: o => o
                        ,right: o => { throw new Exception( "Unexpected case" ); }
                      );

            Assert.Equal( a, b );

        }

        [Fact]
        public void map_the_right_through_on_a_left_action_if_it_is_a_right() {

            var a = new Right();
            var b = Either<Left,Right>
                      .Right( a )
                      .IfLeft( l => Either<object,Right>.Left( a ))
                      .Match(
                         left: o => { throw new Exception( "Unexpected case" ); }
                        ,right: o => o
                      );

            Assert.Equal( a, b );

        } 

        [Fact]
        public void allow_you_to_specify_an_action_if_it_is_a_right_either() {

            var a = new object();
            var b = Either<Left,Right>
                      .Right( new Right() )
                      .IfRight( l => Either<Left,object>.Right( a ))
                      .Match(
                         left: o => { throw new Exception( "Unexpected case" ); }
                        ,right: o => o
                      );

            Assert.Equal( a, b );
        }

        [Fact]
        public void map_the_left_through_on_an_right_action_if_it_is_a_left() {

            var a = new Left();
            var b = Either<Left,Right>
                      .Left( a )
                      .IfRight( l => Either<Left,object>.Right( a ))
                      .Match(
                         left: o => o
                        ,right: o =>{ throw new Exception( "Unexpected case" ); } 
                      );

            Assert.Equal( a, b );

        }

        [Fact]
        public void provide_a_lifted_left() {

            var a = new object();
            var b = Either<Left,Right>
                      .Left( new Left() )
                      .LiftIfLeft( l => a )
                      .Match(
                         left: o => o
                        ,right: o => { throw new Exception( "Unexpected case" ); }
                      );

            Assert.Equal( a, b );
            
        }

        [Fact]
        public void provide_a_lifted_right() {

            var a = new object();
            var b = Either<Left,Right>
                      .Right( new Right() )
                      .LiftIfRight( l => a )
                      .Match(
                         left: o => { throw new Exception( "Unexpected case" ); }
                        ,right: o => o
                      );

            Assert.Equal( a, b );
        }

        [Fact]
        public void allow_you_to_match_against_the_left_with_an_action() {

            var a = new object();
            var b = (object)null;

            var either = Either<object,object>.Left( a );

            either.Match(
               left: o => b = o
              ,right: o => { throw new Exception( "Unexpected case" ); } 
            );

            Assert.Equal( a, b );


        }

        [Fact]
        public void allow_you_to_match_against_the_right_with_an_action() {

            var a = new object();
            var b = (object)null;

            var either = Either<object,object>.Right( a );

            either.Match(
               left: o => { throw new Exception( "Unexpected case" ); }
              ,right: o => b = o
            );

            Assert.Equal( a, b );
        } 


        private class Left { }
        private class Right { }
     }
}

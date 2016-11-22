using System;
using Xunit;

namespace lqd.net.functional.test {


    public class fmap_will {

        // done: apply the function to the object to be mapped
        // apply an action to the object begin mapped
        // done: throw an argument null exception if the object to be mapped is null
        // done: throw an argument null exception if the function to apply is null


        [Fact]
        public void apply_the_function_to_the_object_to_be_mapped() {

            var a = new object();


            Assert.Equal( a, a.fmap( f ) );
        }

        [Fact]
        public void apply_an_action_to_the_object_begin_mapped() {
            
            var a = new object();
            var b = (object)null;
            var f = (Action<object>)( x => b = x );

            a.fmap( f );

            Assert.Equal( a, b );
        }

        [Fact]
        public void throws_an_argument_null_exception_if_the_object_to_be_mapped_is_null () {

            var a = (object)null;


            Assert.Throws<ArgumentNullException>( () => a.fmap( f ) );
        }


        [Fact]
        public void throw_an_argument_null_exception_if_the_function_to_apply_is_null() {

            var a = new object();
            var f = (Func<object,object>)null;


            Assert.Throws<ArgumentNullException>( () => a.fmap( f ) );
        }


        private static object f( object a ) {
            return a;
        }

    }
}

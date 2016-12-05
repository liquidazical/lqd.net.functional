using System;

namespace lqd.net.functional {

    /// <summary>
    /// Either monad that can either be a left or a right. 
    /// </summary>
    public class Either<left,right> {

        // Create a Left either
        public static Either<left,right> Left( left value ) {

            return new LeftE( value );
        }

        // Create a right either
        public static Either<left,right> Right( right value ) {

            return new RightE( value );
        }


        // Continuation where the function is applied if it is a left value
        public Either<Q,right> IfLeft<Q>( Func<left,Either<Q,right>> f ) {

            if ( f == null ) throw new ArgumentNullException( nameof( f ) );


            return Match(
               left: f
              ,right: Either<Q,right>.Right
            );              

        }

        public Either<Q,right> LiftIfLeft<Q>( Func<left,Q> f ) {

            if ( f == null ) throw new ArgumentNullException( nameof( f ) );


            return Match(
               left  : o => Either<Q,right>.Left( f( o ) ) 
              ,right : Either<Q,right>.Right
            );              

        }


        // Continuation where the function is applied if it is a right value
        public Either<left,Q> IfRight<Q>( Func<right,Either<left,Q>> f ) {

            if ( f == null ) throw new ArgumentNullException( nameof( f ) );


            return Match(
               left: Either<left,Q>.Left
              ,right: f
            );              

        }

        public Either<left,Q> LiftIfRight<Q>( Func<right,Q> f ) {

            if ( f == null ) throw new ArgumentNullException( nameof( f ) );


            return Match(
               left  : Either<left,Q>.Left
              ,right : o => Either<left,Q>.Right( f( o ) )
            );

        }


        // Continuation where you can specify a function that is applied for 
        // both left and right
        public Q Match<Q>
                   ( Func<left,Q> left  
                   , Func<right,Q> right ) {

            if ( left == null ) throw new ArgumentNullException( nameof( left ) );
            if ( right == null ) throw new ArgumentNullException( nameof( right ) );


            if ( this is LeftE ) {
                return left( (this as LeftE).Value );

            } else if ( this is RightE ) {
                return right( (this as RightE).Value );
            }
            throw new Exception( "Unexpected case" );

        }


        public void Match
                     ( Action<left> left
                     , Action<right> right ) {

            if ( left == null ) throw new ArgumentNullException( nameof( left ) );
            if ( right == null ) throw new ArgumentNullException( nameof( right ) );


            Match(
               left: l => { left( l ); return new object(); }
              ,right: r => { right( r ); return new object(); }
            );

        }


        private class LeftE : Either<left,right> {

            public readonly left Value;

            public LeftE
                    ( left value ) {

                if ( value == null ) throw new ArgumentNullException( nameof( value ) );


                this.Value = value;
            }
        }

        private class RightE : Either<left,right> {

            public readonly right Value;

            public RightE
                    ( right value ) {

                if ( value == null ) throw new ArgumentNullException( nameof( value ) );


                this.Value = value;
            }
        }


        private Either() { }
    }

}

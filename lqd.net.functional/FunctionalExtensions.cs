using System;

namespace lqd.net.functional {

    public static class FunctionalExtensions {


        /// <summary>
        /// Applies f to p and returns the result.  If either p or f is null 
        /// an ArgumentNullException will be returned.
        /// </summary>
        public static Q fmap<P,Q>
                         ( this P p
                         , Func<P,Q> f ) {

            if ( p == null ) throw new ArgumentNullException( nameof( p ) );
            if ( f == null ) throw new ArgumentNullException( nameof( f ) );


            return f( p );
        }


        public static void fmap<P>
                            ( this P p 
                            , Action<P> a ) {

            if ( p == null ) throw new ArgumentNullException( nameof( p ) );
            if ( a == null ) throw new ArgumentNullException( nameof( a ) );

            a( p );
        }
    }
}

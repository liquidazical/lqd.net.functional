using System;

namespace lqd.net.functional {

    /// <summary>
    /// Implementation of the Maybe monad
    /// </summary>
    public class Maybe<P> {


        /// <summary>
        /// Creates a Maybe that represents having a value
        /// </summary>
        public static Maybe<P> just
                               ( P value  ) {

            return new Just( value );
        }

        /// <summary>
        /// Creates a Maybe that represents having no value
        /// </summary>
        public static Maybe<P> nothing
                                 ( ) {

            return new Nothing();
        }

        /// <summary>
        /// Returns true if the Maybe represent a value
        /// </summary>
        public bool HasValue() {

          return
            Match(
               just: v => true
              ,nothing: () => false
            );
        }        

        public Maybe<Q> Bind<Q>
                         ( Func<P, Maybe<Q>> f ) {

          if ( f == null ) throw new ArgumentNullException( nameof( f ) );


          return 
            Match(
               just: f 
              ,nothing: () => Maybe<Q>.nothing()
            );

        }

        public Maybe<P> IfNothing
                         ( Func<Maybe<P>> f )  {

          if ( f == null ) throw new ArgumentNullException( nameof( f ) );

          return
            Match(  
               just: v => this
              ,nothing: f
            );
        }


        /// <summary>
        /// Allow you to specify what should be returned for either case of 
        /// having a value or not having a value
        /// </summary>
        public Q Match<Q>
                  ( Func<P,Q> just  
                  , Func<Q> nothing ) {

           if ( just == null ) throw new ArgumentNullException( nameof( just ) );
           if ( nothing == null ) throw new ArgumentNullException( nameof( nothing ) );


           if ( this is Just ) {
              return just( (this as Just).value );
           }

           if ( this is Nothing ) {
               return nothing( );
           }

           throw new Exception( "Case not handeled" );
        } 


        /// <summary>
        /// Allow you to specify what should be performed for either case of 
        /// having a value or not having a value
        /// </summary>
        public void Match
                     ( Action<P> just 
                     , Action nothing ) {

            Match(
               just: p => { just( p ); return new object();  }
              ,nothing: () => { nothing(); return new object();  }
            );

        }


        private class Just
                       : Maybe<P> {

            public P value { get; private set; }

            public Just
                    ( P value ) {
        
                if ( value == null ) throw new ArgumentNullException( nameof( value ) );


                this.value = value;
            }
        }
        
        private class Nothing
                       : Maybe<P> { }
               

        private Maybe() {  } // ensure that you can only create a maybe via the factory methods just and nothing
    }

}

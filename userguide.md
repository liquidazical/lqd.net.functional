# lqd.net.functional user guide

* Either
* fmap
* Maybe
* Result


## Either

This is an implementation of a monad that represents a choice of eithe a left or right value. The following methods are supported:

* Left - factory method when you have a value
* Right - factory method when you have one or more errors
* IfLeft - Continuation where the function is applied if it is a left value
* LiftIfLeft - Syntactic sugar over IfLet that will lift the result into an either monand
* IfRight - Continuation where the function is applied if it is a right value 
* LiftIfRight - Syntactic sugar over IfRight that will lift the result into an either monand
* Match - allows you to specify what should happen in either case 

```
using lqd.net.functional
  ...

  var a = new object();
  var either = Either<object,object>.Left( a );

  var b = either.Match(
     left: o => o
    ,right: o => { throw new Exception( "Unexpected case" ); } 
  );

  Assert.Equal( a, b );
```

```
using lqd.net.functional
  ...

  var a = new object();
  var either = Either<object,object>.Right( a );

  var b = either.Match(
     left: o => { throw new Exception( "Unexpected case" ); }
    ,right: o => o
  );

  Assert.Equal( a, b );
```


## fmap

Allows you to apply a function to an object.  Think of it as an equivalent of Select but for objects instead of IEnumerable`S

```

using lqd.net.functional;
using System;
using System.Collections.Generic;
using System.Linq;


namespace test {

	public class Example{
	
	
		public IEnumerable<Projection> GetForId( Guid id ) {

			return 
			  repository
			    .Entities()
			    .Single()
			    .fmap( to_projection )
			    ;

		}

		private static Projection to_projection
								   ( Entity Projection ) { ... }
			

	}

	public class Entity { ... }

	public class Projection { ... }

}

```

There is also a variant that accepts an action rather than a function

## Maybe

This is an implemenation of the Maybe monad. The following methods are supported:

* Just - factory method when you have a value
* Nothing - factory method when you do not have a value
* Bind - allows you to specify a continuation base on the value if it has one
* Match - allows you to specify what should happen in either case 
* HasValue - tells you if the maybe has a value ( note this is syntactic sugar over the match )
* IfNothing - allows you to  specify a continuation if there is no value


```
using lqd.net.functional
  ...

  var a = new object();
  var m = Maybe<object>.Just( a );
  
  var b = m.Match(
      just: o => o
     ,nothing: () => new object()
  );
  
  
  Assert.Equal( a, b );

```

```
using lqd.net.functional
  ...

  var a = new object();
  var m = Maybe<object>.Nothing();
  
  var b = m.Match(
       just: o => { throw new Exception( "This case should never execute" ); }
      ,nothing: () => a
  );
  
  Assert.Equal( a, b );
```


## Result

This is an implementation of a monad that represents the states of either a success with a value or an error with a description in the form of a ResultError. The following methods are supported:

* Success - factory method when you have a value
* Error - factory method when you have one or more errors
* Bind - allows you to specify a continuation base on the value if it has one
* Match - allows you to specify what should happen in either case 
* Verify - allows you to apply a check to the value if it has one which will result in the result or an error  

```
using lqd.net.functional
  ...

  var a = new object();
  var r = Result<object>.Success( a );
  
  var b = r.Match(
      success: o => o
     ,error: errors => new object()
  );
  
  
  Assert.Equal( a, b );

```

```
using lqd.net.functional
  ...

  var a = new object();
  var r = Result<object>.Error();
  
  var b = r.Match(
       success: o => { throw new Exception( "This case should never execute" ); }
      ,error: errors => a
  );
  
  Assert.Equal( a, b );
```
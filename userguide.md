# lqd.net.functional user guide


* fmap


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

There is also a variant that 
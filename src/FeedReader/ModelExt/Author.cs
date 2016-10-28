using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsServer.Model
{
    public partial class Author
    {

        public static Author FindByName(string name, NewsCrud_DBContext context)
        {

            return context.Author.FirstOrDefault(a => a.Name == name);
            //doStuff
            
        }

    }
}

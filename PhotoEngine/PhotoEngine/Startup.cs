using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PhotoEngine.Startup))]

namespace PhotoEngine
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}

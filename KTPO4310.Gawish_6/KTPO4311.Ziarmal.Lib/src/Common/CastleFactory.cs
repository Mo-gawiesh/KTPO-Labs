using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Ziarmal.Lib.src.Common
{
    public static class CastleFactory
    {
        ///<summary>Контейнер</summary>
        
        public static IWindsorContainer container {  get; private set; }

        static CastleFactory()
        {
            //создать объект контейнер
            container = new WindsorContainer();
        }
    }
}

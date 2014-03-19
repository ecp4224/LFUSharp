using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFUSharp;

namespace LFUSharpExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IntBasedCache();
            CustomBasedCache();
            Console.ReadKey();
        }

        static void IntBasedCache()
        {
            LFUIntCache<string> cache = new LFUIntCache<string>(); //Create a basic LFU cache that will hold strings
            int k1 = cache.Add("testing"); //Add the item "testing" and get the key for it
            int k2 = cache.Add("test"); //Add the item "test" and get the key for it
            cache[55] = "t"; //Add the item "t" with the k 55

            string test = cache[k1]; //Get the item with the key "k1" and increase its frequency by 1
            test = cache[k1]; //Get the item with the key "k1" and increase its frequency by 1
            test = cache[55]; //Get the item with the key "55" and increase its frequency by 1

            Console.WriteLine(test); //OUTPUT: t
            //Print the least frequently used object in the cache
            Console.WriteLine(cache.LeastFrequentlyUsedObject); //OUTPUT: test
        }

        static void CustomBasedCache()
        {
            MyObject obj1 = new MyObject();
            obj1.var1 = 1;
            obj1.var2 = 1;
            MyObject obj3 = new MyObject();
            obj3.var1 = 2;
            obj3.var2 = 1;
            MyObject obj2 = new MyObject();
            obj2.var1 = 5;
            obj2.var2 = 5;
            LFUCache<MyKey, MyObject> customCache = new LFUCache<MyKey, MyObject>();
            MyKey key = customCache.Add(obj1);
            MyKey key2 = customCache.Add(obj2);
            MyKey key3 = customCache.Add(obj3);

            
            //........

            //obj1.var1 + obj1.var2
            //OUTPUT: 2
            Console.WriteLine("" + (customCache[key].var1 + customCache[key].var2)); //The frequency count will go up 2, because we access it twice

            MyObject obj = customCache[key3];
            //obj3.var1 + obj3.var2
            //OUTPUT: 3
            Console.WriteLine("" + (obj.var1 + obj.var2)); //The frequency count will go up 1, because we only access it once

            obj = customCache.LeastFrequentlyUsedObject; //The least frequently used object will be obj2 because we haven't requested it yet.
            //obj2.var1 + obj2.var2
            //OUTPUT: 10
            Console.WriteLine("" + (obj.var1 + obj.var2)); //This will be obj2.var1 + obj2.var2
        }

        public class MyObject : LFUSharp.Core.IKeyable
        {
            public int var1;
            public int var2;
            private MyKey key;
            private static int keyCount;
            public MyObject()
            {
                keyCount++;
                key = new MyKey();
                key.key = keyCount;
            }

            object LFUSharp.Core.IKeyable.UniqueKey()
            {
                return key;
            }
        }

        public class MyKey
        {
            public int key;
            public override bool Equals(object obj)
            {
                if (obj is MyKey)
                    return key == ((MyKey)obj).key;
                return false;
            }
            public override int GetHashCode()
            {
                return key;
            }
            public override string ToString()
            {
                return "" + key;
            }
        }
    }
}

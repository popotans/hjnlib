using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Internal;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Wrappers;
using MongoDB.Bson;
namespace NjhLib.Utils.mMongoDb
{
    /// <summary>
    ///  参考：http://blog.sina.com.cn/s/blog_41768f970100rtrt.html
    /// </summary>
    public class Demo
    {
        string connstr = "mongodb://localhost";
        MongoServer server = null;
        public Demo()
        {
            server = MongoServer.Create(connstr);
            MongoDatabase db = server.GetDatabase("testDB");
            MongoCollection collection = db.GetCollection("employes");
            BsonDocument doc = new BsonDocument
            {
                {"name","niejunhua"},{"title","fun man"}
            };
            collection.Insert(doc);
        }
    }
}

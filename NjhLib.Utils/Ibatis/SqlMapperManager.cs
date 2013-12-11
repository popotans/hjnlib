using System;
using System.Collections.Generic;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using IBatisNet.DataMapper.SessionStore;
using System.Text;

namespace NjhLib.Utils.Ibatis
{
    public class SqlMapperManager
    {
        private static readonly object objSync = new object();
        private static readonly IDictionary<string, ISqlMapper> dictMappers = null;

        static SqlMapperManager()
        {
            dictMappers = new Dictionary<string, ISqlMapper>();
        }
        /// <summary>
        /// 实例化SqlMap对象
        /// </summary>
        /// <param name="mapperName">通常.config文件</param>
        /// <returns></returns>
        public static ISqlMapper GetMapper(string mapperName)
        {
            if (string.IsNullOrEmpty(mapperName))
            {
                throw new Exception("MapperName为空!");
            }
            if (mapperName.ToLower().LastIndexOf(".config") == -1)
            {
                mapperName += ".config";
            }
            ISqlMapper mapper = null;
            if (dictMappers.ContainsKey(mapperName))
            {
                mapper = dictMappers[mapperName];
            }
            else
            {
                if (mapper == null)
                {
                    lock (objSync)
                    {
                        if (mapper == null)
                        {
                            mapper = new DomSqlMapBuilder().Configure(mapperName);
                            mapper.SessionStore = new HybridWebThreadSessionStore(mapper.Id);
                            dictMappers.Add(mapperName, mapper);
                        }
                    }
                }
            }
            return mapper;
        }
    }
}


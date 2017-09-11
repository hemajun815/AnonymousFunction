using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace WebHelper
{
    /// <summary>
    /// 修改web.config
    /// 2015-10-24 14:09 hemajun
    /// </summary>
    public class HelperWebConfig
    {
        #region 修改config文件

        /// <summary>
        /// 获取config文件AppSetting
        /// </summary>
        public static string GetAppSetting(string configNa, string key)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = String.Format("{0}{1}.config", AppDomain.CurrentDomain.BaseDirectory, configNa);
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute _key = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (_key != null)
                {
                    if (_key.Value == key)
                    {
                        _key = nodes[i].Attributes["value"];
                        return _key.Value;
                    }
                }
            }
            return string.Empty;
        }

        public static string GetAppSetting(string key)
        {
            return GetAppSetting("Web", key);
        }
        /// <summary>
        /// 修改AppSetting节点
        /// </summary>
        public static void UpdateAppSetting(string configNa, string key, string value)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = String.Format("{0}{1}.config", AppDomain.CurrentDomain.BaseDirectory, configNa);
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute _key = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (_key != null)
                {
                    if (_key.Value == key)
                    {
                        //对目标元素中的第二个属性赋值
                        _key = nodes[i].Attributes["value"];
                        _key.Value = value;
                        break;
                    }
                }
            }
            //保存上面的修改
            doc.Save(strFileName);
        }
        public static void UpdateAppSetting(string key, string value)
        {
            UpdateAppSetting("Web", key, value);
        }
        /// <summary>
        /// 修改connectionString节点
        /// </summary>
        public static void UpdateConnectionString(string configNa, string name, string value)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = String.Format("{0}{1}.config", AppDomain.CurrentDomain.BaseDirectory, configNa);
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute _name = nodes[i].Attributes["name"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (_name != null)
                {
                    if (_name.Value == name)
                    {
                        //对目标元素中的第二个属性赋值
                        _name = nodes[i].Attributes["connectionString"];
                        _name.Value = value;
                        break;
                    }
                }
            }
            //保存上面的修改
            doc.Save(strFileName);
        }
        public static void UpdateConnectionString(string name, string value)
        {
            UpdateConnectionString("Web", name, value);
        }
        #endregion
    }
}

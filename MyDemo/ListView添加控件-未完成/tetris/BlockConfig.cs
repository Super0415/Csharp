using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace tetris
{
    class BlockConfig
    {
        private Keys _upKey;            //上
        private Keys _downKey;          //下
        private Keys _leftKey;          //左
        private Keys _rightKey;         //右
        private Keys _deasilKey;        //顺时针
        private Keys _contraKey;        //逆时针
        private int _blockNumX;         //水平格子数
        private int _blockNumY;         //垂直格子数
        private int _blockColnum;          //格子像素
        private Color _blockColor;          //画布背景颜色
        private InfoArr _blockInfo = new InfoArr(); //砖块信息
        #region  私有变量属性
        public Keys UpKey
        {
            get { return _upKey;  }
            set { _upKey = value; }
        }
        public Keys DownKey
        {
            get { return _downKey; }
            set { _downKey = value; }
        }
        public Keys LeftKey
        {
            get { return _leftKey; }
            set { _leftKey = value; }
        }
        public Keys RightKey
        {
            get { return _rightKey; }
            set { _rightKey = value; }
        }
        public Keys DeasilKey
        {
            get { return _deasilKey; }
            set { _deasilKey = value; }
        }
        public Keys ContraKey
        {
            get { return _contraKey; }
            set { _contraKey = value; }
        }
        public int BlockNumX
        {
            get { return _blockNumX; }
            set
            {
                if(value >= 10 && value <= 50)
                    _blockNumX = value; }
        }
        public int BlockNumY
        {
            get { return _blockNumY; }
            set
            {
                if(value >= 15 && value <= 50)
                    _blockNumY = value;
            }
        }
        public int BlockCol
        {
            get { return _blockColnum; }
            set
            {
                if (value >= 10 && value <= 30)
                    _blockColnum = value;
            }
        }
        public Color BlockColor
        {
            get { return _blockColor; }
            set { _blockColor = value; }
        }

        

        public InfoArr BlockInfo
        {
            get { return _blockInfo; }
            set { _blockInfo = value; }
        }
        #endregion

        public void LoadFromXmlFile()
        {
            XmlTextReader reader;
            if (File.Exists("BlockSet.xml"))
            {
                reader = new XmlTextReader("BlockSet.xml");
            }
            else
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                Stream sm = asm.GetManifestResourceStream("tetris.BlockSet.xml");
                reader = new XmlTextReader(sm);
            }

            string key = "";
            try
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "ID")
                        {
                            key = reader.ReadElementString().Trim();
                            _blockInfo.Add(key, "");
                        }
                        else if (reader.Name == "Color")
                        {
                            _blockInfo[key] = reader.ReadElementString().Trim();
                        }
                        else if (reader.Name == "UpKey")
                        {
                            _upKey = (Keys)Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "DownKey")
                        {
                            _downKey = (Keys)Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "LeftKey")
                        {
                            _leftKey = (Keys)Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "RightKey")
                        {
                            _rightKey = (Keys)Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "DeasilKey")
                        {
                            _deasilKey = (Keys)Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "ContraKey")
                        {
                            _contraKey = (Keys)Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "BlockNumX")
                        {
                            _blockNumX = Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "BlockNumY")
                        {
                            _blockNumY = Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "BlockColnum")
                        {
                            _blockColnum = Convert.ToInt32(reader.ReadElementString().Trim());
                        }
                        else if (reader.Name == "BlockColor")
                        {
                            _blockColor = Color.FromArgb(Convert.ToInt32(reader.ReadElementString().Trim()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public void SaveToXmlFile()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<blockset></blockset>");

            XmlNode root = doc.SelectSingleNode("blockset");

            for (int i = 0; i < _blockInfo.Length; i++)
            {
                XmlElement xelType = doc.CreateElement("type");

                XmlElement xelID = doc.CreateElement("ID");
                xelID.InnerText = ((BlockInfo)_blockInfo[i]).GetIdStr();
                xelType.AppendChild(xelID);

                XmlElement xelColor = doc.CreateElement("Color");
                xelColor.InnerText = ((BlockInfo)_blockInfo[i]).GetColorStr();
                xelType.AppendChild(xelColor);

                root.AppendChild(xelType);
            }

            XmlElement xelKey = doc.CreateElement("key");

            XmlElement xelUpKey = doc.CreateElement("UpKey");
            xelUpKey.InnerText = Convert.ToInt32(_upKey).ToString();
            xelKey.AppendChild(xelUpKey);

            XmlElement xelDownKey = doc.CreateElement("DownKey");
            xelDownKey.InnerText = Convert.ToInt32(_downKey).ToString();
            xelKey.AppendChild(xelDownKey);

            XmlElement xelLeftKey = doc.CreateElement("LeftKey");
            xelLeftKey.InnerText = Convert.ToInt32(_leftKey).ToString();
            xelKey.AppendChild(xelLeftKey);

            XmlElement xelRightKey = doc.CreateElement("RightKey");
            xelRightKey.InnerText = Convert.ToInt32(_rightKey).ToString();
            xelKey.AppendChild(xelRightKey);

            XmlElement xelDeasilKey = doc.CreateElement("DeasilKey");
            xelDeasilKey.InnerText = Convert.ToInt32(_deasilKey).ToString();
            xelKey.AppendChild(xelDeasilKey);

            XmlElement xelContraKey = doc.CreateElement("ContraKey");
            xelContraKey.InnerText = Convert.ToInt32(_contraKey).ToString();
            xelKey.AppendChild(xelContraKey);

            root.AppendChild(xelKey);

            //界面
            XmlElement xelsurface = doc.CreateElement("surface");

            XmlElement xelBlockNumX = doc.CreateElement("BlockNumX");
            xelBlockNumX.InnerText = _blockNumX.ToString();
            xelsurface.AppendChild(xelBlockNumX);

            XmlElement xelBlockNumY = doc.CreateElement("BlockNumY");
            xelBlockNumY.InnerText = _blockNumY.ToString();
            xelsurface.AppendChild(xelBlockNumY);

            XmlElement xelBlockColnum = doc.CreateElement("BlockColnum");
            xelBlockColnum.InnerText = _blockColnum.ToString();
            xelsurface.AppendChild(xelBlockColnum);

            XmlElement xelBlockColor = doc.CreateElement("BlockColor");
            xelBlockColor.InnerText = _blockColor.ToArgb().ToString();
            xelsurface.AppendChild(xelBlockColor);



            root.AppendChild(xelsurface);


            doc.Save("BlockSet.xml");
        }

        


    }
}

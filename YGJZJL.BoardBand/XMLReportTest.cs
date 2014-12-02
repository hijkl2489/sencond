using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Data;
using CoreFS.CA06;

namespace YGJZJL.BoardBand
{
	class XMLReportTest
	{
		String filename = "";
		String reporttr ="";//报表文件目录
		static Excel.Application ExApp = null;


		/// <summary>构造函数</summary>
		/// <param name="filename">无后缀的报表文件名</param>
		public XMLReportTest(String filename)
		{
			this.filename = filename;
			reporttr = System.Environment.CurrentDirectory + "\\temp" + "\\" + filename;//
		}

		/// <summary>获取Excel实例</summary>
		static void GetExcelInstance()
		{
			if (ExApp==null)
			{
				ExApp = new Excel.Application();
			}
		}


		/// <summary>退出Excel</summary>
		public static void CloseExcel()
		{
			try
			{
				if (ExApp != null)
				{
					ExApp.Quit();
				}
			}
			catch (Exception ex1)
			{
				//强制把excel进程退出，关闭时，会出异常
			}
			finally
			{
				ExApp = null;
			}

		}


		/// <summary>返回Declare.xml文件</summary>
		public XmlDocument  GetXMLDeclare()
		{
			XmlDocument xmlDoc = new XmlDocument();
			String reportdeclare=reporttr+"\\Declare.xml";
			xmlDoc.Load(reportdeclare);
			return xmlDoc;
		}



		/// <summary>返回Model.xml文件</summary>
		public XmlDocument GetXMLModel()
		{
			XmlDocument xmlDoc = new XmlDocument();
			String reportdeclare = reporttr + "\\Model.xml";
			xmlDoc.Load(reportdeclare);
			return xmlDoc;
		}


		/// <summary>返回Model.xml文件</summary>
		public XmlDocument GetXMLModel(string modelFileName)
		{
			XmlDocument xmlDoc = new XmlDocument();
			String reportdeclare = reporttr + "\\" + modelFileName + ".xml";
			xmlDoc.Load(reportdeclare);
			return xmlDoc;
		}



		/// <summary>返回动态行定义的源字段配置list</summary>
		/// rowsourceindex,第几个动态行定义索引，从0开始
		public System.Collections.ArrayList GetRowSourceItem(int rowsourceindex)
		{
			System.Collections.ArrayList retrunlist = new System.Collections.ArrayList();
			XmlDocument xmlDoc = GetXMLDeclare();
			XmlNodeList sourcenodelist = xmlDoc.SelectNodes(@"Config/Body/Row/Source");
			XmlNode sourcenode = sourcenodelist[rowsourceindex];
			XmlNodeList sourceitemlist = sourcenode.SelectNodes("SourceItem");
			for (int i = 0; i < sourceitemlist.Count;i++ )
			{
				retrunlist.Add(sourceitemlist[i].InnerText.ToString().Trim());
			}
			return retrunlist;
		}


		/// <summary>返回动态行定义的源字段配置属性list</summary>
		/// rowsourceindex,第几个动态行定义索引，从0开始
		/// attributename,属性名称
		public System.Collections.ArrayList GetRowSourceAttribute(int rowsourceindex,String attributename)
		{
			System.Collections.ArrayList retrunlist = new System.Collections.ArrayList();
			XmlDocument xmlDoc = GetXMLDeclare();
			XmlNodeList sourcenodelist = xmlDoc.SelectNodes(@"Config/Body/Row/Source");
			XmlNode sourcenode = sourcenodelist[rowsourceindex];
			XmlNodeList sourceitemlist = sourcenode.SelectNodes("SourceItem");
			for (int i = 0; i < sourceitemlist.Count; i++)
			{
				XmlAttribute nodeattribute = ((XmlElement)sourceitemlist[i]).GetAttributeNode(attributename);
				retrunlist.Add(nodeattribute.Value.ToString().Trim());
			}
			return retrunlist;
		}


		/// <summary>返回报表头定义的源字段配置list</summary>
		public System.Collections.ArrayList GetHeadSourceItem()
		{
			System.Collections.ArrayList retrunlist = new System.Collections.ArrayList();
			XmlDocument xmlDoc = GetXMLDeclare();
			XmlNodeList sourcenodelist = xmlDoc.SelectNodes(@"Config/Head/Source/SourceItem");
			for (int i = 0; i < sourcenodelist.Count; i++)
			{
				retrunlist.Add(sourcenodelist[i].InnerText.ToString().Trim());
			}
			return retrunlist;
		}



		/// <summary>返回报表尾定义的源字段配置list</summary>
		public System.Collections.ArrayList GetFootSourceItem()
		{
			System.Collections.ArrayList retrunlist = new System.Collections.ArrayList();
			XmlDocument xmlDoc = GetXMLDeclare();
			XmlNodeList sourcenodelist = xmlDoc.SelectNodes(@"Config/Foot/Source/SourceItem");
			for (int i = 0; i < sourcenodelist.Count; i++)
			{
				retrunlist.Add(sourcenodelist[i].InnerText.ToString().Trim());
			}
			return retrunlist;
		}





		/// <summary>返回指定节点路径值的list</summary>
		/// XMLPath,declare 文件中的节点路径，如定义XMLpath=@"Config/Head/Source"
		public System.Collections.ArrayList GetNodeValueListByXMLPath(String XMLPath)
		{
			System.Collections.ArrayList retrunlist = new System.Collections.ArrayList();
			XmlDocument xmlDoc = GetXMLDeclare();
			XmlNodeList sourcenodelist = xmlDoc.SelectNodes(XMLPath);
			for (int i = 0; i < sourcenodelist.Count; i++)
			{
				retrunlist.Add(sourcenodelist[i].InnerText.ToString().Trim());
			}
			return retrunlist;
		}


		/// <summary>返回指定节点路径值的属性list</summary>
		/// XMLPath,declare 文件中的节点路径，如定义XMLpath=@"Config/Head/Source"
		public System.Collections.ArrayList GetNodeAttributeListByXMLPath(String XMLPath,String attributename)
		{
			System.Collections.ArrayList retrunlist = new System.Collections.ArrayList();
			XmlDocument xmlDoc = GetXMLDeclare();
			XmlNodeList sourcenodelist = xmlDoc.SelectNodes(XMLPath);
			for (int i = 0; i < sourcenodelist.Count; i++)
			{
				XmlAttribute nodeattribute = ((XmlElement)sourcenodelist[i]).GetAttributeNode(attributename);
				retrunlist.Add(nodeattribute.Value.ToString().Trim());
			}
			return retrunlist;
		}


		


		/// <summary>创建固定格式即类型为1的报表文件，对于这种报表，可以看作只有报表头的报表</summary>
		/// <param name="headobject">存有报表头数据的Object</param>
		public Boolean CreateFixXMLReportFile(Object headobject)
		{
			return CreateFixXMLReportFile("Model", headobject);
		}

		/// <summary>创建固定格式即类型为1的报表文件，对于这种报表，可以看作只有报表头的报表</summary>
		/// <param name="printModel">模板文件名</param>
		/// <param name="headobject">存有报表头数据的Object</param>
		public Boolean CreateFixXMLReportFile(string printModel,Object headobject)
		{
			Boolean succflag = false;//成功与否标志
			XmlDocument modelxmlDoc = GetXMLModel(printModel);//模板xml
			XmlDocument declarexmlDoc = GetXMLDeclare();//定义xml
			if (headobject is DataTable)
			{
				modelxmlDoc=HandleXMLReportHead(modelxmlDoc,declarexmlDoc,(DataTable)headobject);
			}
			else if (headobject is System.Collections.ArrayList)
			{
				modelxmlDoc = HandleXMLReportHead(modelxmlDoc, declarexmlDoc, (System.Collections.ArrayList)headobject);
			}
			else
			{
				throw new Exception("报表头只能接受DataTable和ArrayList形式的数据源！");
			}
			modelxmlDoc.Save(reporttr + "\\" + filename + ".xml");//生成报表文件
			succflag = true;
			return succflag;
		}





		/// <summary>导出XML报表文件</summary>
		/// <param name="headlist">存有报表头数据的Arraylist</param>
		/// <param name="columnlist">存有报表体列名数据的Arraylist</param>
		/// <param name="rowlist">存有报表体行数据的Arraylist</param>
		/// <param name="footlist">存有报表尾数据的Arraylist</param>
		public Boolean ExportXMLReportFile(System.Collections.ArrayList headlist, System.Collections.ArrayList columnlist, System.Collections.ArrayList rowlist, System.Collections.ArrayList footlist)
		{
			//此段为excel导出
			Boolean succflag = false;//成功与否标志

			XmlDocument declarexmlDoc = new XmlDocument();//Declare.xml
			declarexmlDoc = GetXMLDeclare();
			XmlNode selectNode = null;
			selectNode = declarexmlDoc.SelectSingleNode(@"Config/Body/Row/RowBeginIndex");
			int rowbeginindex = int.Parse(selectNode.InnerText.Trim());//动态行开始索引

			XmlDocument modelxmlDoc = new XmlDocument();//模板xml
			modelxmlDoc = GetXMLModel();
			XmlNodeList modellist = modelxmlDoc.GetElementsByTagName("Table");//XmlNodeList，模板xml
			selectNode = modellist.Item(0);//一定有一个，取第一个Table节点
			XmlElement tableelement = (XmlElement)selectNode;//转换成XmlElement

			XmlAttribute ExpandedRowCount = tableelement.GetAttributeNode("ss:ExpandedRowCount");//行数属性
			int modelrowcount = int.Parse(ExpandedRowCount.Value.Trim());//模板定义总行数

			int dynamicrowlistcount = rowlist.Count;//传入的动态行 list容量
			XmlNodeList modelrowlist = modelxmlDoc.GetElementsByTagName("Row");//模板定义的XML信息，所有Row节点信息
			int fixrowcount = modelrowcount - 1;//得到固定行数量，目前动态行只有一行
			//int fixrowcount = modelrowlist.Count - 1;//得到固定行数量
			int totalrowcount = fixrowcount + dynamicrowlistcount;//总行数
			ExpandedRowCount.Value = totalrowcount.ToString();//对ss:ExpandedRowCount属性赋值

			//////////////////
			XmlAttribute ExpandedColumnCount = tableelement.GetAttributeNode("ss:ExpandedColumnCount");//列数属性
			int dynamiccolumnlistcount = columnlist.Count;//传入的动态列 list容量
			int modelcolumncount = int.Parse(ExpandedColumnCount.Value.Trim());
			int totalcolumncount = modelcolumncount + dynamiccolumnlistcount;//多几列设置，没关系
			ExpandedColumnCount.Value = totalcolumncount.ToString();
			//////////////////

            

			//固定格式报表头处理
			int headlistcount = headlist.Count;//传入的headlist容量
			if (headlistcount > 0)//如果head有变量要替代
			{
				for (int i = 0; i < (rowbeginindex - 1); i++)//获取head
				{
					XmlNode fixrownode = modelrowlist[i];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到第i行的 Row节点
					XmlNodeList fixdatanode = ((XmlElement)fixrownode).GetElementsByTagName("Cell");//此处得到第i行的 Row节点的所有Data节点信息
					for (int j = 0; j < fixdatanode.Count; j++)
					{
						XmlNodeList cellnodelist = fixdatanode[j].ChildNodes;
						for (int h = 0; h < cellnodelist.Count; h++)//遍历cell节点的子节点
						{
							String value = cellnodelist[h].InnerText.Trim();//取cell节点的子节点的InnerText
							if (value.Equals("") || !value.Contains("_VAR"))//如果为空或者不含变量定义串的_VAR，直接执行下一循环
							{
								continue;
							}
							for (int k = 0; k < headlistcount; k++)
							{
								String oldstr = "HEAD_VAR(" + k.ToString()+")";
								String newstr = headlist[k].ToString();
								value = value.Replace(oldstr, newstr);//将value中的变量HEAD_VAR（k）替换成headlist第k个元素
								if (!value.Contains("_VAR"))//如果没有要替换的变量
								{
									break;
								}
							}
							cellnodelist[h].InnerText = value;//重新对处理过的Data节点的InnerText赋值
						}
					}
				}
			}

			//固定格式报表尾处理
			int footfixindex = rowbeginindex + 1;//报表尾行开始的索引
			int footlistcount = footlist.Count;//传入的footlist容量
			if (footlistcount > 0)//如果foot有变量要替代
			{
				for (int i = (footfixindex - 1); i < modelrowlist.Count; i++)//获取foot
				{
					XmlNode fixrownode = modelrowlist[i];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到第i行的 Row节点
					XmlNodeList fixdatanode = ((XmlElement)fixrownode).GetElementsByTagName("Cell");//此处得到第i行的 Row节点的所有Data节点信息
					for (int j = 0; j < fixdatanode.Count; j++)
					{
						XmlNodeList cellnodelist = fixdatanode[j].ChildNodes;
						for (int h = 0; h < cellnodelist.Count; h++)//遍历cell节点的子节点
						{
							String value = cellnodelist[h].InnerText.Trim();//取cell节点的子节点的InnerText
							if (value.Equals("") || !value.Contains("_VAR"))//如果为空或者不含变量定义串的_VAR，直接执行下一循环
							{
								continue;
							}
							for (int k = 0; k < footlistcount; k++)
							{
								String oldstr = "FOOT_VAR(" + k.ToString()+ ")";
								String newstr = footlist[k].ToString();
								value = value.Replace(oldstr, newstr);//将value中的变量HEAD_VAR（k）替换成headlist第k个元素
								if (!value.Contains("_VAR"))//如果没有要替换的变量
								{
									break;
								}
							}
							cellnodelist[h].InnerText = value;//重新对处理过的Data节点的InnerText赋值
						}
					}
				}
			}


			//报表体中的动态列列数处理
			XmlNode fixcolumnrownode = modelrowlist[rowbeginindex-2];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到动态列所在的 Row节点
			XmlNodeList columncellnodelist = ((XmlElement)fixcolumnrownode).GetElementsByTagName("Cell");//此处得到第i行的 Row节点的所有Cell节点信息
			XmlNode columncellnode = columncellnodelist.Item(0);//此处得到第i行的 Row节点的第1个Cell节点信息
            
			for (int i = 0; i < (columnlist.Count-1); i++)
			{
				XmlNode insertcolumn = columncellnode.Clone();//克隆一个同结构的动态定义列的列节点.每次插入都必须重新克隆，不然只能插入一条
				fixcolumnrownode.InsertBefore(insertcolumn, columncellnode);
			}

			//报表体中的动态行列数处理
			XmlNode fixrowrownode = modelrowlist[rowbeginindex - 1];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到动态行所在的 Row节点
			XmlNodeList rowcellnodelist = ((XmlElement)fixrowrownode).GetElementsByTagName("Cell");//此处得到第i行的 Row节点的所有Cell节点信息
			XmlNode rowcellnode = rowcellnodelist.Item(0);//此处得到第i行的 Row节点的第1个Cell节点信息
            
			for (int i = 0; i < (columnlist.Count-1); i++)
			{
				XmlNode insertrowcolumn = rowcellnode.Clone();//克隆一个同结构的动态定义行的列节点.每次插入都必须重新克隆，不然只能插入一条
				fixrowrownode.InsertBefore(insertrowcolumn, rowcellnode);
			}




			//报表体中的动态列处理
			if (columnlist.Count > 0)//如果动态列有变量要替代
			{
				XmlNodeList columndatalist = ((XmlElement)fixcolumnrownode).GetElementsByTagName("Cell");//此处得到第i行的 Row节点的所有Cell节点信息
				for (int h = 0; h < columndatalist.Count; h++)//Data节点数
				{
					XmlNodeList cellnodelist = columndatalist[h].ChildNodes;

					for (int j = 0; j < cellnodelist.Count;j++ )
					{
						String value = cellnodelist[j].InnerText.Trim();//取cell节点的子节点的InnerText
						if (value.Equals("") || !value.Contains("_VAR"))//如果为空或者不含变量定义串的_VAR，直接执行下一循环
						{
							continue;
						}
						value = columnlist[h].ToString().Trim();
						cellnodelist[j].InnerText = value;//重新对处理过的Data节点的InnerText赋值
						break;
					}
				}
			}


			//报表体中的动态行处理
			if (dynamicrowlistcount > 0)//如果动态row有变量要替代
			{
				int definerowindex = 0;
				XmlNode rootnode = modellist.Item(0);//modellist，模板xml，取第一个Table节点，其中Table为Row,Column的父节点
				for (int h = 0; h < dynamicrowlistcount; h++)//dynamicrowlistcount动态行总行数
				{
					System.Collections.ArrayList Tagdatalist = (System.Collections.ArrayList)rowlist[h];
					XmlNode fixrownode = modelrowlist[rowbeginindex - 1 + definerowindex];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到动态行定义的 Row节点
					XmlNode insertnode = fixrownode.Clone();//克隆一个同结构的动态定义Row节点
					XmlNodeList fixdatanode = ((XmlElement)insertnode).GetElementsByTagName("Cell");//克隆动态定义Row节点的全部Data节点信息
					int perdynamicrowvaluecount = fixdatanode.Count;//每行的单元格数
					for (int j = 0; j < perdynamicrowvaluecount; j++)//遍历克隆Row的Data节点信息
					{
						XmlNodeList cellnodelist = fixdatanode[j].ChildNodes;
						for (int i = 0; i < cellnodelist.Count; i++)
						{
							XmlNode aabb = cellnodelist[i];
							String value = cellnodelist[i].InnerText.Trim();//取cell节点的子节点的InnerText
							if (value.Equals("") || !value.Contains("_VAR"))//如果为空或者不含变量定义串的_VAR，直接执行下一循环
							{
								continue;
							}
							value = Tagdatalist[j].ToString().Trim();
							cellnodelist[i].InnerText = value;
							break;
						}
					}
					XmlNode refnode = fixrownode;
					rootnode.InsertBefore(insertnode, refnode);
					++definerowindex;
				}
				XmlNode removerownode = modelrowlist[rowbeginindex - 1 + definerowindex];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到动态行定义的 Row节点，i为多行动态用
				rootnode.RemoveChild(removerownode);//移除定义行
			}

			modelxmlDoc.Save(reporttr + "\\" + filename + ".xml");//生成报表文件
			return succflag;

		}





		/// <summary>创建报表体行动态报表文件</summary>
		/// <param name="head_object">存有报表头数据的object</param>
		/// <param name="row_object">存有报表体数据的object</param>
		/// <param name="foot_object">存有报表尾数据的object</param>
		public Boolean CreateXMLReportFile(Object head_object, Object row_object, Object foot_object)
		{
			Boolean succflag = false;//成功与否标志
			XmlDocument declarexmlDoc =GetXMLDeclare();//Declare.
			XmlDocument modelxmlDoc = GetXMLModel();//模板xml

			//报表头处理
			if (head_object is DataTable)
			{
				modelxmlDoc = HandleXMLReportHead(modelxmlDoc, declarexmlDoc, (DataTable)head_object);
			}
			else if (head_object is System.Collections.ArrayList)
			{
				modelxmlDoc = HandleXMLReportHead(modelxmlDoc, declarexmlDoc, (System.Collections.ArrayList)head_object);
			}
			else
			{
				throw new Exception("报表头只能接受DataTable和ArrayList形式的数据源！");
			}

			//报表尾处理
			if (foot_object is DataTable)
			{
				modelxmlDoc = HandleXMLReportFoot(modelxmlDoc, declarexmlDoc, (DataTable)foot_object);
			}
			else if (foot_object is System.Collections.ArrayList)
			{
				modelxmlDoc = HandleXMLReportFoot(modelxmlDoc, declarexmlDoc, (System.Collections.ArrayList)foot_object);
			}
			else
			{
				throw new Exception("报表尾只能接受DataTable和ArrayList形式的数据源！");
			}

			//报表体处理
			if (row_object is DataTable)
			{
				modelxmlDoc = HandleXMLReportBody(modelxmlDoc, declarexmlDoc, (DataTable)row_object);
			}
			else if (row_object is System.Collections.ArrayList)
			{
				modelxmlDoc = HandleXMLReportBody(modelxmlDoc, declarexmlDoc, (System.Collections.ArrayList)row_object);
			}
			else
			{
				throw new Exception("报表体只能接受DataTable和ArrayList形式的数据源！");
			}
			modelxmlDoc.Save(reporttr + "\\" + filename + ".xml");//生成报表文件
			succflag = true;
			return succflag;
		}


		/// <summary>处理报表头,返回处理后的报表文件，按config/head/source/sourceitem定义的字段源顺序处理</summary>
		/// <param name="modelxmlDoc">待处理的模板xml</param>
		/// <param name="declarexmlDoc">报表定义xml</param>
		/// <param name="head_datatable">存有报表头数据的DataTable</param>
		private XmlDocument HandleXMLReportHead(XmlDocument modelxmlDoc, XmlDocument declarexmlDoc, DataTable head_datatable)
		{
			XmlNode selectNode = declarexmlDoc.SelectSingleNode(@"Config/Body/Row/RowBeginIndex");
			int rowbeginindex = int.Parse(selectNode.InnerText.Trim());//动态行开始索引

			XmlNodeList modelrowlist = modelxmlDoc.GetElementsByTagName("Row");//模板定义的XML信息，所有Row节点信息
			System.Collections.ArrayList headsourcelist = GetHeadSourceItem();//得到报表头数据源字段定义
            
			//固定格式报表头处理
			int headrowcount = head_datatable.Rows.Count;//传入的head容量，只处理第1行数据
			if (headrowcount > 0)//如果head有变量要替代
			{
				for (int i = 0; i < (rowbeginindex - 1); i++)//获取head
				{
					XmlNode fixrownode = modelrowlist[i];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到第i行的 Row节点
					XmlNodeList fixdatanode = ((XmlElement)fixrownode).GetElementsByTagName("Cell");//此处得到第i行的 Row节点的所有Cell节点信息
					for (int j = 0; j < fixdatanode.Count; j++)
					{
						XmlNodeList cellnodelist = fixdatanode[j].ChildNodes;
						for (int h = 0; h < cellnodelist.Count; h++)//遍历cell节点的子节点
						{
							String value = cellnodelist[h].InnerText.Trim();//取cell节点的子节点的InnerText
							if (value.Equals("") || !value.Contains("_VAR"))//如果为空或者不含变量定义串的_VAR，直接执行下一循环
							{
								continue;
							}
							for (int g = 0; g < headsourcelist.Count; g++)
							{
								String sourceitemname = headsourcelist[g].ToString().Trim();
								String oldstr = "HEAD_VAR(" + g.ToString()+")";
								String newstr="";
								try
								{
									newstr=head_datatable.Rows[0][sourceitemname].ToString().Trim();
								}
								catch(Exception e1)//如果源中没有该字段，赋值为空
								{
									newstr = "";
								}
								value = value.Replace(oldstr, newstr);//将value中的变量HEAD_VAR（k）替换成headlist第k个元素
								if (!value.Contains("_VAR"))//如果没有要替换的变量
								{
									break;
								}
							}
							cellnodelist[h].InnerText = value;//重新对处理过的Data节点的InnerText赋值
						}
					}
				}
			}
			return modelxmlDoc;
		}


		/// <summary>处理报表头,返回处理后的报表文件,按list中的数值顺序处理</summary>
		/// <param name="modelxmlDoc">待处理的模板xml</param>
		/// <param name="declarexmlDoc">报表定义xml</param>
		/// <param name="head_arraylist">存有报表头数据的arraylist</param>
		private XmlDocument HandleXMLReportHead(XmlDocument modelxmlDoc, XmlDocument declarexmlDoc, System.Collections.ArrayList head_arraylist)
		{
			XmlNode selectNode = declarexmlDoc.SelectSingleNode(@"Config/Body/Row/RowBeginIndex");
			int rowbeginindex = int.Parse(selectNode.InnerText.Trim());//动态行开始索引

			XmlNodeList modelrowlist = modelxmlDoc.GetElementsByTagName("Row");//模板定义的XML信息，所有Row节点信息

			//固定格式报表头处理
			int headlistcount = head_arraylist.Count;//传入的head容量
			if (headlistcount > 0)//如果head有变量要替代
			{
				for (int i = 0; i < (rowbeginindex - 1); i++)//获取head
				{
					XmlNode fixrownode = modelrowlist[i];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到第i行的 Row节点
					XmlNodeList fixdatanode = ((XmlElement)fixrownode).GetElementsByTagName("Cell");//此处得到第i行的 Row节点的所有Cell节点信息
					for (int j = 0; j < fixdatanode.Count; j++)
					{
						XmlNodeList cellnodelist = fixdatanode[j].ChildNodes;
						for (int h = 0; h < cellnodelist.Count; h++)//遍历cell节点的子节点
						{
							String value = cellnodelist[h].InnerText.Trim();//取cell节点的子节点的InnerText
							if (value.Equals("") || !value.Contains("_VAR"))//如果为空或者不含变量定义串的_VAR，直接执行下一循环
							{
								continue;
							}
							for(int g=0;g<headlistcount;g++)
							{
								String oldstr = "HEAD_VAR(" + g.ToString() + ")";
								String newstr = head_arraylist[g].ToString().Trim();
								value = value.Replace(oldstr, newstr);//将value中的变量HEAD_VAR（k）替换成headlist第k个元素
								if (!value.Contains("_VAR"))//如果没有要替换的变量
								{
									break;
								}
							}
							cellnodelist[h].InnerText = value;//重新对处理过的Data节点的InnerText赋值
						}
					}
				}
			}
			return modelxmlDoc;
		}


		/// <summary>处理报表尾,返回处理后的报表文件，按config/foot/source/sourceitem定义的字段源顺序处理</summary>
		/// <param name="modelxmlDoc">待处理的模板xml</param>
		/// <param name="declarexmlDoc">报表定义xml</param>
		/// <param name="foot_datatable">存有报表尾数据的DataTable</param>
		private XmlDocument HandleXMLReportFoot(XmlDocument modelxmlDoc, XmlDocument declarexmlDoc, DataTable foot_datatable)
		{
			XmlNode selectNode = declarexmlDoc.SelectSingleNode(@"Config/Body/Row/RowBeginIndex");
			int rowbeginindex = int.Parse(selectNode.InnerText.Trim());//动态行开始索引

			XmlNodeList modelrowlist = modelxmlDoc.GetElementsByTagName("Row");//模板定义的XML信息，所有Row节点信息
			System.Collections.ArrayList footsourcelist = GetFootSourceItem();//得到报表头数据源字段定义

			//固定格式报表头处理
			int footrowcount = foot_datatable.Rows.Count;//传入的foot容量，只处理第1行数据
			if (footrowcount > 0)//如果foot有变量要替代
			{
				for (int i = rowbeginindex; i < modelrowlist.Count; i++)//获取foot,索引从0开始
				{
					XmlNode fixrownode = modelrowlist[i];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到第i行的 Row节点
					XmlNodeList fixdatanode = ((XmlElement)fixrownode).GetElementsByTagName("Cell");//此处得到第i行的 Row节点的所有Cell节点信息
					for (int j = 0; j < fixdatanode.Count; j++)
					{
						XmlNodeList cellnodelist = fixdatanode[j].ChildNodes;
						for (int h = 0; h < cellnodelist.Count; h++)//遍历cell节点的子节点
						{
							String value = cellnodelist[h].InnerText.Trim();//取cell节点的子节点的InnerText
							if (value.Equals("") || !value.Contains("_VAR"))//如果为空或者不含变量定义串的_VAR，直接执行下一循环
							{
								continue;
							}
							for (int g = 0; g < footsourcelist.Count; g++)
							{
								String sourceitemname = footsourcelist[g].ToString().Trim();
								String oldstr = "FOOT_VAR(" + g.ToString() + ")";
								String newstr = "";
								try
								{
									newstr = foot_datatable.Rows[0][sourceitemname].ToString().Trim();
								}
								catch (Exception e1)//如果源中没有该字段，赋值为空
								{
									newstr = "";
								}
								value = value.Replace(oldstr, newstr);//将value中的变量HEAD_VAR（k）替换成headlist第k个元素
								if (!value.Contains("_VAR"))//如果没有要替换的变量
								{
									break;
								}
							}
							cellnodelist[h].InnerText = value;//重新对处理过的Data节点的InnerText赋值
						}
					}
				}
			}
			return modelxmlDoc;
		}


		/// <summary>处理报表尾,返回处理后的报表文件,按list中的数值顺序处理</summary>
		/// <param name="modelxmlDoc">待处理的模板xml</param>
		/// <param name="declarexmlDoc">报表定义xml</param>
		/// <param name="foot_arraylist">存有报表尾数据的arraylist</param>
		private XmlDocument HandleXMLReportFoot(XmlDocument modelxmlDoc, XmlDocument declarexmlDoc, System.Collections.ArrayList foot_arraylist)
		{
			XmlNode selectNode = declarexmlDoc.SelectSingleNode(@"Config/Body/Row/RowBeginIndex");
			int rowbeginindex = int.Parse(selectNode.InnerText.Trim());//动态行开始索引

			XmlNodeList modelrowlist = modelxmlDoc.GetElementsByTagName("Row");//模板定义的XML信息，所有Row节点信息

			//固定格式报表头处理
			int footlistcount = foot_arraylist.Count;//传入的foot容量
			if (footlistcount > 0)//如果foot有变量要替代
			{
				for (int i = rowbeginindex; i < modelrowlist.Count; i++)//获取foot,索引从0开始
				{
					XmlNode fixrownode = modelrowlist[i];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到第i行的 Row节点
					XmlNodeList fixdatanode = ((XmlElement)fixrownode).GetElementsByTagName("Cell");//此处得到第i行的 Row节点的所有Cell节点信息
					for (int j = 0; j < fixdatanode.Count; j++)
					{
						XmlNodeList cellnodelist = fixdatanode[j].ChildNodes;
						for (int h = 0; h < cellnodelist.Count; h++)//遍历cell节点的子节点
						{
							String value = cellnodelist[h].InnerText.Trim();//取cell节点的子节点的InnerText
							if (value.Equals("") || !value.Contains("_VAR"))//如果为空或者不含变量定义串的_VAR，直接执行下一循环
							{
								continue;
							}
							for (int g = 0; g < footlistcount; g++)
							{
								String oldstr = "FOOT_VAR(" + g.ToString() + ")";
								String newstr = foot_arraylist[g].ToString().Trim();
								value = value.Replace(oldstr, newstr);//将value中的变量HEAD_VAR（k）替换成headlist第k个元素
								if (!value.Contains("_VAR"))//如果没有要替换的变量
								{
									break;
								}
							}
							cellnodelist[h].InnerText = value;//重新对处理过的Data节点的InnerText赋值
						}
					}
				}
			}
			return modelxmlDoc;
		}




		/// <summary>处理报表体,返回处理后的报表文件，按list顺序处理</summary>
		/// <param name="modelxmlDoc">待处理的模板xml</param>
		/// <param name="declarexmlDoc">报表定义xml</param>
		/// <param name="body_arraylist">存有报表体数据的arraylist</param>
		private XmlDocument HandleXMLReportBody(XmlDocument modelxmlDoc, XmlDocument declarexmlDoc, System.Collections.ArrayList body_arraylist)
		{
			XmlNode selectNode = declarexmlDoc.SelectSingleNode(@"Config/Body/Row/RowBeginIndex");
			int rowbeginindex = int.Parse(selectNode.InnerText.Trim());//动态行开始索引
			int dynamicrowlistcount = body_arraylist.Count;//传入的动态行 list容量
            
			XmlNodeList modellist = modelxmlDoc.GetElementsByTagName("Table");//XmlNodeList，模板xml
			selectNode = modellist.Item(0);//一定有一个，取第一个Table节点
			XmlElement tableelement = (XmlElement)selectNode;//转换成XmlElement
			XmlAttribute ExpandedRowCount = tableelement.GetAttributeNode("ss:ExpandedRowCount");//行数属性
			int modelrowcount = int.Parse(ExpandedRowCount.Value.Trim());//模板定义总行数
            
			XmlNodeList modelrowlist = modelxmlDoc.GetElementsByTagName("Row");//模板定义的XML信息，所有Row节点信息
			int fixrowcount = modelrowcount - 1;//得到固定行数量，目前动态行只有一行
			int totalrowcount = fixrowcount + dynamicrowlistcount;//总行数
			ExpandedRowCount.Value = totalrowcount.ToString();//对ss:ExpandedRowCount属性赋值

			//报表体中的动态行处理
			if (dynamicrowlistcount > 0)//如果动态row有变量要替代
			{
				int definerowindex = 0;
				XmlNode rootnode = modellist.Item(0);//modellist，模板xml，取第一个Table节点，其中Table为Row,Column的父节点
				for (int h = 0; h < dynamicrowlistcount; h++)//dynamicrowlistcount动态行总行数
				{
					System.Collections.ArrayList Tagdatalist = (System.Collections.ArrayList)body_arraylist[h];
					XmlNode fixrownode = modelrowlist[rowbeginindex - 1 + definerowindex];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到动态行定义的 Row节点
					XmlNode insertnode = fixrownode.Clone();//克隆一个同结构的动态定义Row节点
					XmlNodeList fixdatanode = ((XmlElement)insertnode).GetElementsByTagName("Cell");//克隆动态定义Row节点的全部Data节点信息
					int perdynamicrowvaluecount = fixdatanode.Count;//每行的单元格数
					for (int j = 0; j < perdynamicrowvaluecount; j++)//遍历克隆Row的cell节点信息
					{
						XmlNodeList cellnodelist = fixdatanode[j].ChildNodes;
						for (int i = 0; i < cellnodelist.Count; i++)//遍历克隆Row的cell节点的子节点信息
						{
							String value = cellnodelist[i].InnerText.Trim();//取cell节点的子节点的InnerText
							if (value.Equals("") || !value.Contains("_VAR"))//如果为空或者不含变量定义串的_VAR，直接执行下一循环
							{
								continue;
							}
							for (int k = 0; k < Tagdatalist.Count; k++)
							{
								String oldstr = "BODYROW_VAR(" + k.ToString() + ")";
								String newstr = Tagdatalist[k].ToString();
								value = value.Replace(oldstr, newstr);//将value中的变量HEAD_VAR（k）替换成headlist第k个元素
								if (!value.Contains("_VAR"))//如果没有要替换的变量
								{
									break;
								}
							}
							cellnodelist[i].InnerText = value;//重新对处理过的Data节点的InnerText赋值
						}
					}
					XmlNode refnode = fixrownode;

					rootnode.InsertBefore(insertnode, refnode);
					++definerowindex;
				}
				XmlNode removerownode = modelrowlist[rowbeginindex - 1 + definerowindex];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到动态行定义的 Row节点，i为多行动态用
				rootnode.RemoveChild(removerownode);//移除定义行
			}
			return modelxmlDoc;
		}




		/// <summary>处理报表体,返回处理后的报表文件，按config/body/row/source/sourceitem定义的字段源顺序处理</summary>
		/// <param name="modelxmlDoc">待处理的模板xml</param>
		/// <param name="declarexmlDoc">报表定义xml</param>
		/// <param name="body_datatable">存有报表体数据的DataTable</param>
		private XmlDocument HandleXMLReportBody(XmlDocument modelxmlDoc, XmlDocument declarexmlDoc, DataTable body_datatable)
		{
			XmlNode selectNode = declarexmlDoc.SelectSingleNode(@"Config/Body/Row/RowBeginIndex");
			int rowbeginindex = int.Parse(selectNode.InnerText.Trim());//动态行开始索引
			int dynamicrowlistcount = body_datatable.Rows.Count;//传入的动态行 list容量
			System.Collections.ArrayList rowsourceitem = GetRowSourceItem(0);//第一个动态源字段定义

			XmlNodeList modellist = modelxmlDoc.GetElementsByTagName("Table");//XmlNodeList，模板xml
			selectNode = modellist.Item(0);//一定有一个，取第一个Table节点
			XmlElement tableelement = (XmlElement)selectNode;//转换成XmlElement
			XmlAttribute ExpandedRowCount = tableelement.GetAttributeNode("ss:ExpandedRowCount");//行数属性
			int modelrowcount = int.Parse(ExpandedRowCount.Value.Trim());//模板定义总行数

			XmlNodeList modelrowlist = modelxmlDoc.GetElementsByTagName("Row");//模板定义的XML信息，所有Row节点信息
			int fixrowcount = modelrowcount - 1;//得到固定行数量，目前动态行只有一行
			int totalrowcount = fixrowcount + dynamicrowlistcount;//总行数
			ExpandedRowCount.Value = totalrowcount.ToString();//对ss:ExpandedRowCount属性赋值

			//报表体中的动态行处理
			if (dynamicrowlistcount > 0)//如果动态row有变量要替代
			{
				int definerowindex = 0;
				XmlNode rootnode = modellist.Item(0);//modellist，模板xml，取第一个Table节点，其中Table为Row,Column的父节点
				for (int h = 0; h < dynamicrowlistcount; h++)//dynamicrowlistcount动态行总行数
				{
					XmlNode fixrownode = modelrowlist[rowbeginindex - 1 + definerowindex];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到动态行定义的 Row节点
					XmlNode insertnode = fixrownode.Clone();//克隆一个同结构的动态定义Row节点
					XmlNodeList fixdatanode = ((XmlElement)insertnode).GetElementsByTagName("Cell");//克隆动态定义Row节点的全部Data节点信息
					int perdynamicrowvaluecount = fixdatanode.Count;//每行的单元格数
					for (int j = 0; j < perdynamicrowvaluecount; j++)//遍历克隆Row的cell节点信息
					{
						XmlNodeList cellnodelist = fixdatanode[j].ChildNodes;
						for (int i = 0; i < cellnodelist.Count; i++)//遍历克隆Row的cell节点的子节点信息
						{
							String value = cellnodelist[i].InnerText.Trim();//取cell节点的子节点的InnerText
							if (value.Equals("") || !value.Contains("_VAR"))//如果为空或者不含变量定义串的_VAR，直接执行下一循环
							{
								continue;
							}
							for (int k = 0; k < rowsourceitem.Count; k++)
							{
								String rowsourceitemname = rowsourceitem[k].ToString().Trim();
								String newstr = "";
								try
								{
									newstr = body_datatable.Rows[h][rowsourceitemname].ToString().Trim();
								}
								catch (Exception)
								{
									newstr = "";
								}
								String oldstr = "BODYROW_VAR(" + k.ToString()+")";
								value = value.Replace(oldstr, newstr);//将value中的变量HEAD_VAR（k）替换成headlist第k个元素
								if (!value.Contains("_VAR"))//如果没有要替换的变量
								{
									break;
								}
							}
							cellnodelist[i].InnerText = value;//重新对处理过的Data节点的InnerText赋值
						}
					}
					XmlNode refnode = fixrownode;

					rootnode.InsertBefore(insertnode, refnode);
					++definerowindex;
				}
				XmlNode removerownode = modelrowlist[rowbeginindex - 1 + definerowindex];//modelrowlist模板定义的XML信息，所有Row节点信息，此处得到动态行定义的 Row节点，i为多行动态用
				rootnode.RemoveChild(removerownode);//移除定义行
			}
			return modelxmlDoc;
		}



		/// <summary>打印XML报表文件</summary>
		public void PrintReportXMLFile()
		{
			GetExcelInstance();
			object MissingValue = Type.Missing;
			ExApp.Visible = false;
			ExApp.Application.Workbooks.Open(reporttr + "\\" + filename + ".xml",
			                                 MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue,
			                                 MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue
				);
			//((Excel.Worksheet)ExApp.ActiveWorkbook.ActiveSheet).PrintPreview(true);

			try
			{
				((Excel.Worksheet)ExApp.ActiveWorkbook.ActiveSheet).PrintOut(MissingValue, MissingValue, 1, false, "", false, false, null);
			}
			catch (Exception eex4)
			{
                MessageBox.Show("PrintReportXMLFile" + eex4.Message);
			}
            
			//ExApp.ActiveWindow.SelectedSheets.PrintOut(1, MissingValue, 1, false, "", false, false, null);
			ExApp.ActiveWorkbook.Close(false, reporttr + "\\" + filename + ".xml", null);
			//ExApp.Quit();
			//ExApp = null;

			//System.Threading.Thread.Sleep(6000);

		}


		/// <summary>打开XML报表文件</summary>
		public void OpenXMLReportFile()
		{
			GetExcelInstance();
			object MissingValue = Type.Missing;
			ExApp.Visible = true;
			ExApp.Application.Workbooks.Open(reporttr + "\\" + filename + ".xml",
			                                 MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue,
			                                 MissingValue, MissingValue, MissingValue, MissingValue, MissingValue, MissingValue
				);
		}

	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace YGJZJL.Car
{
    /// <summary>
    /// 基础表类，用于操作基础表
    /// </summary>
    class BaseData
    {
        private DataTable m_dtWL = new DataTable();  //物料表基础信息
        private DataTable m_dtFHDW = new DataTable();  //发货单位表基础信息
        private DataTable m_dtSHDW = new DataTable();  //收货单位表基础信息
        private DataTable m_dtCYDW = new DataTable();  //承运单位表基础信息

        public BaseData()
        {
            m_dtWL.Clear();
            m_dtFHDW.Clear();
            m_dtSHDW.Clear();
            m_dtCYDW.Clear();
        }
        /// <summary>
        /// 物料表
        /// </summary>
        public DataTable dtWL
        {
            get
            {
                return m_dtWL;
            }
            set
            {
                m_dtWL = value;
            }
        }
        /// <summary>
        /// 发货单位表
        /// </summary>
        public DataTable dtFHDW
        {
            get
            {
                return m_dtFHDW;
            }
            set
            {
                m_dtFHDW = value;
            }
        }
        /// <summary>
        /// 收货单位表
        /// </summary>
        public DataTable dtSHDW
        {
            get
            {
                return m_dtSHDW;
            }
            set
            {
                m_dtSHDW = value;
            }
        }
        /// <summary>
        /// 发货承运
        /// </summary>
        public DataTable dtCYDW
        {
            get
            {
                return m_dtCYDW;
            }
            set
            {
                m_dtCYDW = value;
            }
        }
    }
}

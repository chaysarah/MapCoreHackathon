using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.GUI.Forms
{
    public partial class btnScanForm : Form
    {
        IDNMcMapViewport m_CurrViewport;
        private static MCTSScanFormParams m_ScanFormParams;

        public static MCTSScanFormParams ScanFormParams { get { return m_ScanFormParams; } }
        
            
        

        public btnScanForm()
        {
            InitializeComponent();
            m_CurrViewport = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport;
            
            cmbCoordSys.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            //cmbCoordSys.Text = DNEMcPointCoordSystem._EPCS_SCREEN.ToString();

            LoadFormParams();
        }

        private void LoadFormParams()
        {
            if (m_ScanFormParams == null)
                m_ScanFormParams = new MCTSScanFormParams();
            
            if (m_ScanFormParams.ScanSQParams == null)
                this.lblSQParams.Text = "Undefined";
            else
                this.lblSQParams.Text = "Defined";

            cmbCoordSys.Text = m_ScanFormParams.EMcPointCoordSystem.ToString();
            switch (m_ScanFormParams.ScanType)
            {
                case MCTSScanFormParams.EMCTScanType.Polygon:
                    rbPolyScan.Checked = true; break;
                case MCTSScanFormParams.EMCTScanType.Rectangle:
                    rbRectScan.Checked = true; break;
                case MCTSScanFormParams.EMCTScanType.Point:
                    rbPointScan.Checked = true;
                    ctrl3DVectorManualPointScan.SetVector3D(m_ScanFormParams.SpecificPoint);
                    break;
                case MCTSScanFormParams.EMCTScanType.SpecificPoint:
                    rbManualPointScan.Checked = true;
                    ctrl3DVectorManualPointScan.SetVector3D( m_ScanFormParams.SpecificPoint);
                    break;
            }
            CompletelyInsideOnly = m_ScanFormParams.ReturnCompletelyInsideOnly;
            Tolerance = m_ScanFormParams.PointScanTolerance;

            chxGetScanExtendedDataAsync.Checked = m_ScanFormParams.IsAsync;
        }

        private void btnScanTypeOK_Click(object sender, EventArgs e)
        {

            MCTSScanFormParams.EMCTScanType scanType= MCTSScanFormParams.EMCTScanType.Point;
            DNEMcPointCoordSystem coordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbCoordSys.Text);
            if (rbPointScan.Checked)
            {
                MCTester.ObjectWorld.ScanPointGeometry pointScan = new MCTester.ObjectWorld.ScanPointGeometry(
                    coordSys, 
                    m_ScanFormParams.ScanSQParams, 
                    CompletelyInsideOnly, 
                    Tolerance, new DNSMcVector3D());

                pointScan.IsGetScanExtendedDataAsync = chxGetScanExtendedDataAsync.Checked;

                pointScan.StartPointScan();
            }
            if (rbManualPointScan.Checked)
            {
                scanType = MCTSScanFormParams.EMCTScanType.SpecificPoint;
                MCTester.ObjectWorld.ScanPointGeometry pointScan = new MCTester.ObjectWorld.ScanPointGeometry(
                    coordSys, 
                    m_ScanFormParams.ScanSQParams, 
                    CompletelyInsideOnly, 
                    Tolerance, 
                    ctrl3DVectorManualPointScan.GetVector3D());

                pointScan.IsGetScanExtendedDataAsync = chxGetScanExtendedDataAsync.Checked;

                pointScan.StartManualPointScan();
            }
            if (rbPolyScan.Checked)
            {
                scanType = MCTSScanFormParams.EMCTScanType.Polygon;
                MCTester.ObjectWorld.ScanPolygonGeometry polyScan = new MCTester.ObjectWorld.ScanPolygonGeometry(
                    coordSys, 
                    m_ScanFormParams.ScanSQParams, 
                    CompletelyInsideOnly, 
                    Tolerance);

                polyScan.IsGetScanExtendedDataAsync = chxGetScanExtendedDataAsync.Checked;

                polyScan.StartPolyScan();
            }
            if (rbRectScan.Checked)
            {
                scanType = MCTSScanFormParams.EMCTScanType.Rectangle;
                MCTester.ObjectWorld.ScanBoxGeometry BoxScan = new MCTester.ObjectWorld.ScanBoxGeometry(
                    coordSys, 
                    m_ScanFormParams.ScanSQParams, 
                    CompletelyInsideOnly, 
                    Tolerance);

                BoxScan.IsGetScanExtendedDataAsync = chxGetScanExtendedDataAsync.Checked;

                BoxScan.StartRectScan();
            }

            m_ScanFormParams.ScanType = scanType;
            m_ScanFormParams.EMcPointCoordSystem = coordSys;
            m_ScanFormParams.ReturnCompletelyInsideOnly = CompletelyInsideOnly;
            m_ScanFormParams.PointScanTolerance = Tolerance;
            m_ScanFormParams.IsAsync = chxGetScanExtendedDataAsync.Checked;
            this.Close();
        }

        private void btnScanSQParams_Click(object sender, EventArgs e)
        {
            if (m_ScanFormParams.ScanSQParams == null)
                m_ScanFormParams.ScanSQParams = new DNSQueryParams();
            MCTester.MapWorld.ScanSQParamsForm ScanSQP = new MCTester.MapWorld.ScanSQParamsForm(m_ScanFormParams.ScanSQParams);
            if (ScanSQP.ShowDialog() == DialogResult.OK)
            {
                m_ScanFormParams.ScanSQParams = ScanSQP.CurrSQParams;
                this.lblSQParams.Text = "Defined";
            }
        }

        public bool CompletelyInsideOnly
        {
            get { return chxCompletelyInsideOnly.Checked; }
            set { chxCompletelyInsideOnly.Checked = value; }
        }

        public float Tolerance
        {
            get { return ntxTolerance.GetFloat(); }
            set { ntxTolerance.SetFloat(value); }
        }

        private void btnRestoreDefualtValues_Click(object sender, EventArgs e)
        {
            m_ScanFormParams = new MCTSScanFormParams();
            LoadFormParams();
        }
    }


    public class MCTSScanFormParams
    {
        public enum EMCTScanType { Polygon, Rectangle, Point , SpecificPoint};

        private EMCTScanType m_ScanType;
        public EMCTScanType ScanType
        {
            get { return m_ScanType; }
            set { m_ScanType = value; }
        }
        
        private DNSMcVector3D m_SpecificPoint;
        public MapCore.DNSMcVector3D SpecificPoint
        {
            get { return m_SpecificPoint; }
            set { m_SpecificPoint = value; }
        }

        private DNEMcPointCoordSystem m_EMcPointCoordSystem;
        public MapCore.DNEMcPointCoordSystem EMcPointCoordSystem
        {
            get { return m_EMcPointCoordSystem; }
            set { m_EMcPointCoordSystem = value; }
        }
        
        private bool m_bReturnCompletelyInsideOnly;
        public bool ReturnCompletelyInsideOnly
        {
            get { return m_bReturnCompletelyInsideOnly; }
            set { m_bReturnCompletelyInsideOnly = value; }
        }
        
        private float m_fPointScanTolerance;
        public float PointScanTolerance
        {
            get { return m_fPointScanTolerance; }
            set { m_fPointScanTolerance = value; }
        }

        private DNSQueryParams m_ScanSQParams;
        public MapCore.DNSQueryParams ScanSQParams
        {
            get { return m_ScanSQParams; }
            set { m_ScanSQParams = value; }
        }

        private bool m_IsAsync;
        public bool IsAsync
        {
            get { return m_IsAsync; }
            set { m_IsAsync = value; }
        }

        public MCTSScanFormParams()
        {
            m_ScanType = EMCTScanType.Point;
            m_EMcPointCoordSystem = DNEMcPointCoordSystem._EPCS_SCREEN;
            m_bReturnCompletelyInsideOnly = false;
            m_fPointScanTolerance = 0;
            m_ScanSQParams = null;
            m_IsAsync = false;
        }
    }
}
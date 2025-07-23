using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.IO;
using MCTester.GUI.Map;
using MCTester.Managers.MapWorld;

namespace MCTester.General_Forms
{
    public partial class frmQuaryDemonstration : Form
    {
        private IDNMcOverlay activeOverlay;
        private StreamReader STR;
        //private IDNMcTexture m_DefaultTexture;
        private List<string> lSourceLines;
        private string[] sourceLineValues;
        private string path;
        private IDNMcObjectSchemeItem m_ObjSchemeItem;
        private DNSMcBColor m_ItemColor= new DNSMcBColor (255, 255, 0, 255);
        private DNEFillStyle m_FillStyle = DNEFillStyle._EFS_SOLID;
        private DNELineStyle m_LineStyle = DNELineStyle._ELS_SOLID;
        //private EDrawnItemType m_ItemType;
        //private DNMcTextItem m_DefaultText;
        //private IDNMcVectorMapLayer m_CurrentObject;
        //IDNMcIconHandleTexture m_iconTexture;
        //DNMcImageFileTexture m_filetexture;
      



        public frmQuaryDemonstration()
        {
            InitializeComponent();
            activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;

        }

        private void btnGasInTLV_Click(object sender, EventArgs e)
        {
            IDNMcMapProduction mapProduction = DNMcMapProduction.Create();
             try
             {
                //Draw Polygon of City
                //path = "C:\\Query\\telaviv-polygon.txt";
                //STR = new StreamReader(path);
                //while (!STR.EndOfStream)
                //{
                //    string line = STR.ReadLine();
                //    sourceLineValues = line.Split(',');

                //}
                //m_ItemColor = new DNSMcBColor(255, 255, 0, 150);
                //m_ObjSchemeItem = (DNMcObjectSchemeItem)DNMcPolygonItem.Create(DNEItemSubTypeFlags._EISTF_WORLD | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                //                                                                            m_LineStyle,
                //                                                                            m_ItemColor,
                //                                                                            2,
                //                                                                            null,
                //                                                                            new DNSMcFVector2D(1, 1),
                //                                                                            1f,m_FillStyle);


                //int count=sourceLineValues.Length/2;
                //int w=0;                                                              

                //DNSMcVector3D [] polyPt= new DNSMcVector3D [count];
                //for (int i = 0; i < sourceLineValues.Length-1; i += 2)
                //{
                //    polyPt[w].x = double.Parse(sourceLineValues[i]);
                //    polyPt[w].y = double.Parse(sourceLineValues[i + 1]);
                //    polyPt[w].z = 0;
                //    w++;
                //}
                //IDNMcObject obj = DNMcObject.Create(activeOverlay,
                //                      m_ObjSchemeItem,
                //                      DNEMcPointCoordSystem._EPCS_WORLD,
                //                      polyPt,
                //                      false); 

                 //**************load polygon from file***************
                 activeOverlay.LoadObjects("C:\\Query\\PolygonCarmel.m");
                 //************** Add gas in Carmel*******************
                 try
                 {
                     path = "C:\\Query\\GasInHaifa.txt";
                     STR = new StreamReader(path);
                     lSourceLines = new List<string>();
                     while (!STR.EndOfStream)
                     {
                         string line = STR.ReadLine();
                         lSourceLines.Add(line.TrimEnd(','));
                     }
                     for (int i = 0; i < lSourceLines.Count; i++)
                     {

                         string points = lSourceLines[i];
                         sourceLineValues = points.Split(',');
                         DNSMcVector3D[] pointPt = new DNSMcVector3D[1];
                         pointPt[0].x = double.Parse(sourceLineValues[0]);
                         pointPt[0].y = double.Parse(sourceLineValues[1]);
                         pointPt[0].z = 0;
                         float radius = 150;
                         m_ObjSchemeItem = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                                     DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                                     DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER,
                                                                                                     (float)radius,
                                                                                                     (float)radius,
                                                                                                     0,
                                                                                                     360,
                                                                                                     0,
                                                                                                     m_LineStyle,
                                                                                                     m_ItemColor = new DNSMcBColor(0, 255, 0, 190),
                                                                                                     2,
                                                                                                     null,
                                                                                                     new DNSMcFVector2D(1, 1),
                                                                                                     2,
                                                                                                     m_FillStyle,
                                                                                                     m_ItemColor);


                         ((IDNMcEllipseItem)m_ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                         IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                    m_ObjSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                   pointPt,
                                                   false);
                         this.Close();
                     }


                 }
                 catch (MapCoreException McEx)
                 {
                     MapCore.Common.Utilities.ShowErrorMessage("", McEx);
                 }
                 
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("", McEx);
               }

                //****************second option of drawing polygon by id*******************
                //try
                //{
                //    IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                //    uint[] featuresId = new uint[1]{1846};
                //    string path = "C:\\Maps\\Vectory\\Normal\\is_cty_AREAS";//Path of Layer
                //    Dictionary<object, uint> currViewports = MCTester.Managers.MapWorld.Manager_MCViewports.AllParams;
                //    foreach (object keyViewport in currViewports.Keys)
                //    {
                //        mapProduction.SetUpdateNativeVectorLayerParam((IDNMcMapViewport)keyViewport, path);
                //    }
                //    IDNMcObject newObject = null;
                //    foreach (uint id in featuresId)
                //    {
                        
                //        mapProduction.GetVectorObjectByID(id, out newObject);
                //        newObject = newObject.Clone(activeOverlay, true);
                //        newObject.SetVisibilityOption(DNEActionOptions._EAO_USE_SELECTOR);
                //    }

                //}
                //catch (MapCoreException McEx)
                //{
                //    MapCore.Common.Utilities.ShowErrorMessage("GetVectorObjectByID", McEx);
                //}
            //****************ADD GAS STATIONS**************

                //try
                //{
                //    uint[] featuresIdGas = new uint[0];
                //    string pathGas = "C:\\Maps\\Vectory\\Normal\\is_gas_POINTS";//Path of Layer
                //    Dictionary<object, uint> currViewportsGas = MCTester.Managers.MapWorld.Manager_MCViewports.AllParams;
                //    foreach (object keyViewport in currViewportsGas.Keys)
                //    {
                //        mapProduction.SetUpdateNativeVectorLayerParam((IDNMcMapViewport)keyViewport, pathGas);
                //    }
                //    IDNMcObject newObjectGas = null;
                //    foreach (uint id in featuresIdGas)
                //    {
                        
                //        mapProduction.GetVectorObjectByID(id, out newObjectGas);
                //        newObjectGas = newObjectGas.Clone(activeOverlay, true);
                //        newObjectGas.SetVisibilityOption(DNEActionOptions._EAO_USE_SELECTOR);
                //    }

                //}
                //catch (MapCoreException McEx)
                //{
                //    MapCore.Common.Utilities.ShowErrorMessage("GetVectorObjectByID", McEx);
                //}
                //this.Close();

                
            }

        private void btnGasInSight_Click(object sender, EventArgs e)
        {
           
            //**************load Ellipse Sight Area from file***************
            activeOverlay.LoadObjects("C:\\Query\\EllipseSIGHTp.m");
            //**************Load Gas Station in Sight***********************
             try
                 {
                     path = "C:\\Query\\GasInHaifa_SightPresentation.txt";
                     STR = new StreamReader(path);
                     lSourceLines = new List<string>();
                     while (!STR.EndOfStream)
                     {
                         string line = STR.ReadLine();
                         lSourceLines.Add(line.TrimEnd(','));
                     }
                     for (int i = 0; i < lSourceLines.Count; i++)
                     {

                         string points = lSourceLines[i];
                         sourceLineValues = points.Split(',');
                         DNSMcVector3D[] pointPt = new DNSMcVector3D[1];
                         pointPt[0].x = double.Parse(sourceLineValues[0]);
                         pointPt[0].y = double.Parse(sourceLineValues[1]);
                         pointPt[0].z = double.Parse(sourceLineValues[2]);

                         float radius = 100;
                         m_ObjSchemeItem = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                                                  DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                                                  DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER,
                                                                                                                  (float)radius,
                                                                                                                  (float)radius,
                                                                                                                  0,
                                                                                                                  360,
                                                                                                                  0,
                                                                                                                  m_LineStyle,
                                                                                                                  m_ItemColor = new DNSMcBColor(0, 0, 255, 190),
                                                                                                                  2,
                                                                                                                  null,
                                                                                                                  new DNSMcFVector2D(1, 1),
                                                                                                                  2,
                                                                                                                  m_FillStyle = DNEFillStyle._EFS_CROSS,
                                                                                                                  m_ItemColor);


                         ((IDNMcEllipseItem)m_ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                         IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                    m_ObjSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                   pointPt,
                                                   false);
                     }
                         this.Close();
                     }
                 catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("", McEx);
                    }
        
        }

        private void btnGasOnRoad_Click(object sender, EventArgs e)
        {
            try
            {
                //*************ADD Road**************
                path = "C:\\Query\\road3.txt";
                STR = new StreamReader(path);
                lSourceLines = new List<string>();
                
                while (!STR.EndOfStream)
                {
                    string line = STR.ReadLine();
                    lSourceLines.Add(line.TrimEnd(','));
                }
                
                for (int i = 0; i < lSourceLines.Count; i++)
                {
                    int w = 0;
                    string line = lSourceLines[i];
                    sourceLineValues = line.Split(',');
                    DNSMcVector3D[] linePt = new DNSMcVector3D[(sourceLineValues.Length)/2];
                    for (int z = 0; z < sourceLineValues.Length - 1; z += 2)
                    {
                        linePt[w].x = double.Parse(sourceLineValues[z]);
                        linePt[w].y = double.Parse(sourceLineValues[z + 1]);
                        linePt[w].z = 0;
                        w++;
                    }
                    m_ObjSchemeItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                                m_LineStyle,
                                                                                                m_ItemColor,
                                                                                                2,
                                                                                                null,
                                                                                                new DNSMcFVector2D(1, 1),
                                                                                                1f);

                    ((IDNMcLineItem)m_ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                          m_ObjSchemeItem,
                                          DNEMcPointCoordSystem._EPCS_WORLD,
                                         linePt,
                                          false);  
                }
               
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("", McEx);
            }
            //**************** Add gas station On Road 2*****************
            try
            {
                path = "C:\\Query\\GAS_ON_ROAD2.txt";
                STR = new StreamReader(path);
                lSourceLines = new List<string>();
                while (!STR.EndOfStream)
                {
                    string line = STR.ReadLine();
                    lSourceLines.Add(line.TrimEnd(','));
                }
                for (int i = 0; i < lSourceLines.Count;i++ )
                {
                   
                    string points = lSourceLines[i];
                    sourceLineValues = points.Split(',');
                    DNSMcVector3D[] pointPt = new DNSMcVector3D[1];
                    pointPt[0].x=double.Parse(sourceLineValues[0]);
                    pointPt[0].y = double.Parse(sourceLineValues[1]);
                    pointPt[0].z = 0;
                    float radius = 150;
                    m_ObjSchemeItem = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                                DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER,
                                                                                                (float)radius,
                                                                                                (float)radius,
                                                                                                0,
                                                                                                360,
                                                                                                0,
                                                                                                m_LineStyle,
                                                                                                m_ItemColor = new DNSMcBColor(0,255, 0, 190),
                                                                                                2,
                                                                                                null,
                                                                                                new DNSMcFVector2D(1, 1),
                                                                                                2,
                                                                                                m_FillStyle,
                                                                                                m_ItemColor);


                    ((IDNMcEllipseItem)m_ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false); 
                    IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                               m_ObjSchemeItem,
                                               DNEMcPointCoordSystem._EPCS_WORLD,
                                              pointPt,
                                              false);
                }
                this.Close();

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("", McEx);
            }
        }
        }
    }

        


    


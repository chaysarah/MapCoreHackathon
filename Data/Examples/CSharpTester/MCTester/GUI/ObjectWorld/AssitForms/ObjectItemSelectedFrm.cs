using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;
using MCTester.Managers.ObjectWorld;
using MCTester.GUI.Map;
using MCTester.Managers.MapWorld;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class ObjectItemSelectedFrm : Form
    {
        private IDNMcObjectSchemeItem m_SelectedItem;
        private IDNMcObject m_SelectedObject;
        private bool m_IsItemSelectedDefualt;
        private string m_ActionType;
        private int m_SelectedObjectIndex;
        private int m_SelectedItemIndex;

        private List<IDNMcObjectSchemeNode> m_itemsList;
        private List<IDNMcObject> m_ObjectsList;

        private IDNMcObject[] m_ObjectsArr;
        private IDNMcObjectSchemeNode[] m_ItemsArr;

        private string DefaultText = "Default: ";

        public ObjectItemSelectedFrm(string actionType)
        {
            InitializeComponent();
            loadObjectList();
            m_ActionType = actionType;
        }

        private void loadObjectList()
        {
            m_ObjectsList = new List<IDNMcObject>();

            IDNMcOverlayManager activeOM = Manager_MCOverlayManager.ActiveOverlayManager;

            IDNMcOverlay[] overlays = activeOM.GetOverlays();

            foreach (IDNMcOverlay overlay in overlays)
            {
                IDNMcObject[] objectsInOverlay = overlay.GetObjects();
                foreach (IDNMcObject obj in objectsInOverlay)
                {
                    string name = Manager_MCNames.GetNameByObject(obj);
                    lstObject.Items.Add(name);
                    m_ObjectsList.Add(obj);
                }
            }
            m_ObjectsArr = m_ObjectsList.ToArray();
            if (m_ObjectsArr.Length == 1)
                lstObject.SelectedIndex = 0;
        }

        private void GetSelectedObjectAndItem(bool isCheckItem = true)
        {
            m_SelectedObject = null;
            m_SelectedItem = null;
            
            m_SelectedObjectIndex = lstObject.SelectedIndex;
            if (isCheckItem && lstItems != null)
            {
                m_SelectedItemIndex = lstItems.SelectedIndex;
                if (m_SelectedItemIndex != -1)
                {
                    m_IsItemSelectedDefualt = lstItems.Items[m_SelectedItemIndex].ToString().StartsWith(DefaultText);
                }
            }
        }

        private void btnOKItemList_Click(object sender, EventArgs e)
        { 
            GetSelectedObjectAndItem();

            if (btnOKPlay.Text == "OK")
            {
                if (m_SelectedObjectIndex >= 0 && m_SelectedItemIndex >= 0)
                {
                   
                    switch (m_ActionType)
                    {
                        
                        case "Edit":
                            try
                            {
                                MCTMapFormManager.MapForm.EditModeManagerCallback.EditItemResultsEvent += new EditItemResultsEventArgs(EditSymbology);
                                if (m_IsItemSelectedDefualt)
                                    MCTMapFormManager.MapForm.EditMode.StartEditObject(SelectedObject, null, EditModePropertiesBase.EnableAddingNewPointsForMultiPointItem);
                                else
                                    MCTMapFormManager.MapForm.EditMode.StartEditObject(SelectedObject, SelectedItem, EditModePropertiesBase.EnableAddingNewPointsForMultiPointItem);

                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("StartEditObject", McEx);
                                return;
                            }
                            
                            break;
                        case "Init":
                            try
                            {
                                MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitSymbology);

                                if (m_IsItemSelectedDefualt)
                                    MCTMapFormManager.MapForm.EditMode.StartInitObject(SelectedObject,null, EditModePropertiesBase.EnableDistanceDirectionMeasureForMultiPointItem);
                                else
                                    MCTMapFormManager.MapForm.EditMode.StartInitObject(SelectedObject, SelectedItem, EditModePropertiesBase.EnableDistanceDirectionMeasureForMultiPointItem);
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("StartInitObject", McEx);
                                return;
                            }
                            break;                        
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    MessageBox.Show("You must choose both, Object and an Item", "Invalid chosen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
                currentPath = (System.IO.Path.GetDirectoryName(currentPath) + @"\PathAnimation.csv");

                if (System.IO.File.Exists(currentPath) == true)
                {
                    List<DNSPathAnimationNode> PathAnimationNode = new List<DNSPathAnimationNode>();

                    bool IsLoop = true;
                    bool AutomaticRotation = true;
                    float RotationAdditionalYaw = 0;
                    float StartingTimePoint = 0;
                    DNEPositionInterpolationMode PositionInterpolationMode = DNEPositionInterpolationMode._EPIM_LINEAR;
                    DNERotationInterpolationMode RotationInterpolationMode = DNERotationInterpolationMode._ERIM_LINEAR;
                    System.IO.StreamReader StreamReader = new System.IO.StreamReader(currentPath);

                    while (!StreamReader.EndOfStream)
                    {
                        string currLine = StreamReader.ReadLine();
                        string[] values = currLine.Split(',');

                        IsLoop = bool.Parse(values[0]);
                        AutomaticRotation = bool.Parse(values[1]);
                        RotationAdditionalYaw = float.Parse(values[2]);
                        StartingTimePoint = float.Parse(values[3]);
                        PositionInterpolationMode = (DNEPositionInterpolationMode)Enum.Parse(typeof(DNEPositionInterpolationMode), values[4]);
                        RotationInterpolationMode = (DNERotationInterpolationMode)Enum.Parse(typeof(DNERotationInterpolationMode), values[5]);

                        DNSPathAnimationNode node = new DNSPathAnimationNode();

                        node.Position.x = float.Parse(values[6]);
                        node.Position.y = float.Parse(values[7]);
                        node.Position.z = float.Parse(values[8]);
                        node.fTime = float.Parse(values[9]);
                        node.ManualRotation.fYaw = float.Parse(values[10]);
                        node.ManualRotation.fPitch = float.Parse(values[11]);
                        node.ManualRotation.fRoll = float.Parse(values[12]);
                        node.ManualRotation.bRelativeToCurrOrientation = bool.Parse(values[13]);

                        PathAnimationNode.Add(node);
                    }

                    if (chxAnimatedAll.Checked == true)
                    {
                        for (int i = 0; i < m_ObjectsArr.Length; i++)
                        {
                            IDNMcObject selectedObject = m_ObjectsArr[i];
                            IDNMcObjectScheme scheme = selectedObject.GetScheme();
                            IDNMcObjectSchemeItem item = (IDNMcObjectSchemeItem)scheme.GetNodes(DNENodeKindFlags._ENKF_ANY_ITEM)[0];
                            if (item != null)
                            {
                                try
                                {
                                    scheme.SetObjectRotationItem(item);
                                }
                                catch (MapCoreException McEx)
                                {
                                    MapCore.Common.Utilities.ShowErrorMessage("SetObjectRotationItem", McEx);
                                }

                                try
                                {
                                    selectedObject.PlayPathAnimation(PathAnimationNode.ToArray(),
                                                                            PositionInterpolationMode,
                                                                            RotationInterpolationMode,
                                                                            StartingTimePoint,
                                                                            RotationAdditionalYaw,
                                                                            AutomaticRotation,
                                                                            IsLoop);

                                }
                                catch (MapCoreException McEx)
                                {
                                    MapCore.Common.Utilities.ShowErrorMessage("PlayPathAnimation", McEx);
                                }
                            }
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    else
                    {
                        if (lstObject.SelectedItem != null && lstItems.SelectedItem != null)
                        {
                            IDNMcObjectScheme scheme = SelectedObject.GetScheme();
                            try
                            {
                                scheme.SetObjectRotationItem(SelectedItem);
                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("SetObjectRotationItem", McEx);
                            }

                            try
                            {
                                SelectedObject.PlayPathAnimation(PathAnimationNode.ToArray(),
                                                                        PositionInterpolationMode,
                                                                        RotationInterpolationMode,
                                                                        StartingTimePoint,
                                                                        RotationAdditionalYaw,
                                                                        AutomaticRotation,
                                                                        IsLoop);

                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("PlayPathAnimation", McEx);
                            }

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            MessageBox.Show("You must choose both, Object and an Item", "Invalid chosen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }                    
                }
                else
                {
                    MessageBox.Show("Invalid path: " + currentPath, "Invalid chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }

        private void EditSymbology(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
            MCTMapFormManager.MapForm.EditModeManagerCallback.EditItemResultsEvent -= new EditItemResultsEventArgs(EditSymbology);
            if (nExitCode == 1)
            {
                if (Manager_MCTSymbology.IsExistAnchorPoints(pObject))
                {
                    Manager_MCTSymbology.RemoveTempAnchorPoints(pObject);
                    Manager_MCTSymbology.ShowAnchorPoints(pObject);
                }
            }
        }

        private void InitSymbology(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
            MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new InitItemResultsEventArgs(InitSymbology);
            if (nExitCode == 1)
            {
                if (Manager_MCTSymbology.IsExistAnchorPoints(pObject))
                {
                    Manager_MCTSymbology.RemoveTempAnchorPoints(pObject);
                    Manager_MCTSymbology.ShowAnchorPoints(pObject);
                }
            }
        }

        private void btnAnimationStop_Click(object sender, EventArgs e)
        {
            GetSelectedObjectAndItem(false);

            if (chxAnimatedAll.Checked == true)
            {
                for (int i = 0; i < lstObject.Items.Count; i++)
                {
                    try
                    {
                        SelectedObject.StopPathAnimation();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("StopPathAnimation", McEx);
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                if (lstObject.SelectedItem != null)
                {
                    try
                    {
                        SelectedObject.StopPathAnimation();
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("StopPathAnimation", McEx);
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    MessageBox.Show("You must choose both, Object and an Item", "Invalid chosen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public IDNMcObjectSchemeItem SelectedItem
        {
            get
            {
                if (m_SelectedItem == null)
                {
                    m_SelectedItem = (IDNMcObjectSchemeItem)m_ItemsArr[m_SelectedItemIndex];
                }
                return m_SelectedItem;
            }
            set
            {
                if (value != null && m_itemsList != null)
                {
                    for (int i = 1; i < m_itemsList.Count; i++)
                    {
                        if (m_itemsList[i] == value)
                        {
                            m_SelectedItemIndex = i;
                            lstItems.SelectedIndex = m_SelectedItemIndex;
                            m_SelectedItem = value;
                        }
                    }
                }
                else
                    lstItems.ClearSelected();
            }
        }

        public IDNMcObject SelectedObject
        {
            get
            {
                if (m_SelectedObject == null)
                {
                    m_SelectedObject = m_ObjectsArr[m_SelectedObjectIndex];
                }
                return m_SelectedObject;
            }
            set
            {
                if (value != null)
                {
                    m_SelectedObjectIndex = m_ObjectsList.IndexOf(value);
                    m_SelectedObject = value;
                    lstObject.SelectedIndex = m_SelectedObjectIndex;
                }
                else
                    lstObject.ClearSelected();
            }
        }

        private void lstObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                m_itemsList = new List<IDNMcObjectSchemeNode>();

                GetSelectedObjectAndItem(false);

                if (m_SelectedObjectIndex < 0)
                    return;

                IDNMcObjectScheme objScheme = SelectedObject.GetScheme();
                IDNMcObjectSchemeItem defualtItem = objScheme.GetEditModeDefaultItem();
                IDNMcObjectSchemeNode[] itemsInScheme = objScheme.GetNodes(DNENodeKindFlags._ENKF_ANY_ITEM);

                lstItems.Items.Clear();
                if (itemsInScheme.Length > 0)
                {
                    if (defualtItem != null)
                    {
                        lstItems.Items.Add(DefaultText + Manager_MCNames.GetNameByObject(defualtItem));
                        m_itemsList.Add(defualtItem);
                    }
                    else
                    {
                        lstItems.Items.Add(DefaultText + "None");
                        m_itemsList.Add(null);
                    }
                    foreach (IDNMcObjectSchemeNode item in itemsInScheme)
                    {
                        lstItems.Items.Add(Manager_MCNames.GetNameByObject(item));
                        m_itemsList.Add(item);
                    }
                    m_ItemsArr = m_itemsList.ToArray();
                    if(defualtItem == null)
                        lstItems.SelectedIndex = 1;
                    else
                        lstItems.SelectedIndex = 0;
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetScheme()/GetNodes()", McEx);
            }
        }

        private void btnSelectByScaning_Click(object sender, EventArgs e)
        {
            this.Hide();
            MCTMapForm.OnMapClicked += new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
        }

        void MCTMapForm_OnMapClicked(Point PointOnMap, DNSMcVector3D PointIn3D, DNSMcVector3D PointInImage, bool IsHasIntersection)
        {
            Point selectedPoint = PointOnMap;
            DNSMcVector3D pointItem = new DNSMcVector3D((Double)selectedPoint.X, (Double)selectedPoint.Y, 0);
            
            DNSMcScanPointGeometry scanPointGeometry = new DNSMcScanPointGeometry(DNEMcPointCoordSystem._EPCS_SCREEN, pointItem, 0);

            DNSQueryParams queryParams = new DNSQueryParams();
            queryParams.eTargetsBitMask = DNEIntersectionTargetType._EITT_OVERLAY_MANAGER_OBJECT;
            queryParams.eTerrainPrecision = DNEQueryPrecision._EQP_DEFAULT;
            queryParams.uItemTypeFlagsBitField = 0;

            IDNMcMapViewport currViewport = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport;
            DNSTargetFound[] TargetFounds = currViewport.ScanInGeometry(scanPointGeometry, false, queryParams);

            if (TargetFounds.Length > 0)
            {
                this.Show();

                MCTMapForm.OnMapClicked -= new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
                List<DNSTargetFound> lst = new List<DNSTargetFound>(TargetFounds);

                DNSTargetFound targetFound = lst.Find(x => x.ObjectItemData.pItem != null && x.ObjectItemData.pItem.GetParents() != null && x.ObjectItemData.pItem.GetParents().Length > 0 &&  x.ObjectItemData.pItem.GetParents()[0] is IDNMcObjectLocation);
                if (targetFound.ObjectItemData.pObject == null || targetFound.ObjectItemData.pItem == null)
                {
                    foreach(DNSTargetFound findTargetFound in TargetFounds)
                    {
                        IDNMcObjectSchemeNode item = null;
                        IDNMcObjectSchemeNode[] nodes = findTargetFound.ObjectItemData.pObject.GetScheme().GetNodes(DNENodeKindFlags._ENKF_SYMBOLIC_ITEM);
                        foreach(IDNMcObjectSchemeNode node in nodes)
                        {
                            IDNMcObjectSchemeNode[] parents = node.GetParents();
                            if (parents != null && parents.Length > 0)
                            {
                                List<IDNMcObjectSchemeNode> objectSchemeNodes = new List<IDNMcObjectSchemeNode>(parents);
                                item = objectSchemeNodes.Find(x => x is IDNMcObjectLocation);
                                if (item != null)
                                {
                                    SelectedItem = node as IDNMcObjectSchemeItem;
                                    SelectedObject = findTargetFound.ObjectItemData.pObject;
                                    break;
                                }
                            }
                        }
                       
                       // IDNMcObjectSchemeNode item = (new List<IDNMcObjectSchemeNode>(nodes)).Find(x => x.GetParents() != null && x.GetParents().Length > 0 && (new List<IDNMcObjectSchemeNode>(x.GetParents())).Find(y => y is IDNMcObjectLocation));
                    }
                }
                else
                {
                    SelectedObject = targetFound.ObjectItemData.pObject;
                    SelectedItem = targetFound.ObjectItemData.pItem;
                }
            }
        }
    }
}
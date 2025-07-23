using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using System.Linq;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MCTester.GUI.Map;
using MCTester.Controls;
using System.Windows.Forms;
using System.IO;
using MapCore.Common;

namespace MCTester.Managers.ObjectWorld
{
    public static class Manager_MCObjectSchemeItem
    {
        static Dictionary<object,uint> dObjectSchemeItem;

        static Manager_MCObjectSchemeItem()
        {
            dObjectSchemeItem = new Dictionary<object, uint>();
        }

        public static void AddNewItem(IDNMcObjectSchemeItem item)
        {
            if (item != null)
            {
                if (!dObjectSchemeItem.ContainsKey(item))
                {
                    dObjectSchemeItem.Add(item, item.GetID());
                }
            }
        }

        public static void RemoveItem(IDNMcObjectSchemeItem item)
        {
            if (item != null)
                dObjectSchemeItem.Remove(item);
        }

        public static void SetStandaloneItemID(IDNMcObjectSchemeItem item, uint newID)
        {
            uint currID = item.GetID();
            dObjectSchemeItem[item] = newID;
        }

        public static Dictionary<object, uint> GetStandaloneItems()
        {
            Dictionary<object, uint> dObjectSchemeItems = new Dictionary<object, uint>();
            Dictionary<object, uint> allParams = AllParams;
            foreach (object obj in allParams.Keys)
            {
                dObjectSchemeItems.Add(obj, allParams[obj]);
            }

            foreach (IDNMcOverlayManager mcOverlayManager in Manager_MCOverlayManager.AllParams.Keys)
            {
                try
                {
                    IDNMcObjectScheme[] mcObjectSchemes = mcOverlayManager.GetObjectSchemes();
                    if (mcObjectSchemes != null && mcObjectSchemes.Length > 0)
                    {
                        foreach (IDNMcObjectScheme objectScheme in mcObjectSchemes)
                        {
                            IDNMcObjectSchemeNode[] objectSchemeNodes = objectScheme.GetNodes(DNENodeKindFlags._ENKF_ANY_ITEM);
                            if (objectSchemeNodes != null && objectSchemeNodes.Length > 0)
                            {
                                foreach (IDNMcObjectSchemeItem item in objectSchemeNodes)
                                {
                                    if (!dObjectSchemeItems.ContainsKey(item))
                                        dObjectSchemeItems.Add(item, item.GetID());
                                }
                            }
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetStandaloneItems", McEx);
                }
            }
            return dObjectSchemeItems;
        }

        // return all standalone items and the items that exist in edit mode or grid regions 
        public static Dictionary<object, uint > AllParams
        {
            get {
                Dictionary<object, uint> dObjectSchemeItems = dObjectSchemeItem;

               
                Array m_UtilityPictureTypes = Enum.GetValues(typeof(DNEUtilityPictureType));
                Array m_Utility3DEditItemType = Enum.GetValues(typeof(DNEUtility3DEditItemType));
                try
                {
                    foreach (MCTMapForm mapform in MCTMapFormManager.AllMapForms)
                    {
                        IDNMcLineItem ppInactiveLine, ppActiveLine;

                        mapform.Viewport.GetLocalMapFootprintItem(out ppInactiveLine, out ppActiveLine);
                        if (ppInactiveLine != null && !dObjectSchemeItems.ContainsKey(ppInactiveLine))
                            dObjectSchemeItems.Add(ppInactiveLine, ppInactiveLine.GetID());
                        if (ppActiveLine != null && !dObjectSchemeItems.ContainsKey(ppActiveLine))
                            dObjectSchemeItems.Add(ppActiveLine, ppActiveLine.GetID());

                        foreach (DNEUtilityPictureType UtilityPictureTypes in m_UtilityPictureTypes)
                        {
                            if (UtilityPictureTypes != DNEUtilityPictureType._EUPT_TYPES)
                            {
                                IDNMcPictureItem item = mapform.EditMode.GetUtilityPicture(UtilityPictureTypes);
                                if (item != null && !dObjectSchemeItems.ContainsKey(item))
                                    dObjectSchemeItems.Add(item, item.GetID());
                            }
                        }
                        IDNMcRectangleItem pRectangle;
                        IDNMcLineItem pLine;
                        IDNMcTextItem pText;

                        mapform.EditMode.GetUtilityItems(out pRectangle, out pLine, out pText);

                        if (pRectangle != null && !dObjectSchemeItems.ContainsKey(pRectangle))
                            dObjectSchemeItems.Add(pRectangle, pRectangle.GetID());
                        if (pLine != null && !dObjectSchemeItems.ContainsKey(pLine))
                            dObjectSchemeItems.Add(pLine, pLine.GetID());
                        if (pText != null && !dObjectSchemeItems.ContainsKey(pText))
                            dObjectSchemeItems.Add(pText, pText.GetID());

                        foreach (DNEUtility3DEditItemType Utility3DEditItemType in m_Utility3DEditItemType)
                        {
                            if (Utility3DEditItemType != DNEUtility3DEditItemType._EUEIT_TYPES)
                            {
                                IDNMcObjectSchemeItem item = mapform.EditMode.GetUtility3DEditItem(Utility3DEditItemType);
                                if (item != null && !dObjectSchemeItems.ContainsKey(item))
                                    dObjectSchemeItems.Add(item, item.GetID());
                            }
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("AllParams", McEx);
                }

                try
                {
                    Dictionary<object, uint> grids = Manager_MCGrid.AllParams;
                    foreach (IDNMcMapGrid mapGrid in grids.Keys)
                    {
                        DNSGridRegion[] gridRegions = mapGrid.GetGridRegions();
                        foreach (DNSGridRegion gridRegion in gridRegions)
                        {
                            if (gridRegion.pGridLine != null && (!dObjectSchemeItems.ContainsKey(gridRegion.pGridLine)))
                                dObjectSchemeItems.Add(gridRegion.pGridLine, gridRegion.pGridLine.GetID());

                            if (gridRegion.pGridText != null && (!dObjectSchemeItems.ContainsKey(gridRegion.pGridText)))
                                dObjectSchemeItems.Add(gridRegion.pGridText, gridRegion.pGridText.GetID());
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("AllParams", McEx);
                }

               
                
                return dObjectSchemeItems;
            }
        }

        public static Dictionary<object, uint > GetChildren(object Parent)
        {
            try
            {
                if (Parent is IDNMcObjectSchemeItem)
                {
                    IDNMcObjectSchemeItem objectSchemeItem = (IDNMcObjectSchemeItem)Parent;
                    Dictionary<object, uint> Ret = new Dictionary<object, uint>();
                    uint i = 0;
                    IDNMcObjectSchemeNode[] objectItems = objectSchemeItem.GetChildren();
                    IDNMcObjectSchemeItem currItem;
                    foreach (IDNMcObjectSchemeNode currChild in objectItems)
                    {
                        currItem = (IDNMcObjectSchemeItem)currChild;
                        Ret.Add(currItem, i++);
                    }
                    return Ret;
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetChildren", McEx);
            }
            return null;
        }


        public static Dictionary<IDNMcTexture, IDNMcObjectSchemeNode> dicTextureItems = new Dictionary<IDNMcTexture, IDNMcObjectSchemeNode>();
        //public static List<IDNMcTexture> lstSaveTextureToFolder = new List<IDNMcTexture>();

        public static void LoadStadaloneItem(IDNMcObjectSchemeNode schemeNode, bool saveTexturesToFolder = false)
        {
            try
            {
                //Add the loaded item fonts to dFont dictionary
                if (schemeNode is IDNMcTextItem)
                {
                    IDNMcFont font = null;
                    uint propId = 0;
                    for (byte i = 0; i <= 255; i++)
                    {
                        ((IDNMcTextItem)schemeNode).GetFont(out font, out propId, i);
                        bool nonState = CheckNonStatePropId(propId);
                        if (font != null && !nonState)
                            Manager_MCFont.AddToDictionary(font);
                        if (propId == (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                            break;
                    }
                }
                if(schemeNode is IDNMcLineBasedItem)
                {
                    IDNMcTexture texture = null;
                    uint propId = 0;

                    for (byte i = 0; i <= 255; i++)
                    {
                        ((IDNMcLineBasedItem)schemeNode).GetLineTexture(out texture, out propId, i);
                        bool nonState = CheckNonStatePropId(propId);
                        if (texture != null && !nonState)
                        {
                            Manager_MCTextures.AddToDictionary(texture);
                            if (saveTexturesToFolder)
                                CheckSaveTextureToFolder(texture, schemeNode);
                        }
                        if (propId == (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                            break;
                    }
                }
                if (schemeNode is IDNMcClosedShapeItem)
                {
                    IDNMcTexture texture = null;
                    uint propId = 0;

                    for (byte i = 0; i <= 255; i++)
                    {
                        ((IDNMcClosedShapeItem)schemeNode).GetFillTexture(out texture, out propId, i);
                        bool nonState = CheckNonStatePropId(propId);
                        if (texture != null && !nonState)
                        {
                            Manager_MCTextures.AddToDictionary(texture);
                            if (saveTexturesToFolder)
                                CheckSaveTextureToFolder(texture, schemeNode);
                        }
                        if (propId == (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                            break;
                    }
                }
                if (schemeNode is IDNMcManualGeometryItem)
                {
                    IDNMcTexture texture = null;
                    uint propId = 0;

                    for (byte i = 0; i <= 255; i++)
                    {
                        ((IDNMcManualGeometryItem)schemeNode).GetTexture(out texture, out propId, i);
                        bool nonState = CheckNonStatePropId(propId);
                        if (texture != null && !nonState)
                        {
                            Manager_MCTextures.AddToDictionary(texture);
                            if (saveTexturesToFolder)
                                CheckSaveTextureToFolder(texture, schemeNode);
                        }
                        if (propId == (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                            break;
                    }
                }
                //Add the loaded item textures to dTextures dictionary
                if (schemeNode is IDNMcPictureItem)
                {
                    IDNMcTexture texture = null;
                    uint propId = 0;

                    for (byte i = 0; i <= 255; i++)
                    {
                        ((IDNMcPictureItem)schemeNode).GetTexture(out texture, out propId, i);
                        bool nonState = CheckNonStatePropId(propId);
                        if (texture != null && !nonState)
                        {
                            Manager_MCTextures.AddToDictionary(texture);
                            if(saveTexturesToFolder)
                                CheckSaveTextureToFolder(texture, schemeNode);
                        }
                        if (propId == (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                            break;
                    }

                }

                //Add the loaded item meshes to dMesh dictionary
                if (schemeNode is IDNMcMeshItem)
                {
                    IDNMcMesh mesh = null;
                    uint propId = 0;

                    for (byte i = 0; i <= 255; i++)
                    {
                        ((IDNMcMeshItem)schemeNode).GetMesh(out mesh, out propId, i);
                        bool nonState = CheckNonStatePropId(propId);
                        if (mesh != null && !nonState)
                            Manager_MCMesh.AddToDictionary(mesh);
                        if (propId == (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                            break;
                    }

                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("LoadStadaloneItem", McEx);
            }
        }

        private static bool CheckNonStatePropId(uint propId)
        {
            return (propId == (uint)DNEPredefinedPropertyIDs._EPPI_NO_STATE_PROPERTY_ID || propId == (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID);
        }

        public static void CheckSaveTextureToFolder(IDNMcTexture texture, IDNMcObjectSchemeNode schemeNode = null)
        {
            if (texture != null)
            {
                if (texture is IDNMcMemoryBufferTexture)
                    AddTextureToDic(texture, schemeNode);
                else if (texture is IDNMcImageFileTexture)
                {
                    IDNMcImageFileTexture m_imageTexture = texture as IDNMcImageFileTexture;
                    DNSMcFileSource fileSource = m_imageTexture.GetImageFile();
                    if (m_imageTexture != null && fileSource.bIsMemoryBuffer && fileSource.aFileMemoryBuffer != null)
                    {
                        AddTextureToDic(texture, schemeNode);
                    }
                }
            }
        }

        private static void AddTextureToDic(IDNMcTexture texture, IDNMcObjectSchemeNode schemeNode)
        {
            if (!dicTextureItems.ContainsKey(texture))
                dicTextureItems.Add(texture, schemeNode);
        }

        public static void SaveTexturesToFolder()
        {
            if (dicTextureItems.Count > 0)
            {
                string folderPath;
                FolderSelectDialog FSD = new FolderSelectDialog();
                FSD.Title = "Folder to select";
                FSD.InitialDirectory = @"c:\";
                if (FSD.ShowDialog(IntPtr.Zero))
                {
                    folderPath = FSD.FileName;
                    string[] names = new string[2];

                    for (int i = 0; i < dicTextureItems.Count; i++)
                    {
                        IDNMcTexture texture = dicTextureItems.Keys.ElementAt(i);
                        IDNMcObjectSchemeNode schemeNode = dicTextureItems[texture];
                        string picItemName = "";

                        if(schemeNode != null)
                        {
                            Managers.Manager_MCNames.GetNameByObjectArrayByNameAndId(schemeNode, names);
                            names[0] = names[0].Replace("\"", "").Replace("?", "").Replace(">", "").Replace("<", "").Replace("\\", "").Replace("/", "").Replace("*", "").Replace(":", "").Replace("|", "").Replace(" ", "");
                            picItemName = names[1].Trim().Replace(" ", "") + "_" + names[0];
                        }
                        string filePath = Path.Combine(folderPath, i.ToString() + "_" + (picItemName == "" ? texture.GetHashCode().ToString() : picItemName));
                        if (texture is IDNMcMemoryBufferTexture)
                        {
                            SaveMemoryBufferTextureToFile(texture as IDNMcMemoryBufferTexture, filePath + ".bmp");
                        }
                        else if (texture is IDNMcImageFileTexture)
                        {
                            SaveImageFileTextureToFile(texture as IDNMcImageFileTexture, filePath);
                        }
                    }
                    dicTextureItems.Clear();
                }
            }
            else
            {
                MessageBox.Show("Only image file texture defined as memory buffer or \nmemory buffer texture can save to folder. ", "Save All Embedded Textures To Folder - Missing Match Textures", MessageBoxButtons.OK);
            }
        }

        public static void SaveImageFileTextureToFile(IDNMcImageFileTexture imageTexture, string currentPath = "")
        {
            string filePath = "";
            try
            {
                if (imageTexture != null)
                {
                    string pstrImageFormatExtension = "";
                    DNSMcFileSource fileSource = imageTexture.GetImageFile();
                    if (fileSource.bIsMemoryBuffer && fileSource.aFileMemoryBuffer != null)
                    {
                        if (currentPath == "")
                        {
                            SaveFileDialog sfd = new SaveFileDialog();
                            if (!String.IsNullOrEmpty(fileSource.strFormatExtension))
                            {
                                sfd.DefaultExt = pstrImageFormatExtension = fileSource.strFormatExtension;
                                sfd.Filter = string.Concat(pstrImageFormatExtension.ToUpper(), " files (*.", pstrImageFormatExtension.ToLower(), ")|*.", pstrImageFormatExtension.ToLower());
                            }
                            sfd.RestoreDirectory = true;
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                filePath = sfd.FileName;
                            }
                        }
                        else
                        {
                            filePath = currentPath;
                        }

                        if (!filePath.EndsWith(pstrImageFormatExtension))
                            filePath = filePath + "." + pstrImageFormatExtension;

                        FileStream fileStream = null;
                        try
                        {
                            fileStream = File.Create(filePath);
                            fileStream.Write(fileSource.aFileMemoryBuffer, 0, fileSource.aFileMemoryBuffer.Length);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error open/write to file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (fileStream != null)
                                fileStream.Close();
                        }

                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetImageFile", McEx);
            }
        }

        public static void SaveMemoryBufferTextureToFile(IDNMcMemoryBufferTexture memoryBufferTexture, string filePath2 = "")
        {
            if (memoryBufferTexture != null)
            {
                IDNMcImage image = null;
                byte[] buffer;
                uint srcWidth, srcHeight;
                memoryBufferTexture.GetSourceSize(out srcWidth, out srcHeight);
                DNEPixelFormat pixelFormat = memoryBufferTexture.GetPixelFormat();
                memoryBufferTexture.GetToMemoryBuffer(srcWidth, srcHeight, pixelFormat, 0, out buffer);
                try
                {
                    image = DNMcImage.Create(buffer, srcWidth, srcHeight, pixelFormat);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("InitializeFromPixelBuffer", McEx);
                }
                string filePath = "";
                if (filePath2 == "")
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Bitmap files(*.bmp) |*.bmp| JPEG files (*.jpg, *.jpeg)|*.jpg;*.jpeg| TIFF files (*.tif, *.tiff)|*.tif; *.tiff| GIFF files (*.gif)|*.gif| PNG files (*.png)|*.png| Icon files (*.ico)|*.ico| All Files|*.*";
                    sfd.RestoreDirectory = true;

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        filePath = sfd.FileName;
                    }
                }
                else
                    filePath = filePath2;

                if (filePath != "")
                {
                    try
                    {
                        image.Save(filePath);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("IDNMcImage.Save", McEx, Environment.NewLine + "file path: " + filePath);
                    }
                }
            }
        }

        public static void LoadStadaloneItems(IDNMcObjectScheme objectScheme, bool saveTexturesToFolder = false)
        {
            try
            {
               IDNMcObjectSchemeNode[] schemeNodeArr = objectScheme.GetNodes(DNENodeKindFlags._ENKF_ANY_NODE);
                foreach (IDNMcObjectSchemeNode schemeNode in schemeNodeArr)
                {
                    LoadStadaloneItem(schemeNode, saveTexturesToFolder);
                }

                DNSObjectOperationsParams objectOperationsParams = objectScheme.GetEditModeParams();
                List<IDNMcObjectSchemeNode> objectSchemeNodes = new List<IDNMcObjectSchemeNode>();

                if (objectOperationsParams.pUtilityLine != null)
                    objectSchemeNodes.Add(objectOperationsParams.pUtilityLine);
                if (objectOperationsParams.ap3DEditUtilityItems != null)
                {
                    List<IDNMcObjectSchemeItem> objectSchemeItems = new List<IDNMcObjectSchemeItem>(objectOperationsParams.ap3DEditUtilityItems);
                    objectSchemeNodes.AddRange(objectSchemeItems.FindAll(x => x != null && !objectSchemeNodes.Contains(x)));
                }
                if (objectOperationsParams.apUtilityPictures != null)
                {
                    List<IDNMcObjectSchemeItem> objectSchemeItems2 = new List<IDNMcObjectSchemeItem>(objectOperationsParams.apUtilityPictures);
                    objectSchemeNodes.AddRange(objectSchemeItems2.FindAll(x => x != null && !objectSchemeNodes.Contains(x)));
                }
                objectSchemeNodes = objectSchemeNodes.Distinct().ToList();
                foreach (IDNMcObjectSchemeItem item in objectSchemeNodes)
                {
                    LoadStadaloneItem(item, saveTexturesToFolder);
                    AddNewItem(item);
                }

                if (saveTexturesToFolder)
                    SaveTexturesToFolder();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("LoadStadaloneItems", McEx);
            }
        }

    }
}

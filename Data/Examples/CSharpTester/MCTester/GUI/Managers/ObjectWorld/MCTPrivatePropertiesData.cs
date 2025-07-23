using MapCore;
using MapCore.Common;
using MCTester.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UnmanagedWrapper;

namespace MCTester.Managers.ObjectWorld
{
    public class MCTPrivatePropertiesData
    {
        public static Dictionary<uint, PrivatePropertyControls> privatePropertyControls = new Dictionary<uint, PrivatePropertyControls>();

        public static List<uint> lstDiffMC = new List<uint>();

        public static bool bIsChangeDicKey = false;

        public static void RemovePropertyId(uint lastId, uint currId, CtrlObjStatePropertyBase control)
        {
            if (lastId != currId)
            {
                RemovePropertyId(lastId, control);
            }
        }

        public static void RemovePropertyId(uint currId, CtrlObjStatePropertyBase control)
        {
            if (currId != DNMcConstants._MC_EMPTY_ID && privatePropertyControls.ContainsKey(currId) && !bIsChangeDicKey)
            {
                if (control.RegPropertyID != currId && !control.IsExistIdInStates(currId))
                {
                    PrivatePropertyControls propertyControls = privatePropertyControls[currId];
                    propertyControls.Controls.Remove(control);

                    if (propertyControls.Controls.Count == 0)
                    {
                        lstDiffMC.Remove(currId);
                        privatePropertyControls.Remove(currId);
                    }
                }
            }
        }

        public static void AddPrivatePropertyControls(uint id, object value, CtrlObjStatePropertyBase control)
        {
            if (id != DNMcConstants._MC_EMPTY_ID)
            {
                PrivatePropertyControls propertyControls;
                if (privatePropertyControls.ContainsKey(id))
                {
                    propertyControls = privatePropertyControls[id];
                    if (propertyControls != null)
                    {
                        propertyControls.Value = value;
                        if (propertyControls.Controls.Any(x => x == control))
                        {
                            propertyControls.Controls.Remove(control);
                        }
                        propertyControls.Controls.Add(control);
                    }
                }
                else
                {
                    propertyControls = new PrivatePropertyControls();
                    propertyControls.Value = value;
                    propertyControls.Controls = new List<CtrlObjStatePropertyBase>();
                    propertyControls.Controls.Add(control);
                    privatePropertyControls.Add(id, propertyControls);
                }
            }
        }

        public static string ConvertPropertyValueToStringByType(DNEPropertyType mcPropertyType, object propertyValue)
        {

            string strValue = "";
            try
            {
                if (propertyValue != null)
                {
                    switch (mcPropertyType)
                    {
                        case DNEPropertyType._EPT_ANIMATION:
                            DNSMcAnimation animation = (DNSMcAnimation)propertyValue;
                            if (animation.strAnimationName != null)
                            {
                                strValue = "Animation Name:\t" + animation.strAnimationName.ToString() +
                                    "\nIs Loop:\t" + animation.bLoop.ToString();
                            }
                            break;
                        case DNEPropertyType._EPT_ATTENUATION:
                            DNSMcAttenuation attenuation = (DNSMcAttenuation)propertyValue;
                            strValue = "Const:\t" + attenuation.fConst.ToString() +
                                "\nLinear:\t" + attenuation.fLinear.ToString() +
                                "\nSquare:\t" + attenuation.fSquare.ToString() +
                                "\nRange:\t" + attenuation.fRange.ToString();
                            break;
                        case DNEPropertyType._EPT_BCOLOR:
                            DNSMcBColor btnColor = (DNSMcBColor)propertyValue;
                            strValue = btnColor.ToString();
                            break;
                        case DNEPropertyType._EPT_BCOLOR_ARRAY:
                            DNSArrayProperty<DNSMcBColor> colors = (DNSArrayProperty<DNSMcBColor>)propertyValue;
                            if (colors.aElements != null)
                            {
                                foreach (DNSMcBColor color in colors.aElements)
                                {
                                    strValue += "(" + color.ToString() + ") ";
                                }
                            }

                            break;
                       
                        case DNEPropertyType._EPT_CONDITIONALSELECTOR:
                            IDNMcConditionalSelector conditionalSelector = null;
                            if(propertyValue is MCTConditionalSelectorProperty)
                                conditionalSelector = ((MCTConditionalSelectorProperty)propertyValue).ConditionalSelector;
                            else if(propertyValue is IDNMcConditionalSelector)
                                conditionalSelector = (IDNMcConditionalSelector)propertyValue;
                            if (conditionalSelector != null)
                            {
                                strValue = "ID = " + conditionalSelector.GetID() +
                                "\n" + "Type = " + conditionalSelector.ConditionalSelectorType.ToString();
                            }
                            else
                                strValue = "Null";
                            break;
                        case DNEPropertyType._EPT_FCOLOR:
                            strValue = ((DNSMcFColor)propertyValue).ToString();
                            break;
                        case DNEPropertyType._EPT_FONT:
                            if (propertyValue is IDNMcLogFont)
                            {
                                Font font = Font.FromLogFont(((IDNMcLogFont)propertyValue).LogFont.LogFont);
                                double fontSizeInPoints = Math.Round(font.SizeInPoints);

                                strValue = " Font Name = " + font.Name +
                                    "\n" + ", Size In Points = " + fontSizeInPoints.ToString() +
                                    "\n" + ", Style = " + font.Style +
                                    "\n" + ", Is Unicode = " + ((IDNMcLogFont)propertyValue).LogFont.bIsUnicode.ToString();
                            }
                            else if (propertyValue is IDNMcFileFont)
                            {
                                DNSMcFileSource fileSource = new DNSMcFileSource();
                                int height = 0;
                                ((IDNMcFileFont)propertyValue).GetFontFileAndHeight(out fileSource, out height);
                                strValue = "File Name = " + fileSource.strFileName +
                                   "\n" + " Is Memory Buffer = " + fileSource.bIsMemoryBuffer +
                                   "\n" + " Height = " + height.ToString();
                            }
                            break;
                        case DNEPropertyType._EPT_FVECTOR2D:
                            DNSMcFVector2D fvect2d = (DNSMcFVector2D)propertyValue;
                            strValue = "X = " + fvect2d.x.ToString() + "; Y = " + fvect2d.y.ToString();
                            break;

                        case DNEPropertyType._EPT_FVECTOR3D:
                            DNSMcFVector3D fvect3d = (DNSMcFVector3D)propertyValue;
                            strValue = "X = " + fvect3d.x.ToString() + "; Y = " + fvect3d.y.ToString() + "; Z = " + fvect3d.z.ToString();
                            break;
                            
                        case DNEPropertyType._EPT_ROTATION:
                            DNSMcRotation rotation = (DNSMcRotation)propertyValue;
                            strValue = "Yaw = " + rotation.fYaw.ToString() +
                                "\n" + "Pitch = " + rotation.fPitch.ToString() +
                                "\n" + "Roll = " + rotation.fRoll.ToString() +
                                "\n" + "Relative To Curr Orientation: " + rotation.bRelativeToCurrOrientation.ToString();
                            break;
                        
                          
                        case DNEPropertyType._EPT_UINT_ARRAY:
                            DNSArrayProperty<uint> numbers_arr = (DNSArrayProperty<uint>)propertyValue;
                            if (numbers_arr.aElements != null)
                            {
                                foreach (uint element in numbers_arr.aElements)
                                {
                                    strValue += element.ToString() + " ";
                                }
                            }

                            break;
                        case DNEPropertyType._EPT_INT_ARRAY:
                            DNSArrayProperty<int> numbers_arr_int = (DNSArrayProperty<int>)propertyValue;
                            if (numbers_arr_int.aElements != null)
                            {
                                foreach (int element in numbers_arr_int.aElements)
                                {
                                    strValue += element.ToString() + " ";
                                }
                            }

                            break;
                        case DNEPropertyType._EPT_SUBITEM_ARRAY:
                            DNSArrayProperty<DNSMcSubItemData> subItemsData = (DNSArrayProperty<DNSMcSubItemData>)propertyValue;
                            if (subItemsData.aElements != null)
                            {
                                string ids = "ID: ";
                                string indexes = ", Points Start Index: ";
                                foreach (DNSMcSubItemData itemData in subItemsData.aElements)
                                {
                                    uint id = itemData.uSubItemID;
                                    string sid = id.ToString();
                                    if (id == DNMcConstants._MC_EMPTY_ID)
                                        sid = "MAX";
                                    else if (id == DNMcConstants._MC_EMPTY_ID - 1)
                                        sid = "MAX-1";
                                    ids += sid + " ";
                                    indexes += itemData.nPointsStartIndex.ToString() + " ";
                                }
                                strValue = ids + "\n" + indexes;
                            }

                            break;
                        case DNEPropertyType._EPT_VECTOR2D_ARRAY:
                            DNSArrayProperty<DNSMcVector2D> vector2D_arr = (DNSArrayProperty<DNSMcVector2D>)propertyValue;
                            if (vector2D_arr.aElements != null)
                            {
                                foreach (DNSMcVector2D element in vector2D_arr.aElements)
                                {
                                    strValue = "(X = " + element.x.ToString() + ", Y = " + element.y.ToString() + ") ";
                                }
                            }

                            break;
                        case DNEPropertyType._EPT_FVECTOR2D_ARRAY:
                            DNSArrayProperty<DNSMcFVector2D> fvector2D_arr = (DNSArrayProperty<DNSMcFVector2D>)propertyValue;
                            if (fvector2D_arr.aElements != null)
                            {
                                foreach (DNSMcFVector2D element in fvector2D_arr.aElements)
                                {
                                    strValue += "(X = " + element.x.ToString() + ", Y = " + element.y.ToString() + ") ";
                                }
                            }

                            break;
                        case DNEPropertyType._EPT_VECTOR3D_ARRAY:
                            DNSArrayProperty<DNSMcVector3D> vector3D_arr = (DNSArrayProperty<DNSMcVector3D>)propertyValue;
                            if (vector3D_arr.aElements != null)
                            {
                                foreach (DNSMcVector3D element in vector3D_arr.aElements)
                                {
                                    strValue += "(X = " + element.x.ToString() + ", Y = " + element.y.ToString() + ", Z = " + element.z.ToString() + ") ";
                                }
                            }

                            break;
                        case DNEPropertyType._EPT_FVECTOR3D_ARRAY:
                            DNSArrayProperty<DNSMcFVector3D> fvector3D_arr = (DNSArrayProperty<DNSMcFVector3D>)propertyValue;
                            if (fvector3D_arr.aElements != null)
                            {
                                foreach (DNSMcFVector3D element in fvector3D_arr.aElements)
                                {
                                    strValue += "(X = " + element.x.ToString() + ", Y = " + element.y.ToString() + ", Z = " + element.z.ToString() + ") ";
                                }
                            }

                            break;
                           
                        case DNEPropertyType._EPT_VECTOR2D:
                            DNSMcVector2D vect2d = (DNSMcVector2D)propertyValue;
                            strValue = "X = " + vect2d.x.ToString() + "; Y = " + vect2d.y.ToString();
                            break;
                        case DNEPropertyType._EPT_VECTOR3D:
                            DNSMcVector3D vect3d = (DNSMcVector3D)propertyValue;
                            strValue = "X = " + vect3d.x.ToString() + "; Y = " + vect3d.y.ToString() + "; Z = " + vect3d.z.ToString();
                            break;
                       
                        case DNEPropertyType._EPT_INT:
                            if ((int)propertyValue == int.MaxValue)
                                strValue = "MAX";
                            else
                                strValue = propertyValue.ToString();
                            break;
                        case DNEPropertyType._EPT_UINT:
                            if ((uint)propertyValue == uint.MaxValue)
                                strValue = "MAX";
                            else if((uint)propertyValue == uint.MaxValue -1)
                                strValue = "MAX-1";
                            else
                                strValue = propertyValue.ToString();
                            break;
                        case DNEPropertyType._EPT_FLOAT:
                            if ((float)propertyValue == float.MaxValue)
                                strValue = "MAX";
                            else
                                strValue = propertyValue.ToString();
                            break;
                        case DNEPropertyType._EPT_DOUBLE:
                            if ((double)propertyValue == double.MaxValue)
                                strValue = "MAX";
                            else
                                strValue = propertyValue.ToString();
                            break;

                        default:
                            /* _EPT_BOOL
                             * _EPT_MESH
                             * _EPT_TEXTURE
                             * _EPT_STRING
                             * _EPT_BYTE
                             * _EPT_SBYTE
                             */

                            strValue = propertyValue.ToString();
                            break;
                    }
                }
                else
                {
                    strValue = "Null";
                }
            }catch(MapCoreException ex)
            {
                Utilities.ShowErrorMessage("Get Private Property Value", ex);
            }
           
            return strValue;
        }

        private static bool IsExistDifferentTypes(DNEPropertyType existPropertyType, DNEPropertyType currentPropertyType, object existValue, object currentValue, string strEnumType, uint propertyId, IDNMcObjectScheme objectScheme)
        {
            if (existPropertyType != currentPropertyType)
                return true;
            else if (existPropertyType == DNEPropertyType._EPT_ENUM)
            {
                string strExistEnumType = "";
                if (privatePropertyControls.ContainsKey(propertyId))
                {
                    strExistEnumType = privatePropertyControls[propertyId].Controls.First().GetPropertyEnumType();
                    if (strEnumType == strExistEnumType)
                        return false;
                    else
                        return true;
                }
                if (existValue != null && currentValue != null) // check value from mapcore
                {
                    Type existValueType = null, currentValueType = null;
                    if (existValue != null)
                        existValueType = existValue.GetType();
                    if (currentValue != null)
                        currentValueType = currentValue.GetType();
                    //int numExistValue = 0, numCurrentValue = 0;
                    if (existValueType.IsEnum && currentValueType.IsEnum && existValueType.Name != currentValueType.Name)// different enum type
                    {
                        return true;
                    }
                    if (existValueType.IsEnum == false)
                    {
                        string existEnumFullName = GetEnumNameFromActualName(objectScheme, propertyId);

                        if (existEnumFullName != currentValueType.Name.Replace("DNE", "E"))  // different enum type
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static string GetEnumNameFromActualName(IDNMcObjectScheme objectScheme,uint propertyId, string propertyName  = "")
        {
            string enumName = "";
            try
            {
                if(propertyName == "")
                    enumName = objectScheme.GetEnumPropertyActualType(propertyId);
                else
                    enumName = objectScheme.GetEnumPropertyActualType(propertyName);

                int index1 = enumName.IndexOf(".");
                if (index1 >= 0)
                    enumName = enumName.Substring(index1 + 1);
                int index2 = enumName.IndexOf("|");
                if (index2 >= 0)
                    enumName = enumName.Substring(0, enumName.Length - index2);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetEnumPropertyActualType", McEx);
            }
            return enumName;
        }

        public static bool IsExistDifferentValues(DNEPropertyType mcPropertyType, object existValue, object currentValue, IDNMcObjectScheme objectScheme , uint propertyId)
        {
            if (existValue == null && currentValue == null)
                return false;
            Type existValueType = null, currentValueType = null;
            if(existValue != null)
                 existValueType = existValue.GetType();
            if(currentValue != null)
                currentValueType = currentValue.GetType();
           
            if(mcPropertyType != DNEPropertyType._EPT_CONDITIONALSELECTOR && (existValue == null || currentValue == null))
                return true;
            if (mcPropertyType == DNEPropertyType._EPT_BOOL ||
               mcPropertyType == DNEPropertyType._EPT_INT ||
               mcPropertyType == DNEPropertyType._EPT_UINT ||
               mcPropertyType == DNEPropertyType._EPT_DOUBLE ||
               mcPropertyType == DNEPropertyType._EPT_SBYTE ||
               mcPropertyType == DNEPropertyType._EPT_BYTE ||
               mcPropertyType == DNEPropertyType._EPT_ENUM ||
               mcPropertyType == DNEPropertyType._EPT_FLOAT)
                return (existValue.ToString() != currentValue.ToString());
            if (mcPropertyType == DNEPropertyType._EPT_VECTOR2D)
            {
                DNSMcVector2D existValueVector2D = (DNSMcVector2D)existValue;
                DNSMcVector2D currentValueVector2D = (DNSMcVector2D)currentValue;
                return (existValueVector2D.x != currentValueVector2D.x || existValueVector2D.y != currentValueVector2D.y);
            }
            if (mcPropertyType == DNEPropertyType._EPT_FVECTOR2D)
            {
                DNSMcFVector2D existValueVector2D = (DNSMcFVector2D)existValue;
                DNSMcFVector2D currentValueVector2D = (DNSMcFVector2D)currentValue;
                return (existValueVector2D.x != currentValueVector2D.x || existValueVector2D.y != currentValueVector2D.y);
            }
            if (mcPropertyType == DNEPropertyType._EPT_VECTOR3D)
            {
                DNSMcVector2D existValueVector3D = (DNSMcVector2D)existValue;
                DNSMcVector2D currentValueVector3D = (DNSMcVector2D)currentValue;
                return (existValueVector3D.x != currentValueVector3D.x || existValueVector3D.y != currentValueVector3D.y);
            }
            if (mcPropertyType == DNEPropertyType._EPT_FVECTOR3D)
            {
                DNSMcFVector3D existValueVector3D = (DNSMcFVector3D)existValue;
                DNSMcFVector3D currentValueVector3D = (DNSMcFVector3D)currentValue;
                return (existValueVector3D.x != currentValueVector3D.x || existValueVector3D.y != currentValueVector3D.y);
            }
            if (mcPropertyType == DNEPropertyType._EPT_BCOLOR)
            {
                DNSMcBColor existValueBColor = (DNSMcBColor)existValue;
                DNSMcBColor currentValueBColor = (DNSMcBColor)currentValue;
                return (existValueBColor.a != currentValueBColor.a || existValueBColor.b != currentValueBColor.b ||
                        existValueBColor.g != currentValueBColor.g || existValueBColor.r != currentValueBColor.r);
            }
            if (mcPropertyType == DNEPropertyType._EPT_FCOLOR)
            {
                DNSMcFColor existValueFColor = (DNSMcFColor)existValue;
                DNSMcFColor currentValueFColor = (DNSMcFColor)currentValue;
                return (existValueFColor.a != currentValueFColor.a || existValueFColor.b != currentValueFColor.b ||
                        existValueFColor.g != currentValueFColor.g || existValueFColor.r != currentValueFColor.r);
            }
            if (mcPropertyType == DNEPropertyType._EPT_STRING)  
            {
                DNMcVariantString existValueString , currentValueString;
                if (existValue is DNMcVariantString)
                    existValueString = (DNMcVariantString)existValue;
                else
                    existValueString = new DNMcVariantString(existValue.ToString(), false);

                if (currentValue is DNMcVariantString)
                    currentValueString = (DNMcVariantString)currentValue;
                else
                    currentValueString = new DNMcVariantString(currentValue.ToString(), false);

                if (existValueString.bIsUnicode != currentValueString.bIsUnicode)
                    return true;

                if (existValueString.astrStrings == null && currentValueString.astrStrings == null)
                    return false;
                if ((existValueString.astrStrings != null && currentValueString.astrStrings == null) ||
                    (existValueString.astrStrings == null && currentValueString.astrStrings != null))
                    return true;
                if (existValueString.astrStrings.Length != currentValueString.astrStrings.Length)
                    return true;
                for (int i = 0; i < existValueString.astrStrings.Length; i++)
                {
                    if (existValueString.astrStrings[i] != currentValueString.astrStrings[i])
                    {
                        return true;
                    }
                }
                return false;
            }
            if (mcPropertyType == DNEPropertyType._EPT_TEXTURE)
            {
                IDNMcTexture existValueTexture = (IDNMcTexture)existValue;
                IDNMcTexture currentValueTexture = (IDNMcTexture)currentValue;

                return (existValueTexture != currentValueTexture);
            }
            if (mcPropertyType == DNEPropertyType._EPT_FONT)
            {
                IDNMcFont existValueFont = (IDNMcFont)existValue;
                IDNMcFont currentValueFont = (IDNMcFont)currentValue;

                return (existValueFont != currentValueFont);
            }
            if (mcPropertyType == DNEPropertyType._EPT_ROTATION)
            {
                DNSMcRotation existValueRotation = (DNSMcRotation)existValue;
                DNSMcRotation currentValueRotation = (DNSMcRotation)currentValue;

                if (existValueRotation.fYaw != currentValueRotation.fYaw || existValueRotation.fPitch != currentValueRotation.fPitch ||
                   existValueRotation.fRoll != currentValueRotation.fRoll || existValueRotation.bRelativeToCurrOrientation != currentValueRotation.bRelativeToCurrOrientation)
                    return true;

                return false;
            }
            if (mcPropertyType == DNEPropertyType._EPT_ATTENUATION)
            {
                DNSMcAttenuation existValueAttenuation = (DNSMcAttenuation)existValue;
                DNSMcAttenuation currentValueAttenuation = (DNSMcAttenuation)currentValue;
                if (existValueAttenuation.fConst != currentValueAttenuation.fConst || existValueAttenuation.fLinear != currentValueAttenuation.fLinear ||
                    existValueAttenuation.fSquare != currentValueAttenuation.fSquare || existValueAttenuation.fRange != currentValueAttenuation.fRange)
                    return true;

                return false;
            }
            if (mcPropertyType == DNEPropertyType._EPT_ANIMATION)
            {
                DNSMcAnimation existValueAnimation = (DNSMcAnimation)existValue;
                DNSMcAnimation currentValueAnimation = (DNSMcAnimation)currentValue;
                if (existValueAnimation.strAnimationName != currentValueAnimation.strAnimationName || existValueAnimation.bLoop != currentValueAnimation.bLoop )
                    return true;

                return false;
            }
            if (mcPropertyType == DNEPropertyType._EPT_MESH)
            {
                IDNMcMesh existValueMesh = (IDNMcMesh)existValue;
                IDNMcMesh currentValueMesh = (IDNMcMesh)currentValue;

                DNEMeshType existValueMeshType = existValueMesh.MeshType;
                DNEMeshType currentValueMeshType = currentValueMesh.MeshType;

                if (existValueMeshType != currentValueMeshType)
                    return true;
                if (existValueMeshType == DNEMeshType._EMT_NONE)
                    return false;
                if (existValue != currentValue)
                    return true;
                
                return false;
            }
            if (mcPropertyType == DNEPropertyType._EPT_CONDITIONALSELECTOR)
            {
                MCTConditionalSelectorProperty existConditionalSelectorProperty, currentConditionalSelectorProperty;
                IDNMcConditionalSelector existConditionalSelector = null, currentConditionalSelector = null;
                if (existValue is MCTConditionalSelectorProperty)
                {
                    existConditionalSelectorProperty = (MCTConditionalSelectorProperty)existValue;
                    existConditionalSelector = ((MCTConditionalSelectorProperty)existValue).ConditionalSelector;
                }
                else if (existValue is IDNMcConditionalSelector)
                    existConditionalSelector = (IDNMcConditionalSelector)existValue;

                if (currentValue is MCTConditionalSelectorProperty)
                {
                    currentConditionalSelectorProperty = (MCTConditionalSelectorProperty)currentValue;
                    currentConditionalSelector = ((MCTConditionalSelectorProperty)currentValue).ConditionalSelector;
                }
                else if (currentValue is IDNMcConditionalSelector)
                    currentConditionalSelector = (IDNMcConditionalSelector)currentValue;
                return (existConditionalSelector != currentConditionalSelector);

            }
            if(mcPropertyType == DNEPropertyType._EPT_UINT_ARRAY)
            {
                return CompreBetweenArrays<uint>(existValue, currentValue, mcPropertyType);
            }
            if (mcPropertyType == DNEPropertyType._EPT_INT_ARRAY)
            {
                return CompreBetweenArrays<int>(existValue, currentValue, mcPropertyType);
            }
            if (mcPropertyType == DNEPropertyType._EPT_FVECTOR2D_ARRAY)
            {
                return CompreBetweenArrays<DNSMcFVector2D>(existValue, currentValue, mcPropertyType);
            }
            if (mcPropertyType == DNEPropertyType._EPT_FVECTOR3D_ARRAY)
            {
                return CompreBetweenArrays<DNSMcFVector3D>(existValue, currentValue, mcPropertyType);
            }
            if (mcPropertyType == DNEPropertyType._EPT_VECTOR2D_ARRAY)
            {
                return CompreBetweenArrays<DNSMcVector2D>(existValue, currentValue, mcPropertyType);
            }
            if (mcPropertyType == DNEPropertyType._EPT_VECTOR3D_ARRAY)
            {
                return CompreBetweenArrays<DNSMcVector3D>(existValue, currentValue, mcPropertyType);
            }
            if (mcPropertyType == DNEPropertyType._EPT_BCOLOR_ARRAY)
            {
                return CompreBetweenArrays<DNSMcBColor>(existValue, currentValue, mcPropertyType);
            }
            if (mcPropertyType == DNEPropertyType._EPT_SUBITEM_ARRAY)
            {
                return CompreBetweenArrays<DNSMcSubItemData>(existValue, currentValue, mcPropertyType);
            }
            return true;
        }

        private static bool CompreBetweenArrays<T>(object property1, object property2, DNEPropertyType mcPropertyType)
        {
            DNSArrayProperty<T> arrayProperty1 = (DNSArrayProperty<T>)property1;
            DNSArrayProperty<T> arrayProperty2 = (DNSArrayProperty<T>)property2;
            if (arrayProperty1.aElements == null && arrayProperty2.aElements == null)
                return false;
            if ((arrayProperty1.aElements != null && arrayProperty2.aElements == null) ||
                (arrayProperty1.aElements == null && arrayProperty2.aElements != null))
                return true;
            if ((arrayProperty1.aElements.Length == 0) && (arrayProperty2.aElements.Length == 0))
                return false;
            if (arrayProperty1.aElements.Length != arrayProperty2.aElements.Length)
                return true;

            for (int i = 0; i < arrayProperty1.aElements.Length; i++)
            {
                if (mcPropertyType != DNEPropertyType._EPT_SUBITEM_ARRAY)
                {
                    if (!arrayProperty1.aElements[i].Equals(arrayProperty2.aElements[i]))
                    {
                        return true;
                    }
                }
                else  // DNSMcSubItemData hasn't Equals function.
                {
                    DNSMcSubItemData mcSubItemData1 = (DNSMcSubItemData)(object)arrayProperty1.aElements[i];
                    DNSMcSubItemData mcSubItemData2 = (DNSMcSubItemData)(object)arrayProperty2.aElements[i];
                    if ((mcSubItemData1.nPointsStartIndex != mcSubItemData2.nPointsStartIndex) ||
                        (mcSubItemData1.uSubItemID != mcSubItemData2.uSubItemID))
                        return true;
                }
            }
            return false;
        }

        private static System.Windows.Forms.TabPage GetCurrentTabPage(System.Windows.Forms.Control control)
        {
            if (control.Parent is System.Windows.Forms.TabPage)
                return control.Parent as System.Windows.Forms.TabPage;
            else
                return GetCurrentTabPage(control.Parent);
        }

        public static void SetControlsValue(uint id, object value, bool isAddIdToDiffMCList, CtrlObjStatePropertyBase control)
        {
            if (isAddIdToDiffMCList)
                lstDiffMC.Add(id);
            if (privatePropertyControls.ContainsKey(id))
            {
                foreach (CtrlObjStatePropertyBase existControl in privatePropertyControls[id].Controls)
                {
                    existControl.SetValue(id, value);
                }
            }
            AddPrivatePropertyControls(id, value, control);
        }

        public static MCTPrivatePropertyValidationResult GetValueOfExistPrivatePropertyNew(uint id, object value, CtrlObjStatePropertyBase control, DNSByteBool state, object dicKey = null)
        {
            MCTPrivatePropertyValidationResult propertyValidationResult = new MCTPrivatePropertyValidationResult();
            propertyValidationResult.ResultValidation2 = value;
            try
            {
                if (id != DNMcConstants._MC_EMPTY_ID)
                {
                    System.Windows.Forms.TabPage tabPage = GetCurrentTabPage(control);
                    DNEPropertyType currentPropertyType = control.GetPropertyType();
                    DNEPropertyType mcPropertyType;
                    object mcPropertyValue;
                    if (control.CurrentObjectSchemeNode != null)
                    {
                        string strEnumPropertyType = control.GetPropertyEnumType();
                        IDNMcObjectScheme objectScheme = control.CurrentObjectSchemeNode.GetScheme();
                        bool isExistDiffInMC = false;
                        if (!lstDiffMC.Contains(id) && objectScheme != null)
                        {
                            DNSVariantProperty sVariantProperty;
                            try
                            {
                                sVariantProperty = objectScheme.GetPropertyDefault(id);
                                mcPropertyType = sVariantProperty.eType;
                                mcPropertyValue = sVariantProperty.Value;
                                if (IsExistDifferentTypes(mcPropertyType, currentPropertyType, mcPropertyValue, value, strEnumPropertyType, id, objectScheme))
                                {
                                    propertyValidationResult.ErrorType = MCTPrivatePropertyValidationResult.EErrorType.InvalidType;
                                    return propertyValidationResult;
                                }
                                if (IsExistDifferentValues(currentPropertyType, mcPropertyValue, value, objectScheme, id))
                                {
                                    IDNMcObjectSchemeNode[] nodes = objectScheme.GetNodesByPropertyID(id);
                                    if (nodes != null && nodes.Length > 0)
                                    {
                                        isExistDiffInMC = true;
                                        if (nodes.Length == 1 && nodes[0] == control.CurrentObjectSchemeNode)
                                            isExistDiffInMC = false;

                                        if (isExistDiffInMC)
                                        {
                                            propertyValidationResult.IsAddIdToDiffMCList = true;
                                            propertyValidationResult.ErrorType = MCTPrivatePropertyValidationResult.EErrorType.InvalidValue;
                                            if (currentPropertyType == DNEPropertyType._EPT_ENUM)
                                                propertyValidationResult.ResultValidation1 = ConvertEnumValueToUintValue(objectScheme, id, mcPropertyValue);
                                            else
                                                propertyValidationResult.ResultValidation1 = mcPropertyValue;
                                            return propertyValidationResult;
                                        }
                                    }
                                }
                            }
                            catch (MapCoreException )
                            { }
                        }
                        PrivatePropertyControls propertyControls = null;

                        if (isExistDiffInMC == false && privatePropertyControls.ContainsKey(id))
                        {
                            propertyControls = privatePropertyControls[id];
                            object existValue = propertyControls.Value;

                            bool isNeedAskUser = false;
                            DNEPropertyType existPropertyType = propertyControls.Controls.First().GetPropertyType();
                            if (IsExistDifferentTypes(existPropertyType, currentPropertyType, existValue, value, strEnumPropertyType, id, objectScheme))
                            {
                                propertyValidationResult.ErrorType = MCTPrivatePropertyValidationResult.EErrorType.InvalidType;
                                return propertyValidationResult;
                            }
                            else if (IsExistDifferentValues(currentPropertyType, existValue, value, objectScheme, id))
                            {
                                CtrlObjStatePropertyBase findControl = propertyControls.Controls.Find(x => x == control);
                                if (findControl != null)
                                {
                                    if (dicKey != null)
                                    {
                                        isNeedAskUser = findControl.IsExistIdInDic(dicKey, id, state);
                                    }
                                    else
                                    {
                                        if (!state.AsBool && findControl.IsExistIdInStates(id))
                                            isNeedAskUser = true;
                                        else if (state.AsBool && findControl.RegPropertyID == id)
                                            isNeedAskUser = true;
                                        else if (state.AsBool && findControl.IsExistIdInStates(id))
                                        {
                                            List<byte> lstStatesOfId = findControl.GetStatesOfId(id);
                                            if (lstStatesOfId.Count > 1)
                                                isNeedAskUser = true;
                                            else if (lstStatesOfId[0] != state.AsByte)
                                                isNeedAskUser = true;
                                        }
                                    }
                                }
                                if (propertyControls.Controls.Any(x => x != control) || isNeedAskUser)
                                {
                                    propertyValidationResult.ErrorType = MCTPrivatePropertyValidationResult.EErrorType.InvalidValue;
                                    propertyValidationResult.ResultValidation1 = propertyControls.Value;

                                    return propertyValidationResult;
                                }
                                else // update control in list
                                {
                                    propertyControls.Value = value;
                                }
                            }
                            else
                            {
                                AddPrivatePropertyControls(id, value, control);
                            }
                        }
                        else
                        {
                            AddPrivatePropertyControls(id, value, control);
                        }
                        return propertyValidationResult;
                    }
                    return propertyValidationResult;
                }
                return propertyValidationResult;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Private Property", McEx);
            }
            return propertyValidationResult;
        }
        
        public static void RemoveAllPrivatePropertyControlsData()
        {
            privatePropertyControls.Clear();
            lstDiffMC.Clear();
        }

        public class PrivatePropertyControls
        {
            public object Value;
            public List<CtrlObjStatePropertyBase> Controls;
        }

        public static string ConvertEnumValueToText(object mcObject, uint propertyId, DNEPropertyType propertyType, object propertyValue)
        {
            string[] enumNames = null;
            Array arrayEnumValues;
            bool mIsCanNumber = false;
            string cellText = CheckUintValue(propertyType, propertyValue);
            string enumName = "";
            Type EnumType;

            GetEnumValuesByPropertyId(mcObject, propertyId, out enumNames, out arrayEnumValues, out mIsCanNumber, out enumName, out EnumType);
            if (enumNames != null && enumNames.Length > 0)
            {
                if (enumName.ToLower().Contains("flags"))
                {
                    cellText = "";
                    if ((uint)propertyValue == 0)
                        return arrayEnumValues.GetValue(0).ToString();
                    else
                    {
                        for (int i=0;i < arrayEnumValues.GetLength(0); i++)
                        {
                            if ((((uint)propertyValue) & ((int)arrayEnumValues.GetValue(i))) != 0)
                                cellText += enumNames[i] + ",";
                        }
                        cellText = cellText.TrimEnd(',');
                    }
                }
                else
                {
                    int index = GetEnumIndexInArray(arrayEnumValues, UInt32.Parse(propertyValue.ToString()));
                    if (index != -1)
                    {
                        cellText = enumNames[index];
                        if (mIsCanNumber)
                        {
                            cellText += "(" + CheckUintValue(propertyType, propertyValue) + ")";
                        }
                           
                    }
                }
            }
            return cellText;
        }

        private static string CheckUintValue(DNEPropertyType propertyType, object propertyValue)
        {
            string strValue = propertyValue.ToString();
            if (propertyType == DNEPropertyType._EPT_UINT)
            {
                if ((uint)propertyValue == uint.MaxValue)
                    strValue = "MAX";
                else if ((uint)propertyValue == uint.MaxValue - 1)
                    strValue = "MAX-1";
            }
            return strValue;
        }

        public static object ConvertEnumValueToUintValue(object mcSchemeObject, uint propertyId, object propertyValue)
        {
            string[] enumNames = null;
            Array arrayEnumValues;
            bool mIsCanNumber = false;
            string cellText = propertyValue.ToString();
            string enumFullName = "";
            Type enumType;

            GetEnumValuesByPropertyId(mcSchemeObject, propertyId, out enumNames, out arrayEnumValues, out mIsCanNumber, out enumFullName, out enumType);
            if (enumNames != null && enumNames.Length > 0)
            {
                int index = GetEnumIndexInArray(arrayEnumValues, UInt32.Parse(propertyValue.ToString()));
                if (index != -1)
                {
                    return arrayEnumValues.GetValue(index);
                }
            }
            return propertyValue;
        }

        public static string GetPropertyValueAsText(object mcObject, uint propertyId, DNEPropertyType propertyType, object propertyValue)
        {
            string strValue = "";
            // add uint or byte types - for numeric property with enum values - like DNESegmentType
            if (propertyType == DNEPropertyType._EPT_ENUM || propertyType == DNEPropertyType._EPT_UINT || propertyType == DNEPropertyType._EPT_BYTE)  
                strValue = ConvertEnumValueToText(mcObject, propertyId, propertyType, propertyValue);
            else 
                strValue = ConvertPropertyValueToStringByType(propertyType, propertyValue);
            return strValue;
        }

        public static void GetEnumValuesByPropertyId(object mcdnObject, uint id, out string[] enumNames, out Array arrayEnumValues, out bool bCanNumber, out string enumName, out Type enumType)
        {
            IDNMcObjectSchemeNode mcObjectSchemeNode;
            IDNMcObjectScheme mcObjectScheme;
            IDNMcObject mcObject;
            enumNames = null;
            arrayEnumValues = null;
            bCanNumber = false;

            string enumFullName = "";
            enumName = "";
            enumType = null;

            if (mcdnObject is IDNMcObjectSchemeNode)
            {
                mcObjectSchemeNode = (IDNMcObjectSchemeNode)mcdnObject;
                enumFullName = mcObjectSchemeNode.GetScheme().GetEnumPropertyActualType(id);
            }
            else if (mcdnObject is IDNMcObjectScheme)
            {
                mcObjectScheme = mcdnObject as IDNMcObjectScheme;
                enumFullName = mcObjectScheme.GetEnumPropertyActualType(id);
            }
            else
            {
                mcObject = (IDNMcObject)mcdnObject;
                if (mcObject != null)
                    enumFullName = mcObject.GetEnumPropertyActualType(id);
            }
            if (enumFullName != null && enumFullName != "")
            {
                enumName = enumFullName;
                int index = enumName.LastIndexOf('.');
                string interfaceName = "";
                if (index > 0)
                {
                    interfaceName = enumName.Substring(0, index);
                    enumName = enumName.Substring(index + 1);
                }

                index = enumName.LastIndexOf('|');
                if (index > 0)
                {
                    bCanNumber = true;
                    enumName = enumName.Substring(0, index);
                }
                string enumName2 = enumName;
                if (enumName2 == "EState")
                {
                    if (interfaceName == "IMcParticleEffectItem")
                        enumType = typeof(DNEParticleEffectState);
                    else if (interfaceName == "IMcSoundItem")
                        enumType = typeof(DNESoundState);
                }
                else
                {
                    var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                   .Where(x => x.IsEnum && x.Namespace == "MapCore" && (x.Module.Name.StartsWith("DN") && x.Module.Name.Contains("MapCore") && x.Module.Name.Contains(".dll")) && x.Name == ("DN" + enumName2));


                    List<Type> lstTypes = types.ToList();
                    if (lstTypes != null)
                    {
                        enumType = lstTypes.FirstOrDefault();
                    }
                }
                if (enumType != null)
                {
                    enumNames = Enum.GetNames(enumType);
                    arrayEnumValues = Enum.GetValues(enumType);
                }
                else
                    NotFoundEnumTypeUserMsg(enumFullName);
                /* }
                 else
                     NotFoundEnumTypeUserMsg(enumFullName);*/
            }
        }
        
        public static int GetEnumIndexInArray(Array arrayEnumValues, uint value)
        {
            int index = 0;
            foreach (object arrValue in arrayEnumValues)
            {
                try
                {
                    if (value == (int)arrValue)
                    {
                        return index;
                    }
                }
                catch (InvalidCastException )
                {
                    if (value == (uint)arrValue)
                    {
                        return index;
                    }
                }
                index++;
            }
            return -1;
        }

        private static void NotFoundEnumTypeUserMsg(string enumName)
        {
            MessageBox.Show("Enum Property Type " + enumName + " Not Found",
                "Get Enum Property Actual Type",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

    }

    public class MCTPrivatePropertyValidationResult
    {
        public enum EErrorType { InvalidValue, InvalidType, None };

        private EErrorType m_ErrorType;
        private object m_ResultValidation1;
        private object m_ResultValidation2;
        private bool m_IsAddIdToDiffMCList;
        private string m_ErrorMessage;

        public EErrorType ErrorType
        {
            set
            {
                if (value == EErrorType.InvalidValue)
                    ErrorMessage = "ID used in several properties. Equalize values.";
                if (value == EErrorType.InvalidType)
                    ErrorMessage = "Same ID is already used for another property type";

                m_ErrorType = value;
            }
            get
            {
                return m_ErrorType;
            }
        }

        public object ResultValidation1
        {
            get { return m_ResultValidation1; }

            set { m_ResultValidation1 = value; }
        }

        public object ResultValidation2
        {
            get { return m_ResultValidation2; }

            set { m_ResultValidation2 = value; }
        }

        public bool IsAddIdToDiffMCList
        {
            get { return m_IsAddIdToDiffMCList; }

            set { m_IsAddIdToDiffMCList = value; }
        }

        public string ErrorMessage
        {
            get { return m_ErrorMessage; }

            set { m_ErrorMessage = value; }
        }

        public MCTPrivatePropertyValidationResult()
        {
            ErrorType = EErrorType.None;
            IsAddIdToDiffMCList = false;
        }
    }
}

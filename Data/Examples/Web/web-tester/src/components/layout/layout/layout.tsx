import { useEffect, useRef, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { MenuItem } from 'primereact/menuitem';
import { ConfirmDialog } from 'primereact/confirmdialog';

import { enums, LayerDetails, LayerParams, Layerslist, ViewportParams, StandardViewportWindow, ViewportWindow } from 'mapcore-lib';
import './styles/layout.css';
import Footer from '../footer/footer';
import MapLayout from '../../layout/mapLayout/mapLayout';
import GenericDialog from '../../dialog/dialog';
import TabMenuModel from '../../shared/models/tab-menu.model';
import Tabmenu from '../../shared/tabs/tabmenu';
import { addViewportStandard, addViewportJson } from '../../../redux/mapWindow/mapWindowAction';
import { selectMaxViewportId } from '../../../redux/mapWindow/mapWindowReducer';
import { AppState } from '../../../redux/combineReducer';
import { removeMinimizedDialog, setDialogType, setGlobalSizeFactor, setmaximaizeWindow, setScreenResolution } from '../../../redux/MapCore/mapCoreAction';
import addJsonViewportService from '../../../services/addJsonViewport.service';
import { selectMapcoreInitialized } from '../../../redux/MapCore/mapCoreReducer';
import { DialogTypesEnum } from '../../../tools/enum/enums';
import generalService from '../../../services/general.service';
import { runAsyncCodeSafely, runCodeSafely } from '../../../common/services/error-handling/errorHandler';
import { setShowDialog } from '../../../redux/ObjectWorldTree/objectWorldTreeActions';
import openOppositeMapService from '../../../services/openOppositeMap.service';

export default function Layout() {
  const dialogTypesArr: DialogTypesEnum[] = useSelector((state: AppState) => state.mapCoreReducer.dialogTypesArr);
  const viewportWindows: ViewportWindow[] = useSelector((state: AppState) => state.mapWindowReducer.viewportWindows);
  const [MenuWin, setMenuWin] = useState<MenuItem[]>([]);
  const [flagAuto, setflagAuto] = useState<boolean>(true);
  const dispatch = useDispatch();
  let maxLayerId: number = useSelector(selectMaxViewportId);
  const mapCoreInitialized: boolean = useSelector(selectMapcoreInitialized);
  const [windowSize, setWindowSize] = useState(null);
  const [confirmDialogVisible, setConfirmDialogVisible] = useState(false);
  const [confirmDialogMessage, setConfirmDialogMessage] = useState('');

  const rootElementRef = useRef(null);

  useEffect(() => {
    rootElementRef.current = document.getElementById('root');
  }, []);

  function getWindowSize() {
    const { innerWidth, innerHeight } = window;
    dispatch(setScreenResolution({ innerWidth, innerHeight }))
    return { innerWidth, innerHeight };
  }

  useEffect(() => {
    runCodeSafely(() => {
      setStyleGlobalSizeFactor()
      setWindowSize(getWindowSize());
      function handleWindowResize() {
        setWindowSize(getWindowSize());
        dispatch(setScreenResolution(windowSize))
      }
      window.addEventListener('resize', handleWindowResize);
      return () => {
        window.removeEventListener('resize', handleWindowResize);
      };
    }, "Layout.UseEffect")
  }, []);

  useEffect(() => {
    runAsyncCodeSafely(async () => {
      if (mapCoreInitialized) {
        await loadJsonFileFromUrl();
      }
    }, 'Layout.UseEffect of mapCoreInitialized');
  }, [mapCoreInitialized])

  const loadJsonFileFromUrl = async () => {
    const urlParams = new URLSearchParams(window.location.search);
    const urlJsonFile = urlParams.get('jsonFile');
    if (urlJsonFile) {
      setflagAuto(true);
      const printViewportPath = urlParams.get('printViewportPath');
      if (!printViewportPath) {
        throw new Error('printViewportPath search parameter is missing from url');
      }
      await addJsonViewportService.readDeviceParamsFromJson(urlJsonFile)
      dispatch(addViewportJson(maxLayerId + 1, urlJsonFile, null, printViewportPath));
    }
    else
      setflagAuto(false)
  }

  const setStyleGlobalSizeFactor = () => {
    const root = document.documentElement;
    const fontSizeInPx = window.innerHeight * 1.35 / 100;
    let isFontSizeSmaller = fontSizeInPx < 12;//check if font size less then 10px
    let finalSizeFactor = isFontSizeSmaller ? 12 / fontSizeInPx : 1;
    console.log('global size factor', finalSizeFactor)
    root.style.setProperty('--size-factor', `${finalSizeFactor}`);
    dispatch(setGlobalSizeFactor(finalSizeFactor));
  }
  useEffect(() => {
    runCodeSafely(() => {
      createMenu()
    }, "Layout.UseEffect of viewportWindows")
  }, [viewportWindows, dialogTypesArr])

  const addLocaMap = () => {
    let layers = [
      new LayerDetails("http:Maps/Raster/Swiss100K-GW", enums.LayerSourceEnum.Native, new LayerParams(), generalService.layerPropertiesBase.layerIReadCallback, enums.LayerNameEnum.NativeRaster),
      new LayerDetails("http:Maps/DTM/SwissDTM-GW", enums.LayerSourceEnum.Native, new LayerParams(), generalService.layerPropertiesBase.layerIReadCallback, enums.LayerNameEnum.NativeDtm)
    ]
    dispatch(addViewportStandard(new StandardViewportWindow(maxLayerId + 1, new Layerslist([], layers), { x: 1, y: 1 }, new ViewportParams(MapCore.IMcMapCamera.EMapType.EMT_2D, 1, true))));
  }
  const addJsonMap = () => {
    dispatch(setDialogType(DialogTypesEnum.addJsonViewport));
  }
  const addMapManully = () => {
    dispatch(setDialogType(DialogTypesEnum.addMapManuly));
  }
  function tileWindows() {
    generalService.calcSizeAndPositionCanvases();
    dispatch(setmaximaizeWindow(0))
  }
  const openChooseLayersDialog = () => {
    dispatch(setDialogType(DialogTypesEnum.chooseLayers));
  }
  const open3DMapAlongsideTo2D = () => {
    const isBlockMessageObj = openOppositeMapService.openOppositeDimensionMap();
    if (isBlockMessageObj.isBlock) {
      setConfirmDialogMessage(isBlockMessageObj.message);
      setConfirmDialogVisible(true);
    }
  }
  const openObjectPropertiesDialog = () => {
    dispatch(setDialogType(DialogTypesEnum.objectProperties));
  }
  //#endregion
  const openMapWorldTreeDialog = () => {
    dispatch(setDialogType(DialogTypesEnum.mapWorldTree));
  }
  const openObjectWorldTreeDialog = () => {
    dispatch(setDialogType(DialogTypesEnum.objectWorldTree));
  }
  const openStandAloneSpatialQueries = () => {
    dispatch(setDialogType(DialogTypesEnum.standAloneSpatialQueries));
  }
  const createMenu = () => {
    let arr: MenuItem[] = [];
    viewportWindows.forEach(element => {
      let add: MenuItem;
      add = { label: "Map viewport " + element.id, command: () => { dispatch(setmaximaizeWindow(element.id)) } }
      arr.push(add)
    });
    arr.push({ separator: true });
    dialogTypesArr.forEach(dialogType => {
      let dialogName = Object.getOwnPropertyDescriptor(DialogTypesEnum, dialogType).value;
      let wordsName = dialogName.split(/(?=[A-Z])/g).join(' ');
      let finalName = wordsName[0].toUpperCase() + wordsName.slice(1);

      let add: MenuItem;
      add = {
        label: finalName, command: () => {
          dispatch(setShowDialog({ hideFormReason: null, dialogType: null }));
          dispatch(setDialogType(dialogType))
          dispatch(removeMinimizedDialog(dialogType))
        }
      }
      arr.push(add)
    });

    setMenuWin(arr);
  }


  let errorList = useSelector((state: AppState) => state.mapCoreReducer.errorList);
  const toast = useRef(null);
  useEffect(() => {
    if (toast.current) {
      toast.current.clear();
      toast.current.show(errorList)
    }
  }, [errorList]);

  const toolbar: TabMenuModel =
  {
    items:
      [
        {
          header: 'Open Map',
          menuItems: [
            { label: "Open map manually", icon: "http:mctester icons/tsbOpenMapManually.Image.png", action: addMapManully },
            { label: "Open from json", icon: "http:mctester icons/tsbLoadViewport.Image.png", action: addJsonMap },
            { label: "Open map scheme", icon: "http:mctester icons/tsbOpenMapScheme.Image.png", action: addLocaMap },
            { label: "several layers", icon: "http:mctester icons/OpenMapQuickly.png", action: openChooseLayersDialog },
            { label: "2D/3D", action: open3DMapAlongsideTo2D },
            { label: "Tile windows", icon: "http:mctester icons/tsbTileWindows.Image.png", action: tileWindows },
            { label: "Windows list", icon: "http:mctester icons/tsbWindowsList.Image.png", menu: MenuWin },
            { label: "General properties", icon: "http:mctester icons/tsbGeneralProperties.Image.png" },
            { label: "object properties", icon: "http:mctester icons/tsbObjectProperties.Image.png", action: openObjectPropertiesDialog }
          ]
        },
        { header: "Map world", action: openMapWorldTreeDialog },
        { header: "Object world", action: openObjectWorldTreeDialog },
        {
          header: 'Calculation',
          menuItems: [
            { label: "Geographic calculation", icon: "http:mctester icons/tsbGeographicCalculations.Image.png" },
            { label: "Geometric calculation", icon: "http:mctester icons/tsbGeometricCalculations.Image.png" },
            { label: "Spatial queries", icon: "http:mctester icons/tsbStandaloneSQ.Image.png", action: openStandAloneSpatialQueries },
          ]
        },
        {
          header: 'General Operations',
          menuItems: [
            { label: "Map production tool", icon: "http:mctester icons/tsbMapProductionTool.Image.png" },]
        },
      ]
  };
  return (
    <div className={flagAuto ? "layout__auto" : "layout__standard"} >
      <Tabmenu closeNav={() => { }} flagMenu={true} toolbar={toolbar} />
      <MapLayout></MapLayout>
      <Footer></Footer>
      <div className="card" style={{ position: 'absolute' }}>
        {/* <Toast ref={toast} /> */}
      </div>
      {dialogTypesArr.length !== 0 && <GenericDialog></GenericDialog>}

      <ConfirmDialog
        contentClassName='form__confirm-dialog-content'
        message={confirmDialogMessage}
        header=''
        footer={<div></div>}
        visible={confirmDialogVisible}
        onHide={e => { setConfirmDialogVisible(false) }}
      />
    </div >
  );
}



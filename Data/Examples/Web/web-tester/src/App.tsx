import { useEffect } from 'react';
import { Provider } from "react-redux";

import { OpenMapService, MapCoreData, MapCoreService } from 'mapcore-lib'
import './styles/App.css';
import store from "./redux/store";
import Layout from './components/layout/layout/layout';
import useErrorBoundary from './common/services/error-handling/use-error-boundary';
import { setInitMapCore } from './redux/MapCore/mapCoreAction';
import generalService from './services/general.service';
import { runCodeSafely, treatError } from './common/services/error-handling/errorHandler';

function App() {
  const mapCoreInitialized: boolean = store.getState()?.mapCoreReducer?.mapCoreInitialized


  const init = async () => {
    await McStartMapCore();
    MapCore.IMcMapDevice.FetchOptionalComponents((MapCore.IMcMapDevice.EOptionalComponentFlags.EOCF_ALL as any).value);
    const urlParams = new URLSearchParams(window.location.search);
    const urlJsonFile = urlParams.get('jsonFile');
    if (urlJsonFile) {
      generalService.ErrorDisplayAlert = false;
    }
    runCodeSafely(() => {
      if (!mapCoreInitialized) {
        console.log(`init Map Core`);
        generalService.OpenMapService = new OpenMapService()
        MapCoreService.initMapCore();
        MapCoreData.onErrorCallback = treatError;
        document.title = "MC Web Tester" + MapCore.IMcMapDevice.GetVersion();
        generalService.initBaseProperties();
        store.dispatch(setInitMapCore(true));
      }
    }, 'App.init');
  }
  useEffect(() => {
    init();
  }, [])
  const { ErrorBoundary } = useErrorBoundary({
    onDidCatch: (error) => {
      console.error('ErrorBoundary App onDidCatch: ' + error.message);
    },
  });

  return (
    <Provider store={store}>
      <ErrorBoundary>
        <Layout></Layout>
      </ErrorBoundary>
    </Provider>
  );
}

export default App;


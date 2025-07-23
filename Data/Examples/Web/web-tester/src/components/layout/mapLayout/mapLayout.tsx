import React, { useEffect, useState } from 'react';
import MapWindow from '../mapWindow/mapWindow';
import { useSelector, useDispatch } from 'react-redux';
import './styles/mapLayout.css'
import { AppState } from '../../../redux/combineReducer';
import { ViewportWindow } from 'mapcore-lib';
export default function MapLayout() {

  const viewportWindows: ViewportWindow[] = useSelector((state: AppState) => state.mapWindowReducer.viewportWindows);

  return (
    <div>
      <div className='map-layout__wrapper' >
        {viewportWindows.map((viewportWindow: ViewportWindow, index: number) => {
          return <MapWindow viewport={viewportWindow} key={viewportWindow.id} index={index} />
        })}
      </div>
    </div>
  )
}

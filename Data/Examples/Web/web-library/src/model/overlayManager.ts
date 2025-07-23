export class OverlayManager {
    overlayManager: MapCore.IMcOverlayManager;
    overlayActive: MapCore.IMcOverlay;

    constructor(overlayManager: MapCore.IMcOverlayManager, overlayActive: MapCore.IMcOverlay) {
        this.overlayManager = overlayManager;
        this.overlayActive = overlayActive;
    }
}
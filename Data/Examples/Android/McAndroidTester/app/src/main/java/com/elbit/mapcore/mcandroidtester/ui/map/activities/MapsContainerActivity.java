package com.elbit.mapcore.mcandroidtester.ui.map.activities;

import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import com.google.android.material.navigation.NavigationView;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import androidx.core.view.GravityCompat;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import android.os.Handler;
import android.util.TypedValue;
import android.view.ContextMenu;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.SubMenu;
import android.view.View;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcMesh;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.managers.MapWorld.Manager_AMCTMapForm;
import com.elbit.mapcore.mcandroidtester.model.SamplePoint;
import com.elbit.mapcore.mcandroidtester.ui.adapters.MenuListAdapter;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.MapFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.TerrainsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs.RasterMapLayerFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs.RasterTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs.StaticObjectsTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.MeshPropertyDialogFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_layers_treeview.TerrainLayersTreeViewFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_terrain_treeview.ViewportTerrainTreeViewFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs.BaseLayerDetailsTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.layers_tabs.MapLayerFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.overlay_manager_treeview.OverlayManagerTreeViewFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.private_properties.TexturePropertyDialogFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_tabs.LayersDetailsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_tabs.TerrainDetailsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.terrain_tabs.TerrainDetailsTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.ViewPortDetailsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map_actions.treeviews.viewport_tabs.ViewPortDetailsTabsFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.ObjectPropertiesBase;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.ObjectPropertiesFragment;
import com.elbit.mapcore.mcandroidtester.ui.objects.fragments.properties_tabs.ObjectPropertiesTabsGeneralFragment;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Consts;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.elbit.mapcore.mcandroidtester.utils.customviews.SampleLocationPointsBttn;
import com.elbit.mapcore.mcandroidtester.utils.expandableNavigationDrawer.data.BaseItem;
import com.elbit.mapcore.mcandroidtester.utils.expandableNavigationDrawer.data.MenuDataProvider;

import java.util.Observer;
import java.util.Set;

import com.elbit.mapcore.Interfaces.OverlayManager.IMcTexture;

import pl.openrnd.multilevellistview.ItemInfo;
import pl.openrnd.multilevellistview.MultiLevelListView;
import pl.openrnd.multilevellistview.OnItemClickListener;

public class MapsContainerActivity extends AppCompatActivity implements MapFragment.OnLocationPointUpdatedListener, ViewPortDetailsFragment.OnFragmentInteractionListener, RasterMapLayerFragment.OnFragmentInteractionListener, MapLayerFragment.OnFragmentInteractionListener, BaseLayerDetailsTabsFragment.OnFragmentInteractionListener, LayersDetailsFragment.OnFragmentInteractionListener, TerrainDetailsTabsFragment.OnFragmentInteractionListener, TerrainDetailsFragment.OnFragmentInteractionListener, TerrainLayersTreeViewFragment.OnFragmentInteractionListener, TerrainsFragment.OnFragmentInteractionListener, NavigationView.OnNavigationItemSelectedListener, MapFragment.OnFragmentInteractionListener, ViewportTerrainTreeViewFragment.OnFragmentInteractionListener, ViewPortDetailsTabsFragment.OnFragmentInteractionListener, RasterTabsFragment.OnFragmentInteractionListener, StaticObjectsTabsFragment.OnFragmentInteractionListener, ObjectPropertiesFragment.OnFragmentInteractionListener, ObjectPropertiesTabsGeneralFragment.OnFragmentInteractionListener, SampleLocationPointsBttn.OnSamplePointListener {

    private static final String CheckGroup = "CheckGroup";

    public MapFragment mMapFragment;
    public String mCurFragmentTag;
    DrawerLayout mDrawerLayout;
    public static byte[] overlayManagerBuffer = null;
   // public static byte[] overlayBuffer = null;
    private MultiLevelListView multiLevelListView;
    private ActionBarDrawerToggle mToggle;
    private String mFragmentTagToReturnAfterSampling;
    private SamplePoint mSamplePoint;
    public int mapFragCommitId;
    public static int actionBarHeight;
    private String mMenuListCheckGroup = "";
    //Handler handler = new Handler();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //requestWindowFeature(Window.FEATURE_ACTION_MODE_OVERLAY);
        setContentView(R.layout.activity_map_container_drawer_menu);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        mDrawerLayout = (DrawerLayout) findViewById(R.id.map_container_drawer_layout);
        mToggle = new ActionBarDrawerToggle(this, mDrawerLayout, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close) {
            @Override
            public void onDrawerSlide(View drawerView, float slideOffset) {
                super.onDrawerSlide(drawerView, slideOffset);
                //added to prevent the surface view to hide the navigation drawer
                if (slideOffset != 0)
                    drawerView.bringToFront();
            }
        };
        mDrawerLayout.setDrawerListener(mToggle);
        mToggle.syncState();
        BaseApplication.setCurrActivityContext(this);
        mSamplePoint = new SamplePoint();
        initMapFragment();
        if(savedInstanceState != null)
            mMenuListCheckGroup = savedInstanceState.getString(CheckGroup);
        initNavDrawerMenu(mMenuListCheckGroup);
        TypedValue tv = new TypedValue();

        if (getTheme().resolveAttribute(android.R.attr.actionBarSize, tv, true))
        {
            actionBarHeight = TypedValue.complexToDimensionPixelSize(tv.data,getResources().getDisplayMetrics());
        }

        // not needed, call automatically internal in mapcore in viewport.render() function.
        //handler.post(Funcs.getPerformPendingCalculationsRunnable(handler, this));
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();

        // Stop the timer when the activity is destroyed
        //Funcs.removeCallbacks(handler);
    }

    private void initMapFragment() {
        if (mMapFragment == null) {
            addStaticMapFragment();
        }
    }

    @Override
    protected void onResume() {
        super.onResume();
    }

    @Override
    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        if (intent.getBooleanExtra("newViewPort", false)) {
            mMapFragment.mView.mRenderer.mNewViewPort = true;
        }
    }

    private void initNavDrawerMenu(String menuListCheckGroup) {
        multiLevelListView = (MultiLevelListView) findViewById(R.id.multiLevelMenu);
        // custom ListAdapter
        MenuListAdapter listAdapter = new MenuListAdapter(this, menuListCheckGroup);
        multiLevelListView.setAdapter(listAdapter);
        multiLevelListView.setOnItemClickListener(mOnItemClickListener);
        listAdapter.setDataItems(MenuDataProvider.getInitialItems());
    }

    @Override
    protected void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putString(CheckGroup, mMenuListCheckGroup);
    }

    private OnItemClickListener mOnItemClickListener = new OnItemClickListener() {

        @Override
        public void onItemClicked(MultiLevelListView parent, View view, Object item, ItemInfo itemInfo) {
            ((BaseItem) item).getAction().run();
            mDrawerLayout.closeDrawers();
        }

        @Override
        public void onGroupItemClicked(MultiLevelListView parent, View view, Object item, ItemInfo itemInfo) {
            //showItemDescription(item, itemInfo);
        }
    };

    private void addStaticMapFragment() {
        if (mMapFragment == null)
            mMapFragment = new MapFragment();
        FragmentTransaction transaction = getSupportFragmentManager().beginTransaction();
        transaction.add(R.id.fragment_container, mMapFragment, MapFragment.class.getSimpleName());
        transaction.addToBackStack(mMapFragment.getClass().getSimpleName());
        mapFragCommitId = transaction.commit();
    }

    @Override
    public void onFragmentInteraction(Uri uri) {

    }

    @Override
    public void onCreateContextMenu(ContextMenu menu, View v, ContextMenu.ContextMenuInfo menuInfo) {
        super.onCreateContextMenu(menu, v, menuInfo);
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.menu_map_editmode, menu);
    }

    /*    @Override
        public boolean dispatchKeyEvent(KeyEvent event) {
            if ((mMapFragment != null) && (mMapFragment.mView != null) && (mMapFragment.mView.IsEditModeActive())) {
                if (event.getKeyCode() == KeyEvent.KEYCODE_BACK && event.getAction() == KeyEvent.ACTION_UP) {
                    // handle your back button code here
                    return false; // consumes the back key event - ActionMode is not finished
                }
            }
            return super.dispatchKeyEvent(event);
        }*/
    @Override
    public boolean onContextItemSelected(MenuItem item) {
        return super.onContextItemSelected(item);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        menu.clear();
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.activity_map_container_drawer, menu);
        return true;
    }

    @Override
    public boolean onMenuOpened(int featureId, Menu menu) {
        if(menu != null) {
            boolean isExistViewport = Manager_AMCTMapForm.getInstance().getCurMapForm() != null;
            MenuItem item = menu.findItem(R.id.nav_remove_viewport);
            if(item != null) {
                item.setEnabled(isExistViewport);
            }
        }

        return super.onMenuOpened(featureId, menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();
        if (id == R.id.nav_viewport_terrains) {
            openTreeView(ViewportTerrainTreeViewFragment.class.getCanonicalName(), ViewportTerrainTreeViewFragment.class.getSimpleName());
            return true;
        }
        if (id == R.id.nav_terrain_layers) {
            openTreeView(TerrainLayersTreeViewFragment.class.getCanonicalName(), TerrainLayersTreeViewFragment.class.getSimpleName());
            return true;
        }
        if (id == R.id.nav_map_container) {
            addViewPortsItemsToMenu(item);
            return true;
        }
        if (id == R.id.nav_object_world) {
            openTreeView(OverlayManagerTreeViewFragment.class.getCanonicalName(), OverlayManagerTreeViewFragment.class.getSimpleName());
            return true;
        }
        if(id == R.id.nav_remove_viewport)
        {
            handleRemoveViewport();
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    private void handleRemoveViewport() {
        mMapFragment.mView.RemoveCurrentViewport();
    }

   private void openTreeView(String fragmentPath, String fragmentTag) {
        if (!mCurFragmentTag.equals(fragmentTag)) {
            FragmentTransaction transaction = getSupportFragmentManager().beginTransaction();
            Fragment curFrag = getSupportFragmentManager().findFragmentByTag(mCurFragmentTag);
            if (curFrag != null) {
                if (curFrag instanceof MapFragment) {
                    transaction.hide(curFrag).addToBackStack(MapFragment.class.getSimpleName());
                } else
                    transaction.remove(curFrag);//.addToBackStack(curFrag.getClass().getSimpleName());
            }
            Fragment treeViewFragmentToShow = getSupportFragmentManager().findFragmentByTag(fragmentTag);
            if (treeViewFragmentToShow == null)
                treeViewFragmentToShow = Fragment.instantiate(this, fragmentPath);
            if (!treeViewFragmentToShow.isAdded()) {
                getSupportFragmentManager().popBackStack(mapFragCommitId, 0);
                transaction.add(R.id.fragment_container, treeViewFragmentToShow, fragmentTag).addToBackStack(fragmentTag);
            } else {
                if (!treeViewFragmentToShow.isVisible()) {
                    getSupportFragmentManager().popBackStack(mapFragCommitId, 0);
                    transaction.show(treeViewFragmentToShow).addToBackStack(fragmentTag);
                }
            }

            mCurFragmentTag = fragmentTag;
            transaction.commit();
        }
    }

    private void removePreviousOpenedTreeViews() {
        if (getSupportFragmentManager().getFragments().size() > 1 && getSupportFragmentManager().getFragments().get(0) instanceof MapFragment) {
            FragmentTransaction fragmentManager = getSupportFragmentManager().beginTransaction();
            for (int i = 1; i < getSupportFragmentManager().getFragments().size(); i++) {
                Fragment fragment = getSupportFragmentManager().getFragments().get(i);
                if (fragment != null)
                    fragmentManager.remove(fragment);
            }
            fragmentManager.commit();
        }
    }

    private void addViewPortsItemsToMenu(MenuItem item) {
        SubMenu subMenu = item.getSubMenu();
        subMenu.clear();
        if (Manager_AMCTMapForm.getInstance().isAnyViewportExist()) {
            Set<Integer> viewPortsIds = Manager_AMCTMapForm.getInstance().getViewPortsHashCodes();
            for (final Object id : viewPortsIds) {
                subMenu.add(String.valueOf(id)).setOnMenuItemClickListener(new MenuItem.OnMenuItemClickListener() {
                    @Override
                    public boolean onMenuItemClick(MenuItem item) {
                        Funcs.runMapCoreFunc(new Runnable() {
                            @Override
                            public void run() {
                                mMapFragment.mView.mRenderer.changeViewPort((Integer) id);
                            }
                        });
                        return false;
                    }
                });
            }
        }
    }


    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.map_container_drawer_layout);
        if ((mMapFragment != null) && (mMapFragment.mView != null) && (mMapFragment.mView.IsEditModeActive())) {
           // mMapFragment.mView.CancelEditMode();
           // mMapFragment.RemoveObjectInitMode();
        }

        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else if (mCurFragmentTag.equals(MapFragment.class.getSimpleName())) {
            AlertMessages.ShowYesNoMessage(this, "Exit", "Do you want to close the viewport? all your data will be deleted.", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    finishAffinity();
                    int p = android.os.Process.myPid();
                    android.os.Process.killProcess(p);
                    return;
                }


            });
        }
        //if there are nested fragments (as in tabs) pop them
        else if (getSupportFragmentManager().getBackStackEntryCount() > 0 && getSupportFragmentManager().findFragmentByTag(getSupportFragmentManager().getBackStackEntryAt(getSupportFragmentManager().getBackStackEntryCount() - 1).getName()) != null && getSupportFragmentManager().findFragmentByTag(getSupportFragmentManager().getBackStackEntryAt(getSupportFragmentManager().getBackStackEntryCount() - 1).getName()).getChildFragmentManager().getBackStackEntryCount() > 0)
            getSupportFragmentManager().findFragmentByTag(getSupportFragmentManager().getBackStackEntryAt(getSupportFragmentManager().getBackStackEntryCount() - 1).getName()).getChildFragmentManager().popBackStack();
        else {
            super.onBackPressed();
            //check if after back you get to map fragment
            if (getSupportFragmentManager().getBackStackEntryCount() == 1 && getSupportFragmentManager().getBackStackEntryAt(0).getName().equals(MapFragment.class.getSimpleName())) {
                {
                    mMapFragment.setTitle();
                    mCurFragmentTag = MapFragment.class.getSimpleName();
                    //remove all the unnecessary treeviews fragments that was created during switching between treeviews.
                    //need also to present the correct mapfragment menu and not previous treeview menu
                    removePreviousOpenedTreeViews();
                    //to present the correct mapfragment menu and not previous treeview menu
                    invalidateOptionsMenu();
                }
            }
        }
		// call to create text item only after app do back
        if(ObjectPropertiesBase.Text.getInstance().mIsWaitForCreate)
            ObjectPropertiesBase.Text.getInstance().updateTextCompleted();
    }

    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        return false;
    }

    //@Override
    public void TextureTabsFragmentInteraction(IMcTexture createdTexture, String strActionTag) {
        switch (strActionTag) {
            case Consts.TextureFragmentsTags.TEXTURE_FROM_PROPERTIES_LIST:
                TexturePropertyDialogFragment texturePropertyDialogFragment = (TexturePropertyDialogFragment) getSupportFragmentManager().findFragmentByTag(getSupportFragmentManager().getBackStackEntryAt(getSupportFragmentManager().getBackStackEntryCount() - 1).getName()).getChildFragmentManager().findFragmentByTag(TexturePropertyDialogFragment.class.getSimpleName());
                if (texturePropertyDialogFragment != null)
                    texturePropertyDialogFragment.onTextureActionsCompleted(createdTexture);
                break;
            case Consts.TextureFragmentsTags.TEXTURE_FROM_CREATE_NEW_PICTURE:
                mMapFragment.drawPictureAfterCreateTexture();
                break;
        }
    }
    
    public void MeshTabsFragmentInteraction(IMcMesh createdMesh, String strActionTag) {
        switch (strActionTag) {
            case Consts.MeshFragmentsTags.MESH_FROM_PROPERTIES_LIST:
                MeshPropertyDialogFragment MeshPropertyDialogFragment = (MeshPropertyDialogFragment) getSupportFragmentManager().findFragmentByTag(getSupportFragmentManager().getBackStackEntryAt(getSupportFragmentManager().getBackStackEntryCount() - 1).getName()).getChildFragmentManager().findFragmentByTag(MeshPropertyDialogFragment.class.getSimpleName());
                if (MeshPropertyDialogFragment != null)
                    MeshPropertyDialogFragment.onMeshActionsCompleted(createdMesh);
                break;
            case Consts.MeshFragmentsTags.MESH_FROM_CREATE_NEW_MESH:
                mMapFragment.drawMeshAfterCreateMesh();
                break;
        }
    }

    @Override
    public void onSamplePoint(String fragmentTagToReturnTo, Observer observer) {
        mFragmentTagToReturnAfterSampling = fragmentTagToReturnTo;
        mSamplePoint.addObserver(observer);
    }

    @Override
    public void onLocationPointUpdated(float x, float y) {
        //if(y>actionBarHeight)
        //    y-= actionBarHeight;
        mSamplePoint.updateSamplePoint(x, y);
        if (mFragmentTagToReturnAfterSampling != null && !mFragmentTagToReturnAfterSampling.equals("")) {
            Fragment callingFragment = getSupportFragmentManager().findFragmentByTag(mFragmentTagToReturnAfterSampling);
            if (callingFragment != null) {
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        returnToCallingFragment();
                    }
                });
            }
        }
        mSamplePoint.deleteObservers();
    }

    private void returnToCallingFragment() {
        Fragment fragment = getSupportFragmentManager().findFragmentByTag(mFragmentTagToReturnAfterSampling);
        //reset the tag to prevent calling again
        if (fragment != null) {
            mCurFragmentTag = mFragmentTagToReturnAfterSampling;
            mFragmentTagToReturnAfterSampling = null;
            getSupportFragmentManager().beginTransaction().hide(mMapFragment).show(fragment).commit();
        }
    }

    public String getMenuListCheckGroup() {
        return mMenuListCheckGroup;
    }

    public void setMenuListCheckGroup(String menuListCheckGroup) {
        this.mMenuListCheckGroup = menuListCheckGroup;
    }
}
